namespace TP10_PreguntadORT.Models
{
    public static class Juego
    {
       
        public static string Username { get; private set; } = "";
        public static int PuntajeActual { get; private set; }
        public static int CantidadPreguntasCorrectas { get; private set; }
        public static int ContadorNroPreguntaActual { get; private set; }
        public static Pregunta PreguntaActual { get; private set; }
        public static List<Pregunta> ListaPreguntas { get; private set; } = new List<Pregunta>();
        public static List<Respuesta> ListaRespuestas { get; private set; } = new List<Respuesta>();

       
        private static void InicializarJuego()
        {
            Username = "";
            PuntajeActual = 0;
            CantidadPreguntasCorrectas = 0;
            ContadorNroPreguntaActual = 0;
            PreguntaActual = null;
            ListaRespuestas = new List<Respuesta>();
        }

        public static List<Categoria> ObtenerCategorias()
        {
            return BD.ObtenerCategorias();
        }

        public static void CargarPartida(string username, int categoria)
        {
            InicializarJuego();
            Username = username;
            ListaPreguntas = BD.ObtenerPreguntas(categoria);

            if (ListaPreguntas != null && ListaPreguntas.Count > 0)
            {
                PreguntaActual = ListaPreguntas[0];
            }
        }

        public static Pregunta ObtenerProximaPregunta()
        {
            if (ListaPreguntas == null || ContadorNroPreguntaActual >= ListaPreguntas.Count)
            {
                return null;
            }

            PreguntaActual = ListaPreguntas[ContadorNroPreguntaActual];
            return PreguntaActual;
        }

        public static List<Respuesta> ObtenerProximasRespuestas(int idPregunta)
        {
            ListaRespuestas = BD.ObtenerRespuestas(idPregunta);
            return ListaRespuestas;
        }

        public static bool VerificarRespuesta(int idRespuesta)
        {
            bool laHizoCorrecta = false;

            if (ListaRespuestas != null)
            {
                foreach (Respuesta respuesta in ListaRespuestas)
                {
                    if (respuesta.IdRespuesta == idRespuesta)
                    {
                        laHizoCorrecta = respuesta.Correcta;
                     
                    }
                }
            }

            if (laHizoCorrecta)
            {
                PuntajeActual += 100;
                CantidadPreguntasCorrectas++;
            }

            ContadorNroPreguntaActual++;

            if (ListaPreguntas != null && ContadorNroPreguntaActual < ListaPreguntas.Count)
                PreguntaActual = ListaPreguntas[ContadorNroPreguntaActual];
            else
                PreguntaActual = null;

            return laHizoCorrecta;
        }
    }
}