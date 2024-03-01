using Microsoft.EntityFrameworkCore;

namespace CRCRegistros.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Users> Users { get; set; }
    public DbSet<Items> Items { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Emprestimo> Emprestimo { get; set; }
}