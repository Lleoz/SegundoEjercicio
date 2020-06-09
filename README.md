# Backend del Segundo Ejercicio

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
