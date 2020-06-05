using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalb2020.Models
{
    public class DetalleNota
    {
        [Key]
        public int DetalleNotaId{get;set;}


        [ForeignKey("DetalleActividad")]//llave foranea
        public int DetalleActividadId { get; set; }
        // Objeto que representa la clave externa.
        [ForeignKey("DetalleActividadId")]
        public virtual DetalleActividad DetalleActividad { get; set; }//porque la tabla AsignacionAlumno tiene relacion a muchos 
        //que viene de alumno y clase



        [ForeignKey("Alumno")]//llave foranea
        public int Carne { get; set; }
        // Objeto que representa la clave externa.
        [ForeignKey("Carne")]
        public virtual Alumno Alumno { get; set; }//porque la tabla AsignacionAlumno tiene relacion a muchos 
        //que viene de alumno y clase
        public int ValorNota {get;set;}

    }
}