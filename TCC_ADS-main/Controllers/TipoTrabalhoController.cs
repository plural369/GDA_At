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
    public class TipoTrabalhoController : Controller
    {
        private readonly Contexto _contexto;
        public TipoTrabalhoController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<ActionResult> Inicio()
        {
            return View(await _contexto.tipoTrabalho.ToListAsync());
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastro(TipoTrabalho tipoTrabalho)
        {
            if (ModelState.IsValid)
            {
                var check = await _contexto.tipoTrabalho.FirstOrDefaultAsync(i => i.Nome == tipoTrabalho.Nome);
                if (check == null)
                {
                    _contexto.tipoTrabalho.Add(tipoTrabalho);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Inicio));
                }
            }
            TempData["Message"] = "Tipo de Trabalho já Existente !!";
            return RedirectToAction(nameof(Inicio));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            var dado = await _contexto.tipoTrabalho.FindAsync(id);

            if (dado == null)
            {
                return NotFound();
            }

            return View(dado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(TipoTrabalho tipoTrabalho)
        {
            if (ModelState.IsValid)
            {
                var check = await _contexto.tipoTrabalho.FirstOrDefaultAsync(i => i.Nome == tipoTrabalho.Nome);
                if (check == null)
                {
                    _contexto.Entry(tipoTrabalho).State = EntityState.Modified;
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Inicio));
                }
            }
            TempData["Message"] = "Tipo de Trabalho já Existente !!";
            return RedirectToAction(nameof(Inicio));
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int? id)
        {
            var dados = await _contexto.tipoTrabalho.FindAsync(id);
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

            var tipoTrabalho = await _contexto.tipoTrabalho.FirstOrDefaultAsync(m => m.Id == id);
            if (tipoTrabalho == null)
            {
                return NotFound();
            }

            return View(tipoTrabalho);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoTrabalho = await _contexto.tipoTrabalho.FindAsync(id);
            _contexto.tipoTrabalho.Remove(tipoTrabalho);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Inicio));
        }
    }
}
