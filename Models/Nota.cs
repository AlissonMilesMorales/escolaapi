using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaAPI.Models;

public class Nota
{
    [Key]
    public int Id { get; set; }

    public int IdAluno { get; set; }
    public Aluno Aluno { get; set; }

     public int IdCurso { get; set; }
    public Curso Curso { get; set; }
    public double Nota1 { get; set; }
    public double Nota2 { get; set; }
    public double Nota3 { get; set; }
}
