using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MpesaLib.Helpers;
using MpesaLib;
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

        public IActionResult About()
        {
            var consumerKey = _configuration["MpesaConfiguration:ConsumerKey"];
            
            var consumerSecret = _configuration["MpesaConfiguration:ConsumerSecret"];

            var passKey = _configuration["MpesaConfiguration:PassKey"];

            //Get Token
            var accesstoken = _mpesaClient.GetAuthToken(consumerKey, consumerSecret, RequestEndPoint.AuthToken);

            //LipaNaMpesaOnline
            LipaNaMpesaOnlineDto lipaOnline = new LipaNaMpesaOnlineDto
            {
                AccountReference = "test",
                Amount = "1",
                PartyA = "254725589166",
                PartyB = _configuration["MpesaConfiguration:LNMOshortCode"],
                BusinessShortCode = _configuration["MpesaConfiguration:LNMOshortCode"],
                CallBackURL = _configuration["MpesaConfiguration:CallBackUrl"],
                PhoneNumber = "254725589166",
                Passkey = passKey,
                TransactionDesc = "test"

            };
            var lipaNaMpesa = _mpesaClient.MakeLipaNaMpesaOnlinePayment(lipaOnline, accesstoken, RequestEndPoint.LipaNaMpesaOnline);


            ViewData["Message2"] = lipaNaMpesa;

            return View();
        }




    }
}
