using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diplomskirad.Data;
using diplomskirad.Models;

namespace ZavrsniRadIgorMatic.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _WebHostEnvironment;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _WebHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserRoles>();
            foreach (var user in users)
            {

                var thisViewModel = new UserRoles();
                thisViewModel.KorisnickiID = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.BrojTelefona = user.PhoneNumber;
                if (thisViewModel.BrojTelefona == null)
                {
                    thisViewModel.BrojTelefona = "Broj telefona nije unijet";
                }

                thisViewModel.Roles = await GetUserRoles(user);
                if (thisViewModel.Roles[0] == "Pacijent")
                {
                    userRolesViewModel.Add(thisViewModel);
                }
            }
            return View(userRolesViewModel);
        }
        public async Task<IActionResult> GetLaborant()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserRoles>();
            foreach (var user in users)
            {

                var thisViewModel = new UserRoles();
                thisViewModel.KorisnickiID = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.BrojTelefona = user.PhoneNumber;
                if (thisViewModel.BrojTelefona == null)
                {
                    thisViewModel.BrojTelefona = "Broj telefona nije unijet";
                }

                thisViewModel.Roles = await GetUserRoles(user);
                if (thisViewModel.Roles[0] == "Laborant")
                {
                    userRolesViewModel.Add(thisViewModel);
                }
            }
            return View(userRolesViewModel);
        }
        private async Task<List<string>> GetUserRoles(IdentityUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            UserRoles us = new UserRoles();
            us.KorisnickiID = user.Id;
            us.Email = user.Email;
            us.BrojTelefona = user.PhoneNumber;
            if (us.BrojTelefona == null)
            {
                us.BrojTelefona = "Broj telefona nije unijet";
            }
            var roles = await _userManager.GetRolesAsync(user);
            us.Roles = roles.ToList();

            return View("Delete", us);
        }
        public async Task<IActionResult> Delete2(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            UserRoles us = new UserRoles();

            var roles = await _userManager.GetRolesAsync(user);
            us.Roles = roles.ToList();
            IdentityResult result = await _userManager.DeleteAsync(user);
            if (us.Roles[0] == "Pacijent")
            {
                return RedirectToAction("GetUsers");
            }
            else
            {
                return RedirectToAction("GetLaborant");
            }

        }
        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"Korisnik sa = {userId} nije pronadjen";
                return View("Nije pronadjen");
            }
            ViewBag.UserName = user.UserName;
            var model = new List<RolesViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new RolesViewModel
                {
                    UlogeID = role.Id,
                    UlogeIme = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selektovano = true;
                }
                else
                {
                    userRolesViewModel.Selektovano = false;
                }
                model.Add(userRolesViewModel);
            }
            return View("ChangeRole", model);
        }
        [HttpPost]
        public async Task<IActionResult> Manage(List<RolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Nije moguce ukloniti postojece uloge korisnika");
                return View(model);
            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selektovano).Select(y => y.UlogeIme));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Nije moguce ukloniti postojece uloge korisnika");
                return View(model);
            }
            if (model.Where(x => x.Selektovano).Select(y => y.UlogeIme).FirstOrDefault() == "Pacijent")
            {
                return RedirectToAction("GetUsers");
            }
            else
            {
                return RedirectToAction("GetLaborant");
            }

        }



    }
}


