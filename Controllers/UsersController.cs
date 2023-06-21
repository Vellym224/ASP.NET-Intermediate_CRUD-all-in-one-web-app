using ITSAIntermediate_VelaphiMhlanga.Data;
using ITSAIntermediate_VelaphiMhlanga.Models;
using ITSAIntermediate_VelaphiMhlanga.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ITSAIntermediate_VelaphiMhlanga.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UsersController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var recusers = await applicationDbContext.RecUsers.ToListAsync();
            return View(recusers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserViewModel addUserRequest)
        {
            var recuser = new RecUser()
            {
                Id = Guid.NewGuid(),
                Name = addUserRequest.Name,
                Surname = addUserRequest.Surname,
                DateOfBirth = addUserRequest.DateOfBirth,
                IdentityNumber = addUserRequest.IdentityNumber,
                Address = addUserRequest.Address,
                EmailAddress = addUserRequest.EmailAddress,
                ContactNumber = addUserRequest.ContactNumber

            };

            await applicationDbContext.RecUsers.AddAsync(recuser);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var recuser = await applicationDbContext.RecUsers.FirstOrDefaultAsync(x => x.Id == id);

            if (recuser != null)
            {
                var viewModel = new UpdateUserViewModel()
                {

                    Id = recuser.Id,
                    Name = recuser.Name,
                    Surname = recuser.Surname,
                    DateOfBirth = recuser.DateOfBirth,
                    IdentityNumber = recuser.IdentityNumber,
                    Address = recuser.Address,
                    EmailAddress = recuser.EmailAddress,
                    ContactNumber = recuser.ContactNumber
                };

                return await Task.Run(() => View("View", viewModel));
            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateUserViewModel model)
        {
            var recuser = await applicationDbContext.RecUsers.FindAsync(model.Id);

            if (recuser != null)
            {
                recuser.Name = model.Name;
                recuser.Surname = model.Surname;
                recuser.DateOfBirth = model.DateOfBirth;
                recuser.IdentityNumber = model.IdentityNumber;
                recuser.Address = model.Address;
                recuser.EmailAddress = model.EmailAddress;
                recuser.ContactNumber = model.ContactNumber;

                await applicationDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateUserViewModel model)
        {
            var recuser = await applicationDbContext.RecUsers.FindAsync(model.Id);

            if (recuser != null)
            {
                applicationDbContext.RecUsers.Remove(recuser);
                await applicationDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
