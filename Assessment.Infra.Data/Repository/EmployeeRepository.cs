using Assessment.Domain.Interfaces;
using Assessment.Domain.Models;
using Assessment.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Assessment.Infra.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private AssessmentDBContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(AssessmentDBContext ctx, ILogger<EmployeeRepository> logger)
        {
            _context = ctx;
            _logger = logger;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            try
            {
                return await _context.Employees.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving employees from the database.");
                return Enumerable.Empty<Employee>();
            }
        }

        public async Task<Employee?> GetEmployee(int id)
        {
            try
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
                if (employee == null)
                {
                    _logger.LogWarning($"Employee with ID {id} not found.");
                    return null;
                }
                return employee;
            }
            catch (Exception)
            {
                _logger.LogError($"Error retrieving employee with ID {id} from the database.");
                return null;
            }
        }

        public async Task<bool> AddEmployee(Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding employee to the database.");
                return false;
            }

        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            try
            {
                var existing = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employee.Id);

                if (existing == null)
                {
                    _logger.LogError($"Error retrieving employee with ID {employee.Id} from the database.");
                    return false;
                }
                else
                {
                    existing.Name = employee.Name;
                    existing.JobPosition = employee.JobPosition;
                    existing.Email = employee.Email;
                    return await _context.SaveChangesAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating employee in the database.");
                return false;
            }

        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                _logger.LogError($"Error retrieving employee with ID {id} from the database.");
                return false;
            }
            else
            {
                _context.Employees.Remove(employee);
                return await _context.SaveChangesAsync() > 0;
            }
        }
    }
}