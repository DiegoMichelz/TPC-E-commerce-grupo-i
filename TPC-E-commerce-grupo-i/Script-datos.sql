USE master;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ECommerceDB')
BEGIN
    CREATE DATABASE ECommerceDB;
END
GO

USE ECommerceDB;
GO

-- 0. ELIMINACIÓN DE TABLAS SI YA EXISTEN (En orden inverso por las Foreign Keys)
DROP TABLE IF EXISTS HistorialEstadoPedido;
DROP TABLE IF EXISTS ItemsPedido;
DROP TABLE IF EXISTS Imagenes;
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
    IdMarca INT NULL FOREIGN KEY REFERENCES Marcas(Id),
    IdCategoria INT NULL FOREIGN KEY REFERENCES Categorias(Id),
    Activo BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE Imagenes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdArticulo INT NOT NULL FOREIGN KEY REFERENCES Articulos(Id),
    ImagenUrl VARCHAR(1000) NOT NULL
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

-- 4. DATOS INICIALES
INSERT INTO Marcas (Descripcion) VALUES 
('Samsung'), ('Philips'), ('Logitech'), ('Xiaomi'), ('JBL');
GO

INSERT INTO Categorias (Descripcion) VALUES 
('Audio & Gadgets'), ('Pequeños Electro'), ('Informática & Gaming'), ('Hogar Inteligente');
GO

INSERT INTO FormasEntrega (Descripcion, Costo, Estado) VALUES 
('Retiro en Local', 0.00, 1), ('Envío a Domicilio', 2500.00, 1);
GO

INSERT INTO FormasPago (Descripcion, Estado) VALUES 
('Efectivo / Transferencia', 1), ('Tarjeta de Crédito / Débito', 1);
GO

-- 5. ARTÍCULOS (20 productos ordenados por ID estricto)
INSERT INTO Articulos (Codigo, Nombre, Descripcion, Precio, Stock, IdMarca, IdCategoria, Activo) VALUES 
('ART001', 'Auriculares Inalámbricos', 'Auriculares Bluetooth con cancelación de ruido', 35000.00, 15, 5, 1, 1),
('ART002', 'Parlante Bluetooth Portátil', 'Parlante resistente al agua IPX7', 28000.00, 20, 5, 1, 1),
('ART003', 'Smartwatch Deportivo', 'Reloj inteligente con monitor de ritmo cardíaco', 49000.00, 12, 4, 1, 1),
('ART004', 'Auriculares de Diadema', 'Sonido estéreo de alta fidelidad con micrófono', 22000.00, 18, 2, 1, 1),
('ART005', 'Power Bank 10000mAh', 'Cargador portátil con carga rápida USB-C', 19000.00, 25, 4, 1, 1),
('ART006', 'Cafetera Espresso', 'Cafetera automática con espumador de leche', 120000.00, 5, 2, 2, 1),
('ART007', 'Licuadora de Mano', 'Licuadora de acero inoxidable 600W', 55000.00, 10, 2, 2, 1),
('ART008', 'Tostadora Eléctrica', 'Ranura doble con niveles de tostado ajustables', 32000.00, 14, 2, 2, 1),
('ART009', 'Pava Eléctrica Digital', 'Control de temperatura y corte automático', 41000.00, 16, 2, 2, 1),
('ART010', 'Batidora de Pedestal', 'Bowl de acero y múltiples velocidades', 89000.00, 8, 2, 2, 1),
('ART011', 'Teclado Mecánico RGB', 'Teclado mecánico switch red en español', 45000.00, 10, 3, 3, 1),
('ART012', 'Mouse Gamer', 'Mouse óptico con 6 botones programables', 18000.00, 25, 3, 3, 1),
('ART013', 'Monitor 24 pulgadas Full HD', 'Monitor LED IPS 75Hz con puerto HDMI', 185000.00, 6, 1, 3, 1),
('ART014', 'Notebook Office 15.6', 'Procesador Core i3, 8GB RAM, SSD 256GB', 350000.00, 4, 1, 3, 1),
('ART015', 'Pendrive 64GB USB 3.0', 'Memoria portátil de alta velocidad', 9500.00, 30, 3, 3, 1),
('ART016', 'Foco LED Smart RGB', 'Bombilla inteligente compatible con asistentes de voz', 12000.00, 40, 4, 4, 1),
('ART017', 'Cámara de Seguridad Wi-Fi', 'Cámara de vigilancia 1080p con visión nocturna', 42000.00, 11, 4, 4, 1),
('ART018', 'Enchufe Inteligente Wi-Fi', 'Controla tus electrodomésticos desde el celular', 15000.00, 22, 4, 4, 1),
('ART019', 'Tira LED RGB Smart', '5 metros de luces multicolores con app', 21000.00, 19, 4, 4, 1),
('ART020', 'Sensor de Movimiento Inalámbrico', 'Notificaciones de alerta en tiempo real', 18500.00, 15, 4, 4, 1);
GO

-- 6. DOS IMÁGENES CORRECTAS Y VERIFICADAS POR CADA ARTÍCULO
INSERT INTO Imagenes (IdArticulo, ImagenUrl) VALUES 
-- ART001: Auriculares Inalámbricos (over-ear)
(1, 'https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=500&q=80'),
(1, 'https://images.unsplash.com/photo-1546435770-a3e426bf472b?w=500&q=80'),

