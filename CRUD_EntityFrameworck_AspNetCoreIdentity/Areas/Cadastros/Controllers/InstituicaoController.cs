using CRUD_EntityFrameworck_AspNetCoreIdentity.Data;
using Modelo.Cadastros;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_EntityFrameworck_AspNetCoreIdentity.Data.DAL.Cadastros;
using Microsoft.AspNetCore.Authorization;

namespace CRUD_EntityFrameworck_AspNetCoreIdentity.Areas.Cadastros.Controllers
{
    [Area("Cadastros")]
    [Authorize]
    public class InstituicaoController : Controller
    {
        private readonly IEScontext _context;
        public InstituicaoDAL InstituicaoDAL { get; set; }
        public InstituicaoController(IEScontext context)
        {
            _context = context;
            InstituicaoDAL = new InstituicaoDAL(context);
        }

        private async Task<IActionResult> ObterVisaoInstituicaoPorId(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instituicao = await InstituicaoDAL.ObterInstituicaoPorId(id);
            if (instituicao == null)
            {
                return NotFound();
            }
            return View(instituicao);
        }
        private async Task<bool> InstituicaoExists(long? id)
        {
            return await InstituicaoDAL.ObterInstituicaoPorId((long)id) != null;
        }
        public async Task<IActionResult> Index()
        {
            return View(await InstituicaoDAL.ObterInstituicoesClasificadasPorNome().ToListAsync());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("InstituicaoId", "Nome", "Endereco")] Instituicao instituicao)
        {
            if (instituicao == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await InstituicaoDAL.RegistrarOuAlterarInstituicao(instituicao);
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Não foi possivel incluir uma instituição!");
                }
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            return await ObterVisaoInstituicaoPorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(long? id, [Bind("InstituicaoId", "Nome", "Endereco")] Instituicao instituicao)
        {
            if (id != instituicao.InstituicaoId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await InstituicaoDAL.RegistrarOuAlterarInstituicao(instituicao);
                }
                catch (DbUpdateConcurrencyException)
                {

                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            return await ObterVisaoInstituicaoPorId(id);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            return await ObterVisaoInstituicaoPorId(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(long? id)
        {
            var instituicao = await InstituicaoDAL.ObterInstituicaoPorId(id);
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await InstituicaoDAL.RemoverInstituicao((long)id);
                }
                catch (DbUpdateException)
                {
                    return NotFound();
                }
            }

            TempData["Message"] = "Intituição " + instituicao.Nome.ToUpper() + " foi removida.";
            return RedirectToAction(nameof(Index));
        }
    }
}
