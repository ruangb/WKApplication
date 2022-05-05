using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WKDomain.Models;
using WKDomain.ModelViews;
using WKManager.Interfaces.Managers;

namespace WKWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoManager _produtoManager;

        public ProdutosController(IProdutoManager produtoManager)
        {
            _produtoManager = produtoManager;
        }

        /// <summary>
        /// Retorna todos os produtos da base
        /// </summary> 
        [HttpGet]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var produtos = await _produtoManager.GetProdutosAsync();

            if (produtos.Any())
                return Ok(produtos);
            else
                return NotFound();
        }

        /// <summary>
        /// Retorna um produto pesquisado pelo id
        /// </summary>
        /// <param name="id" example="1">Id do Produto</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var produto = await _produtoManager.GetProdutoAsync(id);

            if (produto.Id == 0)
                return NotFound();
            else
                return Ok(produto);
        }

        /// <summary>
        /// Insere um Novo Produto
        /// </summary>
        /// <param name="novoProduto">Novo Produto a ser inserido</param>
        [HttpPost]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] NovoProduto novoProduto)
        {
            Produto produtoInserido;

            produtoInserido = await _produtoManager.InsertProdutoAsync(novoProduto);

            return CreatedAtAction(nameof(Get), new { id = produtoInserido.Id }, produtoInserido);
        }

        /// <summary>
        /// Atualiza um Produto
        /// </summary>
        /// <param name="atualizaProduto">Produto a ser atualizado</param>
        [HttpPut]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] AtualizaProduto atualizaProduto)
        {
            var atualizadProduto = await _produtoManager.UpdateProdutoAsync(atualizaProduto);

            if (atualizadProduto == null)
                return NotFound();

            return Ok(atualizadProduto);
        }

        /// <summary>
        /// Deleta um Produto pelo Id
        /// </summary>
        /// <param name="id">Id do Produto</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await _produtoManager.DeleteProdutoAsync(id);

            return NoContent();
        }
    }
}
