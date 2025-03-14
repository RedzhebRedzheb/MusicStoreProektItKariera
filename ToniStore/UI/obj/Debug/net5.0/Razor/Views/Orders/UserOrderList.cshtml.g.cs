#pragma checksum "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\Orders\UserOrderList.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6269170d59ae5bfe3029e891a97a0ca145544c47ec0c0a2b40d2202d268248c8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCoreGeneratedDocument.Views_Orders_UserOrderList), @"mvc.1.0.view", @"/Views/Orders/UserOrderList.cshtml")]
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

#nullable disable
    ;
#nullable restore
#line 1 "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\Orders\UserOrderList.cshtml"
 using MusicStore.Models

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"6269170d59ae5bfe3029e891a97a0ca145544c47ec0c0a2b40d2202d268248c8", @"/Views/Orders/UserOrderList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"aec8fbd3ba3504893c62ec10730ae9e995cf9ed889d33956006311a6197ddacf", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    internal sealed class Views_Orders_UserOrderList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<
#nullable restore
#line 2 "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\Orders\UserOrderList.cshtml"
       IEnumerable<Order>

#line default
#line hidden
#nullable disable
    >
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-sm btn-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", new global::Microsoft.AspNetCore.Html.HtmlString("Cancel Order"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("stretched-link"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\Orders\UserOrderList.cshtml"
  
    ViewData["Title"] = "My Orders";

#line default
#line hidden
#nullable disable

            WriteLiteral("\r\n<div class=\"container\">\r\n    <h1>Order History</h1>\r\n    <div class=\"alert alert-info\">\r\n        Total Spending: ");
            Write(
#nullable restore
#line 11 "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\Orders\UserOrderList.cshtml"
                         ViewBag.TotalSpending.ToString("C")

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n    </div>\r\n\r\n    <div class=\"list-group\">\r\n");
#nullable restore
#line 15 "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\Orders\UserOrderList.cshtml"
         foreach (var order in Model)
        {

#line default
#line hidden
#nullable disable

            WriteLiteral(@"            <div class=""list-group-item list-group-item-action"">
                <div class=""d-flex justify-content-between align-items-start"">
                    <div class=""flex-grow-1"">
                        <div class=""d-flex w-100 justify-content-between"">
                            <h5 class=""mb-1"">Order #");
            Write(
#nullable restore
#line 21 "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\Orders\UserOrderList.cshtml"
                                                     order.Id

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</h5>\r\n                            <small>");
            Write(
#nullable restore
#line 22 "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\Orders\UserOrderList.cshtml"
                                    order.OrderDate.ToString("MMM dd, yyyy")

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</small>\r\n                        </div>\r\n                        <div class=\"row\">\r\n                            <div class=\"col-md-4\">\r\n                                <strong>Status:</strong>\r\n                                <span");
            BeginWriteAttribute("class", " class=\"", 992, "\"", 1041, 3);
            WriteAttributeValue("", 1000, "badge", 1000, 5, true);
            WriteAttributeValue(" ", 1005, "badge-", 1006, 7, true);
            WriteAttributeValue("", 1012, 
#nullable restore
#line 27 "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\Orders\UserOrderList.cshtml"
                                                          GetStatusBadge(order.Status)

#line default
#line hidden
#nullable disable
            , 1012, 29, false);
            EndWriteAttribute();
            WriteLiteral(">");
            Write(
#nullable restore
#line 27 "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\Orders\UserOrderList.cshtml"
                                                                                         order.Status

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</span>\r\n                            </div>\r\n                            <div class=\"col-md-4\">\r\n                                <strong>Items:</strong> ");
            Write(
#nullable restore
#line 30 "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\Orders\UserOrderList.cshtml"
                                                         order.OrderItems.Sum(oi => oi.Quantity)

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n                            </div>\r\n                            <div class=\"col-md-4\">\r\n                                <strong>Total:</strong> ");
            Write(
#nullable restore
#line 33 "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\Orders\UserOrderList.cshtml"
                                                         order.TotalPrice.ToString("C")

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n                            </div>\r\n                        </div>\r\n                        <div class=\"alert alert-info\">\r\n                            Total Spending: ");
            Write(
#nullable restore
#line 37 "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\Orders\UserOrderList.cshtml"
                                             ViewBag.TotalSpending.ToString("C")

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n                            <small>(excludes cancelled orders)</small>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"ms-3\">\r\n");
#nullable restore
#line 42 "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\Orders\UserOrderList.cshtml"
                         if (order.Status == "Pending")
                        {

#line default
#line hidden
#nullable disable

            WriteLiteral("                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6269170d59ae5bfe3029e891a97a0ca145544c47ec0c0a2b40d2202d268248c810728", async() => {
                WriteLiteral("\r\n                                <i class=\"fas fa-trash\"></i>\r\n                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
            WriteLiteral(
#nullable restore
#line 44 "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\Orders\UserOrderList.cshtml"
                                                                  order.Id

#line default
#line hidden
#nullable disable
            );
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 48 "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\Orders\UserOrderList.cshtml"
                        }

#line default
#line hidden
#nullable disable

            WriteLiteral("                    </div>\r\n                </div>\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6269170d59ae5bfe3029e891a97a0ca145544c47ec0c0a2b40d2202d268248c813557", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
            WriteLiteral(
#nullable restore
#line 51 "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\Orders\UserOrderList.cshtml"
                                                       order.Id

#line default
#line hidden
#nullable disable
            );
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n");
#nullable restore
#line 54 "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\Orders\UserOrderList.cshtml"
        }

#line default
#line hidden
#nullable disable

            WriteLiteral("    </div>\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
#nullable restore
#line 58 "C:\Users\Acer\Documents\GitHub\MusicStoreProektItKariera\ToniStore\UI\Views\Orders\UserOrderList.cshtml"
            
    string GetStatusBadge(string status) => status switch
    {
        "Pending" => "warning",
        "Processing" => "primary",
        "Shipped" => "success",
        "Delivered" => "dark",
        "Cancelled" => "danger",
        _ => "secondary"
    };

#line default
#line hidden
#nullable disable

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Order>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
