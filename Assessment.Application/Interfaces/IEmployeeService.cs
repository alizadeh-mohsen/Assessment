using Assessment.Application.ViewModels;

namespace Assessment.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeViewModel>> GetEmployees();
        Task<EmployeeViewModel?> GetEmployee(int id);
        Task<bool> AddEmployee(EmployeeViewModel employee);
        Task<bool> UpdateEmployee(EmployeeViewModel employee);
        Task<bool> DeleteEmployee(int id);
    }
}
