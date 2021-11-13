using CinemaTickets.Models;
using CinemaTickets.UserControls.Halls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Reactive
{

    public class HallReactiveUtils
    {
        private static readonly Subject<SingleHallUserControl> EditHallSubject = new();
        public static readonly IObservable<SingleHallUserControl> EditHallObservable = EditHallSubject;

        private static readonly Subject<SingleHallUserControl> CancelEditHallSubject = new();
        public static readonly IObservable<SingleHallUserControl> CancelEditHallObservable = CancelEditHallSubject;

        private static readonly Subject<Hall> SaveHallSubject = new();
        public static readonly IObservable<Hall> SaveHallObservable = SaveHallSubject;

        private HallReactiveUtils() { }

        public static void OnEditHall(SingleHallUserControl singleHallUserControl)
        {
            EditHallSubject.OnNext(singleHallUserControl);
        }

        public static void OnCancelEditHall(SingleHallUserControl singleHallUserControl)
        {
            CancelEditHallSubject.OnNext(singleHallUserControl);
        }

        public static void OnSaveHall(Hall Hall)
        {
            SaveHallSubject.OnNext(Hall);
        }
    }
}
