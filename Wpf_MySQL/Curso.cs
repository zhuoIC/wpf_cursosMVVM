
namespace NHJ
{
    public class Curso
    {
        int? _idCurso;
        string _nombre;
        int _coste;
        string _descripcion;
        int _horas;

        public Curso()
        {
            ;
        }

        public Curso(int? idCurso, string nombre, int coste, string descripcion, int horas)
        {
            _idCurso = idCurso;
            _nombre = nombre;
            _coste = coste;
            _descripcion = descripcion;
            _horas = horas;
        }

        public int? IdCurso
        {
            get { return _idCurso; }
            set { _idCurso = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public int Coste
        {
            get { return _coste; }
            set { _coste = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public int Horas
        {
            get { return _horas; }
            set { _horas = value; }
        }
    }
}
