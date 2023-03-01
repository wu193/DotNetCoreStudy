using CustomMiddleWare;
using Microsoft.AspNetCore.Http;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseWriteDate();
//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("12313");
//    await next.Invoke();
//});
//app.Map("/WeatherForecast", appBuilder =>
//{
//    app.Use(async (context, next) =>
//    {
       
//        await context.Response.WriteAsync("GetWeatherForecast");  
//        Console.WriteLine("GetWeatherForecast");
//        await next.Invoke();
//    }).Run(async context =>
//    {
//        await context.Response.WriteAsync(DateTime.Now.ToShortDateString());
//    });
//});
//app.Map("/map1", appBuilder =>
//{
//    app.Use(async (context, next) =>
//    {

//        await context.Response.WriteAsync("map1");
//        await next.Invoke();
//    }).Run(async context =>
//    {
//        await context.Response.WriteAsync(DateTime.Now.ToShortDateString());
//    });
//});
app.MapWhen(context => context.Request.Query["q"]=="abc", appBuilder =>
{
    app.Use(async (context, next) =>
    {

        await context.Response.WriteAsync("abc");
        Console.WriteLine("abc");
        await next.Invoke();
    }).Run(async context =>
    {
        await context.Response.WriteAsync(context.Request.Query["q"]);
    });
});
app.Run();
