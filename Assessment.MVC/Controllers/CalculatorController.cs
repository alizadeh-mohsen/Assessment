using Assessment.Application.Interfaces;
using Assessment.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.MVC.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly IWorkingDaysService _workingDaysService;

        public CalculatorController(IWorkingDaysService workingDaysService)
        {
            _workingDaysService = workingDaysService;
        }

        public IActionResult Index()
        {

            return View(new WorkingDaysViewModel
            {
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
            });
        }

        [HttpPost]
        public IActionResult Index(WorkingDaysViewModel model)
        {
            if (ModelState.IsValid)
            {
                model = _workingDaysService.CalculateWorkingDays(model);
            }

            return View(model);
        }
    }
}
