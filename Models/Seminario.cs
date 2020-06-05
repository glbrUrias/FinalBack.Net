using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalb2020.Models
{
    public class Seminario
    {
        [Key]
        public int SeminarioId{get;set;}

        [ForeignKey("Modulo")]//llave foranea
        public int ModuloId { get; set; }
        // Objeto que representa la clave externa.
        [ForeignKey("ModuloId")]
        public virtual Modulo Modulo { get; set; }//porque la tabla AsignacionAlumno tiene relacion a muchos 
        //que viene de alumno y clase

        public string NombreSeminario{get;set;}
        public DateTime FechaInicio {get;set;}
        public DateTime FechaFin {get;set;}
        public virtual List<DetalleActividad> DetalleActividades{get;set;}

    }
}