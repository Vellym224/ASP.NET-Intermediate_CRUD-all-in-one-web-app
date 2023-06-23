using ITSAIntermediate_VelaphiMhlanga.Data;
using ITSAIntermediate_VelaphiMhlanga.Models;
using ITSAIntermediate_VelaphiMhlanga.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITSAIntermediate_VelaphiMhlanga_.Controllers
{
    [Authorize]
    public class CompanyDetailsController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CompanyDetailsController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var companyDetails = await applicationDbContext.CompanyDetails.ToListAsync();
            return View(companyDetails);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCompanyDetailsViewModel addCompanyDetailsRequest)
        {
            var companyDetail = new CompanyDetail()
            {
                Id = Guid.NewGuid(),
                Name = addCompanyDetailsRequest.Name,
                EmailAddress = addCompanyDetailsRequest.EmailAddress,
                ContactNumber = addCompanyDetailsRequest.ContactNumber,
                RegistrationNumber = addCompanyDetailsRequest.RegistrationNumber,
                Address = addCompanyDetailsRequest.Address,
                BusinessType = addCompanyDetailsRequest.BusinessType,
            };

            await applicationDbContext.CompanyDetails.AddAsync(companyDetail);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var companyDetail = await applicationDbContext.CompanyDetails.FirstOrDefaultAsync(x => x.Id == id);

            if (companyDetail != null)
            {
                var viewModel = new UpdateCompanyDetailsViewModel()
                {
                    Id = companyDetail.Id,
                    Name = companyDetail.Name,
                    EmailAddress = companyDetail.EmailAddress,
                    ContactNumber = companyDetail.ContactNumber,
                    RegistrationNumber = companyDetail.RegistrationNumber,
                    Address = companyDetail.Address,
                    BusinessType = companyDetail.BusinessType,
                };

                return await Task.Run(() => View("View", viewModel));
            }


            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateCompanyDetailsViewModel model)
        {
            var companyDetails = await applicationDbContext.CompanyDetails.FindAsync(model.Id);

            if (companyDetails != null)
            {
                companyDetails.Name = model.Name;
                companyDetails.EmailAddress = model.EmailAddress;
                companyDetails.ContactNumber = model.ContactNumber;
                companyDetails.RegistrationNumber = model.RegistrationNumber;
                companyDetails.Address = model.Address;
                companyDetails.BusinessType = model.BusinessType;


                await applicationDbContext.SaveChangesAsync();

                return RedirectToAction("Index");


            }
            return RedirectToAction("Index");

        }


        [HttpPost]
        public async Task<IActionResult> Delete(UpdateCompanyDetailsViewModel model)
        {
            var companyDetails = await applicationDbContext.CompanyDetails.FindAsync(model.Id);

            if (companyDetails != null)
            {
                applicationDbContext.CompanyDetails.Remove(companyDetails);
                await applicationDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}