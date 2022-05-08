using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using WKDomain.Models;
using WKDomain.ModelViews;
using WKWebApp.Manager.Repositories;
using WKWebApp.Models;

namespace WKWebApp.Controllers
{
    //[Route("categoria")]
    [Route("[controller]")]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet("index")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<Categoria> categoria = await _categoriaRepository.ObterCategoriasAsync();

            return View(categoria);
        }

        [HttpGet("detalhes")]
        public ActionResult Detalhes(int id)
        {
            return View();
        }

        [HttpGet("inserir")]
        public ActionResult Inserir()
        {
            ViewBag.Categorias = _categoriaRepository.ObterCategoriasAsync();
            return View();
        }

        [HttpPost]
        [Route("inserir")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InserirAsync(NovaCategoria categoria)
        {
            if (!ModelState.IsValid)
                return View(categoria);

            await _categoriaRepository.InserirCategoriaAsync(categoria);

            TempData["$AlertMessage$"] = "Registro salvo com sucesso!";

            return RedirectToAction("Inserir");
        }

        public ActionResult Editar(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Deletar(int? id)
        {
            if (id != null)
            {
                var obj = await _categoriaRepository.ObterCategoriaAsync(id.Value);

                if (obj != null)
                    return View(obj);
            }

            return RedirectToAction(nameof(Error), new { message = "Id not provided" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                _categoriaRepository.DeletarCategoriaAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
