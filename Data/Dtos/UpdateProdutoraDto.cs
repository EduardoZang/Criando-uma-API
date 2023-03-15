using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmesApi.Data.Dtos{

    public class UpdateProdutoraDto{
         
   [Required]
    public string NomeProdutora { get; set; }

   
    
    }
}