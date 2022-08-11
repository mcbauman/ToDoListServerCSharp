using Microsoft.EntityFrameworkCore;

using ToDoList.Domain;

namespace ToDoList.Data;

public class ApplicationDbContext : DbContext
{
//ctor
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }
    public DbSet<UserClass> Users { get; set; }
    public DbSet<ToDoListClass> ToDoList { get; set; }
}