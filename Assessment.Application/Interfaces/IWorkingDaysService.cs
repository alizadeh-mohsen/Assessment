using Assessment.Application.ViewModels;

namespace Assessment.Application.Interfaces
{
    public interface IWorkingDaysService
    {
        WorkingDaysViewModel CalculateWorkingDays(WorkingDaysViewModel model);
    }
}
