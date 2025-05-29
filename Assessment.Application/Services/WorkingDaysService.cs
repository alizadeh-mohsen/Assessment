using Assessment.Application.Interfaces;
using Assessment.Application.ViewModels;
using Assessment.Domain.Interfaces;

namespace Assessment.Application.Services
{
    public class WorkingDaysService : IWorkingDaysService
    {
        private readonly IWorkingDaysRepository _workingDaysRepository;

        public WorkingDaysService(IWorkingDaysRepository workingDaysRepository)
        {
            _workingDaysRepository = workingDaysRepository;
        }

        public WorkingDaysViewModel CalculateWorkingDays(WorkingDaysViewModel model)
        {
            var holidays = _workingDaysRepository.GetPublicHolidays();

            int workingDays = 0;
            for (DateTime date = model.StartDate.Date; date <= model.EndDate.Date; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday &&
                    date.DayOfWeek != DayOfWeek.Sunday &&
                    !holidays.Contains(date))
                {
                    workingDays++;
                }
            }

            model.WorkingDays = workingDays;
            return model;
        }
    }
}
