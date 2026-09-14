using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SalaDeEscape.Models;

namespace SalaDeEscape.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult VerificarDatos(string nombreUsuario)
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
            Partida nuevaPartida = new Partida
            {
                NombreUsuario = nombreUsuario,
                Fechainicio = DateTime.Now,
                SalaActual = 1
            };

            bd.CrearPartida(nuevaPartida);
            Partida partida = bd.ObtenerPartida(nombreUsuario);

            HttpContext.Session.SetInt32("IDPartida", partida.IDPartida);
            HttpContext.Session.SetString("NombreUsuario", partida.NombreUsuario);
            HttpContext.Session.SetInt32("SalaActual", 1);

            return RedirectToAction("Juego");
        }
    }

    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    public IActionResult Juego()
    {
        int salaActual = HttpContext.Session.GetInt32("SalaActual") ?? 1;
        bool inicioVisto = HttpContext.Session.GetInt32("InicioVisto") == 1;

        if (!inicioVisto)
            return RedirectToAction("HistoriaInicio");

        switch (salaActual)
        {
            case 1:
                return RedirectToAction("Sala1");

            case 2:
                return RedirectToAction("Historia2");

            case 3:
                return RedirectToAction("Historia3");

            case 4:
                return RedirectToAction("Historia4");

            case 5:
                return RedirectToAction("Historia5");

            default:
                return RedirectToAction("Historia5");
        }
    }

    public IActionResult HistoriaInicio()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ComenzarSala1()
    {
        HttpContext.Session.SetInt32("InicioVisto", 1);
        return RedirectToAction("Sala1");
    }

    public IActionResult Sala1()
    {
        if (!PuedeEntrar(1)) return RedirectToAction("Juego");

        Palabras palabra = new Palabras();

        if (HttpContext.Session.GetString("PalabraSala1") == null)
        {
            HttpContext.Session.SetString("PalabraSala1", "CEREBRO");
        }

        ViewBag.Palabra = HttpContext.Session.GetString("PalabraSala1");
        ViewBag.PalabraOculta = palabra.PalabraAGuion(ViewBag.Palabra);

        return View();
    }

    [HttpPost]
    public IActionResult AvanzarSala()
    {
        int salaActual = HttpContext.Session.GetInt32("SalaActual") ?? 1;

        // Avanzamos solamente una sala
        int siguienteSala = salaActual + 1;

        HttpContext.Session.SetInt32("SalaActual", siguienteSala);

        string? nombreUsuario = HttpContext.Session.GetString("NombreUsuario");

        if (!string.IsNullOrEmpty(nombreUsuario))
        {
            BD bd = new BD();
            bd.ActualizarSala(nombreUsuario, siguienteSala);
        }

        return RedirectToAction("Juego");
    }

    public IActionResult Historia2()
    {
        if (!PuedeEntrar(2)) return RedirectToAction("Juego");
        return View();
    }

    [HttpPost]
    public IActionResult ComenzarSala2()
    {
        return RedirectToAction("Sala2");
    }

    public IActionResult Sala2()
    {
        if (!PuedeEntrar(2)) return RedirectToAction("Juego");
        return View();
    }

    public IActionResult Historia3()
    {
        if (!PuedeEntrar(3)) return RedirectToAction("Juego");
        return View();
    }

    [HttpPost]
    public IActionResult ComenzarSala3()
    {
        return RedirectToAction("Sala3");
    }

    public IActionResult Sala3()
    {
        if (!PuedeEntrar(3)) return RedirectToAction("Juego");
        return View();
    }

    public IActionResult Historia4()
    {
        if (!PuedeEntrar(4)) return RedirectToAction("Juego");
        return View();
    }

    [HttpPost]
    public IActionResult ComenzarSala4()
    {
        return RedirectToAction("Sala4");
    }

    public IActionResult Sala4()
    {
        if (!PuedeEntrar(4)) return RedirectToAction("Juego");
        return View();
    }

    [HttpPost]
    public IActionResult TerminarSala4()
    {
        HttpContext.Session.SetInt32("SalaActual", 5);

        string? nombreUsuario = HttpContext.Session.GetString("NombreUsuario");

        if (!string.IsNullOrEmpty(nombreUsuario))
        {
            BD bd = new BD();
            bd.ActualizarSala(nombreUsuario, 5);
        }

        return RedirectToAction("Historia5");
    }

    public IActionResult Historia5()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ComenzarSala5()
    {
        return RedirectToAction("Sala5");
    }

    public IActionResult Sala5()
    {
        if (!PuedeEntrar(5)) return RedirectToAction("Juego");
        return View();
    }

    public IActionResult Victoria()
    {
        return View();
    } 

    private bool PuedeEntrar(int sala)
    {
        int salaActual = HttpContext.Session.GetInt32("SalaActual") ?? 1;
        return salaActual >= sala;
    }
}