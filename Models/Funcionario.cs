using System.ComponentModel.DataAnnotations;

namespace ApiEmpresarial.Models;

public class Funcionario
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [StringLength(14, MinimumLength = 11, ErrorMessage = "CPF inválido.")]
    public string Cpf { get; set; } = string.Empty;

    [Range(1000, 50000, ErrorMessage = "Salário deve estar entre R$1.000 e R$50.000.")]
    public decimal Salario { get; set; }

    public DateTime DataAdmissao { get; set; } = DateTime.UtcNow;

    // Chave estrangeira
    [Required]
    public int EmpresaId { get; set; }

    // Navegação
    public Empresa? Empresa { get; set; }
}