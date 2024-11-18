using System.Collections.Generic;
using System.Threading.Tasks;
using CrudProfisaComDapper.Connections;
using CrudProfisaComDapper.Models.Produto;
using Dapper;

namespace CrudProfisaComDapper.Repositories
{
    public class ProdutoRepository
    {
        private readonly PostgreSqlConnectionFactory _connectionFactory;

        public ProdutoRepository(PostgreSqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<ProdutoResponse> InsertAsync(ProdutoRequest request)
        {
            const string sql = @"
                INSERT INTO produtos (sku, nome, preco, qtd_estoque, data_fabricacao)
                VALUES (@Sku, @Nome, @Preco, @QtdEstoque, @DataFabricacao)
                RETURNING *;
            ";

            using var connection = _connectionFactory.GetConnection();
            return await connection.QuerySingleAsync<ProdutoResponse>(sql, request);
        }

        // Método para atualizar um produto e retornar os dados atualizados
        public async Task<ProdutoResponse> UpdateAsync(int id, ProdutoRequest request)
        {
            const string sql = @"
                UPDATE produtos
                SET sku = @Sku,
                    nome = @Nome,
                    preco = @Preco,
                    qtd_estoque = @QtdEstoque,
                    data_fabricacao = @DataFabricacao
                WHERE id = @Id
                RETURNING *;
            ";

            using var connection = _connectionFactory.GetConnection();
            return await connection.QuerySingleAsync<ProdutoResponse>(sql, new {
                Id = id, 
                request.Sku, 
                request.Nome, 
                request.Preco, 
                request.QtdEstoque, 
                request.DataFabricacao 
            });
        }

        public async Task<IEnumerable<ProdutoResponse>> GetAllAsync()
        {
            const string sql = @"
                SELECT * FROM produtos;
            ";

            using var connection = _connectionFactory.GetConnection();
            return await connection.QueryAsync<ProdutoResponse>(sql);
        }

        public async Task<ProdutoResponse> GetByIdAsync(int id)
        {
            const string sql = @"
                SELECT * FROM produtos
                WHERE id = @Id;
            ";

            using var connection = _connectionFactory.GetConnection();
            return await connection.QueryFirstOrDefaultAsync<ProdutoResponse>(sql, new { Id = id });
        }

        public async Task<IEnumerable<ProdutoResponse>> GetByNameAsync(string nome)
        {
            const string sql = @"
                SELECT * FROM produtos
                WHERE nome ILIKE '%' || @Nome || '%';
            ";

            using var connection = _connectionFactory.GetConnection();
            return await connection.QueryAsync<ProdutoResponse>(sql, new { Nome = nome });
        }

        public async Task<ProdutoResponse> GetBySkuAsync(string sku)
        {
            const string sql = @"
                SELECT * FROM produtos
                WHERE sku = @Sku;
            ";

            using var connection = _connectionFactory.GetConnection();
            return await connection.QueryFirstOrDefaultAsync<ProdutoResponse>(sql, new { Sku = sku });
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = @"
                DELETE FROM produtos
                WHERE id = @Id;
            ";

            using var connection = _connectionFactory.GetConnection();
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
