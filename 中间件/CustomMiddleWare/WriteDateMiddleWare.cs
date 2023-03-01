using Microsoft.AspNetCore.Http;

namespace CustomMiddleWare
{
    public class WriteDateMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly string  framt;
        
        public WriteDateMiddleWare(RequestDelegate next,string framt)
        {
            this.next = next;
            this.framt = framt;
        }
        public Task InvokeAsync(HttpContext context)
        {
            context.Response.WriteAsync(DateTime.Now.ToString(framt));
            return next(context);
        }
    }
}