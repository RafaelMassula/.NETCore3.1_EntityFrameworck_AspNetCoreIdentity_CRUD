using CRUD_EntityFrameworck_AspNetCoreIdentity.Data;
using CRUD_EntityFrameworck_AspNetCoreIdentity.Data.DAL.Discente;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelo.Discente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_EntityFrameworck_AspNetCoreIdentity.Areas.Discente.Controllers
{
    [Area("Discente")]
    public class AcademicoController : Controller
    {
        public IEScontext IEScontext { get; }
        public AcademicoDAL AcademicoDAL { get; }

        public AcademicoController(IEScontext iEScontext)
        {
            IEScontext = iEScontext;
            AcademicoDAL = new AcademicoDAL(iEScontext);
        }
        private async Task<IActionResult> ObterVisaoAcademicoPorId(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var academico = await AcademicoDAL.ObterAcademicosPorId((long)id);
            if (academico == null)
            {
                return NotFound();
            }
            return View(academico);
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await AcademicoDAL.ObeterAcademicosClassificadosPorNome().ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome", "RegistroAcademico", "Nascimento")] Academico academico)
        {
            if (academico == null)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    await AcademicoDAL.RegistrarOuAlterarAcademico(academico);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir dados");
            }
            return View(academico);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            return await ObterVisaoAcademicoPorId(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Academico academico)
        {
            if (academico == null)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    await AcademicoDAL.RegistrarOuAlterarAcademico(academico);
                }
            }
            catch (DbUpdateException)
            {

                ModelState.AddModelError("", "Não foi possível atualizar o academico");
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            return await ObterVisaoAcademicoPorId(id);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var academico = await AcademicoDAL.RemoverAcademico(id);
            TempData["Message"] = $"Acadêmico {academico.Nome.ToUpper()} foi removida.";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            return await ObterVisaoAcademicoPorId(id);
        }
    }
}
