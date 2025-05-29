using System.ComponentModel.DataAnnotations;

namespace Assessment.Application.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        [Display(Name = "Job Position")]
        public string JobPosition { get; set; }
        public required string Email { get; set; }
    }
}
