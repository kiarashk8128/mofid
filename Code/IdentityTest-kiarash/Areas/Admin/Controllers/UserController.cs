using IdentityTest.Areas.Admin.Models.Dto;
using IdentityTest.Areas.Admin.Models.Dto.Roles;
using IdentityTest.Models.Dto;
using IdentityTest.Models.Dto.Account;
using IdentityTest.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IdentityTest.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public UserController(UserManager<User> userManager,RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var users = _userManager.Users.Select(p => new UserListDto
            {
                Id = p.Id,
                FirstName = p.Firstname,
                LastName = p.Lastname,
                UserName = p.UserName,
                PhoneNumber = p.PhoneNumber,
                EmailConfirmed = p.EmailConfirmed,
                AccessFailedCount = p.AccessFailedCount
            }).ToList();
            return View(users);
        }
       
        public IActionResult UserDetails(string Id)
        {
            var user = _userManager.FindByIdAsync(Id).Result;
            AccountInfoDto myAccount = new AccountInfoDto()
            {
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                FullName = $"{user.Firstname} {user.Lastname}",
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                UserName = user.UserName,
            };
            return View(myAccount);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create (RegisterDto register)
        {
            if (ModelState.IsValid == false)
            {
                return View(register);
            }
            User newUser = new User()
            {
                Firstname = register.FirstName,
                Lastname = register.LastName,
                Email = register.Email,
                UserName = register.Email,

            };
            var result = _userManager.CreateAsync(newUser, register.Password).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "User", new { area = "Admin" });
            }
            string message = "";
            foreach (var item in result.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            TempData["Message"] = message;
            return View(register);
        }
        public IActionResult Edit(string Id)
        {
            var user=_userManager.FindByIdAsync(Id).Result;
            UserEditDto userEdit = new UserEditDto()
            {
                Email = user.Email,
                UserName=user.UserName,
                FirstName=user.Firstname,
                LastName=user.Lastname,
                PhoneNumber=user.PhoneNumber,
                Id=user.Id
            };
            return View(userEdit);

        }
        [HttpPost]
        public IActionResult Edit(UserEditDto userEdit)
        {
            var user = _userManager.FindByIdAsync(userEdit.Id).Result;
            user.PhoneNumber= userEdit.PhoneNumber;
            user.UserName=userEdit.UserName;
            user.Firstname=userEdit.FirstName;
            user.Email=userEdit.Email;
            var result= _userManager.UpdateAsync(user).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "User" , new {area="Admin"});

            }
            string message = "";
            foreach (var item in result.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            TempData["Message"] = message;
            return View(userEdit);

        }
        public IActionResult Delete(string Id)
        {
            var user = _userManager.FindByIdAsync(Id).Result;
            UserDeleteDto userDelete = new UserDeleteDto()
            {
                Id = user.Id,
                Email = user.Email,
                FullName = $"{ user.Firstname }  {user.Lastname}",
                UserName = user.UserName,
            };
            return View(userDelete);
        }
        [HttpPost]
        public IActionResult Delete(UserDeleteDto userDelete)
        {
            var user=_userManager.FindByIdAsync(userDelete.Id).Result;
            var result = _userManager.DeleteAsync(user).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "User", new { area = "Admin" });

            }
            string message = "";
            foreach (var item in result.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            TempData["Message"] = message;

            return View(userDelete);
        }
        public IActionResult AddUserRole(string Id)
        {
            var user=_userManager.FindByIdAsync(Id).Result;

            var roles = new List<SelectListItem>(
                _roleManager.Roles.Select(p => new SelectListItem
                {
                    Text= p.Name,
                    Value=p.Name
                })).ToList();
            return View(new AddUserRoleDto
            {
                Id=Id,
                Roles=roles,
                Email=user.Email,
                FullName =$"{user.Firstname}  {user.Lastname}"
            });
        }
        [HttpPost]
        public IActionResult AddUserRole(AddUserRoleDto userRole)
        {
            var user = _userManager.FindByIdAsync(userRole.Id).Result;
            var result = _userManager.AddToRoleAsync(user, userRole.Role).Result;
            return RedirectToAction("UserRoles", "User", new {Id=user.Id, area = "admin" });
            
        }
        public IActionResult UserRoles(string Id)
        {
            var user = _userManager.FindByIdAsync(Id).Result;
            var roles= _userManager.GetRolesAsync(user).Result;
            ViewBag.UserInfo = $"Name : {user.Firstname}  {user.Lastname}      Email: {user.Email}";
            return View(roles);
        }

    }
}
