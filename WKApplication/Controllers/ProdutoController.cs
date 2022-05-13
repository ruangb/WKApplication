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
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoManager _produtoManager;

        public ProdutoController(IProdutoManager produtoManager)
        {
            _produtoManager = produtoManager;
        }

        /// <summary>
        /// Retorna todos os produtos da base
        /// </summary> 
        [HttpGet("listar")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Listar()
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
        [HttpGet("obter/{id}")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Obter(int id)
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
        [HttpPost("inserir")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Inserir([FromBody] NovoProduto novoProduto)
        {
            Produto produtoInserido;

            produtoInserido = await _produtoManager.InsertProdutoAsync(novoProduto);

            return CreatedAtAction(nameof(Obter), new { id = produtoInserido.Id }, produtoInserido);
        }

        /// <summary>
        /// Atualiza um Produto
        /// </summary>
        /// <param name="produto">Produto a ser atualizado</param>
        [HttpPut("atualizar")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Atualizar([FromBody] Produto produto)
        {
            var produtoAtualizado = await _produtoManager.UpdateProdutoAsync(produto);

            if (produtoAtualizado == null)
                return NotFound();

            return Ok(produtoAtualizado);
        }

        /// <summary>
        /// Deleta um Produto pelo Id
        /// </summary>
        /// <param name="id">Id do Produto</param>
        [HttpDelete("deletar/{id}")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Deletar(int id)
        {
            await _produtoManager.DeleteProdutoAsync(id);

            return NoContent();
        }
    }
}
