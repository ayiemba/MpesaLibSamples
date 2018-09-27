using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MpesaLib.Clients;
using MpesaLib.Interfaces;
using MpesaLib.Models;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IConfiguration _configuration;
        private readonly IMpesaClient _mpesaClient;

        public HomeController(IMpesaClient mpesaClient, IConfiguration configuration)
        {
           
            _configuration = configuration;
            _mpesaClient = mpesaClient;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            var consumerKey = _configuration["MpesaConfiguration:ConsumerKey"];
            
            var consumerSecret = _configuration["MpesaConfiguration:ConsumerSecret"];

            var passKey = _configuration["MpesaConfiguration:PassKey"];

            //Get Token
            var accesstoken = await _mpesaClient.GetAuthTokenAsync(consumerKey, consumerSecret, "oauth/v1/generate?grant_type=client_credentials");        

            
            //C2BRegisterUrls
            CustomerToBusinessRegister registerUrl = new CustomerToBusinessRegister
            {
                ConfirmationURL = "https://demo.osl.co.ke:7575/geospatial",
                ValidationURL = "https://demo.osl.co.ke:7575/geospatial",
                ResponseType = "Cancelled",
                ShortCode = "603047"
            };

            //var ulrregistration = await _mpesaClient.RegisterC2BUrlAsync(registerUrl, accesstoken, "mpesa/c2b/v1/registerurl");


            //C2B
            CustomerToBusinessSimulate c2b = new CustomerToBusinessSimulate
            {
                ShortCode = "603047",
                Amount = "1",
                BillRefNumber = "account",
                Msisdn = "254708374149",
            };
            var c2bRequest = await _mpesaClient.MakeC2BPaymentAsync(c2b, accesstoken, "mpesa/c2b/v1/simulate");



            //LipaNaMpesaOnline
            LipaNaMpesaOnline lipaOnline = new LipaNaMpesaOnline
            {
                AccountReference = "test",
                Amount = "1",
                PartyA = "254725589166",
                PartyB = "174379",
                BusinessShortCode = "174379",
                CallBackURL = "https://peternjeru.co.ke/safdaraja/api/callback.php",
                PhoneNumber = "254725589166",
                Passkey = passKey,
                TransactionDesc = "test"

            };
            var lipaNaMpesa = await _mpesaClient.MakeLipaNaMpesaOnlinePaymentAsync(lipaOnline, accesstoken, "mpesa/stkpush/v1/processrequest");


            //B2C
            BusinessToCustomer b2c = new BusinessToCustomer
            {
                Remarks = "test",
                Amount = "1",
                CommandID = "BusinessPayment",
                InitiatorName = "safaricom.13",
                Occasion = "test",
                PartyA = "603047",
                PartyB = "254708374149",
                QueueTimeOutURL = "https://peternjeru.co.ke/safdaraja/api/callback.php",
                ResultURL = "https://peternjeru.co.ke/safdaraja/api/callback.php",
                SecurityCredential = "UzQmvbTuYv6eBJh+ECRhsnpESnvXAiqvrsG5gKPDnrgTVgIJfNhOd0REVcg9Y1xOrkkbv2+oxCOQnMZ1/PFHYaX50ikChzE/P9I1npZm/PWhZYmxWaddz9QmxyNF9XPiADXgj83SFvsrvbQ/ukSzSP+NeA/O2KOOjiu41lOijIXdNCo/Orvg/BIKAwbsEayhWCm0GxJt44Ony/jKGQiTT7KGDEalI4ETwROLu4YUwswyxlRi6GgNOXS12+GnggTxNr8ncRL67XT3vxe1iTD2XkebXWaOD5ep0cTXEN7xB89Br5BdpqEwUMVAp5AmN6uL0IPG3hEWz1ndGX40XVq7og=="
            };

            var b2cRequest = await _mpesaClient.MakeB2CPaymentAsync(b2c, accesstoken, "mpesa/b2c/v1/paymentrequest");


            //B2B
            BusinessToBusiness b2bobject = new BusinessToBusiness
            {
                AccountReference = "test",
                Initiator = "safaricom.13",
                Amount = "1",
                PartyA = "603047",
                PartyB = "600000",
                CommandID = "MerchantToMerchantTransfer",
                QueueTimeOutURL = "https://peternjeru.co.ke/safdaraja/api/callback.php",
                RecieverIdentifierType = "4",
                SecurityCredential = "oChG9b+xdWz10sL8Suz5HP2rQX/rqwMKOVgHStsYBdjCc+brq3ogPr8LMNV2lAK8WFNZbbaXMXWCFApnR/yhe+yq62oJOaSKs/4AywWGZOJ5h2j1z4UCGx19Ss1mR7nsMvTxZqhNH0trwKme01gy1vyiPMsWzML6gux+KxQvGlgWTulLDNBNOBQYAxNiGI/rGJYabtWK4m7iNWFTpzYcGbZBdIILmjK79lFjMVEmKYXuPO/zfl3gTQ7nWzDySoM+UyIwCBl29lr3BKMylj7RPqpfXBkLGe6K3MvBS0KN+IQwIYirFRae0hsQ9mqaHciLN5+Th/QbPhakBrforg6Mvw==",
                SenderIdentifierType = "4",
                ResultURL = "https://peternjeru.co.ke/safdaraja/api/callback.php",
                Remarks = "payment"
            };

            var b2brequest = await _mpesaClient.MakeB2BPaymentAsync(b2bobject, accesstoken, "mpesa/b2b/v1/paymentrequest");

            //ViewData["Message0"] = ulrregistration;

            ViewData["Message1"] = c2bRequest; 

            ViewData["Message2"] = lipaNaMpesa;

            ViewData["Message3"] = b2cRequest;

            ViewData["Message4"] = b2brequest;

            return View();
        }




        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
