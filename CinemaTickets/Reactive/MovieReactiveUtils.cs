using CinemaTickets.Models;
using CinemaTickets.UserControls.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Reactive
{
    class MovieReactiveUtils
    {
        private static readonly Subject<SingleMovieUserControl> EditMovieSubject = new();
        public static readonly IObservable<SingleMovieUserControl> EditMovieObservable = EditMovieSubject;

        private static readonly Subject<SingleMovieUserControl> CancelEditMovieSubject = new();
        public static readonly IObservable<SingleMovieUserControl> CancelEditMovieObservable = CancelEditMovieSubject;

        private static readonly Subject<long> DeleteMovieSubject = new();
        public static readonly IObservable<long> DeleteMovieObservable = DeleteMovieSubject;

        private static readonly Subject<Movie> SaveMovieSubject = new();
        public static readonly IObservable<Movie> SaveMovieObservable = SaveMovieSubject;
        private MovieReactiveUtils()
        {

        }

        public static void OnEditMovie(SingleMovieUserControl singleMovieUserControl)
        {
            EditMovieSubject.OnNext(singleMovieUserControl);
        }

        public static void OnCancelEditMovie(SingleMovieUserControl singleMovieUserControl)
        {
            CancelEditMovieSubject.OnNext(singleMovieUserControl);
        }
        public static void OnDeleteMovie(long id)
        {
            DeleteMovieSubject.OnNext(id);
        }

        public static void OnSaveMovie(Movie movie)
        {
            SaveMovieSubject.OnNext(movie);
        }
    }
}
