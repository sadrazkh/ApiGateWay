using ApiGateWay.Controllers;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.UseContentRoot(Directory.GetCurrentDirectory());
//builder.WebHost.ConfigureKestrel(serverOption =>
//    {
//        serverOption.AddServerHeader = false;
//    }).UseIISIntegration()
//    .ConfigureAppConfiguration((context, configurationBuilder) =>
//    {
//        var env = context.HostingEnvironment;
//        configurationBuilder
//            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
//            .AddEnvironmentVariables();
//        if (context.HostingEnvironment.IsDevelopment() || context.HostingEnvironment.EnvironmentName == "local")
//        {
//        }
//        else
//        {
//        }

//        configurationBuilder.Build();
//    });

// Add services to the container.
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    //.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();


builder.Services.AddOcelot(configuration: builder.Configuration);

builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    //endpoints.MapControllerRoute(
    //    name: "Account",
    //    pattern:"{{area:exists}/{action = Index}/{id ?}}"
    //);
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

});

app.MapControllers();
app.UseOcelot().Wait();

app.Run();
