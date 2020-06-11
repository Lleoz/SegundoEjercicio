# Backend del Segundo Ejercicio

# Integrantes del equipo

- [x] [@Lleoz](https://discordapp.com/users/476574369229832203)
- [x] [@josue.samano](https://discordapp.com/users/335850826318741506)
- [x] [@Vladimir Cabrera](https://discordapp.com/users/683745689892815043)
- [x] [@Romer](https://discordapp.com/users/702955480267358329)
- [x] [@portusan](https://discordapp.com/users/717776768244908053)
- [x] [@RiojasMx](https://discordapp.com/users/201813752356536320)
- [x] [@programando.ideas](https://discordapp.com/users/716354253081542666)

# Se tiene que crear un API que cree, actualice, eliminé y obtenga uno o más registros

1) Crear usuario (Todos los campos son necesarios) 
- Nombre completo
- Fecha de nacimiento // fecha mayor a 18 años
- Orientación sexual
- Email // Validar el email - El email debe ser único
- Teléfono // El teléfono debe ser único.
- Contraseña

2) Actualización de usuario:
- Recibe como parámetro el ID del usuario
- Solo puede actualizar:
	- su nombre
	- fecha nacimiento
	- sexo
	- teléfono. 
	- Haciendo las validaciones necesarias.

3) Actualización de contraseña:
- Recibe como parámetro el email del usuario
- Se debe utilizar un método nuevo de actualización
- Sin embargo, la contraseña se debe generar y enviar por correo.

4) Obtener usuario
- Recibe como parámetro el correo electrónico para obtener el usuario 
- En caso de que esté parámetro no se envié regresara toda la lista de usuarios.

5) Eliminar usuario, recibe como parámetro el id del usuario y elimina dicho registro.
