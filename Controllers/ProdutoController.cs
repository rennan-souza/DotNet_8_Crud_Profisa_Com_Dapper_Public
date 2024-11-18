using CrudProfisaComDapper.Models.Produto;
using CrudProfisaComDapper.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrudProfisaComDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _produtoService;

        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProdutoRequest produtoRequest)
        {
            var produto = await _produtoService.AddProdutoAsync(produtoRequest);
            return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProdutoRequest produtoRequest)
        {
            var produto = await _produtoService.UpdateProdutoAsync(id, produtoRequest);
            return Ok(produto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var produtos = await _produtoService.GetAllProdutosAsync();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var produto = await _produtoService.GetProdutoByIdAsync(id);
            return Ok(produto);
        }

        [HttpGet("search-by-name")]
        public async Task<IActionResult> GetByName([FromQuery] string nome)
        {
            var produtos = await _produtoService.GetProdutosByNameAsync(nome);
            return Ok(produtos);
        }

        [HttpGet("search-by-sku/{sku}")]
        public async Task<IActionResult> GetBySku(string sku)
        {
            var produto = await _produtoService.GetProdutoBySkuAsync(sku);
            return Ok(produto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _produtoService.DeleteProdutoAsync(id);
            return sucesso ? NoContent() : NotFound();
        }
    }
}
