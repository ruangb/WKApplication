using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WKDomain.Models;
using WKDomain.ModelViews;
using WKWebApp.Manager.Repositories;
using WKWebApp.Models;

namespace WKWebApp.Controllers
{
    [Route("[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public ProdutoController(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet("index")]
        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<Produto> produto = await _produtoRepository.GetAsync();

            return View(produto);
        }

        [HttpGet("details")]
        public async Task<ActionResult> DetailsAsync(int id)
        {
            var produto = await _produtoRepository.GetAsync(id);
            return View(produto);
        }

        [HttpGet("insert")]
        public IActionResult InsertAsync()
        {
            var categorias = _categoriaRepository.GetAsync().Result;

            ViewBag.Categorias = new SelectList(categorias, "Id", "Nome");

            return View();
        }

        [HttpPost("insert")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertAsync(NovoProduto produto)
        {
            if (!ModelState.IsValid)
                return View(produto);

            await _produtoRepository.InsertAsync(produto);

            TempData["$AlertMessage$"] = "Registro salvo com sucesso!";

            return RedirectToAction("Insert");
        }

        [HttpGet("edit")]
        public async Task<IActionResult> EditAsync(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });

            var produto = await _produtoRepository.GetAsync(id.Value);

            if (produto == null)
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });

            var categorias = _categoriaRepository.GetAsync().Result;

            categorias.Add(new Categoria() { Id = null, Nome = "Selecione uma opção" });

            ViewBag.Categorias = new SelectList(categorias, "Id", "Nome", categorias.Where(x => x.Id == produto.CategoriaId).FirstOrDefault());

            return View(produto);
        }

        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, Produto produto)
        {
            if (!ModelState.IsValid)
                return View(produto);

            if (id != produto.Id)
                return RedirectToAction(nameof(Error), new { message = "O produto não corresponde" });

            try
            {
                var result = await _produtoRepository.UpdateAsync(produto);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [HttpGet("delete")]
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            if (id != null)
            {
                var obj = await _produtoRepository.GetAsync(id.Value);

                if (obj != null)
                    return View(obj);
            }

            return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
        }

        [HttpPost("delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                _produtoRepository.Delete(id);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

            return RedirectToAction("index");
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
