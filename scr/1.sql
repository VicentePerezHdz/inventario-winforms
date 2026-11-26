-- Base de datos del sistema de inventario.
-- Tablas: Almacen, Empleado y tblusuario (usuarios con acceso al sistema).

CREATE TABLE [dbo].[Almacen](
	[IdAlmacen] int  IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Codigo] [varchar](50) NOT NULL,
	[Fecha_Ingreso] [datetime] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](50),
	Cantidad     INT,
	PrecioUnidad float,

)

CREATE TABLE [dbo].[Empleado](
	[Id_Personal] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Nombre] [varchar](50) NOT NULL,
	[Apellidos] [varchar](50) NOT NULL,
	[Edad] [int] NOT NULL,
	[Domicilio] [varchar](50)  NULL,
	[Colonia] [varchar](50)  NULL,
	[Sexo] [varchar](10)  NULL,
	[Telefono] [varchar](50)  NULL,
	[Email] [varchar](50)  NULL,
	[Salario][FLOAT],
	FOREIGN KEY (IdAlmacen) REFERENCES Almacen(IdAlmacen)
 ) 
 
 CREATE TABLE [dbo].[tblusuario](
  [UsuarioID]	 int(11) IDENTITY(1,1) NOT NULL PRIMARY KEY ,
  [UsuarioRolID] 	int(11)  NULL,
  [Usuario] 		varchar(50)  NULL,
  [Contrasenia]		 varchar(50)  NULL,
  [Correo] 				varchar(150)  NULL,
  [Activo] 			int(11)  NULL,
)

CREATE  TABLE [dbo]. [tblusuariorol] (
  [UsuarioRolID] int(11)  IDENTITY(1,1) NOT NULL PRIMARY KEY,
  [NombreModelo] varchar(50)  NULL,
  [Administrable] int(11)  NULL,
  [FechaRegistro] date  NULL,
  [Activo] int(11)  NULL,

) 

