namespace finalb2020.Models
{
    public class UsuarioRol
    {
        public int UsuarioId{get;set;}
        public int RoleId{get;set;}
        public Usuario Usuario{get;set;}
        public Rol Rol{get;set;}
    }
            
}