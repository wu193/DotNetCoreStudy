using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMiddleWare
{
    public static class WriteDateMiddleWareExtensions
    {
        public static IApplicationBuilder UseWriteDate(this IApplicationBuilder app,string framt="yyyy-MM-dd")
        {
          return  app.UseMiddleware<WriteDateMiddleWare>(framt);
        }
    }
}
