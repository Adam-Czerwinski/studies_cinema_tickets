using CinemaTickets.Authentication;
using CinemaTickets.Models;
using CinemaTickets.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CinemaTickets
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<CinematicketsContext>(options => options.UseSqlServer("Data Source=localhost,1433;Initial Catalog=cinematickets;User ID=sa;Password=Qweasd#21;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
            services.AddSingleton<MainWindow>();

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IHallRepository, HallRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<ISeanceRegistrationRepository, SeanceRegistrationRepository>();
            services.AddScoped<ISeanceRepository, SeanceRepository>();

            services.AddScoped<IPasswordCryption, PasswordCryption>();
            services.AddScoped<IAuthStore, AuthStore>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}
