using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EscolaAPI.Models;

public class Aluno
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public int IdCurso { get; set; } // Para rel obrigatórios NOT NULL
    public Curso Curso { get; set; }
}