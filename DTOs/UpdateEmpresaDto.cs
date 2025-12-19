using System.ComponentModel.DataAnnotations;

namespace ApiEmpresarial.DTOs;

public class UpdateEmpresaDto
{
    [Required]
    public int Id { get; set; }

    [StringLength(100)]
    public string? Nome { get; set; }

    [StringLength(18, MinimumLength = 14)]
    public string? Cnpj { get; set; }

    public string? Endereco { get; set; }
}