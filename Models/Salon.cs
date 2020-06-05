using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace finalb2020.Models
{
    public class Salon
    {
        [Key]
        public int SalonId { get; set; }
        public string NombreSalon { get; set; }
        public string Descripcion { get; set; }
        public int Capacidad { get; set; }
        public virtual List<Clase> Clases { get; set; }//se pone cuando es la relacion: un salon pueden tener
        //muchas clases, y una clase puede tener muchos salones
    }
}