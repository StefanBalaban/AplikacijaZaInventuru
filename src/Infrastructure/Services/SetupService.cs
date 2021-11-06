using System.Threading.Tasks;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class SetupService
    {
        public static async Task InitDataAsync(AppContext context)
        {
            context.Database.Migrate();

            if (!(await context.UnitOfMeasures.AnyAsync()))
            {
                await context.UnitOfMeasures.AddAsync(new ApplicationCore.Entities.UnitOfMeasure
                {
                    Measure = "Komad"
                });
                await context.UnitOfMeasures.AddAsync(new ApplicationCore.Entities.UnitOfMeasure
                {
                    Measure = "Težina"
                });

                await context.SaveChangesAsync();
            }

        }
        public static void InitIdentityAsync(AppIdentityDbContext context)
        {
            context.Database.Migrate();
        }
    }
}