using Dictionary.Dal;
using Dictionary.Dal.Implementation;
using Dictionary.Dal.Interfaces;
using Dictionary.Services.Implementations;
using Dictionary.Services.Implementations.AnotherImplementations;
using Dictionary.Services.Interfaces;
using Dictionary.Services.Interfaces.HelppersInterfaces;
using Dictionary.Services.Interfaces.SortInterfaces;
using DictionaryTatarcha.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dictionary
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Задача высого Dpi процессора.
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;
            Application.Run(ServiceProvider.GetRequiredService<Form1>());
        }
        //Свойство для подключения сервисов.
        public static IServiceProvider ServiceProvider { get; private set; }
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddDbContext<ApplicationDbContext>();
                    services.AddScoped<IDictionaryDal<Noun>, NounDal>();
                    services.AddScoped<IDictionaryDal<Adjective>, AdjectiveDal>();
                    services.AddScoped<IDictionaryDal<Verb>, VerbDal>();
                    services.AddScoped<IWordServise<Noun>, NounServise>();
                    services.AddScoped<IWordServise<Verb>, VerbServise>();
                    services.AddScoped<IWordServise<Adjective>, AdjectiveServise>();
                    services.AddScoped<IWorkWithTextElements, WorkWithTextElements>();
                    services.AddScoped<ISelectionSort, SelectionSort>();
                    services.AddTransient<Form1>();
                });
        }
    }
}