using Application;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Seed data
        modelBuilder.Entity<Customer>().HasData(
            new Customer {Id = 1, FirstName = "Kristian", LastName = "Hollænder", Accounts = new List<Account>()}
        );
        modelBuilder.Entity<Customer>().HasData(
            new Customer {Id = 2, FirstName = "Andy", LastName = "BigBoy", Accounts = new List<Account>()}
        );
        modelBuilder.Entity<Customer>().HasData(
            new Customer {Id = 3, FirstName = "Test", LastName = "Bruger", Accounts = new List<Account>()}
        );
        
        modelBuilder.Entity<Account>().HasData(
            new Account {Id = 1, AccountName = "Ung Konto", Amount = 5000000, CustomerId = 1}
        );
        modelBuilder.Entity<Account>().HasData(
            new Account {Id = 2, AccountName = "Voksen Konto", Amount = 100, CustomerId = 2}
        );
        modelBuilder.Entity<Account>().HasData(
            new Account {Id = 3, AccountName = "Gammel Konto", Amount = 200, CustomerId = 3}
        );
        modelBuilder.Entity<Account>().HasData(
            new Account {Id = 4, AccountName = "Bare en Konto", Amount = 2000, CustomerId = 1}
        );
        
        //Account Model builder
        //Auto generate ID 
        modelBuilder.Entity<Account>()
            .Property(account => account.Id)
            .ValueGeneratedOnAdd();
        
        //User Model builder
        //Auto generate ID
        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
        //Make Emails unique
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        //Customer Model Builder
        //Auto generate ID
        modelBuilder.Entity<Customer>()
            .Property(customer => customer.Id)
            .ValueGeneratedOnAdd();
        
        //Foreign key to Customer ID
        modelBuilder.Entity<Account>()
            .HasOne(account => account.Customer)
            .WithMany(customer => customer.Accounts)
            .HasForeignKey(account => account.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<User> UserTable { get; set; }
}