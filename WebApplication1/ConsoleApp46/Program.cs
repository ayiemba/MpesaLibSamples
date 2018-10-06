using MpesaLib.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp46
{
    class Program
    {
        static void Main(string[] args)
        {
            //Security Credentials
            string certificate = @"C:\Dev\Work\MpesaIntegration\MpesaLibSamples\WebApplication1\WebApplication1\Certificate\prod.cer";


            var B2CsecurityCred = Credentials.EncryptPassword(certificate, "971796");

            Console.WriteLine(B2CsecurityCred);
        }
    }
}
