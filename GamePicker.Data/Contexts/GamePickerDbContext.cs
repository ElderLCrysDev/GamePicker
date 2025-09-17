using Microsoft.EntityFrameworkCore;
using GamePicker.Data.Entities;

namespace GamePicker.Data.Contexts
{
    public class GamePickerDbContext : DbContext
    {
        public GamePickerDbContext(DbContextOptions<GamePickerDbContext> options): base(options){
        }

        public virtual DbSet<Recommendation> Recommendations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recommendation>().ToTable("T001_RECOMENDATIONS");
        }
        public virtual int ExecuteSqlRaw(string sql, params object[] parameters)
        {
            return Database.ExecuteSqlRaw(sql, parameters);
        }
        public virtual Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters)
        {
            return Database.ExecuteSqlRawAsync(sql, parameters);
        }
    }
}