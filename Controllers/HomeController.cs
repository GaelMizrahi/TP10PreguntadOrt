using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP10_PreguntadORT.Models;

namespace TP10_PreguntadORT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        public IActionResult Index()
        {
            return View(); 
        }

       
        public IActionResult ConfigurarJuego()
        {
            ViewBag.Categorias = BD.ObtenerCategorias();
            return View("ConfigurarJuego"); 
        }

       
        [HttpGet]
        public IActionResult Comenzar(string username, int categoria)
        {
            Juego.CargarPartida(username, categoria);
            return RedirectToAction("Jugar");
        }

      
        public IActionResult Jugar()
        {
            Pregunta pregunta = Juego.ObtenerProximaPregunta();

            if (pregunta == null)
            {
              
                ViewBag.Username = Juego.Username;
                ViewBag.Puntaje = Juego.PuntajeActual;
                return View("Fin"); 
            }

            List<Respuesta> respuestas = Juego.ObtenerProximasRespuestas(pregunta.IdPregunta);

            ViewBag.Username = Juego.Username;
            ViewBag.Puntaje = Juego.PuntajeActual;
            ViewBag.Numero = Juego.ContadorNroPreguntaActual + 1; 
            ViewBag.Pregunta = pregunta;
            ViewBag.Respuestas = respuestas;

            return View("Juego"); 
        }

        
        [HttpPost]
        public IActionResult VerificarRespuesta(int idPregunta, int idRespuesta)
        {
            bool correcta = Juego.VerificarRespuesta(idRespuesta);

           
            string textoCorrecto = "";
            if (Juego.ListaRespuestas != null)
            {
                int i;
                for (i = 0; i < Juego.ListaRespuestas.Count; i++)
                {
                    Respuesta respuesta = Juego.ListaRespuestas[i];
                    if (respuesta.Correcta)
                    {
                        textoCorrecto = respuesta.Contenido;
                       
                    }
                }
            }

            ViewBag.Correcta = correcta;
            ViewBag.RespuestaCorrecta = textoCorrecto;
            ViewBag.Puntaje = Juego.PuntajeActual;
            ViewBag.Username = Juego.Username;

            return View("Fin"); 
        }
    }
}