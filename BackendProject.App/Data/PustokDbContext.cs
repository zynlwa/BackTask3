using BackendProject.App.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.App.Data
{
    public class PustokDbContext : DbContext
    {
        public PustokDbContext(DbContextOptions<PustokDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Slider> Sliders { get; set; }
    }
}
