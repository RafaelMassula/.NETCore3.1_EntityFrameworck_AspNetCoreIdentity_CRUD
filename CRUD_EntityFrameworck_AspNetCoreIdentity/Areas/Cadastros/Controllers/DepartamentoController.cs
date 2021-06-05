using capitulo01.Data;
using Modelo.Cadastros;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using capitulo01.Data.DAL.Cadastros;

namespace capitulo01.Areas.Cadastros.Controllers
{
    [Area("Cadastros")]
    public class DepartamentoController : Controller
    {
        private readonly IEScontext _context;
        public DepartamentoDAL DepartamentoDAL { get; }
        public InstituicaoDAL InstituicaoDAL { get; }
        public DepartamentoController(IEScontext context)
        {
            _context = context;
            DepartamentoDAL = new DepartamentoDAL(context);
            InstituicaoDAL = new InstituicaoDAL(context);
        }

        private async Task<IActionResult> ObterVisaoDepartamentoPorId(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
               var departamento = await DepartamentoDAL.ObterDepartamentoPorId((long)id);
            if(departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }
        public async Task<IActionResult> Index()
        {
            return View(await DepartamentoDAL.ObterDepartamentosClassificadosPorNome().ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Instituicoes = await InstituicaoDAL.ObterInstituicoesClasificadasPorNome().ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome", "InstituicaoId")] Departamento departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await DepartamentoDAL.RegistrarOuAlterarDepartamento(departamento);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possivel incluir o departamento.");
            }
            return View(departamento);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return await ObterVisaoDepartamentoPorId(id);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var departamento = await DepartamentoDAL.RemoverDepartamento((long)id);
            TempData["Message"] = $"Departamento {departamento.Nome.ToUpper()} foi removido";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Details(long? id)
        {
            return await ObterVisaoDepartamentoPorId(id);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long? id)
        {
            ViewResult viewDepartamento = (ViewResult)await ObterVisaoDepartamentoPorId(id);
            Departamento departamento = (Departamento)viewDepartamento.Model;
            ViewBag.Instituicoes = new SelectList(InstituicaoDAL.ObterInstituicoesClasificadasPorNome(), "InstituicaoId", "Nome", departamento.InstituicaoId);
            return viewDepartamento;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("DepartamentoId", "Nome", "InstituicaoId")] Departamento departamento)
        {
            if (id != departamento.DepartamentoId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await DepartamentoDAL.RegistrarOuAlterarDepartamento(departamento);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await DepartamentoExistente(departamento.DepartamentoId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Instituicoes = new SelectList(_context.Instituicoes.OrderBy(i => i.Nome), "InstituicaoId", "Nome", departamento.InstituicaoId);
            return View(departamento);
        }
        private async Task<bool> DepartamentoExistente(long? id)
        {
            return await DepartamentoDAL.ObterDepartamentoPorId((long)id) != null;
        }

    }
}
