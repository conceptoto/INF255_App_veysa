DECLARE @TABLE VARCHAR(100) = 'table'
DECLARE @COLUMN VARCHAR(100) = 'column'
DECLARE @DESCRIPCION VARCHAR(100) = 'MS_Description'
DECLARE @CurrentUser sysname
select @CurrentUser = schema_name()

CREATE TABLE Pedidos(CodigoPedidos int NOT NULL, CodigoProductos int NOT NULL, Precio int NOT NULL,Tienda varchar(100) NOT NULL,NombreCliente varchar(100) NOT NULL,CorreoCliente varchar(100) NOT NULL,CelularCliente varchar(100) NOT NULL,Fecha varchar(100) NOT NULL,Estado varchar(100) NOT NULL )

ALTER TABLE Pedidos ADD CONSTRAINT PK_Pedidos PRIMARY KEY (CodigoPedidos)

ALTER TABLE Pedidos ADD Activo INT NOT NULL CONSTRAINT D_Pedidos_Activo DEFAULT (1)

execute sp_addextendedproperty @DESCRIPCION,    'Tabla que alamcena los Pedidos',   'user', @CurrentUser, @TABLE, 'Pedidos'