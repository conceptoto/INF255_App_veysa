# Grupo 11

Este es el repositorio del *Grupo 11*, cuyos integrantes son:

* José Alegría - 201830046-2
* Felipe Ayala - 201830034-9
* Felipe Vega - 201930013-k
* **Tutora**: Sofía Martínez

## Wiki

Puede acceder a la Wiki mediante el siguiente [enlace](https://gitlab.inf.utfsm.cl/wormazab/inf225-2022-2-grupo-11/-/wikis/Home)

## Videos

* [Video presentación cliente](https://www.youtube.com/watch?v=pl63bvYMthk&feature=youtu.be)
* [Video presentación avance hito 1](https://www.youtube.com/watch?v=te-ejGjGeYc)
* [Video presentación avance hito 3](https://www.youtube.com/watch?v=NzaC-j8bzTk)
* [Video presentación prototipo hito 4](https://www.youtube.com/watch?v=hRFH3IocwRE&feature=youtu.be)
* [Video presentación proyecto hito 7](https://www.youtube.com/watch?v=ezzqRLDfzhc)

## Aspectos técnicos relevantes

* Se utiliza docker para el levantamiento del proyecto, este permite un despliegue rápido que evita problemas de compatibilidad.
* El proyecto se modulariza en diferentes contenedores, el front-end, las APIs y las bases de datos están en contenedores independientes. Cabe mencionar que cada API está contenida independiente de las otras. Esta estructura se ejemplifica en el siguiente diagrama

![Estructura proyecto](/ProyectoEjemplo/estructura proyecto.drawio.png "Estructura proyecto")
* El contenedor relacionado con las bases de datos utiliza el DBMS SQL Server 2017 para trabajar con estas.
* Además, se utiliza el framework .NET para la creación de las apis y su librería DbUP para desplegar los cambios fácilmente.
* Por último, react será la tecnología utilizada para el despliegue del front-end.
