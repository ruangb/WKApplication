using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WKDomain.Models;
using WKDomain.ModelViews;
using WKWebApp.Manager.Repositories;

namespace WKWebApp.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public ProdutoController(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<ActionResult> Index()
        {
            IEnumerable<Produto> produto = await _produtoRepository.GetProdutosAsync();

            return View(produto);
        }

        public ActionResult Detalhes(int id)
        {
            return View();
        }

        [Route("inserir")]
        public ActionResult Inserir()
        {
            ViewBag.Categorias = _categoriaRepository.ObterCategoriasAsync();
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

            return RedirectToAction("Create");
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
