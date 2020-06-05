using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace finalb2020.Models
{
    public class Instructor
    {
        [Key]
        public int InstructorId{get;set;}
        public string Apellidos{get;set;}
        public string Nombres{get;set;}
        public string Direccion{get;set;}
        public string Telefono{get;set;}
        public string Comentario{get;set;}
        public string Estatus{get;set;}
        public string Foto{get;set;}
         public virtual List<Clase> Clases{get;set;}//se pone cuando es la relacion: un horario pueden tener
        //muchas clases, y una clase puede tener muchos horarios
    }
}