#pragma checksum "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\Home\Index.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "90c2b2a64e97b048f6802127d0dc7a7e329dbf62f8684d536f2d2911c727a57e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCoreGeneratedDocument.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCoreGeneratedDocument
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\_ViewImports.cshtml"
using UI

#nullable disable
    ;
#nullable restore
#line 2 "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\_ViewImports.cshtml"
using UI.Models

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"90c2b2a64e97b048f6802127d0dc7a7e329dbf62f8684d536f2d2911c727a57e", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"aec8fbd3ba3504893c62ec10730ae9e995cf9ed889d33956006311a6197ddacf", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    internal sealed class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home";

#line default
#line hidden
#nullable disable

            WriteLiteral(@"
<div class=""homepage-content"">
    <section class=""hero-section text-center py-5 text-light mb-5"">
        <div class=""container"">
            <h1 class=""display-3 mb-3 animated fadeIn"">🎼 Welcome to ToniStore</h1>
            <p class=""lead animated fadeIn delay-1s"">Your Ultimate Destination for Musical Instruments & Accessories</p>
            <a href=""/Products"" class=""btn btn-primary btn-lg mt-2 animated fadeInUp delay-2s"">🎸 Shop Now</a>
        </div>
    </section>

    <div class=""container"">
        <section class=""benefits-section mb-5"">
            <h2 class=""text-center mb-4 animated fadeInUp"">🎯 Why Choose ToniStore?</h2>
            <div class=""row text-center g-4"">
                <div class=""col-md-3"">
                    <div class=""h1 mb-3"">🚚</div>
                    <h5>Free Shipping</h5>
                    <p>On orders over $99</p>
                </div>
                <div class=""col-md-3"">
                    <div class=""h1 mb-3"">🛡️</div>
                    <h");
            WriteLiteral(@"5>2-Year Warranty</h5>
                    <p>On all instruments</p>
                </div>
                <div class=""col-md-3"">
                    <div class=""h1 mb-3"">🎓</div>
                    <h5>Expert Support</h5>
                    <p>Our team of musicians is here to help with any questions.</p>
                </div>
                <div class=""col-md-3"">
                    <div class=""h1 mb-3"">🔄</div>
                    <h5>Easy Returns</h5>
                    <p>30-day hassle-free returns on all products.</p>
                </div>
            </div>
        </section>
    </div>
</div>

<style>
    .homepage-content {
        line-height: 1.8;
        font-size: 1.1rem;
        font-family: 'Roboto', sans-serif;
    }

    .hero-section {
        background: linear-gradient(to right, #2c3e50, #34495e); 
        background-size: cover;
        background-position: center;
        padding: 80px 0;
        border-bottom: 3px solid rgba(255, 255, 255, 0.2);
    ");
            WriteLiteral(@"}

        .hero-section h1 {
            font-size: 3.5rem;
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.5);
        }

        .hero-section p {
            font-size: 1.2rem;
            text-shadow: 1px 1px 3px rgba(0, 0, 0, 0.3);
        }

        .hero-section .btn {
            font-size: 1.1rem;
            padding: 12px 24px;
            text-transform: uppercase;
        }

    .benefits-section .h1 {
        font-size: 4rem;
        margin-bottom: 1rem;
    }

    .benefits-section h4 {
        font-weight: 600;
    }

    .benefits-section p {
        font-size: 1rem;
        font-weight: 400;
        color: #6c757d;
    }

    .benefits-section .col-md-3 {
        transition: transform 0.3s ease, box-shadow 0.3s ease;
    }

        .benefits-section .col-md-3:hover {
            transform: translateY(-5px);
            box-shadow: 0 8px 20px rgba(0, 0, 0, 0.1);
        }

    .promo-banner {
        background: #f39c12;
        color: #fff;
 ");
            WriteLiteral(@"       font-size: 1.2rem;
    }

        .promo-banner h3 {
            font-size: 2rem;
        }

    .animated {
        animation-duration: 1s;
        animation-timing-function: ease-in-out;
    }

    .fadeInUp {
        animation-name: fadeInUp;
    }

        .fadeInUp.delay-1s {
            animation-delay: 1s;
        }

        .fadeInUp.delay-2s {
            animation-delay: 2s;
        }

</style>
");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
