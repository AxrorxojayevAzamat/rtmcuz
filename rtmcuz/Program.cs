using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;
using System.Reflection;
using rtmcuz.Data;
using rtmcuz.Resources;
using rtmcuz.Services;
using System.Globalization;
using Microsoft.Extensions.Options;
using rtmcuz.Infrastructure.Filters;
using rtmcuz.Interfaces;
using Serilog;
using Serilog.Exceptions;
using Microsoft.AspNetCore.Identity;
using rtmcuz.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IdentityRtmcUzContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityRtmcUzContextConnection' not found.");
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();
// Add services to the container.

builder.Host.UseSerilog((ctx, lc) => lc
        .Enrich.WithExceptionDetails()
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));

// DAL
builder.Services.AddDbContext<RtmcUzContext>(options =>
              //options.UseMySql("server=localhost;port=3306;database=callcentercrm;uid=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.33-mysql"), x => x.UseNetTopologySuite())
              options.UseNpgsql(configuration.GetConnectionString("RtmcUzContext"), (x) => { })
              );

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<RtmcUzContext>();
builder.Services.AddControllersWithViews();

// localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddSingleton<LocalizationService>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddScoped<ISectionRepository, SectionRespository>();
builder.Services.AddMvc(mvc =>
{
    mvc.Filters.Add<NotFoundExceptionFilter>();
    mvc.Filters.Add<ValidateModelFilter>();
})
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        {
            var assemblyName = new AssemblyName(typeof(ApplicationResource).GetTypeInfo().Assembly.FullName);
            return factory.Create("ApplicationResource", assemblyName.Name);
        };
    });
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("uz-Latn-UZ"),
        new CultureInfo("uz-Cyrl-UZ"),
        new CultureInfo("ru"),
    };
    options.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;


});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RtmcUzContext>();
    // db.Database.EnsureDeleted();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

var requestLocalizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(requestLocalizationOptions.Value);
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Banners}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
