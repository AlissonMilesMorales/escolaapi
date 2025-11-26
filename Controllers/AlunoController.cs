using EscolaAPI.Data;
using EscolaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Mapeia models para db

namespace EscolaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AlunoController : ControllerBase
{
    private readonly EscolaContext _context;

    public AlunoController(EscolaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
    {
        if (_context.Alunos == null)
        {
            return NotFound();
        }
        return await _context.Alunos.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Aluno>> PostAluno(Aluno aluno)
    {
        if (_context.Alunos == null)
        {
            return Problem("Entidade aluno é null");
        }
        _context.Alunos.Add(aluno);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetAlunos", new { id = aluno.Id }, aluno);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Aluno>> GetAluno(int id)
    {
        if (_context == null)
        {
            return Problem();
        }
        var aluno = await _context.Alunos.FindAsync(id);
        if (aluno == null)
        {
            return NotFound("Id: " + id + " não encontrada no banco de dados");
        }
        return aluno;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutAluno(int id, Aluno aluno)
    {
        if (id != aluno.Id)
        {
            return BadRequest();
        }
        _context.Entry(aluno).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AlunoExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAluno(int id)
    {
        if (_context.Alunos == null)
        {
            return Problem();
        }
        var aluno = await _context.Alunos.FindAsync(id);
        if (aluno == null)
        {
            return NotFound();
        }

        _context.Alunos.Remove(aluno);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool AlunoExists(int id)
    {
        return (_context.Alunos?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}


