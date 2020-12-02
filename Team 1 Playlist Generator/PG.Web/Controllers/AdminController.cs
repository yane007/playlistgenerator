using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using PG.Models;
using PG.Services.Contract;
using PG.Web.Models.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;

        public AdminController(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        public async Task<IActionResult> UsersIndex(int pageNumber = 1)
        {
            var allUsers = await _userService.GetAllRegularUsers();
            var users = allUsers.Select(x => x.ToViewModel()).ToPagedList(pageNumber, 10);

            ViewBag.LoggedUserId = _userManager.GetUserId(User);

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> BanUsers(string userId)
        {
            bool isBanned = await _userService.BanUserById(userId);
            return RedirectToAction("UsersIndex", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> UnbanUsers(string userId)
        {
            bool isBanned = await _userService.UnbanUserById(userId);
            return RedirectToAction("UsersIndex", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await _userService.DeleteUserById(userId);
            return RedirectToAction("UsersIndex", "Admin");
        }
    }
}
