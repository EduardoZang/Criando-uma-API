using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos{

    public class CreateProdutoraDto{
         
    [Required]
    public string NomeProdutora { get; set; }
    }
}
