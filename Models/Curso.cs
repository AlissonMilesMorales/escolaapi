using System.ComponentModel.DataAnnotations;

namespace EscolaAPI.Models;

public class Curso
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int CargaHoraria { get; set; }

    public ICollection<Aluno> Alunos { get; } = new List<Aluno>();
}