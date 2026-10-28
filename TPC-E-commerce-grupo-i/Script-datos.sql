-- Script de prueba inicial para TPC E-commerce (Hito 1)

CREATE DATABASE ECommerceDB;
GO

USE ECommerceDB;
GO

-- Tabla de Artículos de prueba
CREATE TABLE Articulos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(50),
    Nombre VARCHAR(100),
    Descripcion VARCHAR(255),
    Precio DECIMAL(18,2),
    Stock INT
);
GO

-- Inserción de productos ficticios
INSERT INTO Articulos (Codigo, Nombre, Descripcion, Precio, Stock) VALUES 
('ART001', 'Auriculares Inalámbricos', 'Auriculares Bluetooth con cancelación de ruido', 35000.00, 15),
('ART002', 'Teclado Mecánico RGB', 'Teclado mecánico switch red en español', 45000.00, 10),
('ART003', 'Mouse Gamer', 'Mouse óptico con 6 botones programables', 18000.00, 25),
('ART004', 'Monitor 24 Pulgadas', 'Monitor LED Full HD 75Hz', 120000.00, 5);
GO