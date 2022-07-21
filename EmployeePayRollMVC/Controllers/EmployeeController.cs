using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System.Collections.Generic;
using System.Linq;

namespace EmployeePayRollMVC.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeBL employeeBL;
        public EmployeeController(IEmployeeBL employeeBL)
        {
            this.employeeBL = employeeBL;
        }
        public IActionResult GetResult()
        {
            List<EmployeeModel> listEmployee = new List<EmployeeModel>();
            listEmployee=employeeBL.GetAllEmployees().ToList();
            return View(listEmployee);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind]EmployeeModel emp)
        {
            if (ModelState.IsValid)
            {
                employeeBL.AddEmployee(emp);
                return RedirectToAction("GetResult");
            }
            return View(emp);
        }
        //To delete the particular employee
        [HttpGet]
        public IActionResult DeleteEmployee(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmployeeModel emp = employeeBL.GetEmployeeData(id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        [HttpPost, ActionName("DeleteEmployee")]
        public IActionResult DeleteConfirm(int? id)
        {
            employeeBL.DeleteEmployee(id);
            return RedirectToAction("GetResult");
        }
        //For updating employee details
        [HttpGet]
        public IActionResult UpdateEmployee(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmployeeModel emp = employeeBL.GetEmployeeData(id);

            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        [HttpPost]
        public IActionResult UpdateEmployee(int id, [Bind] EmployeeModel emp)
        {
            if (id != emp.Emp_id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                employeeBL.UpdateEmployee(emp);
                return RedirectToAction("GetResult");
            }
            return View(emp);
        }
    }
}

