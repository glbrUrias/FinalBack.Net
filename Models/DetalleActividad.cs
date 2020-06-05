using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalb2020.Models
{
    public class DetalleActividad
    {
        [Key]
        public int DetalleActividadId{get;set;}

        [ForeignKey("Seminario")]//llave foranea
        public int SeminarioId { get; set; }
        // Objeto que representa la clave externa.
        [ForeignKey("SeminarioId")]
        public virtual Seminario Seminario { get; set; }//porque la tabla AsignacionAlumno tiene relacion a muchos 
        //que viene de alumno y clase

        public string NombreActividad {get;set;}
        public int NotaActividad{get;set;}
        public DateTime FechaCreacion {get;set;}
        public DateTime FechaEntrega {get;set;}
        public DateTime FechaPostergacion{get;set;}
        public string Estado{get;set;}
        public virtual List<DetalleNota> DetalleNotas{get;set;}

    }
}