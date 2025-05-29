using Moq;
using Assessment.Application.Services;
using Assessment.Application.ViewModels;
using Assessment.Domain.Interfaces;

namespace Assessment.Application.Tests.Services
{
    [TestFixture]
    public class WorkingDaysServiceTests
    {
        private Mock<IWorkingDaysRepository> _workingDaysRepositoryMock;
        private WorkingDaysService _service;

        [SetUp]
        public void SetUp()
        {
            _workingDaysRepositoryMock = new Mock<IWorkingDaysRepository>();
            _service = new WorkingDaysService(_workingDaysRepositoryMock.Object);
        }

        [Test]
        public void CalculateWorkingDays_ExcludesWeekendsAndHolidays()
        {
            // Arrange
            var startDate = new DateTime(2024, 6, 3); // Monday
            var endDate = new DateTime(2024, 6, 7);   // Friday
            var holidays = new HashSet<DateTime> { new DateTime(2024, 6, 5) }; // Wednesday is a holiday

            _workingDaysRepositoryMock.Setup(r => r.GetPublicHolidays()).Returns(holidays);

            var model = new WorkingDaysViewModel
            {
                StartDate = startDate,
                EndDate = endDate
            };

            // Act
            var result = _service.CalculateWorkingDays(model);

            // Assert
            // 5 days (Mon-Fri) - 1 holiday = 4 working days
            Assert.AreEqual(4, result.WorkingDays);
        }

        [Test]
        public void CalculateWorkingDays_ExcludesWeekends()
        {
            // Arrange
            var startDate = new DateTime(2024, 6, 7); // Friday
            var endDate = new DateTime(2024, 6, 10);  // Monday
            _workingDaysRepositoryMock.Setup(r => r.GetPublicHolidays()).Returns(new HashSet<DateTime>());

            var model = new WorkingDaysViewModel
            {
                StartDate = startDate,
                EndDate = endDate
            };

            // Act
            var result = _service.CalculateWorkingDays(model);

            // Assert
            // Friday and Monday are working days, Saturday and Sunday are not
            Assert.AreEqual(2, result.WorkingDays);
        }

        [Test]
        public void CalculateWorkingDays_AllDaysAreHolidays_ReturnsZero()
        {
            // Arrange
            var startDate = new DateTime(2024, 6, 3);
            var endDate = new DateTime(2024, 6, 5);
            var holidays = new HashSet<DateTime>
            {
                new DateTime(2024, 6, 3),
                new DateTime(2024, 6, 4),
                new DateTime(2024, 6, 5)
            };

            _workingDaysRepositoryMock.Setup(r => r.GetPublicHolidays()).Returns(holidays);

            var model = new WorkingDaysViewModel
            {
                StartDate = startDate,
                EndDate = endDate
            };

            // Act
            var result = _service.CalculateWorkingDays(model);

            // Assert
            Assert.AreEqual(0, result.WorkingDays);
        }

        [Test]
        public void CalculateWorkingDays_StartDateAfterEndDate_ReturnsZero()
        {
            // Arrange
            var startDate = new DateTime(2024, 6, 10);
            var endDate = new DateTime(2024, 6, 7);
            _workingDaysRepositoryMock.Setup(r => r.GetPublicHolidays()).Returns(new HashSet<DateTime>());

            var model = new WorkingDaysViewModel
            {
                StartDate = startDate,
                EndDate = endDate
            };

            // Act
            var result = _service.CalculateWorkingDays(model);

            // Assert
            Assert.AreEqual(0, result.WorkingDays);
        }
    }
}