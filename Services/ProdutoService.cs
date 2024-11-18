using CrudProfisaComDapper.Exception;
using CrudProfisaComDapper.Models.Produto;
using CrudProfisaComDapper.Repositories;

namespace CrudProfisaComDapper.Services
{
    public class ProdutoService
    {
        private readonly ProdutoRepository _produtoRepository;

        public ProdutoService(ProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<ProdutoResponse> AddProdutoAsync(ProdutoRequest request)
        {
       
            if (await _produtoRepository.GetBySkuAsync(request.Sku) != null)
                throw new CustomException("Já existe um produto com este SKU.", StatusCodes.Status400BadRequest);

            return await _produtoRepository.InsertAsync(request);
        }

        public async Task<ProdutoResponse> UpdateProdutoAsync(int id, ProdutoRequest request)
        {
            var existingProduto = await _produtoRepository.GetByIdAsync(id);

            if (existingProduto == null)
                throw new CustomException("Produto não encontrado.", StatusCodes.Status404NotFound);

            if (existingProduto.Sku != request.Sku)
            {
                if (await _produtoRepository.GetBySkuAsync(request.Sku) != null)
                {
                    throw new CustomException("Já existe um produto com este SKU.", StatusCodes.Status400BadRequest);
                }
            }

            return await _produtoRepository.UpdateAsync(id, request);
        }

        public async Task<IEnumerable<ProdutoResponse>> GetAllProdutosAsync()
        {
            return await _produtoRepository.GetAllAsync();
        }

        public async Task<ProdutoResponse> GetProdutoByIdAsync(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);

            if (produto == null)
                throw new CustomException("Produto não encontrado.", StatusCodes.Status404NotFound);

            return produto;
        }

        public async Task<IEnumerable<ProdutoResponse>> GetProdutosByNameAsync(string nome)
        {
            return await _produtoRepository.GetByNameAsync(nome);
        }

        public async Task<ProdutoResponse> GetProdutoBySkuAsync(string sku)
        {
            var produto = await _produtoRepository.GetBySkuAsync(sku);

            if (produto == null)
                throw new CustomException("Produto não encontrado.", StatusCodes.Status404NotFound);

            return produto;
        }

        public async Task<bool> DeleteProdutoAsync(int id)
        {
            var existingProduto = await _produtoRepository.GetByIdAsync(id);

            if (existingProduto == null)
                throw new CustomException("Produto não encontrado.", StatusCodes.Status404NotFound);

            return await _produtoRepository.DeleteAsync(id);
        }
    }
}
