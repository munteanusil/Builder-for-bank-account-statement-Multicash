using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MultiCashApp.Data;
using MultiCashApp.Helpers;
using MultiCashApp.IsoWeekHelper;
using MultiCashApp.Middleware;
using MultiCashApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("Server = MKDEV01\\MMR_DEV;Database=MultiCashAppDB;Trusted_Connection=True;TrustServerCertificate=true;")));

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("Admin", policy => policy.Requirements.Add(new RoleRequirement(new string[] { "ADMIN" })));
    opt.AddPolicy("Poweruser", policy => policy.Requirements.Add(new RoleRequirement(new string[] { "ADMIN", "POWERUSER" })));
    opt.AddPolicy("User", policy => policy.Requirements.Add(new RoleRequirement(new string[] { "ADMIN", "POWERUSER", "USER" })));
});

builder.Services.AddScoped<IAuthorizationHandler, CustomAutorization>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


 void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseEndpoints(endpoints =>
 {
     endpoints.MapControllerRoute(
         //name: "SupplierUpload",
         //pattern: "Supplier/Upload",
         //defaults: new { controller = "Supplier", action = "Upload" }

         name: "default",
         pattern: "{controller=Supplier}/{action= Index}/{ id ?}" ); 
     // Alte rute
 });

    // Alte configurații
}







app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

