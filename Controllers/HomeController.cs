using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SalaDeEscape.Models;

namespace SalaDeEscape.Controllers;

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

    // Crea un metodo para verificar los datos del usuario, que al recivir el nombredeUsuario, revise que en la base de datos exista, y si no existe, cree una nueva partida con el nombre de usuario y la fecha actual, y redirija a la vista de Juego, si existe, que le permite cargar sus datos ya guardados, utilizando la funcion de BD.VerificarUsuario y BD.CrearPartida, tambien, que pueda regresar a la sala que se haya guardado una vez que comendo o cuando cierra sesion, guardando solo lo nesesario mediante el uso de Session.
    [HttpPost]
    public IActionResult VerificarDatos(string nombreUsuario, int IDPartida)
    {
        BD bd = new BD();
        if (bd.VerificarUsuario(nombreUsuario))
        {
            Partida partida = bd.ObtenerPartida(nombreUsuario);
            HttpContext.Session.SetInt32("IDPartida", partida.IDPartida);
            HttpContext.Session.SetString("NombreUsuario", partida.NombreUsuario);
            HttpContext.Session.SetInt32("SalaActual", partida.SalaActual);
            return RedirectToAction("Juego");
        }
        else
        {
            Partida nuevaPartida = new Partida {NombreUsuario = nombreUsuario, Fechainicio = DateTime.Now, SalaActual = 1};
            bd.CrearPartida(nuevaPartida);
            HttpContext.Session.SetString("NombreUsuario", nombreUsuario);
            HttpContext.Session.SetInt32("SalaActual", 1);
            return RedirectToAction("Juego");
        }
    }

    //Crea un metodo para cerrar sesion, que limpie la session y redirija a la vista de Index.
    public IActionResult CerrarSesion() 
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    //Crea un metodo para redirigirce a la vista de juego, en donde pueda verificar en que sala se quede y mandarlo a una de ls 5 salas, mediante el uso de Session, y si no hay session, que lo mande a la sala 1.
    public IActionResult Juego()
    {
        int salaActual = HttpContext.Session.GetInt32("SalaActual") ?? 1;
        switch (salaActual)
        {
            case 1:
                return RedirectToAction("Sala1");
            case 2:
                return RedirectToAction("Sala2");
            case 3:
                return RedirectToAction("Sala3");
            case 4:
                return RedirectToAction("Sala4");
            case 5:
                return RedirectToAction("Sala5");
            default:
                return RedirectToAction("Sala1");
        }
    }

    public IActionResult Sala1()
    {
        Palabras palabra = new Palabras();
        ViewBag.Palabra = palabra.ObtenerPalabra();
        ViewBag.PalabraOculta = palabra.PalabraAGuion(ViewBag.Palabra);
        return View();
    }


    // Crea el metodo de verificar la respuesta del usuario, que reciba la respuesta del usuario y la compare con la respuesta correcta, si es correcta, que lo mande a la siguiente sala, y si no es correcta, que le muestre un mensaje de error y lo mantenga en la misma sala.
    [HttpPost]
    public IActionResult VerificarRespuesta(string respuesta)
    {
        string respuestaCorrecta = "El León Cobarde";
        if (respuesta.Equals(respuestaCorrecta, StringComparison.OrdinalIgnoreCase))
        {
            int salaActual = HttpContext.Session.GetInt32("SalaActual") ?? 1;
            salaActual++;
            HttpContext.Session.SetInt32("SalaActual", salaActual);
            return RedirectToAction("Juego");
        }
        else
        {
            ViewBag.MensajeError = "Respuesta incorrecta. Intenta de nuevo.";
            return View("Sala1");
        }
    }

    

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
