using System.ComponentModel.DataAnnotations;

namespace ApiEmpresarial.DTOs;

public class CreateFuncionarioDto
{
    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [StringLength(14, MinimumLength = 11)]
    public string Cpf { get; set; } = string.Empty;

    [Range(1000, 50000)]
    public decimal Salario { get; set; }

    [Required]
    public int EmpresaId { get; set; }
}