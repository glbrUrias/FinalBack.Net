using Microsoft.EntityFrameworkCore;
using finalb2020.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace finalb2020.DataContext
{
    //ESTA CLASE ES LA ENCARGADA DE LLEVAR EL CONTROL DE DATOS CON SQL
    public class FinalDbContext : DbContext
    {
        public FinalDbContext(DbContextOptions<FinalDbContext> options):base(options)
        {

        }
        public DbSet<Alumno> Alumnos{get;set;}
        public DbSet<CarreraTecnica> CarreraTecnicas{get;set;}
        public DbSet<Horario> Horarios{get;set;}
        public DbSet<Salon> Salones{get;set;}
        public DbSet<Instructor> Instructores{get;set;}
        public DbSet<Clase> Clases{get;set;}
        public DbSet<Rol> RolesApp{get;set;}
        public DbSet<Usuario> UsuariosApp{get;set;}
        public DbSet<UsuarioRol> UsuariosRoles{get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioRol>().HasKey(x=> new{x.UsuarioId,x.RoleId});//
        }
        public FinalDbContext()
        {
            
        }
    }  
}