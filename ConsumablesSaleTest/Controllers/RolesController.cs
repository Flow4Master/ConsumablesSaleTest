using ConsumablesSaleTest.Models;
using ConsumablesSaleTest.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumablesSaleTest.Controllers
{
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<ApplicationUser> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users != null)
            {
                var userRolesViewModel = new List<ChangeRoleVM>();
                foreach (ApplicationUser user in users)
                {
                    ChangeRoleVM model = new ChangeRoleVM()
                    {
                        UserId = user.Id,
                        UserEmail = user.Email,
                        UserFullName = user.FullName,
                        UserPhoneNumber = user.PhoneNumber,
                        UserRoles = await _userManager.GetRolesAsync(user),
                    };

                    userRolesViewModel.Add(model);
                }
                return View(userRolesViewModel);
            }
            else 
            { 
                return Content("Пользователи не найдены"); 
            }
        }

        //Edit
        public async Task<IActionResult> Edit(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                ChangeRoleVM model = new ChangeRoleVM
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserFullName = user.FullName,
                    UserPhoneNumber = user.PhoneNumber,
                    UserRoles = await _userManager.GetRolesAsync(user),
                    AllRoles = _roleManager.Roles.ToList()
                };
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            // получаем пользователя
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
