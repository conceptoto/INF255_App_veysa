DECLARE @TABLE VARCHAR(100) = 'table'
DECLARE @COLUMN VARCHAR(100) = 'column'
DECLARE @DESCRIPCION VARCHAR(100) = 'MS_Description'
DECLARE @CurrentUser sysname
select @CurrentUser = schema_name()

CREATE TABLE Insumos(
	CodigoInsumos int NOT NULL,
	Nombre varchar(100) NOT NULL,
   Stock int NOT NULL,
   Descripcion varchar(200), 
   Link_Imagen varchar(200),
   Tienda varchar(100),
   Precio int NOT NULL,
)

ALTER TABLE Insumos
ADD CONSTRAINT PK_Insumos
PRIMARY KEY (CodigoInsumos)

ALTER TABLE Insumos
ADD Activo INT NOT NULL 
CONSTRAINT D_Insumos_Activo
DEFAULT (1)

execute sp_addextendedproperty @DESCRIPCION, 
   'Tabla que almacena los Insumos a utilizar para la fabricacion de manualidades',
   'user', @CurrentUser, @TABLE, 'Insumos'