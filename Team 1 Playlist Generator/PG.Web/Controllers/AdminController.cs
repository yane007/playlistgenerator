using Microsoft.AspNetCore.Mvc;
using PagedList;
using PG.Services.Contract;
using PG.Web.Models.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> UsersIndex(int pageNumber = 1)
        {
            var allUsers = await _userService.GetAllRegularUsers();
            var users = allUsers.Select(x => x.ToViewModel()).ToPagedList(pageNumber, 10);

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
    }
}
