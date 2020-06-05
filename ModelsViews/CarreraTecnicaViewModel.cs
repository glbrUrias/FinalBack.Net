using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using finalb2020.DataContext;
using finalb2020.Models;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace finalb2020.ModelsViews
{

    public class CarreraTecnicaViewModel : INotifyPropertyChanged, ICommand
    {
        private ACCION _accion =ACCION.NINGUNO;
        private FinalDbContext dbContext;

        private CarreraTecnicaViewModel _Instancia;
        public bool _IsGuardar = false;
        public bool _IsCancelar =false;
        public bool _IsNuevo = true;
        public bool _IsModificar = true;
        public bool _IsEliminar = true;
        public bool _IsReadOnlyCarrera=true;
        public int _Posicion;

        public int Posicion
        {
            get
            {
                return _Posicion;
            }
            set
            {
                _Posicion = value;
                NotificarCambio("IsReadOnlyCarrera");
            }
        }
        private CarreraTecnica _Update;
        public CarreraTecnica Update
        {
            get
            {
                return _Update;
            }
            set
            {
                _Update = value;
            }
        }
        
        public bool IsReadOnlyCarrera
        {
            get
            {
                return this._IsReadOnlyCarrera;
            }
            set
            {
                this._IsReadOnlyCarrera = value;
                NotificarCambio("IsReadOnlyCarrera");
            }
        }
        public bool IsEliminar
        {
            get
            {
                return this._IsEliminar;
            }
            set
            {
                this._IsEliminar = value;
                NotificarCambio("IsEliminar");
            }
        }
        public bool IsModificar
        {
            get
            {
                return this._IsModificar;
            }
            set
            {
                this._IsModificar = value;
                NotificarCambio("IsModificar");
            }
        }
        public bool IsNuevo
        {
            get
            {
                return this._IsNuevo;
            }
            set
            {
                this._IsNuevo = value;
                NotificarCambio("IsNuevo");
            }
        }
        public bool IsCancelar
        {
            get
            {
                return this._IsCancelar;
            }
            set
            {
                this._IsCancelar = value;
                NotificarCambio("IsCancelar");
            }
        }
        public bool IsGuardar
        {
            get
            {
                return this._IsGuardar;
            }
            set
            {
                this._IsGuardar = value;
                NotificarCambio("IsGuardar");
            }
        }
        public CarreraTecnicaViewModel Instancia
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
        private CarreraTecnica _ElementoSeleccionado;

        public CarreraTecnica ElementoSeleccionado
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
        private ObservableCollection<CarreraTecnica> _ListaCarreraTecnica;

        public ObservableCollection<CarreraTecnica> ListaCarreraTecnica
        {
            get
            {
                if(_ListaCarreraTecnica == null){
                    _ListaCarreraTecnica = new ObservableCollection<CarreraTecnica>(dbContext.CarreraTecnicas.ToList()); // select * from Alumnos
                }
                return _ListaCarreraTecnica;

            }
            set
            {
                _ListaCarreraTecnica = value;
            }
        }

        public CarreraTecnicaViewModel()
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
            if (parametro.Equals("Nuevo"))
            {
                this._accion = ACCION.NUEVO;
                this.ElementoSeleccionado = new CarreraTecnica();
                UpOffBoton();
            }
            else if (parametro.Equals("Modificar"))
            {
                if (this.ElementoSeleccionado != null)
                {
                    this._accion = ACCION.MODIFICAR;
                    UpOffBoton();

                    this.Posicion = this.ListaCarreraTecnica.IndexOf(this.ElementoSeleccionado);
                    this.Update = new CarreraTecnica();
                    this.Update.Nombre = this.ElementoSeleccionado.Nombre;
                }
                else
                {
                    MessageBox.Show("Seleccione un elemento para Modificar.");
                }
            }
            else if (parametro.Equals("Guardar"))
            {
               switch (this._accion)
                {
                    case ACCION.NUEVO:
                        try
                        {
                            this.dbContext.CarreraTecnicas.Add(this.ElementoSeleccionado); // insert into Alumno values(...)
                            this.dbContext.SaveChanges();

                            this.ListaCarreraTecnica.Add(this.ElementoSeleccionado);
                            MessageBox.Show("Datos almacenados!!!");
                            this._accion=ACCION.NINGUNO;
                            UpOffBoton();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                            this._accion=ACCION.NINGUNO;
                            UpOffBoton();
                        }
                        break;
                    case ACCION.MODIFICAR:
                        if (this.ElementoSeleccionado != null)
                        {

                            this.dbContext.Entry(this.ElementoSeleccionado).State = EntityState.Modified;
                            this.dbContext.SaveChanges();
                            MessageBox.Show("Datos Actualizados!!!");
                            this._accion=ACCION.NINGUNO;
                            UpOffBoton();
                        }
                        else
                        {
                            MessageBox.Show("Debe Seleccionar Un Elemento.");
                            this._accion=ACCION.NINGUNO;
                            UpOffBoton();
                        }
                        break;
                }
            }
            else if (parametro.Equals("Cancelar"))
            {
                if (this._accion == ACCION.MODIFICAR)
                {
                    this.ListaCarreraTecnica.RemoveAt(this.Posicion);
                    ListaCarreraTecnica.Insert(this.Posicion, this.Update);
                }
                this._accion = ACCION.NINGUNO;
                UpOffBoton();
            }
            else if (parametro.Equals("Eliminar"))
            {
                if (this.ElementoSeleccionado != null)
                {
                    MessageBoxResult resultado = MessageBox.Show("Realmente desea eleminiar el registro",
                    "Eliminar", MessageBoxButton.YesNo);
                    if (resultado == MessageBoxResult.Yes)
                    {
                        this.dbContext.Remove(this.ElementoSeleccionado);
                        this.dbContext.SaveChanges();
                        this.ListaCarreraTecnica.Remove(this.ElementoSeleccionado);
                        MessageBox.Show("Elemento eliminado");
                        this._accion = ACCION.NINGUNO;
                        UpOffBoton();

                    }
                }
                else
                {
                    MessageBox.Show("Debe selleccionar un elemento");
                    this._accion = ACCION.NINGUNO;
                    UpOffBoton();
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
        public void UpOffBoton()
        {
            switch (this._accion)
            {
                case ACCION.NINGUNO:
                    this.IsGuardar = false;
                    this.IsCancelar = false;
                    this.IsNuevo = true;
                    this.IsModificar = true;
                    this.IsEliminar = true;
                    break;

                case ACCION.NUEVO:
                    this.IsNuevo = false;
                    this.IsEliminar = false;
                    this.IsModificar = false;
                    this.IsGuardar = true;
                    this.IsCancelar = true;
                    break;

                case ACCION.MODIFICAR:
                    this.IsNuevo = false;
                    this.IsEliminar = false;
                    this.IsModificar = false;
                    this.IsGuardar = true;
                    this.IsCancelar = true;
                    break;
            }
        }
    }
}