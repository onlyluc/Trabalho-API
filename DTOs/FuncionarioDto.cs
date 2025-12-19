namespace ApiEmpresarial.DTOs;

public class FuncionarioDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public decimal Salario { get; set; }
    public DateTime DataAdmissao { get; set; }
    public int EmpresaId { get; set; }
    public string EmpresaNome { get; set; } = string.Empty;
}