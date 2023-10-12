using LR3.Models;
using Microsoft.EntityFrameworkCore;

namespace LR3.DbConnection;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> opts) : base(opts) { }

    public DbSet<Product> Products { get; set; }
}
