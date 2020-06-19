using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using MahApps.Metro.Controls.Dialogs;
using finalb2020.DataContext;
using finalb2020.Models;

namespace finalb2020.ModelsViews
{
    enum ACCIONCLASE
    {
        NINGUNO,
        NUEVO,
        MODIFICAR
    }
    public class ClaseViewModel : INotifyPropertyChanged, ICommand
    {
         private ACCIONCLASE _accion = ACCIONCLASE.NINGUNO;
        public FinalDbContext dbContext;

        public ClaseViewModel _Instancia;
        //private IDialogCoordinator dialogCoordinator;
        
        private IDialogCoordinator dialogCoordinator;

        public bool _IsGuardar = false;
        public bool _IsCancelar =false;
        public bool _IsNuevo = true;
        public bool _IsModificar = true;
        public bool _IsEliminar = true;

        public int _Posicion;
        //SIRVE PARA GUARDAR LA POSICION DEL ELEMENTO QUE SE SELECCIONAR PARA MODIFICAR
        //LA VA IR A TRAER A LA LISTA DE ALUMNOS
        public int Posicion
        {
            get
            {
                return _Posicion;
            }
            set
            {
                _Posicion = value;
                NotificarCambio("IsReadOnlyApellidos");
            }
        }
        private Clase _Update;
        public Clase Update
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
        private bool _IsReadOnlyCarne=true;
        public bool IsReadOnlyCarne
        {
            get
            {
                return _IsReadOnlyCarne;
            }
            set
            {
                _IsReadOnlyCarne = value;
            }
        }
        public bool _IsReadOnlyApellidos=true;
        public bool IsReadOnlyApellidos
        {
            get
            {
                return this._IsReadOnlyApellidos;
            }
            set
            {
                this._IsReadOnlyApellidos = value;
                NotificarCambio("IsReadOnlyApellidos");
            }
        }
        private bool _IsReadOnlyNombres=true;
        public bool IsReadOnlyNombres
        {
            get
            {
                return this._IsReadOnlyNombres;
            }
            set
            {
                this._IsReadOnlyNombres = value;
                NotificarCambio("IsReadOnlyNombres");
            }
        }
        
        public bool _IsReadOnlyNoExpediente =true;
        public bool IsReadOnlyNoExpediente
        {
            get
            {
                return this._IsReadOnlyNoExpediente;
            }
            set
            {
                this._IsReadOnlyNoExpediente = value;
                NotificarCambio("IsReadOnlyNoExpediente");
            }
        }

