namespace ApiEmpresarial.DTOs;

public class UpdateFuncionarioDto
{
    public string? Nome { get; set; }
    public string? Cpf { get; set; }
    public decimal Salario { get; set; }
    public int EmpresaId { get; set; }
}