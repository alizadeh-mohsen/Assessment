using Assessment.Domain.Interfaces;
using Assessment.Domain.Models;
using Assessment.Infra.Data.Context;

namespace Assessment.Infra.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private AssessmentDBContext _context;

        public EmployeeRepository(AssessmentDBContext ctx)
        {
            _context = ctx;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees.ToList();
        }

        public Employee? GetEmployeeById(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == id);
        }

        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            var existing = _context.Employees.FirstOrDefault(e => e.Id == employee.Id);
            if (existing != null)
            {
                existing.Name = employee.Name;
                existing.Job = employee.Job;
                existing.Email = employee.Email;
                _context.SaveChanges();
            }
        }

        public void DeleteEmployee(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }
    }
}