DECLARE @TABLE VARCHAR(100) = 'table'
DECLARE @COLUMN VARCHAR(100) = 'column'
DECLARE @DESCRIPCION VARCHAR(100) = 'MS_Description'
DECLARE @CurrentUser sysname
select @CurrentUser = schema_name()

CREATE TABLE Productos(
    CodigoProductos int NOT NULL, 
    CodigoInsumos varchar(30), 
    Nombre varchar(100) NOT NULL,
    Descripcion varchar(100),
    Tienda varchar(30) NOT NULL,
    Precio int NOT NULL, 
    Cantidad int NOT NULL, 
    TiempoFabricacionEstimado varchar(100) NOT NULL
)

ALTER TABLE Productos 
ADD CONSTRAINT PK_Productos 
PRIMARY KEY (CodigoProductos)

ALTER TABLE Productos 
ADD Activo INT NOT NULL 
CONSTRAINT D_Productos_Activo 
DEFAULT (1)

execute sp_addextendedproperty @DESCRIPCION,    
'Tabla que almacena los productos a utilizar',
'user', @CurrentUser, @TABLE, 'Productos'