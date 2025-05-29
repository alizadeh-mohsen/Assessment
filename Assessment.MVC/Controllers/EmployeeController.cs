using Assessment.Application.Interfaces;
using Assessment.Application.ViewModels;
using Assessment.Domain.Models;
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

        // GET: /Employee/
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> Index()
        {
            var model = await _EmployeeService.GetEmployees();
            return View(model);
        }

        // GET: /Employee/Details/5
        public async Task<ActionResult<EmployeeViewModel>> Details(int id)
        {
            var employee = await _EmployeeService.GetEmployee(id);
            if (employee == null)
                return NotFound();
            return View(employee);
        }

        // GET: /Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Employee/Create
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

        // GET: /Employee/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _EmployeeService.GetEmployee(id);
            if (employee == null)
                return NotFound();
            return View(employee);
        }

        // POST: /Employee/Edit/5
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

        // GET: /Employee/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _EmployeeService.GetEmployee(id);
            if (employee == null)
                return NotFound();
            return View(employee);
        }

        // POST: /Employee/Delete/5
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