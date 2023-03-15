using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models{

    public class Produtora
    {

        [Key]
        [Required]
         public int Id { get; set; }
         
    [Required]
    public string NomeProdutora { get; set; }

    }
}
