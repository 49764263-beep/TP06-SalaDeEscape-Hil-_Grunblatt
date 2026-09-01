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
        
    }
    if(Intentos <= 0)
    {
        Rta.innerText = "NO TENES MÁS INTENTOS"
    }
}