using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Data;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerAspNetIdentity.Services
{
    public class SetupService
    {
        private static async Task WaitUntilCanConnectToDb(ApplicationDbContext context)
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
        public static async Task MigrateContextAsync(ApplicationDbContext context, UserManager<Models.ApplicationUser> userManager, Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole> roleManager)
        {
            await WaitUntilCanConnectToDb(context);
            if (!await context.Users.AnyAsync())
            {
                await AddTestUsers(userManager, roleManager);
            }

            await context.Database.MigrateAsync();
        }

        public static async Task AddTestUsers(UserManager<Models.ApplicationUser> userManager, RoleManager<Microsoft.AspNetCore.Identity.IdentityRole> roleManager)
        {
            var userDesktop = new ApplicationUser { UserName = "desktop", Email = "desktop@test.ba" };
            var userMobile = new ApplicationUser { UserName = "mobile", Email = "mobile@test.ba" };
            var result = await userManager.CreateAsync(userDesktop, "test");
            await userManager.CreateAsync(userMobile, "test");

            if (result.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync("Administrator"))
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR" });
                }

                if ((await userManager.GetUsersInRoleAsync("Administrator")).Count == 0)
                {
                    var createdUser = await userManager.FindByNameAsync("desktop");
                    await userManager.AddToRoleAsync(createdUser, "Administrator");
                }
            }
        }
    }
}