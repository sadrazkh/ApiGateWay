using ApiGateWay.Controllers;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Services.WebFramework.Configuration;
using Services.WebFramework.CustomMapping;
using Services.WebFramework.Middlewares;
using Services.WebFramework.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

IConfiguration Configuration = builder.Configuration;
SiteSettings _siteSetting = Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
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
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    //.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));
builder.Services.InitializeAutoMapper();
builder.Services.AddDbContext(Configuration);
builder.Services.AddUnitOfWork(Configuration);


builder.Services.AddAuthentication();
builder.Services.AddCustomIdentity(_siteSetting.IdentitySettings);


builder.Services.AddOcelot(configuration: builder.Configuration);

builder.Services.AddMinimalMvc();
builder.Services.AddJwtAuthentication(_siteSetting.JwtSettings);
builder.Services.AddCustomApiVersioning();
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.AddServices());


builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCustomExceptionHandler();
app.UseHsts();
app.UseHttpsRedirection();
//app.UseElmahCore(_siteSetting);

app.UseSwaggerAndUI();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

//Use this config just in Develoment (not in Production)
app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseEndpoints(config =>
{
    //config.MapAreaControllerRoute("Test", "Admin", "admin/{Route}");
    config.MapControllers(); // Map attribute routing
    //    .RequireAuthorization(); Apply AuthorizeFilter as global filter to all endpoints
    //config.MapDefaultControllerRoute(); // Map default route {controller=Home}/{action=Index}/{id?}
});

app.UseOcelot().Wait();
app.Run();

