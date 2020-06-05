using System.Collections.Generic;

namespace finalb2020.Models
{
    public class Usuario
    {
        public int Id{get;set;}
        public string Username{get;set;}
        public bool Enabled{get;set;}
        public string Nombres{get;set;}
        public string Apellidos{get;set;}
        public string Email{get;set;}
        public List<UsuarioRol> UsuariosRoles{get; set;}
    }
}