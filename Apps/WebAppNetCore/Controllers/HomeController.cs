using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MpesaLib.Helpers;
using MpesaLib;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebAppNetCore.Responses;
using Microsoft.AspNetCore.Http;

namespace WebAppNetCore.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IConfiguration _configuration;
        private readonly IMpesaClient _mpesaClient;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IMpesaClient mpesaClient, IConfiguration configuration, IMemoryCache memoryCache, ILogger<HomeController> logger)
        {
           
            _configuration = configuration;
            _mpesaClient = mpesaClient;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        private string ConsumerKey { get { return _configuration["MpesaConfiguration:ConsumerKey"]; } }
        private string ConsumerSecret { get { return _configuration["MpesaConfiguration:ConsumerSecret"]; } }
        private string PassKey { get { return _configuration["MpesaConfiguration:PassKey"]; } }
        private string AccessToken { get { return GetToken(); } }       


        public IActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {            
            return View();
        }

        //Lipa Na Mpesa Online - STK Push
        [HttpPost]
        public async Task<IActionResult> LNMO(IFormCollection collection)
        {                   
            LipaNaMpesaOnlineDto lipaOnline = new LipaNaMpesaOnlineDto
            {
                AccountReference = "MpesaLib Sample",
                Amount = collection["amount"],                   
                PartyA = collection["phone_number"],        
                PartyB = _configuration["MpesaConfiguration:LNMOshortCode"],
                BusinessShortCode = _configuration["MpesaConfiguration:LNMOshortCode"],
                CallBackURL = _configuration["MpesaConfiguration:CallBackUrl"],
                PhoneNumber = collection["phone_number"],  
                Passkey = PassKey,
                TransactionDesc = "MpesaLib Sample"

            };

            var lipaNaMpesa = await _mpesaClient.MakeLipaNaMpesaOnlinePaymentAsync(lipaOnline, AccessToken, RequestEndPoint.LipaNaMpesaOnline);


            //ViewData["Message2"] = lipaNaMpesa;

            return RedirectToAction("ShowMpesaResult", new { response = lipaNaMpesa, customerNumber = collection["phone_number"], invoicenumber = collection["invoice_number"] });           
        }


        [HttpPost("/mpesatransactionstatus")]
        public async Task<IActionResult> MpesaOnlineTransactionStatus(IFormCollection formcollection)
        {

            var accesstoken = await _mpesaClient.GetAuthTokenAsync(ConsumerKey, ConsumerSecret, RequestEndPoint.AuthToken);

            var LNMOQuery = new LipaNaMpesaQueryDto
            {
                BusinessShortCode = "174379",
                CheckoutRequestID = formcollection["checkoutRequestId"],
                Passkey = PassKey
            };

            var queryResult = await _mpesaClient.QueryLipaNaMpesaTransactionAsync(LNMOQuery, accesstoken, RequestEndPoint.QueryLipaNaMpesaOnlieTransaction);



            return RedirectToAction("ConfirmMpesaPayment", new { response = queryResult, customerNumber = formcollection["phone_number"] });

        }


        [Route("/showmpesaresult")]
        public IActionResult ShowMpesaResult(string response, string customerNumber, string invoicenumber)
        {

            //deserialize response and query transaction stastus
            var res = JsonConvert.DeserializeObject<MpesaStkResponse>(response);

            //pass data to view/page via viewbag
            ViewBag.ResponseCode = res.ResponseCode;
            ViewBag.CheckoutRequestId = res.CheckoutRequestID;
            ViewBag.MerchantRequestId = res.MerchantRequestID;
            ViewBag.CustomerMessage = res.CustomerMessage;
            ViewBag.ResponseDescription = res.ResponseDescription;
            ViewBag.CustomerNumber = customerNumber;

            return View();
        }

        [Route("/mpesaconfirmation")]
        public IActionResult ConfirmMpesaPayment(string response, string customerNumber)
        {
            ////deserialize response and query transaction stastus
            var res = JsonConvert.DeserializeObject<LNMOQueryResponse>(response);

            //handle edge cases and errors  

            //pass data to view/page via viewbag
            ViewBag.ResultDescription = res.ResultDesc;
            ViewBag.ResultCode = res.ResultCode;
            ViewBag.ResponseDescription = res.ResponseDescription;
            ViewBag.CustomerNumber = customerNumber;

            return View();
        }


        //TODO: make this method async
        private string GetToken()
        {
            var cacheKey = "MpesaAccessToken";

            if (_memoryCache.TryGetValue(cacheKey, out string accessToken))
            {
                _logger.LogInformation("Getting token from memory.....");
                return accessToken;
            }
            else
            {
                _logger.LogInformation("Getting token from Mpesa Server.....");
                accessToken = _mpesaClient.GetAuthToken(ConsumerKey, ConsumerSecret, RequestEndPoint.AuthToken);

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(59));

                _memoryCache.Set(cacheKey, accessToken, cacheEntryOptions);

                return accessToken;
            }
        }


    }
}
