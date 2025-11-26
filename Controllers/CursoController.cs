using EscolaAPI.Data;
using EscolaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Mapeia models para db

namespace EscolaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CursoController : ControllerBase
{
    private readonly EscolaContext _context;

    public CursoController(EscolaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Curso>>> GetCursos()
    {
        if (_context.Cursos == null)
        {
            return NotFound();
        }
        return await _context.Cursos.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Curso>> PostCurso(Curso curso)
    {
        if (_context.Cursos == null)
        {
            return Problem("Entidade curso é null");
        }
        _context.Cursos.Add(curso);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetCursos", new { id = curso.Id }, curso);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Curso>> GetCurso(int id)
    {
        if (_context == null)
        {
            return Problem();
        }
        var curso = await _context.Cursos.FindAsync(id);
        if (curso == null)
        {
            return NotFound("Id: " + id + " não encontrada no banco de dados");
        }
        return curso;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutCurso(int id, Curso curso)
    {
        if (id != curso.Id)
        {
            return BadRequest();
        }
        _context.Entry(curso).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CursoExists(id))
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
    public async Task<IActionResult> DeleteCurso(int id)
    {
        if (_context.Cursos == null)
        {
            return Problem();
        }
        var curso = await _context.Cursos.FindAsync(id);
        if (curso == null)
        {
            return NotFound();
        }

        _context.Cursos.Remove(curso);
        await _context.SaveChangesAsync();
        return NoContent();
    }


    private bool CursoExists(int id)
    {
        return (_context.Cursos?.Any(e => e.Id == id)).GetValueOrDefault();
    }


}