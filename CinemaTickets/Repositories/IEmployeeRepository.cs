using CinemaTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> AddEmployeeAsync(Employee employee);

        Task<Employee> UpdateEmployeeAsync(Employee employee);

        Task RemoveEmployeeAsync(Employee employee);

        List<Employee> GetEmployees();

        bool ExistsByLoginIgnoreCase(string login);

        Employee? GetByLogin(string login);  
    }
}
