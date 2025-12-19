using ApiEmpresarial.DTOs;
using ApiEmpresarial.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiEmpresarial.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpresasController : ControllerBase
{
    private readonly EmpresaService _empresaService;

    public EmpresasController(EmpresaService empresaService)
    {
        _empresaService = empresaService;
    }

    /// <summary>
    /// Lista todas as empresas
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<EmpresaDto>>> GetAll()
    {
        try
        {
            var empresas = await _empresaService.GetAllAsync();
            return Ok(empresas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Erro interno", detalhes = ex.Message });
        }
    }

    /// <summary>
    /// Busca empresa por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<EmpresaDto>> GetById(int id)
    {
        try
        {
            var empresa = await _empresaService.GetByIdAsync(id);
            if (empresa == null)
                return NotFound(new { error = $"Empresa com ID {id} não encontrada." });

            return Ok(empresa);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Erro ao buscar empresa", detalhes = ex.Message });
        }
    }

    /// <summary>
    /// Cria uma nova empresa
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<EmpresaDto>> Create([FromBody] CreateEmpresaDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var empresa = await _empresaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = empresa.Id }, empresa);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = "Erro ao criar empresa", detalhes = ex.Message });
        }
    }

    /// <summary>
    /// Atualiza uma empresa existente
    /// </summary>
    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdateEmpresaDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var success = await _empresaService.UpdateAsync(dto);
            if (!success)
                return NotFound(new { error = $"Empresa com ID {dto.Id} não encontrada." });

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = "Erro ao atualizar empresa", detalhes = ex.Message });
        }
    }

    /// <summary>
    /// Exclui uma empresa (só se não tiver funcionários)
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var success = await _empresaService.DeleteAsync(id);
            if (!success)
                return NotFound(new { error = $"Empresa com ID {id} não encontrada." });

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Erro ao excluir empresa", detalhes = ex.Message });
        }
    }
}