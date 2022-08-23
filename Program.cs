using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ToDoList.Data;
using ToDoList.security;

var builder = WebApplication.CreateBuilder(args);

//Connect To Server
builder.Services.AddDbContext<ApplicationDbContext>(Options => Options.UseNpgsql("name=defaultConnectionString"));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<PasswordHandler>();
builder.Services.AddScoped<token>();

// builder.Services.AddSwaggerGen();

//To Read Tokens
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

    var app = builder.Build();

// Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     
//     app.UseExceptionHandler("/Home/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }


// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

//
// app.UseHttpsRedirection();

//Bindes FIles aus WWWROOT ein
// app.UseStaticFiles();

app.UseCors(builder =>
    builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
);

app.UseRouting();

//Middleware for Token
app.UseAuthentication();

app.UseAuthorization();
app.UseHttpLogging();
//definiert Startseite
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


//ModelbuilderConfig - Relationen cascade/delete

//DTO/PayLoad
//Aufteilung ein einzelne Bausteine Datenbank - Controller - 