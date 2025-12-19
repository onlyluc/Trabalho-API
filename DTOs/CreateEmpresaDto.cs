using System.ComponentModel.DataAnnotations;

namespace ApiEmpresarial.DTOs;

public class CreateEmpresaDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [StringLength(18, MinimumLength = 14)]
    public string Cnpj { get; set; } = string.Empty;

    public string? Endereco { get; set; }
}