        public bool _IsReadOnlyEmail =true;
        public bool IsReadOnlyEmail
        {
            get
            {
                return this._IsReadOnlyEmail;
            }
            set
            {
                this._IsReadOnlyEmail = value;
                NotificarCambio("IsReadOnlyEmail");
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
        public ClaseViewModel Instancia
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
        private Clase _ElementoSeleccionado;

        public Clase ElementoSeleccionado
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
        private ObservableCollection<Clase> _ListaClase;

        public ObservableCollection<Clase> ListaClase
        {
            get
            {
                if (_ListaClase == null)
                {
                    _ListaClase = new ObservableCollection<Clase>(dbContext.Clases.ToList()); // select * from Alumnos
                }
                return _ListaClase;

            }
            set
            {
                _ListaClase = value;
            }
        }

        public ClaseViewModel(IDialogCoordinator instance)
        {
            this.dialogCoordinator = instance;
            this.dbContext = new FinalDbContext();
            this.Instancia = this;
        }
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public bool CanExecute(object parameter)//CUIDADO SI NO ABRE ESTA LA VENTANA ES PORQUE AQUI NO SE LE PUSO TRUE
        {
            return true;
        }

        public async void Execute(object parametro)//aqui hay un problema se copio codigo del profe para que jalara
        //porque con el codigo copidado del video "3era parte", no jalaba
        {
            if (parametro.Equals("Nuevo"))
            {
                this._accion = ACCIONCLASE.NUEVO;
               // UpOffTxt();
                this.ElementoSeleccionado = new Clase();
                UpOffBoton();
                
            }
            else if (parametro.Equals("Modificar"))
            {
                if (this.ElementoSeleccionado != null)
                {
                    this._accion = ACCIONCLASE.MODIFICAR;
                    UpOffBoton();
                   // UpOffTxt();
                    this.Posicion = this.ListaClase.IndexOf(this.ElementoSeleccionado);
                    this.Update = new Clase();
                    this.Update.Ciclo = this.ElementoSeleccionado.Ciclo;
                    this.Update.CupoMaximo = this.ElementoSeleccionado.CupoMaximo;
                    this.Update.CupoMinimo = this.ElementoSeleccionado.CupoMinimo;
                    this.Update.Descripcion = this.ElementoSeleccionado.Descripcion;
                    this.Update.CarreraId = this.ElementoSeleccionado.CarreraId;
                    this.Update.HorarioId = this.ElementoSeleccionado.HorarioId;
                    this.Update.InstructorId = this.ElementoSeleccionado.InstructorId;
                    this.Update.SalonId = this.ElementoSeleccionado.SalonId;
                }
                else
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,"Clases","Seleccione un elemento para Modificar.");
                }
            }
            else if (parametro.Equals("Guardar"))
            {

                switch (this._accion)
                {
                    case ACCIONCLASE.NUEVO:
                        try
                        {
                            
                            this.dbContext.Clases.Add(this.ElementoSeleccionado); // insert into Alumno values(...)
                            this.dbContext.SaveChanges();

                            this.ListaClase.Add(this.ElementoSeleccionado);
                            await this.dialogCoordinator.ShowMessageAsync(this,"Clases","Datos Almacenados!!!");
                            this._accion = ACCIONCLASE.NINGUNO;
                            UpOffBoton();
                           // UpOffTxt();   
                        }
                        catch (Exception e)
                        {
                            await this.dialogCoordinator.ShowMessageAsync(this,"Clases",e.Message);
                            this._accion = ACCIONCLASE.NINGUNO;
                            UpOffBoton();
                           // UpOffTxt();
                        }
                        break;
                    case ACCIONCLASE.MODIFICAR:
                        if (this.ElementoSeleccionado != null)
                        {

                            this.dbContext.Entry(this.ElementoSeleccionado).State = EntityState.Modified;
                            this.dbContext.SaveChanges();
                            await this.dialogCoordinator.ShowMessageAsync(this,"Clases","Datos Actualizados!!!");
                            this._accion = ACCIONCLASE.NINGUNO;
                            UpOffBoton();
                           // UpOffTxt();
                        }
                        else
                        {
                            await this.dialogCoordinator.ShowMessageAsync(this,"Clases","Debe Seleccionar Un Elemento");
                            this._accion = ACCIONCLASE.NINGUNO;
                            UpOffBoton();
                           // UpOffTxt();
                        }
                        break;
                }
            }
            else if (parametro.Equals("Cancelar"))
            {
                if (this._accion == ACCIONCLASE.MODIFICAR)
                {
                    this.ListaClase.RemoveAt(this.Posicion);
                    ListaClase.Insert(this.Posicion, this.Update);
                }
                ElementoSeleccionado=null;
                this._accion = ACCIONCLASE.NINGUNO;
                UpOffBoton();
               // UpOffTxt();
            }
            else if (parametro.Equals("Eliminar"))
            {
                if (this.ElementoSeleccionado != null)
                {
                    MessageDialogResult resultado = await this.dialogCoordinator.ShowMessageAsync(this,
                    "Eliminar Clase",
                    "Esta Seguro de eliminar el Registro?",
                    MessageDialogStyle.AffirmativeAndNegative);

                    if (resultado == MessageDialogResult.Affirmative)
                    {
                        this.dbContext.Remove(this.ElementoSeleccionado);
                        this.dbContext.SaveChanges();
                        this.ListaClase.Remove(this.ElementoSeleccionado);
                        await this.dialogCoordinator.ShowMessageAsync(this,"Clases","Registro eliminado.");
                        this._accion = ACCIONCLASE.NINGUNO;
                        UpOffBoton();
                        //UpOffTxt();
                    }
                    /*MessageBoxResult resultado = MessageBox.Show("Realmente desea eleminiar el registro",
                    "Eliminar", MessageBoxButton.YesNo);
                    if (resultado == MessageBoxResult.Yes)
                    {
                    }
                    */
                }
                else
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,"Clases","Seleccione un elemento.");
                    this._accion = ACCIONCLASE.NINGUNO;
                    UpOffBoton();
                    //UpOffTxt();
                }
            }

        }

        public void NotificarCambio(String propiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }

        }
        public void UpOffBoton()
        {
            switch (this._accion)
            {
                case ACCIONCLASE.NINGUNO:
                    this.IsGuardar = false;
                    this.IsCancelar = false;
                    this.IsNuevo = true;
                    this.IsModificar = true;
                    this.IsEliminar = true;
                    break;

                case ACCIONCLASE.NUEVO:
                    this.IsNuevo = false;
                    this.IsEliminar = false;
                    this.IsModificar = false;
                    this.IsGuardar = true;
                    this.IsCancelar = true;
                    break;

                case ACCIONCLASE.MODIFICAR:
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