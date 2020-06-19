using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace finalb2020.Models
{
    public class CarreraTecnica
    {
        [Key]
        public int CarreraId { get; set; }
        public string Nombre { get; set; }
        public virtual List<Clase> Clases { get; set; }//se pone cuando es la relacion: un carrera pueden tener
        //muchas clases, y una clase puede tener muchos carreras
         public virtual List<Modulo> Modulos { get; set; }//se pone cuando es la relacion: un carrera pueden tener
        //muchas clases, y una clase puede tener muchos carreras
    }
}