using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace finalb2020.Models
{
    public class Horario
    {
        [Key]
        public int HorarioId{get;set;}
        public DateTime HorarioInicio{get;set;}
        public DateTime HorarioFinal{get;set;}
        public virtual List<Clase> Clases{get;set;}//se pone cuando es la relacion: un horario pueden tener
        //muchas clases, y una clase puede tener muchos horarios
        
    }
}