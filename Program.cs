using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ToDoList.Data;

var builder = WebApplication.CreateBuilder(args);

//Connect To Server
builder.Services.AddDbContext<ApplicationDbContext>(Options => Options.UseNpgsql("name=defaultConnectionString"));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(builder =>
    builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
);

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();