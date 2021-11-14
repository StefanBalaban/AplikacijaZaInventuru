using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinUI.Services;
using WinUI.Util;

namespace WinUI
{
    static class Program
    {
        public static IConfiguration Configuration;
        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<Form1>();
            services.AddScoped<Main>();
            services.AddTransient<IApiService, ApiService>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserSubscriptionService, UserSubscriptionService>();
        }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var services = new ServiceCollection();
            ConfigureServices(services);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException, true);
            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            

            var form1 = serviceProvider.GetRequiredService<Form1>();
            try
            {
                Application.Run(form1);
            }
            catch (Exception ex)
            {

                ExceptionHandlerUtil.HandleException(ex);
            }
            

        }
    }
}
