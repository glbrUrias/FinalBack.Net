using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using finalb2020.DataContext;
using finalb2020.Models;

namespace finalb2020.ModelsViews
{
    public class HorarioViewModel : INotifyPropertyChanged, ICommand
    {
        private FinalDbContext dbContext;
       
        private HorarioViewModel _Instancia;

        public HorarioViewModel Instancia
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
        private Horario _ElementoSeleccionado;

        public Horario ElementoSeleccionado
        {
            get
            {
                return this._ElementoSeleccionado;
            }
            set
            {
                this._ElementoSeleccionado = value;
                NotificarCambio("ElementoSeleccionado");                
            }
        }
        private ObservableCollection<Horario> _ListaHorario;

        public ObservableCollection<Horario> ListaHorario 
        {
            get
            {
                if(_ListaHorario == null){
                    _ListaHorario = new ObservableCollection<Horario>(dbContext.Horarios.ToList()); // select * from Alumnos
                }
                return _ListaHorario;

            }
            set
            {
                _ListaHorario = value;
            }
        }

        public HorarioViewModel()
        {
            this.dbContext = new FinalDbContext();
            this.Instancia = this;
        }
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public bool CanExecute(object parameter)//CUIDADO SI NO ABRE ESTA LA VENTANA ES PORQUE AQUI NO SE LE PUSO TRUE
        {
            return true;
        }

        public void Execute(object parametro)//aqui hay un problema se copio codigo del profe para que jalara
        //porque con el codigo copidado del video "3era parte", no jalaba
        {
              if(parametro.Equals("Nuevo"))
              {
                  this.ElementoSeleccionado = new Horario();
              } else if( parametro.Equals("Guardar")) {
                  try
                  { 
                      //Religion r =  this.dbContext.Religiones.Find(1); // Select * from Religiones where ReligionId = 1                      
                      //this.ElementoSeleccionado.Religion = r;                      
                      this.dbContext.Horarios.Add(this.ElementoSeleccionado); // insert into Alumno values(...)
                      this.dbContext.SaveChanges();
                      
                      this.ListaHorario.Add(this.ElementoSeleccionado);                                            
                      MessageBox.Show("Datos almacenados!!!");
                  }catch(Exception e)
                  {
                      MessageBox.Show(e.Message);
                  }
              }          
        }

        public void NotificarCambio(String propiedad)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }

        }
    }
}