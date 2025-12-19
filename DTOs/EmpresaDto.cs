using System.ComponentModel.DataAnnotations;

namespace ApiEmpresarial.DTOs;

public class EmpresaDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string? Endereco { get; set; }
    public DateTime DataCriacao { get; set; }
}