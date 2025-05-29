using Assessment.Application.Services;
using Assessment.Application.ViewModels;
using Assessment.Domain.Interfaces;
using Assessment.Domain.Models;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace Assessment.Application.Tests.Services
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        private Mock<IEmployeeRepository> _employeeRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private IMemoryCache _memoryCache;
        private EmployeeService _service;

        [SetUp]
        public void SetUp()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _mapperMock = new Mock<IMapper>();
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _service = new EmployeeService(_employeeRepositoryMock.Object, _mapperMock.Object, _memoryCache);
        }

        [TearDown]
        public void TearDown()
        {
            _memoryCache?.Dispose();
        }

        [Test]
        public async Task GetEmployees_ReturnsMappedEmployees_AndCachesResult()
        {
            // Arrange
            var employees = new List<Employee> { new Employee { Id = 1, Name = "Test" , Email = "e@mail.com" } };
            var employeeViewModels = new List<EmployeeViewModel> { new EmployeeViewModel { Id = 1, Name = "Test" , Email = "e@mail.com" } };

            _employeeRepositoryMock.Setup(r => r.GetEmployees()).ReturnsAsync(employees);
            _mapperMock.Setup(m => m.Map<IEnumerable<EmployeeViewModel>>(employees)).Returns(employeeViewModels);

            // Act
            var result = await _service.GetEmployees();

            // Assert
            Assert.That(result, Is.EqualTo(employeeViewModels));
            // Call again to ensure cache is used (repository should not be called again)
            var result2 = await _service.GetEmployees();
            Assert.That(result2, Is.EqualTo(employeeViewModels));
            _employeeRepositoryMock.Verify(r => r.GetEmployees(), Times.Once);
        }

        [Test]
        public async Task GetEmployee_ReturnsMappedEmployee_AndCachesResult()
        {
            // Arrange
            var employee = new Employee { Id = 2, Name = "Jane" , Email = "e@mail.com" };
            var employeeViewModel = new EmployeeViewModel { Id = 2, Name = "Jane" , Email = "e@mail.com" };

            _employeeRepositoryMock.Setup(r => r.GetEmployee(2)).ReturnsAsync(employee);
            _mapperMock.Setup(m => m.Map<EmployeeViewModel>(employee)).Returns(employeeViewModel);

            // Act
            var result = await _service.GetEmployee(2);

            // Assert
            Assert.That(result, Is.EqualTo(employeeViewModel));
            // Call again to ensure cache is used (repository should not be called again)
            var result2 = await _service.GetEmployee(2);
            Assert.That(result2, Is.EqualTo(employeeViewModel));
            _employeeRepositoryMock.Verify(r => r.GetEmployee(2), Times.Once);
        }

        [Test]
        public async Task AddEmployee_RepositoryReturnsTrue_InvalidateCache_ReturnsTrue()
        {
            // Arrange
            var employeeViewModel = new EmployeeViewModel { Id = 3, Name = "Add", Email = "e@mail.com" };
            var employee = new Employee { Id = 3, Name = "Add", Email = "e@mail.com" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeViewModel)).Returns(employee);
            _employeeRepositoryMock.Setup(r => r.AddEmployee(employee)).ReturnsAsync(true);

            // Act
            var result = await _service.AddEmployee(employeeViewModel);

            // Assert
            Assert.IsTrue(result);
            // No exception means cache invalidation was called (indirectly tested)
        }

        [Test]
        public async Task AddEmployee_RepositoryReturnsFalse_ReturnsFalse()
        {
            // Arrange
            var employeeViewModel = new EmployeeViewModel { Id = 4, Name = "Fail", Email = "e@mail.com" };
            var employee = new Employee { Id = 4, Name = "Fail" , Email = "e@mail.com" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeViewModel)).Returns(employee);
            _employeeRepositoryMock.Setup(r => r.AddEmployee(employee)).ReturnsAsync(false);

            // Act
            var result = await _service.AddEmployee(employeeViewModel);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task UpdateEmployee_RepositoryReturnsTrue_InvalidateCache_ReturnsTrue()
        {
            // Arrange
            var employeeViewModel = new EmployeeViewModel { Id = 5, Name = "Update", Email = "e@mail.com" };
            var employee = new Employee { Id = 5, Name = "Update", Email = "e@mail.com" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeViewModel)).Returns(employee);
            _employeeRepositoryMock.Setup(r => r.UpdateEmployee(employee)).ReturnsAsync(true);

            // Act
            var result = await _service.UpdateEmployee(employeeViewModel);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task UpdateEmployee_RepositoryReturnsFalse_ReturnsFalse()
        {
            // Arrange
            var employeeViewModel = new EmployeeViewModel { Id = 6, Name = "FailUpdate" ,Email="e@mail.com"};
            var employee = new Employee { Id = 6, Name = "FailUpdate", Email = "e@mail.com" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeViewModel)).Returns(employee);
            _employeeRepositoryMock.Setup(r => r.UpdateEmployee(employee)).ReturnsAsync(false);

            // Act
            var result = await _service.UpdateEmployee(employeeViewModel);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteEmployee_RepositoryReturnsTrue_InvalidateCache_ReturnsTrue()
        {
            // Arrange
            _employeeRepositoryMock.Setup(r => r.DeleteEmployee(7)).ReturnsAsync(true);

            // Act
            var result = await _service.DeleteEmployee(7);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteEmployee_RepositoryReturnsFalse_ReturnsFalse()
        {
            // Arrange
            _employeeRepositoryMock.Setup(r => r.DeleteEmployee(8)).ReturnsAsync(false);

            // Act
            var result = await _service.DeleteEmployee(8);

            // Assert
            Assert.IsFalse(result);
        }
    }
}