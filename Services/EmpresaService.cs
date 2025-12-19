using ApiEmpresarial.Data;
using ApiEmpresarial.DTOs;
using ApiEmpresarial.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEmpresarial.Services;

public class EmpresaService
{
    private readonly AppDbContext _context;

    public EmpresaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmpresaDto>> GetAllAsync()
    {
        var empresas = await _context.Empresas
            .Select(e => new EmpresaDto
            {
                Id = e.Id,
                Nome = e.Nome,
                Cnpj = e.Cnpj,
                Endereco = e.Endereco,
                DataCriacao = e.DataCriacao
            })
            .ToListAsync();
        return empresas;
    }

    public async Task<EmpresaDto?> GetByIdAsync(int id)
    {
        var empresa = await _context.Empresas
            .Where(e => e.Id == id)
            .Select(e => new EmpresaDto
            {
                Id = e.Id,
                Nome = e.Nome,
                Cnpj = e.Cnpj,
                Endereco = e.Endereco,
                DataCriacao = e.DataCriacao
            })
            .FirstOrDefaultAsync();
        return empresa;
    }

    public async Task<EmpresaDto> CreateAsync(CreateEmpresaDto dto)
    {
        var empresa = new Empresa
        {
            Nome = dto.Nome,
            Cnpj = dto.Cnpj,
            Endereco = dto.Endereco,
            DataCriacao = DateTime.UtcNow
        };

        _context.Empresas.Add(empresa);
        await _context.SaveChangesAsync();

        return new EmpresaDto
        {
            Id = empresa.Id,
            Nome = empresa.Nome,
            Cnpj = empresa.Cnpj,
            Endereco = empresa.Endereco,
            DataCriacao = empresa.DataCriacao
        };
    }

    public async Task<bool> UpdateAsync(UpdateEmpresaDto dto)
    {
        var empresa = await _context.Empresas.FindAsync(dto.Id);
        if (empresa == null) return false;

        if (!string.IsNullOrEmpty(dto.Nome)) empresa.Nome = dto.Nome;
        if (!string.IsNullOrEmpty(dto.Cnpj)) empresa.Cnpj = dto.Cnpj;
        if (dto.Endereco != null) empresa.Endereco = dto.Endereco;

        _context.Empresas.Update(empresa);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var empresa = await _context.Empresas.FindAsync(id);
        if (empresa == null) return false;

        // ✅ Proteção: não apagar se tiver funcionários
        var temFuncionarios = await _context.Funcionarios.AnyAsync(f => f.EmpresaId == id);
        if (temFuncionarios)
            throw new InvalidOperationException("Não é possível excluir uma empresa com funcionários.");

        _context.Empresas.Remove(empresa);
        await _context.SaveChangesAsync();
        return true;
    }
}