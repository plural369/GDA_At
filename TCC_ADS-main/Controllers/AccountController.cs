using Gerenciador.Context;
using Gerenciador.Models;
using Gerenciador.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly Contexto _contexto;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, Contexto contexto)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _contexto = contexto;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Autor", "File");
        }

        [HttpGet]
        public IActionResult Register()
        {
            var endereco = _contexto.curso.ToList();
            endereco.Insert(0, new Curso()
            {
                Id = 0,
                Nome = "Selecione o Curso"
            });
            ViewBag.Endereco = endereco;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Copia os dados do RegisterViewModel para o IdentityUser
                var user = new ApplicationUser
                {
                    Nome = model.Nome,
                    UserName = model.Email,
                    Email = model.Email,
                    Cpf = model.Cpf,
                    Rg = model.Rg,
                    Matricula = model.Matricula,
                    CursoId = model.CursoId
                };
                // Armazena os dados do usuário na tabela AspNetUsers
                var result = await userManager.CreateAsync(user, model.Password);
                // Se o usuário foi criado com sucesso, faz o login do usuário
                // usando o serviço SignInManager e redireciona para o Método Action Index
                if (result.Succeeded)
                {
                    // Se o usuário estiver logado e for do perfil Admin
                    // então o usuário é o usuário Admin que esta criando
                    // um novo usuário, assim vamos direcioná-lo para o
                    // usuário Admin para a Action ListRoles
                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }

                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Register", "Account");
                    TempData["Message2"] = "Registrado com Sucesso!! aguarde para ser aprovado!!";
                }
                // Se houver erros então inclui no ModelState
                // que será exibido pela tag helper summary na validação
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Autor", "File");
                }
                ModelState.AddModelError(string.Empty, "Login Inválido");
            }
            return View(model);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"O Email {email} já está sendo usado.");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }

}
