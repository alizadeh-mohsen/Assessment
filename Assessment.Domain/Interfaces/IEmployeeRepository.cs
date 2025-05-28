using Assessment.Domain.Models;

namespace Assessment.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Employee employee);
        void DeleteEmployee(int id);
        Employee? GetEmployeeById(int id);
        IEnumerable<Employee> GetEmployees();
        void UpdateEmployee(Employee employee);
    }
}
