#pragma checksum "C:\Users\chuy_\Documents\Jicamon\Moya\AutNutriYA\Views\Dietas\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b9bea2834bee6bea3ff0fdc473843c63570baeec"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Dietas_Details), @"mvc.1.0.view", @"/Views/Dietas/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Dietas/Details.cshtml", typeof(AspNetCore.Views_Dietas_Details))]
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
#line 1 "C:\Users\chuy_\Documents\Jicamon\Moya\AutNutriYA\Views\_ViewImports.cshtml"
using AutNutriYA;

#line default
#line hidden
#line 2 "C:\Users\chuy_\Documents\Jicamon\Moya\AutNutriYA\Views\_ViewImports.cshtml"
using AutNutriYA.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b9bea2834bee6bea3ff0fdc473843c63570baeec", @"/Views/Dietas/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"97ad70b5e90d06a60f34d8b9e5161286f7677594", @"/Views/_ViewImports.cshtml")]
    public class Views_Dietas_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AutNutriYA.Dieta>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
            BeginContext(25, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\chuy_\Documents\Jicamon\Moya\AutNutriYA\Views\Dietas\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
            BeginContext(70, 119, true);
            WriteLiteral("\r\n<h2>Details</h2>\r\n\r\n<div>\r\n    <h4>Dieta</h4>\r\n    <hr />\r\n    <dl class=\"dl-horizontal\">\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(190, 42, false);
#line 14 "C:\Users\chuy_\Documents\Jicamon\Moya\AutNutriYA\Views\Dietas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Nombre));

#line default
#line hidden
            EndContext();
            BeginContext(232, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(276, 38, false);
#line 17 "C:\Users\chuy_\Documents\Jicamon\Moya\AutNutriYA\Views\Dietas\Details.cshtml"
       Write(Html.DisplayFor(model => model.Nombre));

#line default
#line hidden
            EndContext();
            BeginContext(314, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(358, 39, false);
#line 20 "C:\Users\chuy_\Documents\Jicamon\Moya\AutNutriYA\Views\Dietas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Dia));

#line default
#line hidden
            EndContext();
            BeginContext(397, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(441, 35, false);
#line 23 "C:\Users\chuy_\Documents\Jicamon\Moya\AutNutriYA\Views\Dietas\Details.cshtml"
       Write(Html.DisplayFor(model => model.Dia));

#line default
#line hidden
            EndContext();
            BeginContext(476, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(520, 44, false);
#line 26 "C:\Users\chuy_\Documents\Jicamon\Moya\AutNutriYA\Views\Dietas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Desayuno));

#line default
#line hidden
            EndContext();
            BeginContext(564, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(608, 40, false);
#line 29 "C:\Users\chuy_\Documents\Jicamon\Moya\AutNutriYA\Views\Dietas\Details.cshtml"
       Write(Html.DisplayFor(model => model.Desayuno));

#line default
#line hidden
            EndContext();
            BeginContext(648, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(692, 45, false);
#line 32 "C:\Users\chuy_\Documents\Jicamon\Moya\AutNutriYA\Views\Dietas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.ColacionM));

#line default
#line hidden
            EndContext();
            BeginContext(737, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(781, 41, false);
#line 35 "C:\Users\chuy_\Documents\Jicamon\Moya\AutNutriYA\Views\Dietas\Details.cshtml"
       Write(Html.DisplayFor(model => model.ColacionM));

#line default
#line hidden
            EndContext();
            BeginContext(822, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(866, 42, false);
#line 38 "C:\Users\chuy_\Documents\Jicamon\Moya\AutNutriYA\Views\Dietas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Comida));

#line default
#line hidden
            EndContext();
            BeginContext(908, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(952, 38, false);
#line 41 "C:\Users\chuy_\Documents\Jicamon\Moya\AutNutriYA\Views\Dietas\Details.cshtml"
       Write(Html.DisplayFor(model => model.Comida));

#line default
#line hidden
            EndContext();
            BeginContext(990, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1034, 45, false);
#line 44 "C:\Users\chuy_\Documents\Jicamon\Moya\AutNutriYA\Views\Dietas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.ColacionT));

#line default
#line hidden
            EndContext();
            BeginContext(1079, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1123, 41, false);
#line 47 "C:\Users\chuy_\Documents\Jicamon\Moya\AutNutriYA\Views\Dietas\Details.cshtml"
       Write(Html.DisplayFor(model => model.ColacionT));

#line default
#line hidden
            EndContext();
            BeginContext(1164, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1208, 40, false);
#line 50 "C:\Users\chuy_\Documents\Jicamon\Moya\AutNutriYA\Views\Dietas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Cena));

#line default
#line hidden
            EndContext();
            BeginContext(1248, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1292, 36, false);
#line 53 "C:\Users\chuy_\Documents\Jicamon\Moya\AutNutriYA\Views\Dietas\Details.cshtml"
       Write(Html.DisplayFor(model => model.Cena));

#line default
#line hidden
            EndContext();
            BeginContext(1328, 47, true);
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n<div>\r\n    ");
            EndContext();
            BeginContext(1376, 74, false);
#line 58 "C:\Users\chuy_\Documents\Jicamon\Moya\AutNutriYA\Views\Dietas\Details.cshtml"
Write(Html.ActionLink("Edit", "Edit", new { PK = Model.Nombre, RK = Model.Dia }));

#line default
#line hidden
            EndContext();
            BeginContext(1450, 8, true);
            WriteLiteral(" |\r\n    ");
            EndContext();
            BeginContext(1458, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b9da3239ce76471d99d5c27e14b51985", async() => {
                BeginContext(1480, 12, true);
                WriteLiteral("Back to List");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1496, 10, true);
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AutNutriYA.Dieta> Html { get; private set; }
    }
}
#pragma warning restore 1591
