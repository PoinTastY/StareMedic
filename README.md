# StareMedic
Software de administracion hospitalaria

StareMedic es un programa para administrar las admisiones clinicas al hospital, imprimir las hojas de admision para la firma del paciente/fiador, y sirve como cliente para generar remisiones consumiendo el SDK de Contpaqi, a traves de <a href="https://github.com/PoinTastY/SDKService">SDKService</a>.

<img src="https://github.com/PoinTastY/StareMedic/assets/52047942/fe573a66-d890-4ed6-ab68-fecbd2083125" alt="Main Page View" width="700">

Este proyecto surgio por la necesidad de modernizar el software de admisiones actual de un hospital local, que a su vez, como extra, fuese compatible con su sistema administrativo (Contpaqi Comercial).
Fue asi que por mero gusto propio, decidi probar .Net MAUI, que personalmente nos llama mucho la atencion.
Primero planificamos la estructura de las entidades involucradas para considerar sus relaciones, y construir la base de datos.
La base de datos esta contextualizada con <a href="https://github.com/PoinTastY/StareMedic/blob/master/StareMedic/Data/AppDbContext.cs">Entity Framework</a>, donde se mapearon las tablas con sus respectivas relaciones.

La base de datos corre en Postgres 16, y tanto la conexion a la misma, como la conexion al sdk son configurables desde el archivo <a href="https://github.com/PoinTastY/StareMedic/blob/master/StareMedic/Data/config.json">config.json</a>.

el SDKService corre con .net 4 en 32 bits, es por eso que no pude implementarlo directamente con este framework, y despues de realizar una gran cantidad de pruebas con distintas tecnologias, decidi que lo mas apto seria levantar un servicio en segundo plano, para descartar problemas de dependencias, y que a su vez, me sera util para futuros proyectos, consumiendo solamente un usuario de contpaq, y estando disponible desde cualquier plataforma.

Las <a href="https://github.com/PoinTastY/StareMedic/tree/master/StareMedic/Models/Entities">entidades</a> fueron algo simple, simplemente seguimos el modelo que habiamos establecido para las db, y las reglas del negocio estan bien establecidas. Para exponer los metodos del contexto de la base de datos, utilizamos el manejo del <a href="https://github.com/PoinTastY/StareMedic/blob/master/StareMedic/Models/MainRepo.cs">repositorio principal</a>.

Si bien existen varias formas mas ordenadas de estructurar los proyectos, trate de hacecrlo mas navegable separando las <a href="https://github.com/PoinTastY/StareMedic/tree/master/StareMedic/Views">views</a> y los controles de las entidades lo mejor que pude, nos funciono asi, sin embargo, en futuras actualizaciones y proyectos, implementare las mejores practicas de clean code.

Para la generacion del documento de admision clinica, utilice iTextSharp, que al probarlo, senti que estaba usando LaTex. El <a href="https://github.com/PoinTastY/StareMedic/blob/master/StareMedic/Models/DoCreate.cs">documento</a> fue generado en base a la hoja de admision que manejan actualmente en el hospital, que no es mas que una plantilla membretada que sale mal impresa, pero ahora todo es generado digitalmente:
## Admision Clinica

<img src="https://github.com/PoinTastY/StareMedic/assets/52047942/320b2258-2092-4650-87d4-1107d5943907" alt="Generacion de admision clinica" width="700">

## Sumario clinico

<img src="https://github.com/PoinTastY/StareMedic/assets/52047942/df664e0e-45b1-4c46-b99f-667d9eb9cb4a" alt="Generacion de sumario clinico" width="700">



