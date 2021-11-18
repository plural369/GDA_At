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
    public class ProfessorController : Controller
    {
        private readonly Contexto _contexto;
        public ProfessorController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<ActionResult> Inicio()
        {
            return View(await _contexto.professor.ToListAsync());
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastro(Professor professor)
        {
            if (ModelState.IsValid)
            {
                var check = await _contexto.professor.FirstOrDefaultAsync(i => i.Nome == professor.Nome);
                if (check == null)
                {
                    _contexto.professor.Add(professor);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Inicio));
                }

            }
            TempData["Message"] = "Professor já Existente !!";
            return RedirectToAction(nameof(Inicio));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            var dado = await _contexto.professor.FindAsync(id);

            if (dado == null)
            {
                return NotFound();
            }

            return View(dado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Professor professor)
        {
            if (ModelState.IsValid)
            {
                var check = await _contexto.professor.FirstOrDefaultAsync(i => i.Nome == professor.Nome);
                if (check == null)
                {
                    _contexto.Entry(professor).State = EntityState.Modified;
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Inicio));
                }
                TempData["Message"] = "Professor já Existente !!";
                return RedirectToAction(nameof(Inicio));
             
            }
            return View(professor);
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int? id)
        {
            var dados = await _contexto.professor.FindAsync(id);
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

            var professorr = await _contexto.professor.FirstOrDefaultAsync(m => m.Id == id);
            if (professorr == null)
            {
                return NotFound();
            }

            return View(professorr);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professorr = await _contexto.professor.FindAsync(id);
            _contexto.professor.Remove(professorr);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Inicio));
        }
    }
}
