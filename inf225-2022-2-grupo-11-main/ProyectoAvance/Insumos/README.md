> Lineamientos técnicos

# Puntos a considerar

La solución a desarrollar debe seguir los siguientes lineamientos (imagen referencial al final):

* Cada elemento que se indica a continuación debe estar en un contenedor [Docker](https://www.docker.com/) independiente, para ello cada elemento debe tener su propio archivo [Dockerfile](https://docs.docker.com/engine/reference/builder/) que debe permitir levantar el contenedor de forma independiente.
* Se deben considerar 2 API's:
    * API INSUMOS: Con todo lo referido a la gestión y manejo de los insumos, y los datos referidos a los mismos.
    * API PEDIDOS: Con todo lo referido a la información y gestión de los pedidos y productos.
    > Queda información del negocio que no se ha asociado explicitamente a ninguna de las dos API's definidas, como grupo deben decidir si la asocian a alguna de las definidas o generan nuevas API's.
* Las API's (definididas como las que como grupo decidan incorporar - de ser el caso-) deben ser construidas utilizando [.Net Core 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) y utilizando [Web API](https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio-code).
* Cada API contará con una base de datos **SQL Server 2017** independientes entre si, pero dentro del mismo servidor (un [contenedor Docker](https://hub.docker.com/_/microsoft-mssql-server) como se explica en los puntos siguientes).
* Las API se deben documentar utilizando [Swagger](https://learn.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-6.0), para ello se deben apoyar del uso de [Swashbuckle](https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio). En el archivo [Program.cs](/Ejemplo/Program.cs) del Ejemplo en la sección **#region SWAGGER** hay un ejemplo de como poder configurarlo, y en los *controllers* ejemplos de su uso ( tomo como referencia el archivo [ColorController](/Ejemplo/Controllers/ColorController.cs)).
* El sistema web con el que interactuará el usuario deberá consumir la información desde las API's definidas. Este deberá contar con su propio archivo Dockerfile que permita poder levantarlo como contenedor. **La tecnología a utilizar en este punto será de libre elección de cada equipo, pero debe quedar claramente documentada y justificada**.
* Debe incorporar un archivo [docker-compose](https://docs.docker.com/compose/) que permita levantar todos los componentes (API's y sistema con el que interactúa el usuario) en sus respectivos contenedores mediante la instrucción correspondiente.
* La aplicación de los parches y gestión de las modificaciones de la base de datos se realizará apoyándose del uso de [DBUp](https://dbup.readthedocs.io/en/latest/) y la implementación de apoyo que se explica más adelante.

![001 IMAGEN](001EJEMPLO.png)

* **Se entrega un proyecto Web API de ejemplo, que utiliza una serie de proyectos de referencia para crear rápidamente un mantenedor. Pueden prescindir de este ejemplo, y utilizar otros elementos.**

* Se recomienda el uso de [VS Code](https://code.visualstudio.com/) y alguna [extensión para trabajar con Docker](https://code.visualstudio.com/docs/containers/overview).
 
## Base de datos

Para la creación de la base de datos, se debe utilizar un contenedor de la siguiente manera:

```bat
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Bdpass001." -e 'MSSQL_PID=Enterprise' -p 1433:1433 --name sql1 -h sql1 -d mcr.microsoft.com/mssql/server:2017-latest 
```

> Notar que:
> 1. El usuario es *sa* (para efectos prácticos del curso se usara así).
> 2. La contraseña de acceso es **Bdpass001.**
> 3. La instancia se mapea al mismo puerto, es decir **1433**.

Posteriormente deberá ingresar al contenedor:

```bat
docker exec -it sql1 /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Bdpass001.
```

Y crear dos base de datos:

```sql
create database Nombre
```

Donde nombre, deberá ser sustituido por: **BDXX_INSUMOS**, y **BDXX_PEDIDOS**, donde **XX** corresponde al número de su grupo (01, 02, ... 14).

> **Importante**: Debe actualizar el contenido del archivo [appsettings.json](/Ejemplo/appsettings.json), especificamente **"ConnectionStrings**, el valor asociado a **DefaultConnection**. 

### **Normas de bases de datos**:

No existen lineamientos a la forma de nombrar las tablas y columnas. 

Las únicas restricciones asociadas corresponden al uso de Dapper (como se hace en el ejemplo), y corresponden a:

* La clave primaria de cada tabla (PK) debe estar compuesta por solo una columna. Esta puede ser numérica o no; y en caso de ser numérica autoincremental o no. 

### **Aplicación de los parches**:

Al respecto a la creación de los scripts a utilizar:

* Estos deben almacenarse en el directorio **Scripts**.
* Deben seguir lo lineamientos en la forma de nombrarlos indicados [aquí](https://github.com/wormaza/Transversal.Util.BaseDBUp).
* Notar el contenido de los archivos:  
    * [Dockerfile](/Ejemplo/Dockerfile), donde se indican las instrucciones para copiar los scripts - y ejecutarlos - al levantar el contenedor.  
    * [Program.cs](/Ejemplo/Program.cs), la sección **#region MIGRATION**.
    * [appsettings.json](/Ejemplo/appsettings.json) con la configuración de la migración.

Notar que se puede verificar el resultado de la migración dentro del log al levantar el contenedor:

```bat
docker logs <IDCONTENEDOR>
```

O al revisar el contenido de la tabla **SchemaVersions**, en la base de datos correspondiente (en la tabla esta el historial de scritps ejecutados).

> Sección del [Dockerfile](/Ejemplo/Dockerfile) donde se indican la copia de los parches al contenedor para ser ejecutados al momento que este sea levantado:
> ```yaml 
> COPY ["Ejemplo/Scripts", "/app/Scripts"]
>```

> Sección del [appsettings.json](/Ejemplo/appsettings.json) donde se indican el string de conexión (debe modificarlo según las instrucciones dadas):
> ```js 
> "ConnectionStrings": {
>    "DefaultConnection": "Server=host.docker.internal,1433;Database=EjemploDBUp;Trusted_Connection=false;MultipleActiveResultSets=true;User Id=sa;Password=Bdpass001."
>  }
>```

> Sección del [appsettings.json](/Ejemplo/appsettings.json) donde se indican las variables para realizar la migración (debe modificarlo y seguir el formato al momento de nombrar los parches):
> ```js 
>  "DbMigrationPattern": "TEST",
>  "DbMigration": true,
>  "DbMigrationData": true,
>  "DbMigrationStoredProcedure": true,
>  "DbMigrationDevelopment": false
>```

> Sección del [Program.cs](/Ejemplo/Program.cs) donde se setea el string de conexión:
> ```csharp 
>  #region BASE DE DATOS
>  string conString = ConfigurationExtensions.GetConnectionString(configuration, "DefaultConnection");
>  builder.Services.AddSingleton<string>(conString);
>  #endregion
>```

> Sección del [Program.cs](/Ejemplo/Program.cs) donde se realiza la migración:
> ```csharp 
> #region MIGRATION
> bool migration = GetBoolDefaultFalse("DbMigration");
> bool migrationdata = GetBoolDefaultFalse("DbMigrationData");
> bool migrationdevelop = GetBoolDefaultFalse("DbMigrationDevelopment");
> bool migrationstoredprocedure = GetBoolDefaultFalse> ("DbMigrationStoredProcedure");
> string pattern = GetPattern("DbMigrationPattern");
> string path = String.Format("{0}/Scripts", Directory.> GetCurrentDirectory());
> string migrationout = "Migracion No ejecutada";
> if (migration)
> {
>     DBUpMSMigration m = new DBUpMSMigration(
>         ConfigurationExtensions.GetConnectionString(configuration, "DefaultConnection")
>         , path
>         , pattern
>         , null
>         , migrationdata
>         , migrationdevelop
>         , migrationstoredprocedure
>         , DBUpMSMigration.DataBaseType.SqlServer);
>     ResultMigration r = m.GenerateMigration();
>     migrationout = String.Format("Migration DB: {0}: {1}", r.IsValid ? "Up" : "Error", r.Result);
> }
> #endregion
>```

## Documentación de la API

Como se indico previamente, se utilizará [Swagger](https://learn.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-6.0) para documentar las APIs.

En [Program.cs](/Ejemplo/Program.cs) se realiza la configuración, donde deberá indicar los datos de su respectivo grupo.

> Sección del [Program.cs](/Ejemplo/Program.cs) donde se indican algunos datos de la configuración para la documentación:
> ```csharp 
> #region SWAGGER
> builder.Services.AddSwaggerGen(options =>
> {
>     options.SwaggerDoc("v1", new OpenApiInfo
>     {
>         Version = "v1",
>         Title = "API <NOMBRE>",
>         Description = "Grupo 00 - INF225"
>     });
>     var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
>     options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
> });
> #endregion
> ...
> if (app.Environment.IsDevelopment())
> {
> 
> }
> 
> #region SWAGGER NO SOLO PARA DESARROLLO - PARA EFECTOS PRÁCTICOS DEL RAMO
> app.UseSwagger();
> 
> app.UseSwaggerUI(options =>
> {
>     options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
>     options.RoutePrefix = string.Empty;
> });
> #endregion
>```

Un ejemplo de como poder documentar la API se puede encontrar en el archivo [ColorController](/Ejemplo/Controllers/ColorController.cs):

> ```csharp 
> /// <summary>
> /// Obtiene todos los colores, si no se indican parámetros de consulta se retornan todos
> /// </summary>
> /// <returns></returns>
> /// <response code="200">Returns ...</response>
> [HttpGet("")]
> [ProducesResponseType((int)HttpStatusCode.OK)]
> public async Task<ActionResult<IEnumerable<ColorModel>>> GetColor() => await base.Get();
>```

Es importante notar que **NO** es requisito para Swagger el tener que documentar, pero debe realizarse para poder tener la información solicitada. Lo anterior genera por ejemplo lo siguiente:

![002 IMAGEN](002EJEMPLO.png)

## Ejemplo

El proyecto de ejemplo presenta una forma de realizar consultas simples, haciendo uso de proyectos (librerías) que tienen las estructuras de consultas simples:
* [Model](/Ejemplo/Ejemplo/Models/): contiene las definiciones de las clases, que junto con [Dapper](https://github.com/DapperLib/Dapper) permiten hacer un mapeo a las tablas definidas (ver [Scripts](Ejemplo/Scripts/).)
* [DataAccess](ejemplo/DataAccess/): contiene las acciones básicas para poder interactuar con las tablas (mapeadas a los Models).
* [Business](ejemplo/Business/): Implementan acciones que se realizan con los datos, que van desde las acciones básicas ya definidas, y la implementación de búsquedas o lógicas de negocio más complejas. Se basan en los DataAccess y Models definidos. **El manejo y lógica de negocio se encuentra en esta capa**.
* [Controllers](Ejemplo/Controllers/): Simil al concepto de MVC son quienes reciben las peticiones e interactúan con elementos de la capa de negocio (Business) para dar respuesta a la solicitudes realizadas mediante los verbos HTTP tradicionales.