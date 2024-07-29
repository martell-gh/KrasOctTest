using System;
using System.Windows.Forms;
using KrasOctTest.Data;
using KrasOctTest.Data.Employees;
using KrasOctTest.Services;
using KrasOctTest.TreeComponents;
using Microsoft.Extensions.DependencyInjection;

namespace KrasOctTest
{
    static class Program
    {
        [STAThread]
        static async Task Main()
        {
            var serviceProvider = ConfigureServices();

            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            var initialized = await dbContext.InitializeDatabase();

            if (!initialized) return;
            
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(serviceProvider.GetRequiredService<MainForm>());
        }

        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<ApplicationDbContext>();

            services.AddSingleton<MainForm>();
            services.AddSingleton<SearchEmployee>();
            services.AddSingleton<InputForm>();
            services.AddSingleton<NodeCreateForm>();
            
            services.AddSingleton<IDbContextFactory, DbContextFactory>();
            
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ITreeNodeRepository, TreeNodeRepository>();
            services.AddScoped<INodeService, NodeService>();

            return services.BuildServiceProvider();
        }
    }
}