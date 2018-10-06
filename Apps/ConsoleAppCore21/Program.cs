using MpesaLib.Helpers;
using System;

namespace ConsoleAppCore21
{
    class Program
    {
        static void Main(string[] args)
        {
            //Security Credentials
            string certificate = @"C:\Dev\Work\MpesaIntegration\MpesaLibSamples\certificate\prod.cer";


            var B2CsecurityCred = Credentials.EncryptPassword(certificate, "971796");

            Console.WriteLine(B2CsecurityCred);
        }
    }
}
