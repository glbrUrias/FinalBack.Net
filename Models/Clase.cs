using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalb2020.Models
{
    public class Clase
    {
        [Key]
        public int ClaseId { get; set; }
        public string Descripcion { get; set; }
        public int Ciclo { get; set; }

        [ForeignKey("CarreraTecnica")]
        public int CarreraId { get; set; }
        // Objeto que representa la clave externa.
        [ForeignKey("CarreraId")]
        public virtual CarreraTecnica CarreraTecnica { get; set; }//porque la tabla AsignacionAlumno tiene relacion a muchos 
        //que viene de alumno y clase


        [ForeignKey("Salon")]//llave foranea
        public int SalonId { get; set; }
        // Objeto que representa la clave externa.
        [ForeignKey("SalonId")]
        public virtual Salon Salon { get; set; }//porque la tabla AsignacionAlumno tiene relacion a muchos 
        //que viene de alumno y clase


        [ForeignKey("Horario")]//llave foranea
        public int HorarioId { get; set; }
        // Objeto que representa la clave externa.
        [ForeignKey("HorarioId")]
        public virtual Horario Horario { get; set; }//porque la tabla AsignacionAlumno tiene relacion a muchos 
        //que viene de alumno y clase


        [ForeignKey("Instructor")]//llave foranea
        public int InstructorId { get; set; }
        // Objeto que representa la clave externa.
        [ForeignKey("InstructorId")]
        public virtual Instructor Instructor { get; set; }//porque la tabla AsignacionAlumno tiene relacion a muchos 
        //que viene de alumno y clase

        public int CupoMinimo { get; set; }
        public int CupoMaximo{get;set;}
        
        public virtual List<AsignacionAlumno> AsignacionAlumnos{get;set;}//se coloca asi porque
        //una clase tiene muchas asigancioAlumno
    }
}