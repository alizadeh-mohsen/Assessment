using System.ComponentModel.DataAnnotations;

namespace Assessment.Application.ViewModels
{
    public class WorkingDaysViewModel
    {
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public int? WorkingDays { get; set; }
    }

}
