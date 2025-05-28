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
        public IActionResult Index()
        {
            var model = _EmployeeService.GetEmployees();
            return View(model);
        }

        // GET: /Employee/Details/5
        public IActionResult Details(int id)
        {
            var employee = _EmployeeService.GetEmployee(id);
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _EmployeeService.AddEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: /Employee/Edit/5
        public IActionResult Edit(int id)
        {
            var employee = _EmployeeService.GetEmployee(id);
            if (employee == null)
                return NotFound();
            return View(employee);
        }

        // POST: /Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _EmployeeService.UpdateEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: /Employee/Delete/5
        public IActionResult Delete(int id)
        {
            var employee = _EmployeeService.GetEmployee(id);
            if (employee == null)
                return NotFound();
            return View(employee);
        }

        // POST: /Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _EmployeeService.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }
    }
}