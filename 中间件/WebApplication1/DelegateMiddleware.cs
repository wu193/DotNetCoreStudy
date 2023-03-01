namespace WebApplication1
{
    public class DelegateMiddleware
    {
        public Task DelegateMiddlewarel(HttpContext context, Func<Task> next)
        {
            Console.WriteLine("DelegateMiddleware");
            return next();
        }
    }
}
