using Assessment.Application.Interfaces;
using Assessment.Application.ViewModels;
using Assessment.Domain.Interfaces;
using Assessment.Domain.Models;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace Assessment.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        #region Fields
        private IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly string _employeeListCacheKey = "employee_list";
        private string GetEmployeeCacheKey(int id) => $"employee_{id}"; 
        #endregion

        #region Public Methods
        public EmployeeService(IEmployeeRepository EmployeeRepository, IMapper mapper, IMemoryCache cache)
        {
            _employeeRepository = EmployeeRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetEmployees()
        {
            return await CachedLong(_employeeListCacheKey, async () =>
            {
                var employees = await _employeeRepository.GetEmployees();
                return _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            });
        }

        public async Task<EmployeeViewModel?> GetEmployee(int id)
        {
            string employeeCacheKey = GetEmployeeCacheKey(id);

            return await Cached(employeeCacheKey, async () =>
            {
                var employee = await _employeeRepository.GetEmployee(id);
                return employee == null ? null : _mapper.Map<EmployeeViewModel>(employee);
            });
        }

        public async Task<bool> AddEmployee(EmployeeViewModel employeeViewModel)
        {
            var employee = _mapper.Map<Employee>(employeeViewModel);
            if (await _employeeRepository.AddEmployee(employee))
            {
                InvalidateCache(employee.Id);
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateEmployee(EmployeeViewModel employeeViewModel)
        {
            var employee = _mapper.Map<Employee>(employeeViewModel);
            if (await _employeeRepository.UpdateEmployee(employee))
            {
                InvalidateCache(employee.Id);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            if (await _employeeRepository.DeleteEmployee(id))
            {
                InvalidateCache(id);
                return true;
            }
            return false;
        }

        #endregion

        #region Private Methods

        private async Task<T> CachedLong<T>(string key, Func<Task<T>> getData)
        {
            if (!_cache.TryGetValue(key, out T value))
            {
                value = await getData();
                _cache.Set(key, value);
            }
            return value;
        }

        private async Task<T> Cached<T>(string key, Func<Task<T>> getData)
        {
            if (!_cache.TryGetValue(key, out T value))
            {
                value = await getData();
                _cache.Set(key, value, TimeSpan.FromMinutes(5));
            }
            return value;
        }

        private void InvalidateCache(int id)
        {
            _cache.Remove(_employeeListCacheKey); 
            _cache.Remove(GetEmployeeCacheKey(id)); 
        }

        #endregion
    }
}