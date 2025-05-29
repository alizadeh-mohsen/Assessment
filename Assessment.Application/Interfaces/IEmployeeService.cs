using Assessment.Application.ViewModels;
using Assessment.Domain.Models;

namespace Assessment.Application.Interfaces
{
    public interface IEmployeeService
    {
        // Read: Get all employees
        Task<IEnumerable<EmployeeViewModel>> GetEmployees();

        // Read: Get a single employee by Id
        Task<EmployeeViewModel?> GetEmployee(int id);

        // Create: Add a new employee
        Task<bool> AddEmployee(EmployeeViewModel employee);

        // Update: Update an existing employee
        Task<bool> UpdateEmployee(EmployeeViewModel employee);

        // Delete: Remove an employee by Id
        Task<bool> DeleteEmployee(int id);
    }
}
