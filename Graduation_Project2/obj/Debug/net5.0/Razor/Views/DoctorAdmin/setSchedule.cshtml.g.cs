#pragma checksum "C:\Users\user\source\repos\Graduation_Project2 - Copy\Graduation_Project2\Views\DoctorAdmin\setSchedule.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6160218e3986351f2e2be6d13575720e948e63eb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DoctorAdmin_setSchedule), @"mvc.1.0.view", @"/Views/DoctorAdmin/setSchedule.cshtml")]
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
#nullable restore
#line 1 "C:\Users\user\source\repos\Graduation_Project2 - Copy\Graduation_Project2\Views\_ViewImports.cshtml"
using Graduation_Project2;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\user\source\repos\Graduation_Project2 - Copy\Graduation_Project2\Views\_ViewImports.cshtml"
using Graduation_Project2.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6160218e3986351f2e2be6d13575720e948e63eb", @"/Views/DoctorAdmin/setSchedule.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3cc9ddd6d694cb5becfbf00f6f5d4aa5c420b380", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_DoctorAdmin_setSchedule : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Graduation_Project2.Models.Schedule>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "date", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "setSchedule", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<!DOCTYPE html>\r\n<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6160218e3986351f2e2be6d13575720e948e63eb4462", async() => {
                WriteLiteral(@"
	<title>Schedule</title>

	<style>

		body {
			font-family: Arial, sans-serif;
			margin: 20px;
		}



		h1 {
			text-align: center;
		}



		form {
			max-width: 70%;
			margin: 0 auto;
		}



			form div {
				margin-bottom: 5px;
			}



		label {
			display: inline-block;
			padding: 5px;
		}



		input[type=""datetime-local""],
		input[type=""text""] {
			padding: 5px;
			border: 1px solid #ccc;
			border-radius: 4px;
			font-size: 14px;
			margin: auto 2px;
		}



		button {
			padding: 8px 16px;
			margin: 3px;
			background-color: #4CAF50;
			color: #fff;
			border: none;
			border-radius: 4px;
			cursor: pointer;
		}



			button:hover {
				background-color: #45a049;
			}



			button:active {
				background-color: #3d8e41;
			}

	</style>
	
	
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6160218e3986351f2e2be6d13575720e948e63eb6264", async() => {
                WriteLiteral("\r\n");
                WriteLiteral("\r\n");
#nullable restore
#line 81 "C:\Users\user\source\repos\Graduation_Project2 - Copy\Graduation_Project2\Views\DoctorAdmin\setSchedule.cshtml"
      
		int cont = 0;
	

#line default
#line hidden
#nullable disable
                WriteLiteral("\t<h1>");
                WriteLiteral("\t</h1>\r\n\t<p class=\"error-message\">");
#nullable restore
#line 87 "C:\Users\user\source\repos\Graduation_Project2 - Copy\Graduation_Project2\Views\DoctorAdmin\setSchedule.cshtml"
                        Write(TempData["mass"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</p>\r\n\t");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6160218e3986351f2e2be6d13575720e948e63eb7184", async() => {
                    WriteLiteral("\r\n\t\t\r\n\t\t<label>Date:</label><br>\r\n\t\t<div id=\"bigContainer\"> \r\n\t\t\t<div id=\"date-time-container\">\r\n\t\t\t\t<div>\r\n\t\t\t\t\t");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "6160218e3986351f2e2be6d13575720e948e63eb7593", async() => {
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#nullable restore
#line 94 "C:\Users\user\source\repos\Graduation_Project2 - Copy\Graduation_Project2\Views\DoctorAdmin\setSchedule.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Date);

#line default
#line hidden
#nullable disable
                    __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Name = (string)__tagHelperAttribute_0.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                    BeginWriteTagHelperAttribute();
                    __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                    __tagHelperExecutionContext.AddHtmlAttribute("required", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\r\n");
                    WriteLiteral("\t\t\t\t</div>\r\n\t\t\t</div>\r\n\t\t</div>\r\n");
                    WriteLiteral("\t\t<button type=\"submit\" id=\"addTime\" name=\"btnSubmit\" value=\"add\">Add new Date and Time</button><br />\r\n\t\t<button type=\"submit\" name=\"btnSubmit\" value=\"save\">Save Changes </button>\r\n\t\r\n\t");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n\t<script>/*\r\n\t\tvar counter = 0;\r\n\t\tvar divContainer;\r\n\t\t//var c=");
#nullable restore
#line 110 "C:\Users\user\source\repos\Graduation_Project2 - Copy\Graduation_Project2\Views\DoctorAdmin\setSchedule.cshtml"
           Write(cont);

#line default
#line hidden
#nullable disable
                WriteLiteral(@";
		function addDateTime() {
			counter++;



			var dateTimeContainer = document.getElementById(""date-time-container"");

			var datetimeInput = document.createElement(""input"");
			datetimeInput.type = ""datetime-local"";
			datetimeInput.name = ""Model.schedules.ElementAt(""+ c +"").Date"";
			datetimeInput.required = true;

			var hideInput = document.createElement(""input"");
			hideInput.type = ""hidden"";
			hideInput.name = ""schedules.ElementAt("" + c + "").state"";
			c++;


			var dateTimeWrapper = document.createElement(""div"");
			dateTimeWrapper.appendChild(datetimeInput);
			dateTimeWrapper.appendChild(hideInput);

			if (counter == 32) {
				var btn = document.getElementById(""addTime"");
				btn.style.background = ""#aaa""
				btn.style.color = ""white"";
				btn.disabled = true;
			}

			else if (counter % 8 == 0) {
				var bigContainer = document.getElementById(""bigContainer"");
				bigContainer.style.display = ""flex"";
				divContainer = document.createElement(""div"");
				divConta");
                WriteLiteral(@"iner.appendChild(dateTimeWrapper);
				divContainer.style.margin = ""auto 2px;""
				bigContainer.appendChild(divContainer);
				//var child = dateTimeContainer.children ;
				//child[0].parentNode.insertBefore(divContainer,child[1]);

				//divContainer.appendChild(dateTimeWrapper);
			}
			else if (counter > 8) {
				divContainer.appendChild(dateTimeWrapper);
			}
			else
				dateTimeContainer.appendChild(dateTimeWrapper);
		}

		/*    function setSchedule() {
			  var datetimeInputs = document.getElementsByName(""datetime-local"");

			  var schedules = [];

			  for (var i = 0; i < datetimeInputs.length; i++) {
				var date = datetimeInputs[i].value;

				var schedule = {
				  date: date
				};

				schedules.push(schedule);
			  }

			  // Do something with the schedule information
			  console.log(""Schedules: "" + JSON.stringify(schedules));

			  // You can also perform further processing or send the data to a server using AJAX

			  // Clear the form inputs
			  documen");
                WriteLiteral("t.getElementById(\"date-time-container\").innerHTML = \"\";\r\n\t\t\t} */\r\n\r\n\t</script>\r\n\t\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Graduation_Project2.Models.Schedule> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591