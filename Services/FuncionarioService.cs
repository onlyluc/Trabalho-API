using ApiEmpresarial.Data;
using ApiEmpresarial.DTOs;
using ApiEmpresarial.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEmpresarial.Services;

public class FuncionarioService
{
    private readonly AppDbContext _context;

    public FuncionarioService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<FuncionarioDto>> GetAllAsync()
    {
        return await _context.Funcionarios
            .Include(f => f.Empresa) // carrega nome da empresa
            .Select(f => new FuncionarioDto
            {
                Id = f.Id,
                Nome = f.Nome,
                Cpf = f.Cpf,
                Salario = f.Salario,
                DataAdmissao = f.DataAdmissao,
                EmpresaId = f.EmpresaId,
                EmpresaNome = f.Empresa!.Nome
            })
            .ToListAsync();
    }

    public async Task<FuncionarioDto?> GetByIdAsync(int id)
    {
        return await _context.Funcionarios
            .Include(f => f.Empresa)
            .Where(f => f.Id == id)
            .Select(f => new FuncionarioDto
            {
                Id = f.Id,
                Nome = f.Nome,
                Cpf = f.Cpf,
                Salario = f.Salario,
                DataAdmissao = f.DataAdmissao,
                EmpresaId = f.EmpresaId,
                EmpresaNome = f.Empresa!.Nome
            })
            .FirstOrDefaultAsync();
    }

    public async Task<FuncionarioDto> CreateAsync(CreateFuncionarioDto dto)
    {
        // ✅ Valida se empresa existe
        var empresaExists = await _context.Empresas.AnyAsync(e => e.Id == dto.EmpresaId);
        if (!empresaExists)
            throw new ArgumentException("EmpresaId inválido: empresa não encontrada.");

        var funcionario = new Funcionario
        {
            Nome = dto.Nome,
            Cpf = dto.Cpf,
            Salario = dto.Salario,
            EmpresaId = dto.EmpresaId,
            DataAdmissao = DateTime.UtcNow
        };

        _context.Funcionarios.Add(funcionario);
        await _context.SaveChangesAsync();

        var empresa = await _context.Empresas.FindAsync(dto.EmpresaId);

        return new FuncionarioDto
        {
            Id = funcionario.Id,
            Nome = funcionario.Nome,
            Cpf = funcionario.Cpf,
            Salario = funcionario.Salario,
            DataAdmissao = funcionario.DataAdmissao,
            EmpresaId = funcionario.EmpresaId,
            EmpresaNome = empresa!.Nome
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateFuncionarioDto dto)
    {
        var funcionario = await _context.Funcionarios.FindAsync(id);
        if (funcionario == null) return false;

        if (!string.IsNullOrEmpty(dto.Nome)) funcionario.Nome = dto.Nome;
        if (!string.IsNullOrEmpty(dto.Cpf)) funcionario.Cpf = dto.Cpf;
        if (dto.Salario > 0) funcionario.Salario = dto.Salario;
        if (dto.EmpresaId > 0) funcionario.EmpresaId = dto.EmpresaId;

        _context.Entry(funcionario).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var funcionario = await _context.Funcionarios.FindAsync(id);
        if (funcionario == null) return false;

        _context.Funcionarios.Remove(funcionario);
        await _context.SaveChangesAsync();
        return true;
    }
}