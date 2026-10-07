using System.ComponentModel.DataAnnotations;
namespace MinhaPrimeiraApi.Models;

public class Produto
{
    public int Id {get; set;}
    [Required(ErrorMessage = "O Nome do Produto é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O Nome não pode ter mais de 100 caracteres.")]
    public string Nome {get; set;}
    [Range(0.01, 1000, ErrorMessage = "O preço deve estar entre 0,01 e 1.000.")]public decimal Preco {get; set;}

    // Regras de Negócio não entra na Model, Por exemplo limitar quantidade
    // [Range(1, 100, ErrorMessage = "A Quantidade do produo deve estar entre 1 e 100.")]
    public int Quantidade {get; set;}
}