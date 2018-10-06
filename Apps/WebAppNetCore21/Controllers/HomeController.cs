using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MpesaLib.Helpers;
using MpesaLib.Interfaces;
using MpesaLib.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
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
                ConfirmationURL = "https://blablabala/confirm",
                ValidationURL = "https://blablabala/validate",
                ResponseType = "Cancelled",
                ShortCode = "603047"
            };

            var ulrregistration = await _mpesaClient.RegisterC2BUrlAsync(registerUrl, accesstoken, "mpesa/c2b/v1/registerurl");


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
                CallBackURL = "https://blablabala/callback",
                PhoneNumber = "254725589166",
                Passkey = passKey,
                TransactionDesc = "test"

            };
            var lipaNaMpesa = await _mpesaClient.MakeLipaNaMpesaOnlinePaymentAsync(lipaOnline, accesstoken, "mpesa/stkpush/v1/processrequest");


            //Security Credentials
            string certificate = @"C:\Dev\Work\MpesaIntegration\MpesaLibSamples\certificate\prod.cer";
           

            var B2CsecurityCred = Credentials.EncryptPassword(certificate, "971796");

            Console.WriteLine(B2CsecurityCred); //I just want to see credential


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
                QueueTimeOutURL = "https://blablabala/callback",
                ResultURL = "https://blablabala/callback",
                SecurityCredential = B2CsecurityCred 
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
                CommandID = "MerchantToMerchantTransfer",// Please chack the correct command from Daraja
                QueueTimeOutURL = "https://blablabala/callback",
                RecieverIdentifierType = "4",
                SecurityCredential = B2CsecurityCred, 
                SenderIdentifierType = "4",
                ResultURL = "https://blablabala/callback",
                Remarks = "payment"
            };

            var b2brequest = await _mpesaClient.MakeB2BPaymentAsync(b2bobject, accesstoken, "mpesa/b2b/v1/paymentrequest");

            ViewData["Message0"] = ulrregistration;

            ViewData["Message1"] = c2bRequest; 

            ViewData["Message2"] = lipaNaMpesa;

            ViewData["Message3"] = b2cRequest;

            ViewData["Message4"] = b2brequest;

            return View();
        }




        //public IActionResult Contact()
        //{
        //    ViewData["Message"] = "Your contact page.";

        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
