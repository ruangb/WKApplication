using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
    [Route("[controller]")]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [Route("index")]
        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<Categoria> categoria = await _categoriaRepository.GetAsync();

            return View(categoria);
        }

        [HttpGet("details")]
        public async Task<ActionResult> DetailsAsync(int id)
        {
            var categoria = await _categoriaRepository.GetAsync(id);
            return View(categoria);
        }

        [HttpGet("insert")]
        public async Task<ActionResult> InsertAsync()
        {
            ViewBag.Categorias = await _categoriaRepository.GetAsync();
            return View();
        }

        [HttpPost]
        [Route("insert")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertAsync(NovaCategoria categoria)
        {
            if (!ModelState.IsValid)
                return View(categoria);

            await _categoriaRepository.InsertAsync(categoria);

            TempData["$AlertMessage$"] = "Registro salvo com sucesso!";

            return RedirectToAction("insert");
        }

        [Route("edit")]
        public async Task<ActionResult> EditAsync(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });

            var categoria = await _categoriaRepository.GetAsync(id.Value);

            if (categoria == null)
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });

            return View(categoria);
        }

        [HttpPost]
        [Route("edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Categoria categoria)
        {
            if (!ModelState.IsValid)
                return View(categoria);

            if (id != categoria.Id)
                return RedirectToAction(nameof(Error), new { message = "O categoria não corresponde" });

            try
            {
                var result = await _categoriaRepository.UpdateAsync(categoria);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [Route("delete")]
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            if (id != null)
            {
                var obj = await _categoriaRepository.GetAsync(id.Value);

                if (obj != null)
                    return View(obj);
            }

            return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
        }

        [HttpPost]
        [Route("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                _categoriaRepository.DeleteAsync(id);
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
