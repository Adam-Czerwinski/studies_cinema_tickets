using CinemaTickets.Models;
using CinemaTickets.UserControls.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Reactive
{
    public class EmployeeReactiveUtils
    {
        private EmployeeReactiveUtils() { }

        private static readonly Subject<SingleEmployeeUserControl> EditEmployeeSubject = new();
        public static readonly IObservable<SingleEmployeeUserControl> EditEmployeeObservable = EditEmployeeSubject;

        private static readonly Subject<SingleEmployeeUserControl> CancelEditEmployeeSubject = new();
        public static readonly IObservable<SingleEmployeeUserControl> CancelEditEmployeeObservable = CancelEditEmployeeSubject;

        private static readonly Subject<long> DeleteEmployeeSubject = new();
        public static readonly IObservable<long> DeleteEmployeeObservable = DeleteEmployeeSubject;

        private static readonly Subject<Employee> SaveEmployeeSubject = new();
        public static readonly IObservable<Employee> SaveEmployeeObservable = SaveEmployeeSubject;

        public static void OnEditEmployee(SingleEmployeeUserControl singleEmployeeUserControl)
        {
            EditEmployeeSubject.OnNext(singleEmployeeUserControl);
        }

        public static void OnCancelEditEmployee(SingleEmployeeUserControl singleEmployeeUserControl)
        {
            CancelEditEmployeeSubject.OnNext(singleEmployeeUserControl);
        }
        public static void OnDeleteEmployee(long id)
        {
            DeleteEmployeeSubject.OnNext(id);
        }

        public static void OnSaveEmployee(Employee Employee)
        {
            SaveEmployeeSubject.OnNext(Employee);
        }
    }
}
