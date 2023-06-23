using ITSAIntermediate_VelaphiMhlanga.Data;
using ITSAIntermediate_VelaphiMhlanga.Models;
using ITSAIntermediate_VelaphiMhlanga.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITSAIntermediate_VelaphiMhlanga.Controllers
{
    [Authorize]
    public class EmployeeDetailsController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public EmployeeDetailsController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employeeDetails = await applicationDbContext.EmployeeDetails.ToListAsync();
            return View(employeeDetails);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeDetailsViewModel addEmployeeDetailsRequest)
        {
            var employeeDetail = new EmployeeDetail()
            {
                Id = Guid.NewGuid(),
                Department = addEmployeeDetailsRequest.Department,
                eRole = addEmployeeDetailsRequest.eRole,
                Salary = addEmployeeDetailsRequest.Salary,



            };

            await applicationDbContext.EmployeeDetails.AddAsync(employeeDetail);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult>? View(Guid id)
        {
            var employeeDetails = await applicationDbContext.EmployeeDetails.FirstOrDefaultAsync(x => x.Id == id);

            if (employeeDetails != null)
            {
                var viewModel = new UpdateEmployeeDetailsViewModel()
                {

                    Id = employeeDetails.Id,
                    Department = employeeDetails.Department,
                    eRole = employeeDetails.eRole,
                    Salary = employeeDetails.Salary,

                };

                return await Task.Run(() => View("View", viewModel));
            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeDetailsViewModel model)
        {
            var employeeDetails = await applicationDbContext.EmployeeDetails.FindAsync(model.Id);

            if (employeeDetails != null)
            {
                employeeDetails.Department = model.Department;
                employeeDetails.eRole = model.eRole;
                employeeDetails.Salary = model.Salary;

                await applicationDbContext.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateRecUserViewModel model)
        {
            var employeeDetails = await applicationDbContext.EmployeeDetails.FindAsync(model.Id);

            if (employeeDetails != null)
            {
                applicationDbContext.EmployeeDetails.Remove(employeeDetails);
                await applicationDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}