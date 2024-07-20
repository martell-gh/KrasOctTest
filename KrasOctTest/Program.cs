using System;
using System.Windows.Forms;
using KrasOctTest.Data;
using KrasOctTest.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KrasOctTest
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Создаем сервисный провайдер
            var serviceProvider = ConfigureServices();

            // Запускаем приложение с основной формой
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(serviceProvider.GetRequiredService<MainForm>());
        }

        // Метод для настройки сервисов
        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<TreeDbContext>(); 
            
            // Регистрируем формы
            services.AddTransient<MainForm>();
            services.AddSingleton<IDbContextFactory, DbContextFactory>();
            
            //services.AddTransient<DepartmentForm>();

            // Регистрируем другие зависимости
            //services.AddTransient<DepartmentService>();
            //services.AddTransient<CompanyContext>();

            return services.BuildServiceProvider();
        }
    }
}