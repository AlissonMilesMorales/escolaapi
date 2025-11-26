using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscolaAPI.Data;
using EscolaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EscolaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class NotaController : ControllerBase
{
    private readonly EscolaContext _context;

    public NotaController(EscolaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Nota>>> GetNotas()
    {
        if (_context.Notas == null)
        {
            return NotFound();
        }
        return await _context.Notas.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Nota>> GetNota(int id)
    {
        if (_context.Notas == null)
        {
            return NotFound();
        }

        var nota = await _context.Notas.FindAsync(id);

        if (nota == null)
        {
            return NotFound("Id: " + id + " não encontrada no banco de dados");
        }

        return nota;
    }

    [HttpPost]
    public async Task<ActionResult<Nota>> PostNota(Nota nota)
    {
        if (_context.Notas == null)
        {
            return Problem("Entidade nota é null");
        }

        _context.Notas.Add(nota);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetNota", new { id = nota.Id }, nota);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutNota(int id, Nota nota)
    {
        if (id != nota.Id)
        {
            return BadRequest();
        }

        _context.Entry(nota).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!NotaExists(id))
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
    public async Task<IActionResult> DeleteNota(int id)
    {
        if (_context.Notas == null)
        {
            return Problem();
        }

        var nota = await _context.Notas.FindAsync(id);
        if (nota == null)
        {
            return NotFound();
        }

        _context.Notas.Remove(nota);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool NotaExists(int id)
    {
        return (_context.Notas?.Any(e => e.Id == id)).GetValueOrDefault();
    }

}