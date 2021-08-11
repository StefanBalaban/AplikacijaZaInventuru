using System.Reflection;
using System.Reflection.Emit;
using ApplicationCore.Entities;
using ApplicationCore.Entities.DietPlanAggregate;
using ApplicationCore.Entities.MealAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }

        public DbSet<FoodProduct> FoodProducts { get; set; }
        public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
        public DbSet<FoodStock> FoodStock { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealItem> MealItems { get; set; }
        public DbSet<DietPlan> DietPlans { get; set; }
        public DbSet<DietPlanMeal> DietPlanMeal { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Entity<DietPlanMeal>().HasKey(bc => new { bc.DietPlanId, bc.MealId });
            builder.Entity<DietPlanMeal>()
                .HasOne(bc => bc.DietPlan)
                .WithMany(b => b.DietPlanMeals)
                .HasForeignKey(bc => bc.DietPlanId);
            builder.Entity<DietPlanMeal>()
                .HasOne(bc => bc.Meal)
                .WithMany(c => c.DietPlanMeals)
                .HasForeignKey(bc => bc.MealId);
        }
    }
}