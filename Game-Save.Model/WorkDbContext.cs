using Microsoft.EntityFrameworkCore;

namespace Game_Save.Model
{
    public class WorkDbContext : DbContext
    {
        public DbSet<Testimony> Testimonys { get; set; } = null!;

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=.\\Work.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}