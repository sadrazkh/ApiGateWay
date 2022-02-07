using Autofac.Extensions.DependencyInjection;
using Common;
using Services.WebFramework.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

IConfiguration Configuration = builder.Configuration;
SiteSettings _siteSetting = Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
// Add services to the container.





builder.Services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));
builder.Services.AddUnitOfWork(Configuration);


builder.Services.AddAuthentication();
builder.Services.AddCustomIdentity(_siteSetting.IdentitySettings);
builder.Services.AddJwtAuthentication(_siteSetting.JwtSettings);




builder.Services.AddControllers();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
