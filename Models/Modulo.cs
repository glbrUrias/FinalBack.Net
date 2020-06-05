using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalb2020.Models
{
    public class Modulo
    {
        [Key]
        public int ModuloId{get;set;}

        [ForeignKey("CarreraTecnica")]//llave foranea
        public int CodigoCarrera { get; set; }
        // Objeto que representa la clave externa.
        [ForeignKey("CodigoCarrera")]
        public virtual CarreraTecnica CarreraTecnica{get;set;}//realacion a muchos
        //que viene de alumno y clase
        
        public string NombreModulo {get;set;}
        public int NumeroSeminarios{get;set;}
        public virtual List<Seminario> Seminarios{get;set;}
    }
}