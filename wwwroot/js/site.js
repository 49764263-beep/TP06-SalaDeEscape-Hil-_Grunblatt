﻿function arriesgarLetra() {
    let letra = document.getElementById("Letra").value.toUpperCase();
    let palabra = document.getElementById("Palabra").value;
    let palabraOculta = document.getElementById("PalabraOculta");
    let respuesta = document.getElementById("Rta");
    let letrasFallidas = document.getElementById("LetrasFallidas").innerText;
    let intentos = parseInt(document.getElementById("Intentos").innerText);

    if (respuesta.innerText == "¡GANASTE!") return;
    if (intentos <= 0) return;
    if (letra == "") return;

    let palabraNueva = "";

    if (palabra.includes(letra)) 
    {
        for (let i = 0; i < palabra.length; i++) 
        {
            if (palabra[i] == letra) 
            {
                palabraNueva += letra;
            } else if (palabraOculta.innerText[i] != "_") 
            {
                palabraNueva += palabraOculta.innerText[i];
            } else 
            {
                palabraNueva += "_";
            }
        }
        palabraOculta.innerText = palabraNueva;
    } else 
    {
        if (letrasFallidas != "Ninguna" && letrasFallidas.includes(letra)) return;
        if (letrasFallidas == "Ninguna") 
        {
            letrasFallidas = "";
        }
        letrasFallidas += letra + " ";
        document.getElementById("LetrasFallidas").innerText = letrasFallidas;
        intentos--;
        document.getElementById("Intentos").innerText = intentos;
    }

    document.getElementById("Letra").value = "";

    if (!palabraOculta.innerText.includes("_")) 
    {
        respuesta.innerText = "¡GANASTE!";
        document.getElementById("formSiguienteSala").style.display = "block";
    }

    if (intentos <= 0 && palabraOculta.innerText.includes("_")) 
    {
        respuesta.innerText = "Te quedaste sin intentos.";
    }
}

let aceiteElegido = 0;
let ordenAceites = [];
let ordenCorrecto = [1, 3, 2];

function elegirAceite(numero) {
    aceiteElegido = numero;
    document.getElementById("aceite1").classList.remove("elegido");
    document.getElementById("aceite2").classList.remove("elegido");
    document.getElementById("aceite3").classList.remove("elegido");
    document.getElementById("aceite" + numero).classList.add("elegido");
}

function ponerAceite(lugar) {
    if (aceiteElegido == 0) return;

    if (ordenAceites.length >= 3) return;

    ordenAceites.push(aceiteElegido);
    document.getElementById("mensajeSala2").innerText = "Aceite colocado en el lugar " + lugar + ".";
    aceiteElegido = 0;

    if (ordenAceites.length == 3) 
    {
        let correcto = true;

        for (let i = 0; i < ordenCorrecto.length; i++) {
            if (ordenAceites[i] != ordenCorrecto[i]) 
            {
                correcto = false;
            }
        }

        if (correcto) 
        {
            document.getElementById("mensajeSala2").innerText = "¡Correcto! El Hombre de Hojalata recuperó su movimiento. Pista: CORAZÓN.";
            document.getElementById("formSala3").style.display = "block";
        } else {
            document.getElementById("mensajeSala2").innerText = "El orden no es correcto. Volvé a intentarlo.";
            ordenAceites = [];
        }
    }
}

function encontrarBotonSala3() 
{
    document.getElementById("mensajeSala3").innerText = "¡Encontraste las zapatillas! Pista: ROJO.";
    document.getElementById("formSiguienteSala4").style.display = "block";
}

let respuestasCorrectas = 0;
let preguntasRespondidas = 0;

function responderQuiz(boton, correcto) {
    let pregunta = boton.parentElement;
    if (pregunta.classList.contains("respondida")) 
    {
        return;
    }

    pregunta.classList.add("respondida");
    preguntasRespondidas++;
    if (correcto) 
    {
        respuestasCorrectas++;
        boton.classList.add("correcto");
    } else 
    {
        boton.classList.add("incorrecto");
    }

    if (preguntasRespondidas == 5) 
    {
        if (respuestasCorrectas >= 4) 
        {
            document.getElementById("resultadoQuiz").innerText = "¡Excelente! Pista: VALOR.";
            document.getElementById("formSala5").style.display = "block";
        } else 
        {
            document.getElementById("resultadoQuiz").innerText = "Necesitás al menos 4 respuestas correctas. Volvé a intentarlo recargando la sala.";
        }
    }
}

function resolverFinal() {

    let respuesta = document.getElementById("respuestaFinal").value.toUpperCase().trim();
    let mensaje = document.getElementById("mensajeFinal");

    if (respuesta == "ZAPATILLAS ROJAS" || respuesta == "ZAPATILLASROJAS" || respuesta == "ZAPATILLAS" || respuesta == "ZAPATILLA" || respuesta == "LAS ZAPATILLAS" || respuesta == "LAS ZAPATILLAS ROJAS") {

    mensaje.innerText = "¡LO LOGRASTE! Las zapatillas te llevan de vuelta a casa. Encontraste al Mago y escapaste de la Bruja.";

    document.getElementById("formVictoria").style.display = "block";

    } else {
    mensaje.innerText = "No es la respuesta. Recordá todas las pistas de las salas anteriores.";
    }
}
