# Juego Tower Defense

## Por David Alejandro y Diego

En esta práctica se realiza una pequeña demo de un juego ambientado en el estilo de un Tower Defense, en el cuál el jugador tendrá que defender el castillo de los enemigos mediante el uso de armas que podrán ser obtenidas a través de dinero matando a los enemigos.

La zona principal a defender tendrá 3 vidas; por cada enemigo que entre en el castillo se perderá una vida. Si se pierden todas las vidas, la partida finalizará y se perderá. Si se sobreviven los 60 segundos del juego, se ganará la partida mostrando una pantalla con los puntos totales conseguidos (números de enemigos muertos * 2 + dinero total conseguido).



Al iniciar el juego se mostrará la pantalla inicial con un menú que muestra 3 opciones funcionales:

- Jugar el juego

- Mostrar los créditos del juego

- Salir del juego

Cada una de estas opciones tiene una funcionalidad distinta que depende de la opción descrita en el texto del botón.



El proyecto cuenta con la organización (Managers, Singletons, etc) y requisitos propuestos (torretas, muros, spawn de enemigos, vidas del objetivo a defender, ...) en la guía de la práctica, tales como muros y torretas (en este caso balistas), enemigos, un premio a defender, etc...

También se han incluido varios aspectos extras para la práctica entre los cuales se encuentran los siguientes:

- Cañones como nuevo objeto y nueva opción de compra disponible a partir de 20$. El rango de acción del cañón es superior al de la balista y sus proyectiles (balas de cañón) hacen más daño que las flechas de balistas, aunque el cooldown que tiene es mayor que la balista.

- Barras de vida de los enemigos.

- Click derecho para deseleccionar la opción de compra elegida. Si deja de haber dinero suficiente para comprar ese objeto la imagen se oscurece y se deselecciona.

- Hacer zoom con la rueda del ratón hacia delante y hacia detrás.

- Mover la posición de la cámara presionando la rueda del ratón hacia los lados en la pantalla.

- Barras de munición en la artillería, tanto como para cañones y como para balistas.

- Radio de spawn de enemigos (cada 3 segundos) en forma de anillo al rededor del castillo.

- Botón de volver al menú y salir al terminar las partidas.

- La artillería disparará al enemigo más cercano, siempre y cuando no haya un objeto, muro u otra unidad de artillería delante del objetivo a disparar. En caso de haber algún obstáculo entre ese enemigo y la artillería se elegirá al segundo enemigo más cercano que sea visible si es que lo hay.
