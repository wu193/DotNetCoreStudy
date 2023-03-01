using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMvcViewSamples.Customs
{
    public abstract class CustomRazorPage<TModel> : RazorPage<TModel>
    {
        public string Email { get; } = "admin@xcode.me";
    }
}
