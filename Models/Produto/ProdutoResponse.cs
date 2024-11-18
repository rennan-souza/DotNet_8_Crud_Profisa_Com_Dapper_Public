namespace CrudProfisaComDapper.Models.Produto
{
    public class ProdutoResponse
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Nome { get; set; }
        public decimal? Preco { get; set; }
        public int? QtdEstoque { get; set; }
        public DateTime? DataFabricacao { get; set; }
    }
}
