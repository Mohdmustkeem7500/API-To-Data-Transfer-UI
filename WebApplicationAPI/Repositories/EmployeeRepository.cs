using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using WebApplicationAPI.DataContext;

namespace WebApplicationAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _Context;
        public EmployeeRepository(ApplicationDbContext Context)
        {
            _Context = Context;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await _Context.Employees.AddAsync(employee);
            await _Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee> DeleteEmployee(int Id)
        {
            var result = await _Context.Employees.Where(a => a.Id == Id).FirstOrDefaultAsync();
            if (result!=null)
            {
                _Context.Employees.Remove(result);
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Employee> GetEmployee(int Id)
        {
            return await _Context.Employees
                .FirstOrDefaultAsync(a => a.Id == Id);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _Context.Employees.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> Search(string name)
        {
            IQueryable<Employee> query = _Context.Employees;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.Name.Contains(name));
            }
            return await query.ToListAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await _Context.Employees
                .FirstOrDefaultAsync(a => a.Id == employee.Id);
            if (result!=null)
            {
                result.Name = employee.Name;
                result.City = employee.City;
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
