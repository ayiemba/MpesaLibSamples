#pragma checksum "C:\Dev\Work\MpesaIntegration\MpesaLibSamples\Apps\WebAppNetCore21\Views\Home\About.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "aebfea140e7cd05df2e702ccdb3c2d11c5ea8ca0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_About), @"mvc.1.0.view", @"/Views/Home/About.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/About.cshtml", typeof(AspNetCore.Views_Home_About))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Dev\Work\MpesaIntegration\MpesaLibSamples\Apps\WebAppNetCore21\Views\_ViewImports.cshtml"
using WebApplication1;

#line default
#line hidden
#line 2 "C:\Dev\Work\MpesaIntegration\MpesaLibSamples\Apps\WebAppNetCore21\Views\_ViewImports.cshtml"
using WebApplication1.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"aebfea140e7cd05df2e702ccdb3c2d11c5ea8ca0", @"/Views/Home/About.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"729efaa87342638aecfe1a972ce9f9f8dff55b4c", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_About : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Dev\Work\MpesaIntegration\MpesaLibSamples\Apps\WebAppNetCore21\Views\Home\About.cshtml"
  
    ViewData["Title"] = "Mpesa API test, Initial Requests";

#line default
#line hidden
            BeginContext(68, 4, true);
            WriteLiteral("<h2>");
            EndContext();
            BeginContext(73, 17, false);
#line 4 "C:\Dev\Work\MpesaIntegration\MpesaLibSamples\Apps\WebAppNetCore21\Views\Home\About.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(90, 37, true);
            WriteLiteral("</h2>\r\n<br />\r\n\r\n<h4>C2BRegisterUrl: ");
            EndContext();
            BeginContext(128, 20, false);
#line 7 "C:\Dev\Work\MpesaIntegration\MpesaLibSamples\Apps\WebAppNetCore21\Views\Home\About.cshtml"
               Write(ViewData["Message0"]);

#line default
#line hidden
            EndContext();
            BeginContext(148, 26, true);
            WriteLiteral("</h4>\r\n<br />\r\n\r\n<h4>C2B: ");
            EndContext();
            BeginContext(175, 20, false);
#line 10 "C:\Dev\Work\MpesaIntegration\MpesaLibSamples\Apps\WebAppNetCore21\Views\Home\About.cshtml"
    Write(ViewData["Message1"]);

#line default
#line hidden
            EndContext();
            BeginContext(195, 27, true);
            WriteLiteral("</h4>\r\n<br />\r\n\r\n<h4>LNMO: ");
            EndContext();
            BeginContext(223, 20, false);
#line 13 "C:\Dev\Work\MpesaIntegration\MpesaLibSamples\Apps\WebAppNetCore21\Views\Home\About.cshtml"
     Write(ViewData["Message2"]);

#line default
#line hidden
            EndContext();
            BeginContext(243, 26, true);
            WriteLiteral("</h4>\r\n<br />\r\n\r\n<h4>B2C: ");
            EndContext();
            BeginContext(270, 20, false);
#line 16 "C:\Dev\Work\MpesaIntegration\MpesaLibSamples\Apps\WebAppNetCore21\Views\Home\About.cshtml"
    Write(ViewData["Message3"]);

#line default
#line hidden
            EndContext();
            BeginContext(290, 26, true);
            WriteLiteral("</h4>\r\n<br />\r\n\r\n<h4>B2B: ");
            EndContext();
            BeginContext(317, 20, false);
#line 19 "C:\Dev\Work\MpesaIntegration\MpesaLibSamples\Apps\WebAppNetCore21\Views\Home\About.cshtml"
    Write(ViewData["Message4"]);

#line default
#line hidden
            EndContext();
            BeginContext(337, 11, true);
            WriteLiteral("</h4>\r\n\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591