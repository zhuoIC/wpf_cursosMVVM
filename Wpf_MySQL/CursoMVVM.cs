using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace NHJ
{
    public class CursoMVVM: INotifyPropertyChanged
    {
        DaoMySQL dao;
        ObservableCollection<Curso> listaCursos;
        Curso cursoSeleccionado;

        public Curso CursoSeleccionado
        {
            get { return cursoSeleccionado; }
            set
            {
                if (dao != null && cursoSeleccionado != value)
                {
                    cursoSeleccionado = value;
                }
                NotificarCambioPropiedad("CursoNoSeleccionado");
            }
        }
        public bool CursoNoSeleccionado
        {
            get { return cursoSeleccionado != null; }
        }

        public ObservableCollection<Curso> ListaCursos
        {
            get { return listaCursos; }
            set 
            {
                if (dao != null)
                {
                    listaCursos = value; 
                }
                else
                {
                    listaCursos = null;
                }
                NotificarCambioPropiedad("ListaCursos");
            }
        }
        string _mensaje = "<sin datos>";

        public bool Conectado
        {
            get 
            {
                if (dao != null)
                {
                    return dao.Conectado();
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Desconectado
        {
            get { return !Conectado; }
        }


        public string Mensaje
        {
          get { return _mensaje; }
          set 
          { 
            if (_mensaje != value)
	        {
		        _mensaje = value; 
                NotificarCambioPropiedad("Mensaje");
	        }
          }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotificarCambioPropiedad(string propiedad)
        {
            PropertyChangedEventHandler manejador = PropertyChanged;
            if (propiedad != null)
            {
                manejador(this, new PropertyChangedEventArgs(propiedad));
            }
        }

        public void Conectar()
        {
            try
            {
                if (dao == null)
                {
                    dao = new DaoMySQL();
                    dao.Conectar("localhost", "cursos", "root", "123");
                    Mensaje = "Conectado a la base de datos";
                    NotificarCambioPropiedad("Conectado");
                    NotificarCambioPropiedad("Desconectado");
                }
            }
            catch (Exception e)
            {
                Mensaje = e.Message;
            }
        }

        public void Desconectar()
        {
            try
            {
                if (dao != null)
                {
                    dao.Desconectar();
                    dao = null;
                    ListaCursos = null;
                    Mensaje = "Desconexion de la base de datos";
                    NotificarCambioPropiedad("Conectado");
                    NotificarCambioPropiedad("Desconectado");

                }
            }
            catch (Exception e)
            {

                Mensaje = e.Message;
            }
        }

        public void Listar()
        {
            if (dao != null)
            {
               ListaCursos = dao.Seleccionar(null);
            }
        }

        public void Borrar()
        {
            if (CursoSeleccionado != null)
            {
                if (dao.Borrar(CursoSeleccionado) == 1)
                {
                    Mensaje = "Curso borrado correctamente";
                }
                else
                {
                    Mensaje = "No se pudo borrar el curso seleccionado";
                }
            }
        }

        public ICommand Listar_Click
        {
            get
            {
                return new RelayComand(o => Listar(), o => true);
            }
        }

        public ICommand Conectar_Click
        {
            get
            {
                return new RelayComand(o => Conectar(), o => true);
            }
        }

        public ICommand Desconectar_Click
        {
            get
            {
                return new RelayComand(o => Desconectar(), o => true);
            }
        }

        public ICommand Borrar_Click
        {
            get
            {
                return new RelayComand(o => Borrar(), o => true);
            }
        }
    }
}
