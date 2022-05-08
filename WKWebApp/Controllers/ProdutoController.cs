using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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


        [HttpGet]
        [Route("inserir")]
        public ActionResult Inserir()
        {
            var categorias = _categoriaRepository.ObterCategoriasAsync().Result;

            ViewBag.Categorias = new SelectList(categorias, "Value", "Text");

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

        public ActionResult Deletar(int id)
        {
            return View();
        }

        [HttpPost]
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
    }
}
