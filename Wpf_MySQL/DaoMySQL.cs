using System;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;

namespace NHJ
{
    public class DaoMySQL
    {
        MySqlConnection conexion;
        public bool Conectar(string server, string database, string user, string password)
        {
            string cadenaConexion = "server=" + server + ";database=" + database + ";uid=" + user + ";pwd=" + password + ";";
            try
            {
                conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();
                return true;
            }
            catch (MySqlException)
            {
                throw new Exception("Se ha producido un error al intentar estableces la conexión con la base de datos");
            }
        }

        public void Desconectar()
        {
            if (conexion != null)
            {
                try
                {
                    conexion.Close();
                }
                catch (MySqlException)
                {
                    
                    throw new Exception("No se pudo desconectar de la base de datos");
                }
            }
        }

        public bool Conectado()
        {
            try
            {
                if (conexion != null)
                {
                    return conexion.State == System.Data.ConnectionState.Open; 
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException)
            {
                
                throw new Exception("Fallo al comprobar el estado de la conexión");
            }
        }

        public ObservableCollection<Curso> Seleccionar(int? idCurso)
        {
            ObservableCollection<Curso> resultado = new ObservableCollection<Curso>();
            Curso unCurso;
            string cadena;
            if (idCurso != null)
            {
                cadena = "select idCurso, nombre, coste, descripcion, horas from curso where idCurso = " + idCurso + ";";
            }
            else
            {
                cadena = "select idCurso, nombre, coste, descripcion, horas from curso;";
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand(cadena, conexion);
                MySqlDataReader lector = cmd.ExecuteReader();
                while (lector.Read())
                {
                    unCurso = new Curso();
                    unCurso.IdCurso = int.Parse(lector["idCurso"].ToString());
                    unCurso.Nombre = lector["nombre"].ToString();
                    unCurso.Coste = int.Parse(lector["coste"].ToString());
                    unCurso.Descripcion = lector["descripcion"].ToString();
                    unCurso.Horas = int.Parse(lector["horas"].ToString());
                    resultado.Add(unCurso);
                }
                lector.Close();
                return resultado;
            }
            catch (MySqlException)
            {
                throw new Exception("No se pudo seleccionar la fila");
            }
        }

        public int Insertar(Curso unCurso)
        {
            try
            {
                string cadena;
                if (unCurso != null)
                {
                    cadena = "insert into curso values";
                    MySqlCommand cmd = new MySqlCommand(cadena, conexion);
                    return cmd.ExecuteNonQuery();
                }
                else
                {
                    return -1;
                }
            }
            catch (MySqlException)
            {
                
                throw;
            }
        }



        public int Borrar(Curso unCurso)
        {
            try
            {
                string cadena;
                if (unCurso != null)
                {
                    cadena = "delete from curso where idCurso = " + unCurso.IdCurso + ";";
                    MySqlCommand cmd = new MySqlCommand(cadena, conexion);
                    return cmd.ExecuteNonQuery();
                }
                else
                {
                    return -1;
                }
            }
            catch (MySqlException)
            {
                
                throw new Exception("No se pudo borrar la fila");
            }
        }
    }
}
