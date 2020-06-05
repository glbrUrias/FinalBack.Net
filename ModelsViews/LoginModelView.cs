using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using finalb2020.DataContext;
using finalb2020.Models;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace finalb2020.ModelsViews
{
    public class LoginModelView : INotifyPropertyChanged, ICommand
    {
        private IDialogCoordinator _DialogCoordinator;
        private FinalDbContext _DbContext;
        private MainViewModel _MainViewModel;
        public MainViewModel MainViewModel
        {
            get { return _MainViewModel; }
            set { _MainViewModel = value; }
        }
        
        private Usuario _Usuario;
        private Usuario Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; NotificarCambio("Usuario"); }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; NotificarCambio("Password"); }
        
        }

        private string _Username;
        public string Username
        {
            get { return _Username; }
            set { _Username = value; NotificarCambio("Username");}
        }
        
        private LoginModelView _Instancia;
        public LoginModelView Instancia
        {
            get { return _Instancia; }
            set { _Instancia = value; NotificarCambio("Instancia");}
        }
        
        public LoginModelView(IDialogCoordinator instance, MainViewModel mainViewModel){
            
            this.MainViewModel=mainViewModel;
            this._DialogCoordinator=instance;
            this.Instancia=this;
            this._DbContext=new FinalDbContext();
        }
        
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public bool CanExecute(object parameter)//CUIDADO SI NO ABRE ESTA LA VENTANA ES PORQUE AQUI NO SE LE PUSO TRUE
        {
            return true;
        }

        public async void Execute(object parametro)
        {
            if(parametro is Window)
            {
                Password =((PasswordBox)((Window)parametro).FindName("txtPassword")).Password;
                
                var UsernameParameter=new SqlParameter("@Username",Username);
                var PasswordParameter=new SqlParameter("@Password",Password);
               
                var Resultado = this._DbContext.UsuariosApp
                .FromSqlRaw("sp_AutenticarUsuario @Username, @Password",
                UsernameParameter,PasswordParameter).ToList();
                foreach(Object objeto in Resultado)
                {
                    this.Usuario =(Usuario)objeto;
                    
                }
                if(this.Usuario!=null)
                {
                    await this._DialogCoordinator.ShowMessageAsync(this,"Login",$"Bienvenido {_Usuario.Nombres + " "}{_Usuario.Apellidos}");
                    this.MainViewModel.IsMenuCatalogo=true;
                    this.MainViewModel.Usuario=this.Usuario;
                    ((Window)parametro).Close(); 
                    
                }
                else
                {
                    MessageBox.Show("Usuario Incorrecto");
                }
                
            }
        }

        public void NotificarCambio(string propiedad)
    {
        if(this.PropertyChanged !=null)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
        }
    }
    }
   
}