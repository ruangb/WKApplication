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
        public async Task<ActionResult> Index()
        {
            IEnumerable<Produto> produto = await _produtoRepository.GetProdutosAsync();

            return View(produto);
        }

        [HttpGet("detalhes")]
        public ActionResult Detalhes(int id)
        {
            return View();
        }

        [HttpGet("inserir")]
        public ActionResult Inserir()
        {
            var categorias = _categoriaRepository.ObterCategoriasAsync().Result;

            ViewBag.Categorias = new SelectList(categorias, "Id", "Nome");

            return View();
        }

        [HttpPost]
        [Route("inserir")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InserirAsync(NovoProduto produto)
        {
            if (!ModelState.IsValid)
                return View(produto);

            await _produtoRepository.InsertProdutoAsync(produto);

            TempData["$AlertMessage$"] = "Registro salvo com sucesso!";

            return RedirectToAction("Inserir");
        }

        [Route("editar")]
        public async Task<ActionResult> EditarAsync(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            var produto = await _produtoRepository.GetProdutoAsync(id.Value);

            if (produto == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            var categorias = _categoriaRepository.ObterCategoriasAsync().Result;

            categorias.Add(new Categoria() { Id = 0, Nome = "Selecione uma opção" });

            ViewBag.Categorias = new SelectList(categorias, "Id", "Nome", categorias.Where(x => x.Id == produto.CategoriaId).FirstOrDefault());

            return View(produto);
        }

        [HttpPost]
        [Route("editar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditarAsync(int id, Produto produto)
        {
            if (!ModelState.IsValid)
                return View(produto);

            if (id != produto.Id)
                return RedirectToAction(nameof(Error), new { message = "O produto não corresponde" });

            try
            {
                var result = await _produtoRepository.UpdateProdutoAsync(produto);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [Route("deletar")]
        public ActionResult Deletar(int id)
        {
            return View();
        }

        [HttpPost]
        [Route("deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult Deletar(int id, IFormCollection collection)
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
