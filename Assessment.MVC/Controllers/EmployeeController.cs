using Assessment.Application.Interfaces;
using Assessment.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeService _EmployeeService;

        public EmployeeController(IEmployeeService EmployeeService)
        {
            _EmployeeService = EmployeeService;
        }

        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> Index()
        {
            var model = await _EmployeeService.GetEmployees();
            return View(model);
        }

        public async Task<ActionResult<EmployeeViewModel>> Details(int id)
        {
            var employee = await _EmployeeService.GetEmployee(id);
            if (employee == null)
                return NotFound();
            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _EmployeeService.AddEmployee(employeeViewModel))
                    return RedirectToAction(nameof(Index));
                else
                    return RedirectToAction("Error", "Home");
            }

            return View(employeeViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _EmployeeService.GetEmployee(id);
            if (employee == null)
                return NotFound();
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                if (await _EmployeeService.UpdateEmployee(employee))
                    return RedirectToAction(nameof(Index));
                else
                    return RedirectToAction("Error", "Home");
            }
            return View(employee);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _EmployeeService.GetEmployee(id);
            if (employee == null)
                return NotFound();
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _EmployeeService.DeleteEmployee(id))
                return RedirectToAction(nameof(Index));
            else
                return RedirectToAction("Error", "Home");
        }
    }
}