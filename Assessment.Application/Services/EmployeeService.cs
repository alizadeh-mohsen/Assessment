using Assessment.Application.Interfaces;
using Assessment.Application.ViewModels;
using Assessment.Domain.Interfaces;
using Assessment.Domain.Models;
using AutoMapper;

namespace Assessment.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository EmployeeRepository, IMapper mapper)
        {
            _employeeRepository = EmployeeRepository;
            _mapper = mapper;
        }

        public void AddEmployee(Employee employee)
        {
            _employeeRepository.AddEmployee(employee);
        }

        public void DeleteEmployee(int id)
        {
            _employeeRepository.DeleteEmployee(id);
        }

        public Employee? GetEmployee(int id)
        {
            return _employeeRepository.GetEmployeeById(id);
        }

        public IEnumerable<EmployeeViewModel> GetEmployees()
        {

            var employees = _employeeRepository.GetEmployees();
            return _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

        }

        public void UpdateEmployee(Employee employee)
        {
            _employeeRepository.UpdateEmployee(employee);
        }
    }
}