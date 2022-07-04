using IdentityTest.Areas.Admin.Models.Dto;
using IdentityTest.Areas.Admin.Models.Dto.Roles;
using IdentityTest.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace IdentityTest.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<Role> _rolemanager;
        private readonly UserManager<User> _userManager;
        public RoleController(RoleManager<Role> roleManager,UserManager<User> userManager)
        {
            _rolemanager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var roles = _rolemanager.Roles.Select(p => new RoleListDto  
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            }).ToList();

           
            return View(roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AddNewRoleDto newRole)
        {
            Role role = new Role()
            {
                Description = newRole.Description,
                Name = newRole.Name
            };
            var result = _rolemanager.CreateAsync(role).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Role", new {area="admin"});
            }
            ViewBag.Errors = result.Errors.ToList();
            return View(newRole);
        }
        public IActionResult UserInRole(string Name)
        {
           var usersInRole= _userManager.GetUsersInRoleAsync(Name).Result;
            return View(usersInRole.Select(p => new UserListDto
            {
                FirstName=p.Firstname,
                LastName=p.Lastname,
                UserName=p.UserName,
                PhoneNumber=p.PhoneNumber,
                Id=p.Id

            }));
        }
    }
}
