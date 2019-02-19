using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MpesaLib.Helpers;
using MpesaLib;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WebApplication1.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
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
                accessToken =  _mpesaClient.GetAuthToken(ConsumerKey, ConsumerSecret, RequestEndPoint.AuthToken);

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(59));

                _memoryCache.Set(cacheKey, accessToken, cacheEntryOptions);

                return accessToken;
            }
        }


        public IActionResult Index()
        {
            return View();
        }

        //Lipa Na Mpesa Online - STK Push
        public async Task<IActionResult> LNMO()
        {                   
            LipaNaMpesaOnlineDto lipaOnline = new LipaNaMpesaOnlineDto
            {
                AccountReference = "Sample",
                Amount = "2",                   //replace the amount you want to test. Max is KSh 70,000 per transaction
                PartyA = "",        //replace this number with yours or clients safcom number
                PartyB = _configuration["MpesaConfiguration:LNMOshortCode"],
                BusinessShortCode = _configuration["MpesaConfiguration:LNMOshortCode"],
                CallBackURL = _configuration["MpesaConfiguration:CallBackUrl"],
                PhoneNumber = "",   //replace this number with yours or clients safcom number
                Passkey = PassKey,
                TransactionDesc = "MpesaLib Sample"

            };

            var lipaNaMpesa = await _mpesaClient.MakeLipaNaMpesaOnlinePaymentAsync(lipaOnline, AccessToken, RequestEndPoint.LipaNaMpesaOnline);


            ViewData["Message2"] = lipaNaMpesa;

            return View();
        }




    }
}
