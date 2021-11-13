using CinemaTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Reactive
{
    public class LoginReactiveUtils
    {
        private static readonly Subject<AccountType> LoginSubject = new();
        public static readonly IObservable<AccountType> LoginObservable = LoginSubject;

        private LoginReactiveUtils()
        {
        }

        public static void OnLogin(AccountType accountType)
        {
            LoginSubject.OnNext(accountType);
        }
    }
}