-- ART002: Parlante Bluetooth Portátil
(2, 'https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?w=500&q=80'),
(2, 'https://cdn.pixabay.com/photo/2014/04/09/08/16/speaker-319841_640.jpg'),

-- ART003: Smartwatch Deportivo
(3, 'https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=500&q=80'),
(3, 'https://images.unsplash.com/photo-1579586337278-3befd40fd17a?w=500&q=80'),

-- ART004: Auriculares de Diadema
(4, 'https://images.unsplash.com/photo-1546435770-a3e426bf472b?w=500&q=80'),
(4, 'https://images.unsplash.com/photo-1484704849700-f032a568e944?w=500&q=80'),

-- ART005: Power Bank 10000mAh
(5, 'https://cdn.pixabay.com/photo/2015/11/18/17/54/battery-1049664_640.jpg'),
(5, 'https://images.unsplash.com/photo-1609592424104-94d5218d8d3f?w=500&q=80'),

-- ART006: Cafetera Espresso
(6, 'https://images.pexels.com/photos/302899/pexels-photo-302899.jpeg?auto=compress&cs=tinysrgb&w=500'),
(6, 'https://images.pexels.com/photos/312418/pexels-photo-312418.jpeg?auto=compress&cs=tinysrgb&w=500'),

-- ART007: Licuadora de Mano (immersion blender)
(7, 'https://images.pexels.com/photos/6605167/pexels-photo-6605167.jpeg?auto=compress&cs=tinysrgb&w=500'),
(7, 'https://images.pexels.com/photos/20258351/pexels-photo-20258351.jpeg?auto=compress&cs=tinysrgb&w=500'),

-- ART008: Tostadora Eléctrica
(8, 'https://images.unsplash.com/photo-1525351484163-7529414344d8?w=500&q=80'),
(8, 'https://images.pexels.com/photos/10965747/pexels-photo-10965747.jpeg?auto=compress&cs=tinysrgb&w=500'),

-- ART009: Pava Eléctrica Digital
(9, 'https://images.pexels.com/photos/30319670/pexels-photo-30319670.jpeg?auto=compress&cs=tinysrgb&w=500'),
(9, 'https://images.pexels.com/photos/885455/pexels-photo-885455.jpeg?auto=compress&cs=tinysrgb&w=500'),

-- ART010: Batidora de Pedestal
(10, 'https://images.pexels.com/photos/27009438/pexels-photo-27009438.jpeg?auto=compress&cs=tinysrgb&w=500'),
(10, 'https://images.pexels.com/photos/1450907/pexels-photo-1450907.jpeg?auto=compress&cs=tinysrgb&w=500'),

-- ART011: Teclado Mecánico RGB
(11, 'https://images.pexels.com/photos/40011104/pexels-photo-40011104.jpeg?auto=compress&cs=tinysrgb&w=500'),
(11, 'https://images.pexels.com/photos/33888380/pexels-photo-33888380.jpeg?auto=compress&cs=tinysrgb&w=500'),

-- ART012: Mouse Gamer
(12, 'https://images.unsplash.com/photo-1527864550417-7fd91fc51a46?w=500&q=80'),
(12, 'https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?w=500&q=80'),

-- ART013: Monitor 24 pulgadas Full HD
(13, 'https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?w=500&q=80'),
(13, 'https://images.unsplash.com/photo-1593640408182-31c70c8268f5?w=500&q=80'),

-- ART014: Notebook Office 15.6
(14, 'https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=500&q=80'),
(14, 'https://images.unsplash.com/photo-1517336714731-489689fd1ca8?w=500&q=80'),

-- ART015: Pendrive 64GB USB 3.0
(15, 'https://images.unsplash.com/photo-1593642632823-8f785ba67e45?w=500&q=80'),
(15, 'https://images.unsplash.com/photo-1544652478-6553e5828e1d?w=500&q=80'),

-- ART016: Foco LED Smart RGB
(16, 'https://images.unsplash.com/photo-1550985616-1141154c1e5e?w=500&q=80'),
(16, 'https://images.unsplash.com/photo-1513506003901-1e6a229e2d15?w=500&q=80'),

-- ART017: Cámara de Seguridad Wi-Fi
(17, 'https://images.unsplash.com/photo-1557324232-b8917d4c3dcb?w=500&q=80'),
(17, 'https://images.unsplash.com/photo-1584438784894-089d6a62b8fa?w=500&q=80'),

-- ART018: Enchufe Inteligente Wi-Fi
(18, 'https://images.unsplash.com/photo-1558089687-f282ffcbc126?w=500&q=80'),
(18, 'https://images.unsplash.com/photo-1513519245088-0e12902e5a38?w=500&q=80'),

-- ART019: Tira LED RGB Smart
(19, 'https://images.unsplash.com/photo-1565849904461-04a58ad377e0?w=500&q=80'),
(19, 'https://images.unsplash.com/photo-1550751827-4bd374c3f58b?w=500&q=80'),

-- ART020: Sensor de Movimiento Inalámbrico
(20, 'https://images.unsplash.com/photo-1558002038-1055907df827?w=500&q=80'),
(20, 'https://images.unsplash.com/photo-1563630423388-5d8387cf2141?w=500&q=80');
GO