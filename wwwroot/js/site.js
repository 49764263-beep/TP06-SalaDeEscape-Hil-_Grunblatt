﻿function arriesgarLetra()
{
    if(document.getElementById("Rta").innerText == "GANASTE" || document.getElementById("Rta").innerText == "NO TENES MÁS INTENTOS")
    {
        return;
    }

    let letra = document.getElementById("Letra").value.toUpperCase();
    let palabra = document.getElementById("Palabra").value;
    let palabraOculta = document.getElementById("PalabraOculta");
    let palabraNueva = "";
    let Rta = document.getElementById("Rta");
    let LetrasFallidas = document.getElementById("LetrasFallidas");
    let letrasFallidas = LetrasFallidas.innerText;
    let Intentos = parseInt(document.getElementById("Intentos").innerText);

    if(letrasFallidas.includes(letra) || palabraNueva.includes(letra))
    {
        return;
    }
    if(palabra.includes(letra))
    {
        for(let i = 0; i < palabra.length; i++)
        {
            if(palabra[i] == letra)
            {
                palabraNueva += letra;
            } 
            else if(palabraOculta.innerText[i] != "_")
            {
                palabraNueva += palabraOculta.innerText[i];
            } 
            else
            {
                palabraNueva += "_";
            }
        }
        palabraOculta.innerText = palabraNueva;
    } 
    else
    {
        Intentos--;
        palabraNueva = palabraOculta.innerText;
        letrasFallidas += letra + " ";
        LetrasFallidas.innerText = letrasFallidas;
        document.getElementById("Intentos").innerText = Intentos;
    }
    if(!palabraNueva.includes("_")) 
    {
        Rta.innerText = "GANASTE";
        
        // Mostrar el formulario con el boton para avanzar a la siguiente sala
        document.getElementById("formSiguienteSala").style.display = "block";
    }
    if(Intentos <= 0)
    {
        Rta.innerText = "NO TENES MÁS INTENTOS"
    }
}

const colores = ['verde', 'rojo', 'amarillo', 'azul'];
let patron = [];
let patronUsuario = [];
let nivel = 0;
let bloqueado = true;

function iniciarJuego() {
    patron = [];
    patronUsuario = [];
    nivel = 0;
    document.getElementById("btn-iniciar").style.display = "none";
    document.getElementById("mensaje").innerText = "";
    siguienteRonda();
}

function siguienteRonda() {
    patronUsuario = [];
    nivel++;
    document.getElementById("contador").innerText = nivel - 1;

    // hacer que cuando llegue a 10 victorias, muestre el botón y finalize el juego, y que el mensaje diga "¡FELICIDADES! COMPLETASTE LA SALA 2"
    if (nivel > 10) {
        document.getElementById("mensaje").innerText = "¡FELICIDADES! COMPLETASTE LA SALA 2";
        document.getElementById("mensaje").style.color = "#27ae60";
        document.getElementById("formSala3").style.display = "block";
        bloqueado = true;
        return;
    }

    bloqueado = true;
    let colorRandom = colores[Math.floor(Math.random() * 4)];
    patron.push(colorRandom);
    ejecutarSecuencia();
}

function iluminarColor(color) {
    let el = document.getElementById(color);
    el.classList.add("activo");
    setTimeout(() => el.classList.remove("activo"), 400);
}

function ejecutarSecuencia() {
    let i = 0;
    let interval = setInterval(() => {
        iluminarColor(patron[i]);
        i++;
        if (i >= patron.length) {
            clearInterval(interval);
            bloqueado = false;
        }
    }, 800);
}

function presionarColor(color) {
    if (bloqueado) return;

    iluminarColor(color);
    patronUsuario.push(color);

    let index = patronUsuario.length - 1;

    if (patronUsuario[index] !== patron[index]) {
        document.getElementById("mensaje").innerText = "¡Te equivocaste! Inténtalo de nuevo.";
        document.getElementById("mensaje").style.color = "#e74c3c";
        document.getElementById("btn-iniciar").style.display = "inline-block";
        document.getElementById("btn-iniciar").innerText = "Reintentar";
        bloqueado = true;
        return;
    }

    if (patronUsuario.length === patron.length) {
        bloqueado = true;
        setTimeout(siguienteRonda, 1000);
    }
}