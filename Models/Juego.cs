namespace TP10_PreguntadORT.Models
{
    public class Juego
    {
        public string Username { get; private set; }
        public int PuntajeActual { get; private set; }
        public int CantidadPreguntasCorrectas { get; private set; }
        public int ContadorNroPreguntaActual { get; private set; }
        public Pregunta PreguntaActual { get; private set; }
        public List<Pregunta> ListaPreguntas { get; private set; } 
        public List<Respuesta> ListaRespuestas { get; private set; } 

        private void InicializarJuego()
        {
            Username = "";
            PuntajeActual = 0;
            CantidadPreguntasCorrectas = 0;
            ContadorNroPreguntaActual = 0;
            PreguntaActual = null;
            ListaPreguntas.Clear();
            ListaRespuestas.Clear();
        }
        public List<Categoria> ObtenerCategorias()
        {
            return BD.ObtenerCategorias();
        }

        public void CargarPartida(string username, int categoria)
        {
            InicializarJuego();
            Username = username;
            ListaPreguntas = BD.ObtenerPreguntas(categoria);
            if (ListaPreguntas.Count > 0) 
            {
                PreguntaActual = ListaPreguntas[0];
            }
        }

        public Pregunta ObtenerProximaPregunta()
        {
            if (ContadorNroPreguntaActual >= ListaPreguntas.Count) 
            {
                return null;
            }
            PreguntaActual = ListaPreguntas[ContadorNroPreguntaActual];
            return PreguntaActual;
        }

        public List<Respuesta> ObtenerProximasRespuestas(int idPregunta)
        {
            ListaRespuestas = BD.ObtenerRespuestas(idPregunta);
            return ListaRespuestas;
        }

        public bool VerificarRespuesta(int idRespuesta)
        {
            bool laHizoCorrecta = false;
              foreach (Respuesta respuesta in ListaRespuestas)
    {
        if (respuesta.IdRespuesta == idRespuesta)
        {
            laHizoCorrecta = respuesta.Correcta;               
                    
        }
    }
            if (laHizoCorrecta == true)
            {
                PuntajeActual += 100;
                CantidadPreguntasCorrectas++;
            }
            ContadorNroPreguntaActual++;
            if (ContadorNroPreguntaActual < ListaPreguntas.Count)
                PreguntaActual = ListaPreguntas[ContadorNroPreguntaActual];
            else
                PreguntaActual = null;
            return laHizoCorrecta;
        }
    }
}