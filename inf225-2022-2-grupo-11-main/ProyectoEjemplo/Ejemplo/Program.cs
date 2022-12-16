using Microsoft.OpenApi.Models;
using System.Reflection;
using Transversal.Util.BaseDBUp;
using Ejemplo.DBUp;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
ConfigurationManager configuration = builder.Configuration; 
IWebHostEnvironment environment = builder.Environment;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region SWAGGER
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API EJEMPLO",
        Description = "EJEMPLO GRUPO 00 INF225"
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
#endregion

#region BASE DE DATOS
 string conString = ConfigurationExtensions.GetConnectionString(configuration, "DefaultConnection");
 builder.Services.AddSingleton<string>(conString);
 #endregion

#region MIGRATION
bool migration = GetBoolDefaultFalse("DbMigration");
bool migrationdata = GetBoolDefaultFalse("DbMigrationData");
bool migrationdevelop = GetBoolDefaultFalse("DbMigrationDevelopment");
bool migrationstoredprocedure = GetBoolDefaultFalse("DbMigrationStoredProcedure");
string pattern = GetPattern("DbMigrationPattern");
string path = String.Format("{0}/Scripts", Directory.GetCurrentDirectory());
string migrationout = "Migracion No ejecutada";
if (migration)
{
    DBUpMSMigration m = new DBUpMSMigration(
        ConfigurationExtensions.GetConnectionString(configuration, "DefaultConnection")
        , path
        , pattern
        , null
        , migrationdata
        , migrationdevelop
        , migrationstoredprocedure
        , DBUpMSMigration.DataBaseType.SqlServer);

    ResultMigration r = m.GenerateMigration();
    migrationout = String.Format("Migration DB: {0}: {1}", r.IsValid ? "Up" : "Error", r.Result);
}
#endregion


var app = builder.Build();
app.Logger.LogInformation("Starting Application");
app.Logger.LogInformation(migrationout);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

#region SWAGGER NO SOLO PARA DESARROLLO - SOLO PARA EL RAMO
app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
#endregion

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#region Utiles
bool GetBoolDefaultFalse(string ConfigSection) => Boolean.TryParse(configuration.GetSection(ConfigSection).Value, out bool resp) ? Boolean.Parse(configuration.GetSection(ConfigSection).Value) : resp;
string GetPattern(string ConfigSection) => string.IsNullOrEmpty(configuration.GetSection(ConfigSection).Value.ToString()) ? "" : configuration.GetSection(ConfigSection).Value.ToString();
#endregion