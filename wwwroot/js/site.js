let puntajeActual = 0;
let seleccion = 0;

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


function elegirRespuesta(id) {
  seleccion = id;
  document.getElementById("idRespuesta").value = id;
  let mensaje = document.getElementById("mensaje");
  mensaje.innerHTML = "Elegiste la opcion con id: " + id;
}

function validarSeleccion() {
  if (seleccion == false) {
    alert("Elegi una respuesta antes de enviar.");
    return false;
  }
  return true;
}