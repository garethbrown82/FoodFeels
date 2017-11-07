using System.Data.Entity;

namespace FoodFeels.Models
{
    public class FoodFeelsDBContext : DbContext
    {
        public DbSet<Food> Foods { get; set; }
        public DbSet<Feeling> Feelings { get; set; }
    }
}