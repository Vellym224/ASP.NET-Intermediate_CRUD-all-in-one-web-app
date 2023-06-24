using ITSAIntermediate_VelaphiMhlanga.Data;
using ITSAIntermediate_VelaphiMhlanga.Models;
using ITSAIntermediate_VelaphiMhlanga.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ITSAIntermediate_VelaphiMhlanga.Controllers
{
    [Authorize]

    public class RecUsersController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public RecUsersController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var recUsers = await applicationDbContext.RecUsers.ToListAsync();
            return View(recUsers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRecUsersViewModel addRecUserRequest)
        {
            var recUser = new RecUser()
            {
                Id = Guid.NewGuid(),
                Name = addRecUserRequest.Name,
                Surname = addRecUserRequest.Surname,
                DateOfBirth = addRecUserRequest.DateOfBirth,
                IdentityNumber = addRecUserRequest.IdentityNumber,
                Address = addRecUserRequest.Address,
                EmailAddress = addRecUserRequest.EmailAddress,
                ContactNumber = addRecUserRequest.ContactNumber

            };

            await applicationDbContext.RecUsers.AddAsync(recUser);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var recUsers = await applicationDbContext.RecUsers.FirstOrDefaultAsync(x => x.Id == id);

            if (recUsers != null)
            {
                var viewModel = new UpdateRecUserViewModel()
                {

                    Id = recUsers.Id,
                    Name = recUsers.Name,
                    Surname = recUsers.Surname,
                    DateOfBirth = recUsers.DateOfBirth,
                    IdentityNumber = recUsers.IdentityNumber,
                    Address = recUsers.Address,
                    EmailAddress = recUsers.EmailAddress,
                    ContactNumber = recUsers.ContactNumber
                };

                return await Task.Run(() => View("View", viewModel));
            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateRecUserViewModel model)
        {
            var recUsers = await applicationDbContext.RecUsers.FindAsync(model.Id);

            if (recUsers != null)
            {
                recUsers.Name = model.Name;
                recUsers.Surname = model.Surname;
                recUsers.DateOfBirth = model.DateOfBirth;
                recUsers.IdentityNumber = model.IdentityNumber;
                recUsers.Address = model.Address;
                recUsers.EmailAddress = model.EmailAddress;
                recUsers.ContactNumber = model.ContactNumber;

                await applicationDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateRecUserViewModel model)
        {
            var recUsers = await applicationDbContext.RecUsers.FindAsync(model.Id);

            if (recUsers != null)
            {

                applicationDbContext.RecUsers.Remove(recUsers);
                await applicationDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
