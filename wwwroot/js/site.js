var puntajeActual = 0;


function mostrarResultado(texto, esCorrecta) {
    if (esCorrecta) {
        alert("¡Correcto!");
        puntajeActual = puntajeActual + 100;
        document.getElementById("puntaje").innerHTML = "Puntaje: " + puntajeActual;
        document.getElementById("mensaje").innerHTML = " Bien hecho, era: " + texto;
    } else {
        alert("Incorrecto");
        document.getElementById("mensaje").innerHTML = " Incorrecto. Era otra respuesta.";
    }
}