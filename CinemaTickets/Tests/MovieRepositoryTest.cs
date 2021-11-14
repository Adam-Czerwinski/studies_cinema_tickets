using CinemaTickets.Models;
using CinemaTickets.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Tests
{
    [TestClass]
    public class MovieRepositoryTest
    {
        private ServiceProvider? serviceProvider;
        private IMovieRepository? _movieRepository;

        [TestInitialize]
        public void Initialize()
        {
            ServiceCollection services = new();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
            _movieRepository = serviceProvider.GetService<IMovieRepository>();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<CinematicketsContext>(options => options.UseSqlServer("Data Source=localhost,1433;Initial Catalog=cinematickets;User ID=sa;Password=Qweasd#21;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"), ServiceLifetime.Transient);
            services.AddScoped<IMovieRepository, MovieRepository>();
        }


        [TestMethod]
        public async Task MovieRepository_ShouldAdd_ReturnAdded()
        {
            Movie newMovie = new()
            {
                Title = "Test movie",
                Year = DateTime.Now,
                Duration = 120
            };

            var dbMovie = await _movieRepository!.AddMovieAsync(newMovie);

            Assert.AreEqual(newMovie.Title, dbMovie.Title);
            Assert.AreEqual(newMovie.Year, dbMovie.Year);
            Assert.AreEqual(newMovie.Duration, dbMovie.Duration);
            var movies = _movieRepository.GetMovies();
            Assert.IsTrue(movies.Any(movie => movie.Title == newMovie.Title && movie.Id == newMovie.Id));
        }

        [TestMethod]
        public async Task MovieRepository_ShouldUpdate_ReturnUpdated()
        {
            Movie newMovie = new()
            {
                Title = "Test movie",
                Year = DateTime.Now,
                Duration = 120
            };

            var dbMovie = await _movieRepository!.AddMovieAsync(newMovie);

            dbMovie.Title = "Update title";
            var updatedDbMovie = await _movieRepository!.UpdateMovieAsync(dbMovie);
            var movies = _movieRepository.GetMovies();
            Assert.IsTrue(movies.Any(movie => movie.Title == updatedDbMovie.Title && movie.Id == updatedDbMovie.Id));
        }

        [TestMethod]
        public async Task MovieRepository_ShouldRemove_ReturnNothing()
        {
            Movie newMovie = new()
            {
                Title = "Test movie",
                Year = DateTime.Now,
                Duration = 120
            };

            var dbMovie = await _movieRepository!.AddMovieAsync(newMovie);
            await _movieRepository.RemoveMovieAsync(dbMovie);
            var movies = _movieRepository.GetMovies();
            Assert.IsFalse(movies.Any(movie => movie.Id == dbMovie.Id));
        }


    }
}
