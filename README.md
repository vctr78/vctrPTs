# VisiblePT

-- =========================================================================
-- Hacer uso de los siguientes scripts para la correcta creacion de la DB
-- =========================================================================

-- =============================================
-- Crear base de datos
-- =============================================
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'StoreDB')
BEGIN
    CREATE DATABASE StoreDB;
    PRINT('Database StoreDB created successfully.');
END
ELSE
BEGIN
    PRINT('Database StoreDB already exists.');
END
GO

USE StoreDB;
GO

-- =============================================
-- Crear tabla Products
-- =============================================

IF OBJECT_ID('dbo.Products', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Products;
END
GO

CREATE TABLE dbo.Products
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    DiscountPercent INT NOT NULL CONSTRAINT DF_Products_DiscountPercent DEFAULT(0),
    ImageURL NVARCHAR(500) NOT NULL,
    DateAdd DATETIME NOT NULL CONSTRAINT DF_Products_DateAdd DEFAULT(GETDATE()),

    -- Computed column (matches EF configuration)
    DiscountPrice AS (Price * (1 - (DiscountPercent / 100.0)))
);
GO

-- =============================================
-- Crear procedimiento almacenado SP_GetProducts
-- =============================================

IF OBJECT_ID('dbo.SP_GetProducts', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.SP_GetProducts;
END
GO

CREATE PROCEDURE dbo.SP_GetProducts
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        Description,
        Price,
        DiscountPercent,
        ImageURL,
        DateAdd,
        Price * (1 - (DiscountPercent / 100.0)) AS DiscountPrice
    FROM dbo.Products
    ORDER BY Name;
END
GO

-- =============================================
-- Datos de prueba (opcional)
-- =============================================
INSERT INTO dbo.Products (Name, Description, Price, DiscountPercent, ImageURL)
VALUES 
('Laptop X100', 'Laptop de alto rendimiento', 1200.00, 10, 'https://example.com/img/laptop.jpg'),
('Mouse Óptico', 'Mouse inalámbrico', 25.50, 0, 'https://example.com/img/mouse.jpg'),
('Monitor 27"', 'Monitor IPS 27 pulgadas', 350.99, 15, 'https://example.com/img/monitor.jpg');
GO

-- =============================================
-- Probar procedimiento
-- =============================================
EXEC dbo.SP_GetProducts;
GO
