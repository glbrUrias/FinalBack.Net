using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using finalb2020.Models;
using finalb2020.Views;

namespace finalb2020.ModelsViews
{
    public class MainViewModel : INotifyPropertyChanged, ICommand
    {
        private string _ImgFoto= $"{Environment.CurrentDirectory}\\Images\\tim-foster-o4mP43oPGHk-unsplash.jpg";
        public string ImgFoto
        {
            get { return _ImgFoto; }
            set { _ImgFoto = value; }
        }
        
        private bool _IsMenuLogin=true;
        public bool IsMenuLogin
        {
            get { return _IsMenuLogin; }
            set { _IsMenuLogin = value; NotificarCambio("IsMenuLogin");}
        }
        
        private bool _IsMenuCatalogo = false;
        public bool IsMenuCatalogo
        {
            get { return _IsMenuCatalogo; }
            set { _IsMenuCatalogo = value; NotificarCambio("IsMenuCatalogo"); }
        }
        private Usuario _Usuario;
        public Usuario Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }

        private MainViewModel _Instancia;

        public MainViewModel Instancia
        {
            get
            {
                return this._Instancia;
            }
            set
            {
                this._Instancia = value;
                NotificarCambio("Instancia");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.Equals("LoginView"))
            {
                try
                {
                    LoginView view = new LoginView(this);
                    view.ShowDialog();
                    if (this.Usuario != null)
                    {

                        this.IsMenuCatalogo = true;
                        this.IsMenuLogin=false;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            if (parameter.Equals("AlumnoView"))
            {
                new AlumnoView().ShowDialog();
            }
            if (parameter.Equals("CarreraTecnicaView"))
            {
                new CarreraTecnicaView().ShowDialog();
            }
             if (parameter.Equals("HorarioView"))
            {
                new HorarioView().ShowDialog();
            }
            if (parameter.Equals("InstructorView"))
            {
                new InstructorView().ShowDialog();
            }
            if (parameter.Equals("ClaseView"))
            {
                new ClaseView().ShowDialog();
            }
            if (parameter.Equals("SalonView"))
            {
                new SalonView().ShowDialog();
            }
        }
        public MainViewModel()
        {
            this.Instancia = this;
        }
        public void NotificarCambio(string propiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }
        }
    }
}