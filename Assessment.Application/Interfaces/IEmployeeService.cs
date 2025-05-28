using Assessment.Application.ViewModels;
using Assessment.Domain.Models;

namespace Assessment.Application.Interfaces
{
    public interface IEmployeeService
    {
        // Read: Get all employees
        IEnumerable<EmployeeViewModel> GetEmployees();

        // Read: Get a single employee by Id
        Employee? GetEmployee(int id);

        // Create: Add a new employee
        void AddEmployee(Employee employee);

        // Update: Update an existing employee
        void UpdateEmployee(Employee employee);

        // Delete: Remove an employee by Id
        void DeleteEmployee(int id);
    }
}
