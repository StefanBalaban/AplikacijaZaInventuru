using System.Threading.Tasks;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Net.Sockets;

namespace Infrastructure.Services
{
    public class SetupService
    {
        private static async Task WaitUntilCanConnectToDb(AppContext context)
         {
            var conn = context.Database.GetDbConnection().DataSource;
            var cons = conn.Split(',');
            var maxTimeout = 7;            
            var connectionTimeout = 15000;
            for (int i = 1; i <= maxTimeout; i++)
            {
                try
                {
                    using TcpClient client = new TcpClient();                                    
                    await Task.Delay(connectionTimeout);
                    client.Connect(cons[0], System.Convert.ToInt32(cons[1]));                    
                    await Task.Delay(connectionTimeout);
                    break;
                }
                catch (System.Exception)
                {
                    System.Console.WriteLine($"Attempting to connect to database, attempt {i}/{maxTimeout}..");
                    if (i == maxTimeout)
                    {
                        throw new System.TimeoutException($"Failed to connect to database after {maxTimeout * connectionTimeout} miliseconds");
                    }
                }
            }
        }
        public static async Task MigrateContextAsync(AppContext context)
        {
            await WaitUntilCanConnectToDb(context);
            await context.Database.MigrateAsync();

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
    }
}