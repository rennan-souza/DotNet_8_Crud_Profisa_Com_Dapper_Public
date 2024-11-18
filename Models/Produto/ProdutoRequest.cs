using System.ComponentModel.DataAnnotations;

namespace CrudProfisaComDapper.Models.Produto
{
    public class ProdutoRequest
    {
        [Required(ErrorMessage = "O SKU é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O SKU deve ter no máximo 50 caracteres.")]
        public string Sku { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        public decimal? Preco { get; set; }

        public int? QtdEstoque { get; set; }

        public DateTime? DataFabricacao { get; set; }
    }
}
