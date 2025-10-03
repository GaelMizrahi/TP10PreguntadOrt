using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP10_PreguntadORT.Models;

namespace TP10.Controllers;

public class HomeController : Controller
{
    private static Juego juego = new Juego();
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
            ViewBag.Categorias= BD.ObtenerCategorias();
            return View();
        }
         [HttpGet]
        public IActionResult Comenzar(string username, int categoria)
        {
           
            juego.CargarPartida(username, categoria);
           
            return RedirectToAction("Jugar");
        }
        public IActionResult Jugar()
{
    
    Pregunta pregunta = juego.ObtenerProximaPregunta();


    if (pregunta == null)
    {
        return View("Fin", juego); 
    }

    
    List<Respuesta> respuestas = juego.ObtenerProximasRespuestas(pregunta.IdPregunta);

    ViewBag.Username = juego.Username;
    ViewBag.Puntaje = juego.PuntajeActual;
    ViewBag.Numero = juego.ContadorNroPreguntaActual + 1; 
    ViewBag.Pregunta = pregunta;
    ViewBag.Respuestas = respuestas;

    return View();
}
 [HttpPost]
        public IActionResult VerificarRespuesta(int idPregunta, int idRespuesta)
        {
           
            bool correcta = juego.VerificarRespuesta(idRespuesta);

          
            string contenidoCorrecto = "";
            foreach (Respuesta respuesta in juego.ListaRespuestas)
            {
                if (respuesta.Correcta)
                {
                    contenidoCorrecto = respuesta.Contenido;
                    
                }
            }

            ViewBag.Correcta = correcta;
            ViewBag.RespuestaCorrecta = contenidoCorrecto;
            ViewBag.Puntaje = juego.PuntajeActual;
            ViewBag.Username = juego.Username;

            return View("Respuesta"); 
        }
    }

