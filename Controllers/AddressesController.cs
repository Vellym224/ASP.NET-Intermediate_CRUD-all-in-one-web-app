using ITSAIntermediate_VelaphiMhlanga.Data;
using ITSAIntermediate_VelaphiMhlanga.Models;
using ITSAIntermediate_VelaphiMhlanga.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITSAIntermediate_VelaphiMhlanga.Controllers
{
    public class AddressesController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public AddressesController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var addresses = await applicationDbContext.Addresses.ToListAsync();
            return View(addresses);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddAddressViewModel addAddressRequest)
        {
            var address = new Address()
            {
                Id = Guid.NewGuid(),
                Country = addAddressRequest.Country,
                Province = addAddressRequest.Province,
                City = addAddressRequest.City,
                Suburb = addAddressRequest.Suburb,
                PostalCode = addAddressRequest.PostalCode,
                UnitNumber = addAddressRequest.UnitNumber,
                ComplexName = addAddressRequest.ComplexName,
            };

            await applicationDbContext.Addresses.AddAsync(address);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var addresses = await applicationDbContext.Addresses.FirstOrDefaultAsync(x => x.Id == id);

            if (addresses != null)
            {
                var viewModel = new UpdateAddressViewModel()
                {
                    Id = addresses.Id,
                    Country = addresses.Country,
                    Province = addresses.Province,
                    City = addresses.City,
                    Suburb = addresses.Suburb,
                    PostalCode = addresses.PostalCode,
                    UnitNumber = addresses.UnitNumber,
                    ComplexName = addresses.ComplexName,
                };

                return await Task.Run(() => View("View", viewModel));

            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateAddressViewModel model)
        {
            var addresses = await applicationDbContext.Addresses.FindAsync(model.Id);

            if (addresses != null)
            {
                addresses.Country = model.Country;
                addresses.Province = model.Province;
                addresses.City = model.City;
                addresses.Suburb = model.Suburb;
                addresses.PostalCode = model.PostalCode;
                addresses.UnitNumber = model.UnitNumber;
                addresses.ComplexName = model.ComplexName;

                await applicationDbContext.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateAddressViewModel model)
        {
            var addresses = await applicationDbContext.Addresses.FindAsync(model.Id);

            if (addresses != null)
            {
                applicationDbContext.Addresses.Remove(addresses);
                await applicationDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}