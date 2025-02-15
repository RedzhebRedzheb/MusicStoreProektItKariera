#pragma checksum "C:\Users\Acer\Desktop\kursovarabotamodul12\UI\Views\Orders\UserCart.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "fe2f7d81c6cc5c6105250dfaab5a34e74d51942bab8d919a63f062aa71444110"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCoreGeneratedDocument.Views_Orders_UserCart), @"mvc.1.0.view", @"/Views/Orders/UserCart.cshtml")]
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
#line 1 "C:\Users\Acer\Desktop\kursovarabotamodul12\UI\Views\_ViewImports.cshtml"
using UI

#nullable disable
    ;
#nullable restore
#line 2 "C:\Users\Acer\Desktop\kursovarabotamodul12\UI\Views\_ViewImports.cshtml"
using UI.Models

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"fe2f7d81c6cc5c6105250dfaab5a34e74d51942bab8d919a63f062aa71444110", @"/Views/Orders/UserCart.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"aec8fbd3ba3504893c62ec10730ae9e995cf9ed889d33956006311a6197ddacf", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    internal sealed class Views_Orders_UserCart : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<
#nullable restore
#line 1 "C:\Users\Acer\Desktop\kursovarabotamodul12\UI\Views\Orders\UserCart.cshtml"
       MusicStore.Models.Order

#line default
#line hidden
#nullable disable
    >
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<h1>Your Cart</h1>\r\n\r\n");
#nullable restore
#line 5 "C:\Users\Acer\Desktop\kursovarabotamodul12\UI\Views\Orders\UserCart.cshtml"
 if (Model == null || !Model.OrderItems.Any())
{

#line default
#line hidden
#nullable disable

            WriteLiteral("    <p>Your cart is empty!</p>\r\n");
#nullable restore
#line 8 "C:\Users\Acer\Desktop\kursovarabotamodul12\UI\Views\Orders\UserCart.cshtml"
}
else
{

#line default
#line hidden
#nullable disable

            WriteLiteral("    <table class=\"table\">\r\n        <thead>\r\n            <tr>\r\n                <th>Product</th>\r\n                <th>Quantity</th>\r\n                <th>Unit Price</th>\r\n                <th>Total</th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
#nullable restore
#line 21 "C:\Users\Acer\Desktop\kursovarabotamodul12\UI\Views\Orders\UserCart.cshtml"
             foreach (var item in Model.OrderItems)
            {

#line default
#line hidden
#nullable disable

            WriteLiteral("                <tr>\r\n                    <td>");
            Write(
#nullable restore
#line 24 "C:\Users\Acer\Desktop\kursovarabotamodul12\UI\Views\Orders\UserCart.cshtml"
                         item.Product.Name

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</td>\r\n                    <td>");
            Write(
#nullable restore
#line 25 "C:\Users\Acer\Desktop\kursovarabotamodul12\UI\Views\Orders\UserCart.cshtml"
                         item.Quantity

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</td>\r\n                    <td>");
            Write(
#nullable restore
#line 26 "C:\Users\Acer\Desktop\kursovarabotamodul12\UI\Views\Orders\UserCart.cshtml"
                         item.UnitPrice

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</td>\r\n                    <td>");
            Write(
#nullable restore
#line 27 "C:\Users\Acer\Desktop\kursovarabotamodul12\UI\Views\Orders\UserCart.cshtml"
                          item.Quantity * item.UnitPrice

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</td>\r\n                </tr>\r\n");
#nullable restore
#line 29 "C:\Users\Acer\Desktop\kursovarabotamodul12\UI\Views\Orders\UserCart.cshtml"
            }

#line default
#line hidden
#nullable disable

            WriteLiteral("        </tbody>\r\n    </table>\r\n");
            WriteLiteral("    <a");
            BeginWriteAttribute("href", " href=\"", 777, "\"", 817, 1);
            WriteAttributeValue("", 784, 
#nullable restore
#line 33 "C:\Users\Acer\Desktop\kursovarabotamodul12\UI\Views\Orders\UserCart.cshtml"
              Url.Action("Checkout", "Orders")

#line default
#line hidden
#nullable disable
            , 784, 33, false);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-primary\">Checkout</a>\r\n");
#nullable restore
#line 34 "C:\Users\Acer\Desktop\kursovarabotamodul12\UI\Views\Orders\UserCart.cshtml"
}

#line default
#line hidden
#nullable disable

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MusicStore.Models.Order> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
