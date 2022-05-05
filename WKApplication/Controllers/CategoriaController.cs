﻿using Microsoft.AspNetCore.Http;
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
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaManager _categoriaManager;

        public CategoriasController(ICategoriaManager categoriaManager)
        {
            _categoriaManager = categoriaManager;
        }

        /// <sumamary>
        /// Retorna todos os categorias da base
        /// </sumamary> 
        [HttpGet]
        [ProducesResponseType(typeof(Categoria), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var categorias = await _categoriaManager.GetCategoriasAsync();

            if (categorias.Any())
                return Ok(categorias);
            else
                return NotFound();
        }

        /// <sumamary>
        /// Retorna uma categoria pesquisado pelo id
        /// </sumamary>
        /// <param name="id" example="1">Id do Categoria</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Categoria), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var categoria = await _categoriaManager.GetCategoriaAsync(id);

            if (categoria.Id == 0)
                return NotFound();
            else
                return Ok(categoria);
        }

        /// <sumamary>
        /// Insere uma Nova Categoria
        /// </sumamary>
        /// <param name="novaCategoria">Nova Categoria a ser inserido</param>
        [HttpPost]
        [ProducesResponseType(typeof(Categoria), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] NovaCategoria novaCategoria)
        {
            Categoria categoriaInserido;

            categoriaInserido = await _categoriaManager.InsertCategoriaAsync(novaCategoria);

            return CreatedAtAction(nameof(Get), new { id = categoriaInserido.Id }, categoriaInserido);
        }

        /// <sumamary>
        /// Atualiza uma Categoria
        /// </sumamary>
        /// <param name="atualizaCategoria">Categoria a ser atualizado</param>
        [HttpPut]
        [ProducesResponseType(typeof(Categoria), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] AtualizaCategoria atualizaCategoria)
        {
            var atualizadCategoria = await _categoriaManager.UpdateCategoriaAsync(atualizaCategoria);

            if (atualizadCategoria == null)
                return NotFound();

            return Ok(atualizadCategoria);
        }

        /// <sumamary>
        /// Deleta uma Categoria pelo Id
        /// </sumamary>
        /// <param name="id">Id do Categoria</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Categoria), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoriaManager.DeleteCategoriaAsync(id);

            return NoContent();
        }
    }
}
