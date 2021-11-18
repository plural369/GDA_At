using Gerenciador.Context;
using Gerenciador.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador.Controllers
{
    [Authorize(Roles = "Administrador , Professor")]
    public class CursoController : Controller
    {
        private readonly Contexto _contexto;
        public CursoController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<ActionResult> Inicio()
        {
            return View(await _contexto.curso.ToListAsync());
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastro(Curso curso)
        {
            if (ModelState.IsValid)
            {
                var check = await _contexto.curso.FirstOrDefaultAsync(i => i.Nome == curso.Nome);
                if (check == null)
                {
                    _contexto.curso.Add(curso);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Inicio));
                }
            }
            TempData["Message"] = "Curso já Existente !!";
            return RedirectToAction(nameof(Inicio));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            var dado = await _contexto.curso.FindAsync(id);

            if (dado == null)
            {
                return NotFound();
            }

            return View(dado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Curso curso)
        {
            if (ModelState.IsValid)
            {
                var check = await _contexto.curso.FirstOrDefaultAsync(i => i.Nome == curso.Nome);
                if (check == null)
                {
                    _contexto.Entry(curso).State = EntityState.Modified;
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Inicio));
                }
            }
            TempData["Message"] = "Curso já Existente !!";
            return RedirectToAction(nameof(Inicio));
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int? id)
        {
            var dados = await _contexto.curso.FindAsync(id);
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

            var curssos = await _contexto.curso.FirstOrDefaultAsync(m => m.Id == id);
            if (curssos == null)
            {
                return NotFound();
            }

            return View(curssos);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cursso = await _contexto.curso.FindAsync(id);
            _contexto.curso.Remove(cursso);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Inicio));
        }
    }
}
