using Gerenciador.Context;
using Gerenciador.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador.Controllers
{
    [Authorize(Roles = "Administrador , Professor")]
    public class CategoriaController : Controller
    {
        private readonly Contexto _contexto;
        public CategoriaController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<ActionResult> Inicio()
        {
            return View(await _contexto.categoria.ToListAsync());
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastro(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var check = await _contexto.categoria.FirstOrDefaultAsync(i => i.Nome == categoria.Nome);
                if (check == null)
                {
                    _contexto.categoria.Add(categoria);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Inicio));
                }
            }

            TempData["Message"] = "Categoria já Existente !!";
            return RedirectToAction(nameof(Inicio));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            var dado = await _contexto.categoria.FindAsync(id);

            if (dado == null)
            {
                return NotFound();
            }

            return View(dado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var check = await _contexto.categoria.FirstOrDefaultAsync(i => i.Nome == categoria.Nome);
                if (check == null)
                {
                    _contexto.Entry(categoria).State = EntityState.Modified;
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Inicio));
                }
            }
            TempData["Message"] = "Categoria já Existente !!";
            return RedirectToAction(nameof(Inicio));
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int? id)
        {
            var dados = await _contexto.categoria.FindAsync(id);
            if (dados == null)
            {
                return NotFound();
            }
            return View(dados);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriias = await _contexto.categoria.FirstOrDefaultAsync(m => m.Id == id);
            if (categoriias == null)
            {
                return NotFound();
            }

            return View(categoriias);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _contexto.categoria.FindAsync(id);
            _contexto.categoria.Remove(cliente);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Inicio));
        }

    }
}
