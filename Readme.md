# firstDBusingBackend

Install Packages "npSQL" and "Microsoft Tools"

Into /Programm.cd


    builder.Services.AddDbContext<ApplicationDbContext>(Options => Options.UseNpgsql("name=defaultConnectionString"));

Into /appsettings.json

    "ConnectionStrings": {
    
    "defaultConnectionString": "Host=localhost;Database=BankDataBase;Username=postgres;Password=Cynthia /3pl3ting"
    
    },

Into Data/ApplicationDbContext.cs

    using Microsoft.EntityFrameworkCore;
    
    using WebApplication1.Domain;
    
    namespace WebApplication1.Data;
    
    public class ApplicationDbContext : DbContext
    {
    //ctor
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }
    public DbSet<Bank> Banks { get; set; }
    }

In Terminal

    run in /Library/PostgreSQL/14/scripts/runpsql.sh; exit
    
    dotnet-ef Migrations add "Init"
    
    dotnet-ef Database update


and select in the opened Terminal:

    INSERT INTO "UserDB".public."User" ("FirstName", "LastName", "Age", "Gender", "Score")
    VALUES ('Cynthia','Jepleting',23,'Female',13)