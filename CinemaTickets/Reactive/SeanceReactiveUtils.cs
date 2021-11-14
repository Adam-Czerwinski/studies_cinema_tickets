using CinemaTickets.Models;
using CinemaTickets.UserControls.Seances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Reactive
{
    public class SeanceReactiveUtils
    {
        private static readonly Subject<MoviesHall> SaveSeanceSubject = new();
        public static readonly IObservable<MoviesHall> SaveSeanceObservable = SaveSeanceSubject;

        private static readonly Subject<NewSeanceUserControl> CancelEditSeanceSubject = new();
        public static readonly IObservable<NewSeanceUserControl> CancelEditSeanceObservable = CancelEditSeanceSubject;


        private static readonly Subject<MoviesHall> AttendSeanceSubject = new();
        public static readonly IObservable<MoviesHall> AttendSeanceObservable = AttendSeanceSubject;

        private static readonly Subject<MoviesHall> CancelAttendSeanceSubject = new();
        public static readonly IObservable<MoviesHall> CancelAttendSeanceObservable = CancelAttendSeanceSubject;
        private SeanceReactiveUtils()
        {
        }

        public static void OnCancelEditSeance(NewSeanceUserControl newSeanceUserControl)
        {
            CancelEditSeanceSubject.OnNext(newSeanceUserControl);
        }

        public static void OnSaveSeance(MoviesHall seance)
        {
            SaveSeanceSubject.OnNext(seance);
        }

        public static void OnAttendSeance(MoviesHall seance)
        {
            AttendSeanceSubject.OnNext(seance);
        }

        public static void OnCancelAttendSeance(MoviesHall seance)
        {
            CancelAttendSeanceSubject.OnNext(seance);
        }
    }
}
