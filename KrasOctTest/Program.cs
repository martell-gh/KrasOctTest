using System;
using System.Windows.Forms;
using KrasOctTest.Data;
using KrasOctTest.Data.Employees;
using KrasOctTest.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KrasOctTest
{
    static class Program
    {
        [STAThread]
        static void Main()
        {

            var serviceProvider = ConfigureServices();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(serviceProvider.GetRequiredService<MainForm>());
        }

        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<TreeDbContext>();
            services.AddTransient<EmployeeDbContext>();

            services.AddTransient<MainForm>();
            services.AddTransient<SearchEmployee>();
            services.AddSingleton<ITreeDbContextFactory, TreeDbContextFactory>();
            services.AddSingleton<IEmployeeDbContextFactory, EmployeeDbContextFactory>();

            return services.BuildServiceProvider();
        }
    }
}