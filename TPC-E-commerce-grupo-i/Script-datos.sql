USE master;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ECommerceDB')
BEGIN
    CREATE DATABASE ECommerceDB;
END
GO

USE ECommerceDB;
GO

-- 0. ELIMINACIÓN DE TABLAS SI YA EXISTEN (En orden inverso a las FKs)
DROP TABLE IF EXISTS HistorialEstadoPedido;
DROP TABLE IF EXISTS ItemsPedido;
DROP TABLE IF EXISTS Pedidos;
DROP TABLE IF EXISTS FormasPago;
DROP TABLE IF EXISTS FormasEntrega;
DROP TABLE IF EXISTS Direcciones;
DROP TABLE IF EXISTS Clientes;
DROP TABLE IF EXISTS Articulos;
DROP TABLE IF EXISTS Categorias;
DROP TABLE IF EXISTS Marcas;
GO

-- 1. TABLAS DE CATÁLOGO
CREATE TABLE Marcas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(50) NOT NULL
);
GO

CREATE TABLE Categorias (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(50) NOT NULL
);
GO

CREATE TABLE Articulos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(50) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(255) NULL,
    Precio DECIMAL(18,2) NOT NULL,
    Stock INT NOT NULL DEFAULT 0,
    ImagenUrl VARCHAR(1000) NULL,
    IdMarca INT NULL FOREIGN KEY REFERENCES Marcas(Id),
    IdCategoria INT NULL FOREIGN KEY REFERENCES Categorias(Id),
    Activo BIT NOT NULL DEFAULT 1
);
GO

-- 2. TABLAS DE CLIENTES Y LOGÍSTICA
CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    Telefono VARCHAR(30) NULL,
    Contrasena VARCHAR(100) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE Direcciones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdCliente INT NOT NULL FOREIGN KEY REFERENCES Clientes(Id),
    Calle VARCHAR(100) NOT NULL,
    Numero VARCHAR(20) NOT NULL,
    Piso VARCHAR(10) NULL,
    Departamento VARCHAR(10) NULL,
    CodigoPostal VARCHAR(20) NOT NULL,
    Localidad VARCHAR(100) NOT NULL,
    Provincia VARCHAR(100) NOT NULL,
    Alias VARCHAR(50) NULL,
    EsPredeterminada BIT NOT NULL DEFAULT 0,
    Activo BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE FormasEntrega (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(100) NOT NULL,
    Costo DECIMAL(18,2) NOT NULL DEFAULT 0.00,
    Estado BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE FormasPago (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(100) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);
GO

-- 3. TABLAS DE VENTAS Y PEDIDOS
CREATE TABLE Pedidos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdCliente INT NOT NULL FOREIGN KEY REFERENCES Clientes(Id),
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    IdFormaEntrega INT NOT NULL FOREIGN KEY REFERENCES FormasEntrega(Id),
    IdFormaPago INT NOT NULL FOREIGN KEY REFERENCES FormasPago(Id),
    IdDireccionEnvio INT NULL FOREIGN KEY REFERENCES Direcciones(Id),
    Estado VARCHAR(30) NOT NULL DEFAULT 'Pendiente',
    Total DECIMAL(18,2) NOT NULL
);
GO

CREATE TABLE ItemsPedido (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdPedido INT NOT NULL FOREIGN KEY REFERENCES Pedidos(Id),
    IdArticulo INT NOT NULL FOREIGN KEY REFERENCES Articulos(Id),
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL
);
GO

CREATE TABLE HistorialEstadoPedido (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdPedido INT NOT NULL FOREIGN KEY REFERENCES Pedidos(Id),
    EstadoAnterior VARCHAR(30) NULL,
    EstadoNuevo VARCHAR(30) NOT NULL,
    FechaCambio DATETIME NOT NULL DEFAULT GETDATE(),
    Observaciones VARCHAR(255) NULL
);
GO

-- 4. DATOS DE PRUEBA INICIALES
INSERT INTO Marcas (Descripcion) VALUES ('Samsung'), ('Philips'), ('Logitech'), ('Xiaomi');
INSERT INTO Categorias (Descripcion) VALUES ('Audio & Gadgets'), ('Pequeños Electro'), ('Informática & Gaming');
INSERT INTO FormasEntrega (Descripcion, Costo, Estado) VALUES ('Retiro en Local', 0.00, 1), ('Envío a Domicilio', 2500.00, 1);
INSERT INTO FormasPago (Descripcion, Estado) VALUES ('Efectivo / Transferencia', 1), ('Tarjeta de Crédito / Débito', 1);

INSERT INTO Articulos (Codigo, Nombre, Descripcion, Precio, Stock, ImagenUrl, IdMarca, IdCategoria, Activo) VALUES 
('ART001', 'Auriculares Inalámbricos', 'Auriculares Bluetooth con cancelación de ruido', 35000.00, 15, 'https://images.unsplash.com/photo-1505740420928-5e560c06d30e?auto=format&fit=crop&w=500&q=80', 1, 1, 1),
('ART002', 'Teclado Mecánico RGB', 'Teclado mecánico switch red en español', 45000.00, 10, 'https://images.unsplash.com/photo-1587829741301-dc798b83add3?auto=format&fit=crop&w=500&q=80', 3, 3, 1),
('ART003', 'Mouse Gamer', 'Mouse óptico con 6 botones programables', 18000.00, 25, 'https://images.unsplash.com/photo-1527864550417-7fd91fc51a46?auto=format&fit=crop&w=500&q=80', 3, 3, 1),
('ART004', 'Cafetera Espresso', 'Cafetera automática con espumador de leche', 120000.00, 5, 'https://images.unsplash.com/photo-1570968915860-54d5c301fa9f?auto=format&fit=crop&w=500&q=80', 2, 2, 1);
GO