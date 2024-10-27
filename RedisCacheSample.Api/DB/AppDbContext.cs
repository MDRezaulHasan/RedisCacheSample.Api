using Microsoft.EntityFrameworkCore;
using RedisCacheSample.Api.Models;

namespace RedisCacheSample.Api.DB;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext>options):base(options) { }
    
    public DbSet<User> Users { get; set; }
}