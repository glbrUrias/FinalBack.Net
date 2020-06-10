using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using finalb2020.DataContext;
using finalb2020.Models;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;

namespace finalb2020.ModelsViews
{
    public class InstructorViewModel : INotifyPropertyChanged, ICommand
    {
        public bool _IsGuardar = false;
        public bool _IsCancelar =false;
        public bool _IsNuevo = true;
        public bool _IsModificar = true;
        public bool _IsEliminar = true;
        private ACCION _accion =ACCION.NINGUNO;
        private IDialogCoordinator dialogCoordinator;
        private FinalDbContext dbContext;
      
        private InstructorViewModel _Instancia;

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
        private Instructor _Update;
        public Instructor Update
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
        
        public InstructorViewModel Instancia
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
        private Instructor _ElementoSeleccionado;

        public Instructor ElementoSeleccionado
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
        private ObservableCollection<Instructor> _ListaInstructor;

        public ObservableCollection<Instructor> ListaInstructor
        {
            get
            {
                if(_ListaInstructor == null){
                    _ListaInstructor = new ObservableCollection<Instructor>(dbContext.Instructores.ToList()); // select * from Alumnos
                }
                return _ListaInstructor;

            }
            set
            {
                _ListaInstructor = value;
            }
        }

        public InstructorViewModel(IDialogCoordinator instance)
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
                this._accion = ACCION.NUEVO;
                this.ElementoSeleccionado = new Instructor();
                UpOffBoton();
            }
            else if (parametro.Equals("Modificar"))
            {
                if (this.ElementoSeleccionado != null)
                {
                    this._accion = ACCION.MODIFICAR;
                    UpOffBoton();

                    this.Posicion = this.ListaInstructor.IndexOf(this.ElementoSeleccionado);
                    this.Update = new Instructor();
                    this.Update.Apellidos = this.ElementoSeleccionado.Apellidos;
                    this.Update.Nombres = this.ElementoSeleccionado.Nombres;
                    this.Update.Direccion = this.ElementoSeleccionado.Direccion;
                    this.Update.Telefono = this.ElementoSeleccionado.Telefono;
                    this.Update.Comentario = this.ElementoSeleccionado.Comentario;
                    this.Update.Estatus = this.ElementoSeleccionado.Estatus;
                    this.Update.Foto = this.ElementoSeleccionado.Foto;

                }
                else
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,"Instructor","Seleccione un elemento para Modificar");
                }
            }
            else if (parametro.Equals("Guardar"))
            {
               switch (this._accion)
                {
                    case ACCION.NUEVO:
                        try
                        {
                            this.dbContext.Instructores.Add(this.ElementoSeleccionado); // insert into Alumno values(...)
                            this.dbContext.SaveChanges();

                            this.ListaInstructor.Add(this.ElementoSeleccionado);
                            await this.dialogCoordinator.ShowMessageAsync(this,"Instructor","Datos almacenados!!!");
                            this._accion=ACCION.NINGUNO;
                            UpOffBoton();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                            //await this.dialogCoordinator.ShowMessageAsync(this,"Carrera Tecnica",e.Message);
                            this._accion=ACCION.NINGUNO;
                            UpOffBoton();
                        }
                        break;
                    case ACCION.MODIFICAR:
                        if (this.ElementoSeleccionado != null)
                        {

                            this.dbContext.Entry(this.ElementoSeleccionado).State = EntityState.Modified;
                            this.dbContext.SaveChanges();
                            await this.dialogCoordinator.ShowMessageAsync(this,"Instructor","Datos Actualizados!!!");
                            this._accion=ACCION.NINGUNO;
                            UpOffBoton();
                        }
                        else
                        {
                            await this.dialogCoordinator.ShowMessageAsync(this,"Instructor","Debe Seleccionar Un Elemento");
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
                    this.ListaInstructor.RemoveAt(this.Posicion);
                    ListaInstructor.Insert(this.Posicion, this.Update);
                }
                this._accion = ACCION.NINGUNO;
                UpOffBoton();
            }
            else if (parametro.Equals("Eliminar"))
            {
                if (this.ElementoSeleccionado != null)
                {
                     MessageDialogResult resultado = await this.dialogCoordinator.ShowMessageAsync(this,
                    "Eliminar Instructor",
                    "Esta Seguro de eliminar el Registro?",
                    MessageDialogStyle.AffirmativeAndNegative);

                    if (resultado == MessageDialogResult.Affirmative)
                    {
                        this.dbContext.Remove(this.ElementoSeleccionado);
                        this.dbContext.SaveChanges();
                        this.ListaInstructor.Remove(this.ElementoSeleccionado);
                        await this.dialogCoordinator.ShowMessageAsync(this,"Instructor","Registro eliminado.");
                        this._accion = ACCION.NINGUNO;
                        UpOffBoton();

                    }
                }
                else
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,"Instructor","Seleccione un elemento.");
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