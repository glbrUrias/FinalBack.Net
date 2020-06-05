using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalb2020.Models
{
    public class AsignacionAlumno
    {
        [Key]
        public int AsignacionId { get; set; }
        public DateTime FechaAsignacion { get; set; }
        // Clave Externa
        [ForeignKey("Alumno")]
        public int Carne { get; set; }
        // Objeto que representa la clave externa.
        [ForeignKey("Carne")]
        public virtual Alumno Alumno { get; set; }//porque la tabla AsignacionAlumno tiene relacion a muchos 
        //que viene de alumno y clase


        [ForeignKey("Clase")]
        public int ClaseId { get; set; }
        // Objeto que representa la clave externa.
        [ForeignKey("ClaseId")]
        public virtual Clase Clase { get; set; }//porque la tabla AsignacionAlumno tiene relacion a muchos 
        //que viene de alumno y clase
        

    }
}