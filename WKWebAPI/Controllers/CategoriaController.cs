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
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaManager _categoriaManager;

        public CategoriaController(ICategoriaManager categoriaManager)
        {
            _categoriaManager = categoriaManager;
        }

        /// <sumamary>
        /// Retorna todos os categorias da base
        /// </sumamary> 
        [HttpGet("list")]
        [ProducesResponseType(typeof(Categoria), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListAsync()
        {
            var categorias = await _categoriaManager.GetAsync();

            if (categorias.Any())
                return Ok(categorias);
            else
                return NotFound();
        }

        /// <sumamary>
        /// Retorna uma categoria pesquisado pelo id
        /// </sumamary>
        /// <param name="id" example="1">Id do Categoria</param>
        /// 
        [HttpGet("get/{id}")]
        [ProducesResponseType(typeof(Categoria), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var categoria = await _categoriaManager.GetAsync(id);

            if (categoria.Id == 0)
                return NotFound();
            else
                return Ok(categoria);
        }

        /// <sumamary>
        /// Insere uma Nova Categoria
        /// </sumamary>
        /// <param name="novaCategoria">Nova Categoria a ser inserido</param>
        [HttpPost("insert")]
        [ProducesResponseType(typeof(Categoria), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InsertAsync([FromBody] NovaCategoria novaCategoria)
        {
            Categoria categoriaInserido;

            categoriaInserido = await _categoriaManager.InsertAsync(novaCategoria);

            return CreatedAtAction(nameof(GetAsync), new { id = categoriaInserido.Id }, categoriaInserido);
        }

        /// <sumamary>
        /// Atualiza uma Categoria
        /// </sumamary>
        /// <param name="categoria">Categoria a ser atualizada</param>
        [HttpPut("update")]
        [ProducesResponseType(typeof(Categoria), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync([FromBody] Categoria categoria)
        {
            var categoriaAtualizada = await _categoriaManager.UpdateAsync(categoria);

            if (categoriaAtualizada == null)
                return NotFound();

            return Ok(categoriaAtualizada);
        }

        /// <sumamary>
        /// Deleta uma Categoria pelo Id
        /// </sumamary>
        /// <param name="id">Id do Categoria</param>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(Categoria), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _categoriaManager.DeleteAsync(id);

            return Ok();
        }
    }
}
