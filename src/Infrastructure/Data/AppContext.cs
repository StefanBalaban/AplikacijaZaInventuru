using System.Reflection;
using System.Reflection.Emit;
using ApplicationCore.Entities;
using ApplicationCore.Entities.DietPlanAggregate;
using ApplicationCore.Entities.MealAggregate;
using ApplicationCore.Entities.NotificationAggregate;
using ApplicationCore.Entities.UserAggregate;
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
        public DbSet<DietPlanPeriod> DietPlanPeriod { get; set; }
        public  DbSet<UserContactInfo> UserContactInfo { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<NotificationRule> NotificationRule { get; set; }
        public DbSet<NotificationRuleUserContactInfos> NotificationRuleUserContactInfos { get; set; }
        public DbSet<UserWeightEvidention> UserWeightEvidention { get; set; }
        public DbSet<UserSubscription> UserSubscription { get; set; }

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

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Entity<NotificationRuleUserContactInfos>().HasKey(bc => new { bc.UserContactInfosId, bc.NotificationRuleId});
            builder.Entity<NotificationRuleUserContactInfos>()
                .HasOne(bc => bc.NotificationRule)
                .WithMany(b => b.NotificationRuleUserContactInfos)
                .HasForeignKey(bc => bc.NotificationRuleId);
            builder.Entity<NotificationRuleUserContactInfos>()
                .HasOne(bc => bc.UserContactInfo)
                .WithMany(c => c.NotificationRuleUserContactInfos)
                .HasForeignKey(bc => bc.UserContactInfosId);
        }
    }
}