using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library;

public class ApplicationContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    
    public DbSet<Author> Authors { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        
    }
}