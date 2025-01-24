using Microsoft.EntityFrameworkCore;
using AracTamirRandevuMVC.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<AracTamirRandevuDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlconnection"));
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".MySessionCookie";
    options.IdleTimeout = TimeSpan.FromMinutes(30);   //Session ayarlar�
}
    );

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Anasayfa/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=YonetimPaneli}/{action=Index}/{id?}");
    endpoints.MapControllerRoute("default", "{controller=Anasayfa}/{action=Index}/{id?}");
});

app.Run();
