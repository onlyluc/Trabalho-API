using ApiEmpresarial.DTOs;
using ApiEmpresarial.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiEmpresarial.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FuncionariosController : ControllerBase
{
    private readonly FuncionarioService _funcionarioService;

    public FuncionariosController(FuncionarioService funcionarioService)
    {
        _funcionarioService = funcionarioService;
    }

    [HttpGet]
    public async Task<ActionResult<List<FuncionarioDto>>> GetAll()
    {
        try
        {
            var funcionarios = await _funcionarioService.GetAllAsync();
            return Ok(funcionarios);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Erro interno", detalhes = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FuncionarioDto>> GetById(int id)
    {
        try
        {
            var funcionario = await _funcionarioService.GetByIdAsync(id);
            if (funcionario == null)
                return NotFound(new { error = $"Funcionário com ID {id} não encontrado." });

            return Ok(funcionario);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Erro ao buscar funcionário", detalhes = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<FuncionarioDto>> Create([FromBody] CreateFuncionarioDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var funcionario = await _funcionarioService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = funcionario.Id }, funcionario);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = "Erro ao criar funcionário", detalhes = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateFuncionarioDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var success = await _funcionarioService.UpdateAsync(id, dto);
            if (!success)
                return NotFound(new { error = $"Funcionário com ID {id} não encontrado." });

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = "Erro ao atualizar funcionário", detalhes = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var success = await _funcionarioService.DeleteAsync(id);
            if (!success)
                return NotFound(new { error = $"Funcionário com ID {id} não encontrado." });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Erro ao excluir funcionário", detalhes = ex.Message });
        }
    }
}