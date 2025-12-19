using System.ComponentModel.DataAnnotations;

namespace ApiEmpresarial.Models;

public class Empresa
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome da empresa é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O CNPJ é obrigatório.")]
    [StringLength(18, MinimumLength = 14, ErrorMessage = "CNPJ inválido.")]
    public string Cnpj { get; set; } = string.Empty;

    [StringLength(200)]
    public string? Endereco { get; set; }

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    // Relacionamento 1:N — uma empresa tem muitos funcionários
    public ICollection<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
}