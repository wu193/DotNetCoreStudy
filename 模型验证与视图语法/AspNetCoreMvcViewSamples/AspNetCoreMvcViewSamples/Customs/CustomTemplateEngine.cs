using Microsoft.AspNetCore.Mvc.Razor.Extensions;
using Microsoft.AspNetCore.Razor.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMvcViewSamples.Customs
{
    public class CustomTemplateEngine : MvcRazorTemplateEngine
    {
        public CustomTemplateEngine(RazorEngine engine, RazorProject project) : base(engine, project)
        {
        }

        public override RazorCSharpDocument GenerateCode(RazorCodeDocument codeDocument)
        {
            var csharpDocument = base.GenerateCode(codeDocument);
            var code = csharpDocument.GeneratedCode;

            return csharpDocument;
        }

    }
}
