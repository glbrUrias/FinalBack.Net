using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace finalb2020.Models
{
    public class Alumno
    {
        [Key]
        public int Carne { get; set; } 
        public int NoExpediente { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Email { get; set; }
       public virtual List<AsignacionAlumno> AsignacionAlumnos { get; set; }
    public virtual List<DetalleNota> DetalleNotas { get; set; }
    }
}