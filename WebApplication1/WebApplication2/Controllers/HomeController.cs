using MpesaLib.Clients;
using MpesaLib.Helpers;
using MpesaLib.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {

        /** For MpesaLib versions >= 2.1.0*/

        public HomeController()
        {
            
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            
            //Initialize HttpClient
            HttpClient httpClient = new HttpClient();
            
            //Handle TLS Issues Necessary for .Netframework 45 and .Netframework 40
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            httpClient.BaseAddress = new Uri("https://sandbox.safaricom.co.ke/");

            //Initialize MpesaClient and Pass in httpClient
            var _mpesaClient = new MpesaClient(httpClient);

            var consumerKey = "your consumer key from daraja";

            var consumerSecret = "your consumer secret from daraja";

            var passKey = "your LNMO passkey from daraja/org portal";

            //Request Token (tokens expires after an hour, implement some chaing mechnism)
            var accesstoken = await _mpesaClient.GetAuthTokenAsync(consumerKey, consumerSecret, "oauth/v1/generate?grant_type=client_credentials");


            //Register C2B Urls Object
            CustomerToBusinessRegister registerUrl = new CustomerToBusinessRegister
            {
                ConfirmationURL = "https://blablabala/confirm",
                ValidationURL = "https://blablabala/validate",
                ResponseType = "Cancelled",
                ShortCode = "603047"
            };

            var ulrregistration = await _mpesaClient.RegisterC2BUrlAsync(registerUrl, accesstoken, "mpesa/c2b/v1/registerurl");



            //C2B Object
            CustomerToBusinessSimulate c2b = new CustomerToBusinessSimulate
            {
                ShortCode = "603047",
                Amount = "1",
                BillRefNumber = "account",
                Msisdn = "254708374149",
            };

            //C2B request
            var c2bRequest = await _mpesaClient.MakeC2BPaymentAsync(c2b, accesstoken, "mpesa/c2b/v1/simulate");



            //LipaNaMpesaOnline Object
            LipaNaMpesaOnline lipaOnline = new LipaNaMpesaOnline
            {
                AccountReference = "test",
                Amount = "1",
                PartyA = "2547XXXXXXX", //please replace with your own number
                PartyB = "174379",
                BusinessShortCode = "174379",
                CallBackURL = "https://blablabala/callback",
                PhoneNumber = "2547XXXXXXXX", //replace with your own number
                Passkey = passKey,
                TransactionDesc = "test"

            };

            //LipanaMpesaOnline (STK Push) request
            var lipaNaMpesa = await _mpesaClient.MakeLipaNaMpesaOnlinePaymentAsync(lipaOnline, accesstoken, "mpesa/stkpush/v1/processrequest");



            //Security Credentials
            string certificate = @"C:\Dev\MpesaIntegration\MpesaLibSamples\Certificate\prod.cer";

            var securityCred = Credentials.EncryptPassword(certificate, "971796");

            Console.WriteLine(securityCred); //I just want to see the credential, nothing mcuh here really

            //B2C Object
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
                SecurityCredential = securityCred 
            };

            //B2C request
            var b2cRequest = await _mpesaClient.MakeB2CPaymentAsync(b2c, accesstoken, "mpesa/b2c/v1/paymentrequest");


            //B2B Object
            BusinessToBusiness b2bobject = new BusinessToBusiness
            {
                AccountReference = "test",
                Initiator = "safaricom.13",
                Amount = "1",
                PartyA = "603047",
                PartyB = "600000",
                CommandID = "MerchantToMerchantTransfer", //chack correct command from daraja, don't use commands blindly!
                QueueTimeOutURL = "https://blablabala/callback",
                RecieverIdentifierType = "4",
                SecurityCredential = securityCred, 
                SenderIdentifierType = "4",
                ResultURL = "https://blablabala/callback",
                Remarks = "payment"
            };

            //B2B request
            var b2brequest = await _mpesaClient.MakeB2BPaymentAsync(b2bobject, accesstoken, "mpesa/b2b/v1/paymentrequest");

            /*
             * I am printing the results on the About page
             * You should actually deserialize and do meaningful stuff with the results
             */
            
            ViewData["Message0"] = securityCred;

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

        //public ActionResult Privacy()
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