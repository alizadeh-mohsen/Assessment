using Assessment.Application.ViewModels;
using Assessment.Domain.Models;

namespace Assessment.Application.Interfaces
{
    public interface IWorkingDaysService
    {
        WorkingDaysViewModel CalculateWorkingDays(WorkingDaysViewModel model);
    }
}
