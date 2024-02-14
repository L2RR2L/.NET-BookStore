using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet.Models;
using Projet.ViewModel;

namespace Projet.Controllers
{
    [Authorize(Roles= "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<DefaultUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<DefaultUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new()
                {
                    Name = model.RoleName
                };

                var result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                EditRoleViewModel model = new()
                {
                    Id = role.Id,
                    RoleName = role.Name
                };

                var users = await _userManager.GetUsersInRoleAsync(role.Name);

                foreach(var user in users)
                {
                    
                    /*if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        model.Users.Add(user.UserName);
                    }*/

                    model.Users.Add(user.UserName);
                }

                return View(model);
            }

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);

                if (role != null)
                {
                    role.Name = model.RoleName;

                    var result = await _roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListRoles");
                    }
                }
            }

            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            ViewData["roleId"] = id;
            ViewData["roleName"] = role.Name;

            var model = new List<UserRoleViewModel>();

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name); 

            // Users in role

            foreach(var user in usersInRole)
            {
                UserRoleViewModel userRoleViewModel = new UserRoleViewModel
                {
                    Id = user.Id,
                    Name = user.UserName,
                    IsSelected = true
                };

                model.Add(userRoleViewModel);
            }

            // Users not in role
            var users = await _userManager.Users.ToListAsync();
            var usersNotInRole = users.Where(user => !usersInRole.Contains(user));

            foreach(var user in usersNotInRole)
            {
                UserRoleViewModel userRoleViewModel = new UserRoleViewModel
                {
                    Id = user.Id,
                    Name = user.UserName,
                    IsSelected = false
                };

                model.Add(userRoleViewModel );
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            for(int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].Id);

                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    await _userManager.RemoveFromRoleAsync(user,role.Name);
                }
            }

            return RedirectToAction("EditRole", new { Id = id });
        }
    }

}
