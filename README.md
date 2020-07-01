# Backend de la [Web en Angular](https://github.com/Lleoz/proyectoLogin) y la [App en Ionic](https://github.com/EquipoAngular/proyectoIonic)

## Integrantes del equipo

- [x] [@Lleoz](https://discordapp.com/users/476574369229832203)
- [x] [@josue.samano](https://discordapp.com/users/335850826318741506)
- [x] [@Vladimir Cabrera](https://discordapp.com/users/683745689892815043)
- [x] [@Romer](https://discordapp.com/users/702955480267358329)
- [x] [@portusan](https://discordapp.com/users/717776768244908053)
- [x] [@RiojasMx](https://discordapp.com/users/201813752356536320)
- [x] [@programando.ideas](https://discordapp.com/users/716354253081542666)

## Video
Video demo: [Prueba del cliente y api](https://youtu.be/1rJqbYze7-4)

------------
# Instalación del entorno de trabajo para ejecutar el proyecto
Guía para poder instalar las herramientas necesarias para ejecutar el proyecto.
### 1. Software requerido
- [x] [GIT](https://git-scm.com/downloads) (opcional)
- [x] [Visual Studio 2019 Community](https://visualstudio.microsoft.com/es/vs/community/)
	- Agregar soporte para .net core en la instalación
- [x] [Extension de Visual Studio Conveyor](https://marketplace.visualstudio.com/items?itemName=vs-publisher-1448185.ConveyorbyKeyoti)
- [x] [SQL Server Development Edition](https://go.microsoft.com/fwlink/?linkid=866662)
- [x] [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15) (opcional)
- [x] Comando de .net core para generar la base de datos
	- `dotnet tool install --global dotnet-ef`

### 2. Ejecutar el proyecto
- **Descargar e inicializar el proyecto**
  - `git clone https://github.com/Lleoz/SegundoEjercicio.git`
  - `cd SegundoEjercicio`
  - Abrir el archivo Ejercicio2.Api.sln con Visual Studio
  - Compilar la solución
  - ![alt text](https://github.com/Lleoz/SegundoEjercicio/blob/master/images/inst01.png)
- **Configuración del entorno**
  - Si se desea activar el envio de mail con la contraseña se debe gestionar previamente
  	- Se puede elegir por ejemplo MailJET que posee cuentas free con 200 mails por mes
  - Generar la base de datos
  	- Crear un usuario con permiso de **dbCreator**
	- ![alt text](https://github.com/Lleoz/SegundoEjercicio/blob/master/images/inst03.png)
	- Posicionarse en el directorio Ejercicio2.Api.Context.MsSql
		- cd [directorio del proyecto]\Context\Ejercicio2.Api.Context.MsSql
		- `dotnet-ef database update --startup-project="../../Apis/Ejercicio2.Api.Users/Ejercicio2.Api.Users.csproj" --context="MsSqlContext"`
  - Solución en Visual Studio
  	- Editar el archivo de secretos del proyecto Ejercicio2.Api.Users
  	- ![alt text](https://github.com/Lleoz/SegundoEjercicio/blob/master/images/inst02.png)
  	- Copiar el siguiente contenido y adaptarlo a la configuración local de cada máquina
		```xml
		  {
		  "ConnectionStrings": {
		    "Default": "server=localhost;userid=ejemplo2;password=ejemplo2;database=Ejemplo2DB;connectionreset=true;Allow User Variables=True;SslMode=none",
		    "SqlServerContext": "server=Instancia de SQL Server;User Id=usuario;Password=contraseña;Database=Ejemplo2ApiDB;"
		  },
		  "Email": {
		    "From": "Grupo Angular!",
		    "Dir": "direccion de email del servidor elegido"
		  },
		  "SmtpServer": {
		    "Url": "URL del servidor de mail",
		    "Port": "465",
		    "UseSSL": "true",
		    "RequireAuth": "true",
		    "user": "usuario del servidor de mail",
		    "password": "contraseña del servidor de mail"
		  },
		  "AppSettings": {
		    "OriginCors": [
		      "http://localhost:4200",
		      "http://localhost:8100",
		      "http://localhost",
		      "http://10.0.0.4:4200"
		    ],
		    "secret": "ingresar aqui el secreto",
		    "issuer": "https://localhost:5001",
		    "audience": "ejercicio2.api"
		  },
		  "Serilog": {
		    "MinimumLevel": {
		      "Default": "Debug",
		      "Override": {
			"Microsoft": "Warning",
			"System": "Warning"
		      }
		    },
		    "WriteTo": [
		      {
			"Name": "File",
			"Args": {
			  "path": ".\\LOGs\\log_api_ejemplo2.txt",
			  "rollingInterval": "Day",
			  "rollOnFileSizeLimit": "true",
			  "fileSizeLimitBytes": "10485760"
			}
		      }
		    ]
		  }
		}
		```
- **Ejecución**

------------


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
