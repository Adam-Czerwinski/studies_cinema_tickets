using CinemaTickets.Exceptions;
using CinemaTickets.Models;
using CinemaTickets.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace CinemaTickets.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CinematicketsContext _context;

        public EmployeeRepository(CinematicketsContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            if (!ValidatorUtils.IsValidLogin(employee.Login))
            {
                throw new ValidationException("Nieprawidłowy login");
            }
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            _context.Entry(employee).State = EntityState.Detached;
            return employee;
        }

        public bool ExistsByLoginIgnoreCase(string login)
        {
            return _context.Employees.Any(employee => employee.Login.ToLower() == login.ToLower());
        }

        public Employee? GetByLogin(string login)
        {
            return _context.Employees.AsNoTracking().SingleOrDefault(employee => login.Equals(employee.Login));
        }

        public List<Employee> GetEmployees()
        {
            return _context.Employees.AsNoTracking().ToList();
        }

        public async Task RemoveEmployeeAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            _context.Entry(employee).State = EntityState.Detached;
            return employee;
        }
    }
}
