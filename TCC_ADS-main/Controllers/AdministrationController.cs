using Gerenciador.Context;
using Gerenciador.Models;
using Gerenciador.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador.Controllers
{
   [Authorize(Roles = "Administrador")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Contexto _contexto ;

        public AdministrationController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> _userManager, Contexto contexto)
        {
            this.roleManager = roleManager;
            userManager = _userManager;
            _contexto = contexto;
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // precisamos apenas especificar o nome único da role
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                // Salva a role na tabela AspNetRole
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        // O Id da Role é passado a partir da URL
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            // Localiza a role pelo ID
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role com Id = {id} não foi localizada";
                return View("NotFound");
            }
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            var listaUsuarios = userManager.Users.ToList();
            // Retorna todos os usuários
            foreach (var user in listaUsuarios)
            {
                // Se o usuário existir na role, inclui o nome do usuário
                // para a propriedade Users de EditRoleViewModel
                // Este objeto model é então passado para ser exibido
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }
        // Esta action responde ao POST e recebe a EditRoleViewModel
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role com Id = {model.Id} não foi encontrada";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                // Atualiza a role usando UpdateAsync
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role com Id = {roleId} não foi encontrada";
                return View("NotFound");
            }
            var model = new List<UserRoleViewModel>();
            var listaUsuarios = userManager.Users.ToList();
            foreach (var user in listaUsuarios)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role com Id = {roleId} não foi encontrada";
                return View("NotFound");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role com Id = {id} não foi encontrada";
                return RedirectToAction(nameof(ListUsers));
            }
            else
            {
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("ListRoles");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction(nameof(ListUsers));
                else
                    return RedirectToAction(nameof(ListUsers));
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View("Index", userManager.Users);
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = userManager.Users.Include(a => a.Curso);
            return View(users);
        }



        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var curso = await _contexto.curso.ToListAsync();
            curso.Insert(0, new Curso
            {
                Id = 0,
                Nome = "Selecione um Curso"
            });
            ViewBag.Curso = curso;

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Usuário com Id = {id} não foi encontrado";
                return View("NotFound");
            }
            // GetRolesAsync retorna a lista de Roles
            var userRoles = await userManager.GetRolesAsync(user);
            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Nome = user.Nome,
                Rg = user.Rg,
                Cpf = user.Cpf,
                Matricula = user.Matricula,
                Roles = userRoles
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
           
            if (user == null)
            {
                ViewBag.ErrorMessage = $"Usuário com Id = {model.Id} não foi encontrado";
                return View("NotFound");
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Nome = model.Nome;
                user.Rg = model.Rg;
                user.Cpf = model.Cpf;
                user.Matricula = model.Matricula;
                user.CursoId = model.CursoId;
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }
    }
}
