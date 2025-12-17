using ApiProjeKampi.WebUI.Models;
using ApiProjeKampi.WebApi.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddSignalR();
builder.Services.AddHttpClient("openai", c =>
{
    c.BaseAddress=new Uri("https://api.openapi.com/");

});

// DbContext ekle
builder.Services.AddDbContext<ApiContext>(options =>
{
    options.UseSqlServer("Server=YOUR_SERVER_NAME;initial catalog=ApiYummyDb;integrated security=true;");
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapHub<ChatHub>("/chathub");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();
