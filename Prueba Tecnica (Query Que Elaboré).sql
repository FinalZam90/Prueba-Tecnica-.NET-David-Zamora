CREATE DATABASE PruebaTecnica;

CREATE TABLE Rol(
IdRol INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50))

INSERT INTO Rol (Nombre) VALUES ('Administrador')
INSERT INTO Rol (Nombre) VALUES ('Usuario')

CREATE TABLE Usuario(
IdUsuario INT PRIMARY KEY IDENTITY (1,1),
Nombre VARCHAR(50) NOT NULL,
ApellidoPaterno VARCHAR(50) NOT NULL,
ApellidoMaterno VARCHAR(50),
FechaDeNacimiento DATE,
CURP VARCHAR(50),
Genero VARCHAR(50) NOT NULL,
UserName VARCHAR(50) UNIQUE NOT NULL,
Email VARCHAR(50) UNIQUE NOT NULL,
NumeroDeTelefono VARCHAR(50) NOT NULL,
Celular VARCHAR(50),
[Password] VARCHAR(50) NOT NULL,
Imagen VARCHAR(MAX),
IdRol INT FOREIGN KEY REFERENCES Rol(IdRol)
)


CREATE TABLE Departamento(
IdDepartamento INT PRIMARY KEY IDENTITY (1,1),
Nombre VARCHAR(50)
)

INSERT INTO Departamento (Nombre) VALUES ('Electronica')
INSERT INTO Departamento (Nombre) VALUES ('Limpieza')
INSERT INTO Departamento (Nombre) VALUES ('Salchichonería')
INSERT INTO Departamento (Nombre) VALUES ('Farmacia')
INSERT INTO Departamento (Nombre) VALUES ('Panadería')

CREATE TABLE Proveedor(
IdProveedor INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50) NOT NULL,
NumeroDeTelefono VARCHAR(50) NOT NULL,
Imagen VARCHAR(MAX)
)

INSERT INTO Proveedor (Nombre, NumeroDeTelefono, Imagen) VALUES ('Samsung','5511220030','')
INSERT INTO Proveedor (Nombre, NumeroDeTelefono, Imagen) VALUES ('AlEn','5511481030','')
INSERT INTO Proveedor (Nombre, NumeroDeTelefono, Imagen) VALUES ('FUD','5510306478','')
INSERT INTO Proveedor (Nombre, NumeroDeTelefono, Imagen) VALUES ('GenoLabs','5544116987','')
INSERT INTO Proveedor (Nombre, NumeroDeTelefono, Imagen) VALUES ('Bimbo','5599887744','')

CREATE TABLE Producto(
IdProducto INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50) NOT NULL,
Precio DECIMAL NOT NULL,
Stock INT,
Imagen VARCHAR(MAX),
IdProveedor INT FOREIGN KEY REFERENCES Proveedor(IdProveedor),
IdDepartamento INT FOREIGN KEY REFERENCES Departamento(IdDepartamento),
Descripcion VARCHAR(500) NOT NULL
)

CREATE PROCEDURE ProductoAdd
@Nombre VARCHAR(50),
@Precio DECIMAL,
@Stock INT,
@Imagen VARCHAR(MAX),
@IdProveedor INT,
@IdDepartamento INT,
@Descripcion VARCHAR(500)
AS
INSERT INTO Producto (Nombre, Precio, Stock, Imagen, IdProveedor, IdDepartamento, Descripcion) VALUES (@Nombre, @Precio, @Stock, @Imagen, @IdProveedor, @IdDepartamento, @Descripcion)

CREATE PROCEDURE ProductoUpdate
@IdProducto INT,
@Nombre VARCHAR(50),
@Precio DECIMAL,
@Stock INT,
@Imagen VARCHAR(MAX),
@IdProveedor INT,
@IdDepartamento INT,
@Descripcion VARCHAR(500)
AS
UPDATE Producto SET Nombre = @Nombre, Precio = @Precio, Stock = @Stock, Imagen = @Imagen, IdProveedor = @IdProveedor, IdDepartamento = @IdDepartamento, Descripcion = @Descripcion WHERE IdProducto = @IdProducto

CREATE PROCEDURE ProductoDelete
@IdProducto INT
AS
DELETE FROM Producto WHERE IdProducto = @IdProducto

CREATE PROCEDURE ProductoGetAll
AS
SELECT IdProducto, Producto.Nombre, Precio, Stock, Producto.Imagen, Producto.IdProveedor, Proveedor.Nombre AS 'NombreProveedor', Producto.IdDepartamento, Departamento.Nombre AS 'NombreDepartamento', Descripcion FROM Producto
INNER JOIN Proveedor ON Producto.IdProveedor = Proveedor.IdProveedor
INNER JOIN Departamento ON Producto.IdDepartamento = Departamento.IdDepartamento

CREATE PROCEDURE ProductoGetById
@IdProducto INT
AS
SELECT IdProducto, Producto.Nombre, Precio, Stock, Producto.Imagen, Producto.IdProveedor, Proveedor.Nombre AS 'NombreProveedor', Producto.IdDepartamento, Departamento.Nombre AS 'NombreDepartamento', Descripcion FROM Producto 
INNER JOIN Proveedor ON Producto.IdProveedor = Proveedor.IdProveedor
INNER JOIN Departamento ON Producto.IdDepartamento = Departamento.IdDepartamento
WHERE IdProducto = @IdProducto

