using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using BrainsToDo.Models;
using Task = BrainsToDo.Models.Task;

namespace BrainsToDo.Data
{
  public class DataContext : DbContext
  {
    private string _connectionString;
    
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
      var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
      _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString")!;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseNpgsql(_connectionString);
      optionsBuilder.LogTo(message => Debug.WriteLine(message));
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Task> Tasks { get; set; }

  }  
}