CREATE PROCEDURE ProveedorAdd
@Nombre VARCHAR(50),
@NumeroDeTelefono VARCHAR(50),
@Imagen VARCHAR(MAX)
AS
INSERT INTO Proveedor (Nombre, NumeroDeTelefono, Imagen) VALUES (@Nombre, @NumeroDeTelefono, @Imagen)

CREATE PROCEDURE ProveedorUpdate
@IdProveedor INT,
@Nombre VARCHAR(50),
@NumeroDeTelefono VARCHAR(50),
@Imagen VARCHAR(MAX)
AS
UPDATE Proveedor SET Nombre = @Nombre, NumeroDeTelefono = @NumeroDeTelefono, Imagen = @Imagen WHERE IdProveedor = @IdProveedor

CREATE PROCEDURE ProveedorDelete
@IdProveedor INT
AS
DELETE FROM Proveedor WHERE IdProveedor = @IdProveedor

CREATE PROCEDURE ProveedorGetAll
AS
SELECT IdProveedor, Nombre, NumeroDeTelefono, Imagen FROM Proveedor

CREATE PROCEDURE ProveedorGetById
@IdProveedor INT
AS
SELECT IdProveedor, Nombre, NumeroDeTelefono, Imagen FROM Proveedor WHERE IdProveedor = @IdProveedor

CREATE PROCEDURE UsuarioAdd
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@FechaDeNacimiento DATE,
@CURP VARCHAR(50),
@Genero VARCHAR(50),
@UserName VARCHAR(50),
@Email VARCHAR(50),
@NumeroDeTelefono VARCHAR(50),
@Celular VARCHAR(50),
@Password VARCHAR(50),
@Imagen VARCHAR(MAX),
@IdRol INT
AS
INSERT INTO Usuario (Nombre, ApellidoPaterno, ApellidoMaterno, FechaDeNacimiento, CURP, Genero, UserName, Email, NumeroDeTelefono, Celular, [Password], Imagen, IdRol) VALUES (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechaDeNacimiento, @CURP, @Genero, @UserName, @Email, @NumeroDeTelefono, @Celular, @Password, @Imagen, @IdRol)

CREATE PROCEDURE UsuarioUpdate
@IdUsuario INT,
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@FechaDeNacimiento DATE,
@CURP VARCHAR(50),
@Genero VARCHAR(50),
@UserName VARCHAR(50),
@Email VARCHAR(50),
@NumeroDeTelefono VARCHAR(50),
@Celular VARCHAR(50),
@Password VARCHAR(50),
@Imagen VARCHAR(MAX),
@IdRol INT
AS
UPDATE Usuario SET Nombre = @Nombre, ApellidoPaterno = @ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno, FechaDeNacimiento = @FechaDeNacimiento, CURP = @CURP, Genero = @Genero, UserName = @UserName, Email = @Email, NumeroDeTelefono = @NumeroDeTelefono, Celular = @Celular, [Password] = @Password, Imagen = @Imagen, IdRol = @IdRol WHERE IdUsuario = @IdUsuario

CREATE PROCEDURE UsuarioDelete
@IdUsuario INT
AS
DELETE FROM Usuario WHERE IdUsuario = @IdUsuario

CREATE PROCEDURE UsuarioGetAll
AS
SELECT IdUsuario, Usuario.Nombre, ApellidoPaterno, ApellidoMaterno, FechaDeNacimiento, CURP, Genero, UserName, Email, NumeroDeTelefono, Celular, [Password], Imagen, Usuario.IdRol, Rol.Nombre AS 'NombreRol' FROM Usuario
INNER JOIN Rol ON Usuario.IdRol = Rol.IdRol

CREATE PROCEDURE UsuarioGetById
@IdUsuario INT
AS
SELECT IdUsuario, Usuario.Nombre, ApellidoPaterno, ApellidoMaterno, FechaDeNacimiento, CURP, Genero, UserName, Email, NumeroDeTelefono, Celular, [Password], Imagen, Usuario.IdRol, Rol.Nombre AS 'NombreRol' FROM Usuario
INNER JOIN Rol ON Usuario.IdRol = Rol.IdRol
WHERE IdUsuario = @IdUsuario

CREATE PROCEDURE RolGetAll
AS
SELECT IdRol, Nombre FROM Rol

CREATE PROCEDURE DepartamentoGetAll
AS
SELECT IdDepartamento, Nombre FROM Departamento

CREATE PROCEDURE UsuarioGetByUsername
@UserName VARCHAR(50)
AS
SELECT IdUsuario, Usuario.Nombre, ApellidoPaterno, ApellidoMaterno, FechaDeNacimiento, CURP, Genero, UserName, Email, NumeroDeTelefono, Celular, [Password], Imagen, Usuario.IdRol, Rol.Nombre AS 'NombreRol' FROM Usuario
INNER JOIN Rol ON Usuario.IdRol = Rol.IdRol
WHERE UserName = @UserName
