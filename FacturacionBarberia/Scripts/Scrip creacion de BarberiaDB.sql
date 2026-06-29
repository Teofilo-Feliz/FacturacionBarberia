CREATE DATABASE BarberiaDB;
GO



USE BarberiaDB;
GO


-- TABLA USUARIOS


CREATE TABLE Usuarios
(
    UsuarioId INT IDENTITY(1,1) PRIMARY KEY,

    Nombre NVARCHAR(100) NOT NULL,

    UserName NVARCHAR(50) NOT NULL UNIQUE,

    PasswordHash NVARCHAR(255) NOT NULL,

    Rol VARCHAR(20) NOT NULL
    CONSTRAINT CK_Usuarios_Rol
    CHECK (Rol IN ('Administrador','Cajero')),

    Estado VARCHAR(15) NOT NULL DEFAULT 'Activo'
    CONSTRAINT CK_Usuarios_Estado
    CHECK (Estado IN ('Activo','Inactivo')),

    FechaCreacion DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),

    UsuarioCreacion INT NULL,

    FechaModificacion DATETIME2 NULL,

    UsuarioModificacion INT NULL,

    FechaEliminacion DATETIME2 NULL,

    UsuarioEliminacion INT NULL,

    EstaEliminado BIT NOT NULL DEFAULT(0),

    CONSTRAINT FK_Usuarios_UsuarioCreacion
        FOREIGN KEY (UsuarioCreacion)
        REFERENCES Usuarios(UsuarioId),

    CONSTRAINT FK_Usuarios_UsuarioModificacion
        FOREIGN KEY (UsuarioModificacion)
        REFERENCES Usuarios(UsuarioId),

    CONSTRAINT FK_Usuarios_UsuarioEliminacion
        FOREIGN KEY (UsuarioEliminacion)
        REFERENCES Usuarios(UsuarioId)
);
GO


-- TABLA CLIENTES
CREATE TABLE Clientes
(
    ClienteId INT IDENTITY(1,1) PRIMARY KEY,

    Nombre NVARCHAR(100) NOT NULL,

    Telefono NVARCHAR(20) NULL UNIQUE,

    Correo NVARCHAR(100) NULL UNIQUE,

    CONSTRAINT CK_Clientes_Correo
    CHECK (
        Correo IS NULL
        OR Correo LIKE '%@%.%'
    ),

    Estado VARCHAR(15) NOT NULL DEFAULT 'Activo'
    CONSTRAINT CK_Clientes_Estado
    CHECK (Estado IN ('Activo','Inactivo')),

    FechaCreacion DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),

    UsuarioCreacion INT NULL,

    FechaModificacion DATETIME2 NULL,

    UsuarioModificacion INT NULL,

    FechaEliminacion DATETIME2 NULL,

    UsuarioEliminacion INT NULL,

    EstaEliminado BIT NOT NULL DEFAULT(0),

    CONSTRAINT FK_Clientes_UsuarioCreacion
        FOREIGN KEY (UsuarioCreacion)
        REFERENCES Usuarios(UsuarioId),

    CONSTRAINT FK_Clientes_UsuarioModificacion
        FOREIGN KEY (UsuarioModificacion)
        REFERENCES Usuarios(UsuarioId),

    CONSTRAINT FK_Clientes_UsuarioEliminacion
        FOREIGN KEY (UsuarioEliminacion)
        REFERENCES Usuarios(UsuarioId)
);
GO


-- TABLA SERVICIOS


CREATE TABLE Servicios
(
    ServicioId INT IDENTITY(1,1) PRIMARY KEY,

    Nombre NVARCHAR(100) NOT NULL UNIQUE,

    Precio DECIMAL(10,2) NOT NULL,

    Estado VARCHAR(15) NOT NULL DEFAULT 'Activo',

    CONSTRAINT CK_Servicios_Estado
    CHECK (Estado IN ('Activo','Inactivo')),

    CONSTRAINT CK_Servicios_Precio
    CHECK (Precio > 0),

    FechaCreacion DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),

    UsuarioCreacion INT NULL,

    FechaModificacion DATETIME2 NULL,

    UsuarioModificacion INT NULL,

    FechaEliminacion DATETIME2 NULL,

    UsuarioEliminacion INT NULL,

    EstaEliminado BIT NOT NULL DEFAULT(0),

    CONSTRAINT FK_Servicios_UsuarioCreacion
        FOREIGN KEY (UsuarioCreacion)
        REFERENCES Usuarios(UsuarioId),

    CONSTRAINT FK_Servicios_UsuarioModificacion
        FOREIGN KEY (UsuarioModificacion)
        REFERENCES Usuarios(UsuarioId),

    CONSTRAINT FK_Servicios_UsuarioEliminacion
        FOREIGN KEY (UsuarioEliminacion)
        REFERENCES Usuarios(UsuarioId)
);
GO


-- TABLA FACTURAS


CREATE TABLE Facturas
(
    FacturaId INT IDENTITY(1,1) PRIMARY KEY,

    FechaFactura DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),

    ClienteId INT NOT NULL,

    UsuarioId INT NOT NULL,

    Total DECIMAL(10,2) NOT NULL DEFAULT 0,

    FormaPago VARCHAR(20) NOT NULL,

    Observaciones NVARCHAR(500) NULL,

    CONSTRAINT CK_Facturas_Total
    CHECK (Total >= 0),

    CONSTRAINT CK_Facturas_FormaPago
    CHECK (FormaPago IN ('Efectivo','Tarjeta','Transferencia')),

    FechaCreacion DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),

    UsuarioCreacion INT NULL,

    FechaModificacion DATETIME2 NULL,

    UsuarioModificacion INT NULL,

    FechaEliminacion DATETIME2 NULL,

    UsuarioEliminacion INT NULL,

    EstaEliminado BIT NOT NULL DEFAULT(0),

    CONSTRAINT FK_Facturas_Clientes
        FOREIGN KEY (ClienteId)
        REFERENCES Clientes(ClienteId),

    CONSTRAINT FK_Facturas_Usuarios
        FOREIGN KEY (UsuarioId)
        REFERENCES Usuarios(UsuarioId),

    CONSTRAINT FK_Facturas_UsuarioCreacion
        FOREIGN KEY (UsuarioCreacion)
        REFERENCES Usuarios(UsuarioId),

    CONSTRAINT FK_Facturas_UsuarioModificacion
        FOREIGN KEY (UsuarioModificacion)
        REFERENCES Usuarios(UsuarioId)
);
GO

-- TABLA DETALLE FACTURAS


CREATE TABLE DetalleFacturas
(
    DetalleFacturaId INT IDENTITY(1,1) PRIMARY KEY,

    FacturaId INT NOT NULL,

    ServicioId INT NOT NULL,

    Precio DECIMAL(10,2) NOT NULL,

    Cantidad INT NOT NULL DEFAULT 1,

    SubTotal AS (Precio * Cantidad) PERSISTED,

    CONSTRAINT CK_DetalleFacturas_Precio
    CHECK (Precio > 0),

    CONSTRAINT CK_DetalleFacturas_Cantidad
    CHECK (Cantidad > 0),

    CONSTRAINT FK_DetalleFacturas_Facturas
    FOREIGN KEY (FacturaId)
    REFERENCES Facturas(FacturaId),

    CONSTRAINT FK_DetalleFacturas_Servicios
    FOREIGN KEY (ServicioId)
    REFERENCES Servicios(ServicioId)
);
GO


CREATE INDEX IX_Facturas_ClienteId
ON Facturas(ClienteId);

CREATE INDEX IX_Facturas_UsuarioId
ON Facturas(UsuarioId);

CREATE INDEX IX_DetalleFacturas_FacturaId
ON DetalleFacturas(FacturaId);

CREATE INDEX IX_DetalleFacturas_ServicioId
ON DetalleFacturas(ServicioId);
select * from dbo.Usuarios where UsuarioId = 2


-- SERVICIOS INICIALES

Insert Into Usuarios (


INSERT INTO Servicios (Nombre, Precio, Estado, FechaCreacion,UsuarioCreacion)
VALUES
('Corte Clásico', 350,'Activo','09/06/2026',1),
('Afeitado con Navaja', 250,'Activo','09/06/2026',1),
('Corte + Barba', 550,'Activo','09/06/2026',1),
('Diseño de Barba', 300,'Activo','09/06/2026',1);
GO

Select * from dbo.Usuarios


--BEGIN TRANSACTION;

--SELECT *
--FROM dbo.Usuarios
--WHERE UsuarioId= 1;

--DELETE FROM dbo.Usuarios
--WHERE UsuarioId = 1;

---- Si todo está bien
--COMMIT TRANSACTION;

---- Si necesitas revertir
---- ROLLBACK TRANSACTION;

SELECT TOP 1 *
FROM Usuarios

SELECT
    COLUMN_NAME,
    DATA_TYPE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Usuarios';

select * from dbo.Clientes
select * from dbo.Servicios


-- Agregar campo EstadoFactura

ALTER TABLE Facturas
ADD EstadoFactura VARCHAR(20) NOT NULL
CONSTRAINT CK_Factura_EstadoFactura
CHECK (EstadoFactura IN ('Activa','Anulada'))
DEFAULT 'Activa';

select * from dbo.Facturas
select * from dbo.Clientes
select * from dbo.DetalleFacturas
select * from dbo.facturas

-- Scrips de procedimientos almacenados
CREATE PROCEDURE Dashboard_ObtenerResumen
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        FacturadoHoy =
            ISNULL(
                (
                    SELECT SUM(Total)
                    FROM Facturas
                    WHERE EstadoFactura = 'Activa'
                    AND CAST(FechaFactura AS DATE) = CAST(GETDATE() AS DATE)
                ),0),

        FacturadoSemana =
            ISNULL(
                (
                    SELECT SUM(Total)
                    FROM Facturas
                    WHERE EstadoFactura = 'Activa'
                    AND DATEPART(YEAR, FechaFactura) = DATEPART(YEAR, GETDATE())
                    AND DATEPART(WEEK, FechaFactura) = DATEPART(WEEK, GETDATE())
                ),0),

        FacturadoMes =
            ISNULL(
                (
                    SELECT SUM(Total)
                    FROM Facturas
                    WHERE EstadoFactura = 'Activa'
                    AND YEAR(FechaFactura) = YEAR(GETDATE())
                    AND MONTH(FechaFactura) = MONTH(GETDATE())
                ),0),

        ServiciosMes =
            ISNULL(
                (
                    SELECT SUM(fd.Cantidad)
                    FROM DetalleFacturas fd
                    INNER JOIN Facturas f
                        ON fd.FacturaId = f.FacturaId
                    WHERE f.EstadoFactura = 'Activa'
                    AND YEAR(f.FechaFactura) = YEAR(GETDATE())
                    AND MONTH(f.FechaFactura) = MONTH(GETDATE())
                ),0);
END
GO

CREATE PROCEDURE Dashboard_ObtenerIngresosSemana
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Dia = DATENAME(WEEKDAY, FechaFactura),
        Total = SUM(Total)
    FROM Facturas
    WHERE EstaEliminado = 0
        AND EstadoFactura = 'Activa'
        AND FechaFactura >= DATEADD(DAY, -7, GETDATE())
    GROUP BY
        DATENAME(WEEKDAY, FechaFactura),
        DATEPART(WEEKDAY, FechaFactura)
    ORDER BY DATEPART(WEEKDAY, FechaFactura);
END
GO

CREATE PROCEDURE Dashboard_ObtenerIngresosMeses
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Mes = DATENAME(MONTH, FechaFactura),
        Total = SUM(Total)
    FROM Facturas
    WHERE EstaEliminado = 0
        AND EstadoFactura = 'Activa'
        AND YEAR(FechaFactura) = YEAR(GETDATE())
    GROUP BY
        DATENAME(MONTH, FechaFactura),
        MONTH(FechaFactura)
    ORDER BY MONTH(FechaFactura);
END
GO

CREATE PROCEDURE Dashboard_ObtenerTopServicios
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 5
        s.Nombre AS Servicio,
        Cantidad = SUM(df.Cantidad)
    FROM DetalleFacturas df
    INNER JOIN Facturas f ON f.FacturaId = df.FacturaId
    INNER JOIN Servicios s ON s.ServicioId = df.ServicioId
    WHERE f.EstaEliminado = 0
        AND f.EstadoFactura = 'Activa'
        AND s.EstaEliminado = 0
    GROUP BY s.Nombre
    ORDER BY SUM(df.Cantidad) DESC;
END
GO

-- UPDATE: propina y retiros

IF COL_LENGTH('dbo.Facturas', 'Propina') IS NULL
BEGIN
    ALTER TABLE Facturas
    ADD Propina DECIMAL(10,2) NOT NULL
        CONSTRAINT DF_Facturas_Propina DEFAULT(0);

    ALTER TABLE Facturas
    ADD CONSTRAINT CK_Facturas_Propina
    CHECK (Propina >= 0);
END
GO

IF OBJECT_ID('dbo.Retiros', 'U') IS NULL
BEGIN
    CREATE TABLE Retiros
    (
        RetiroId INT IDENTITY(1,1) PRIMARY KEY,
        FechaRetiro DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
        UsuarioId INT NOT NULL,
        Monto DECIMAL(10,2) NOT NULL,
        Motivo NVARCHAR(150) NOT NULL,
        Observacion NVARCHAR(500) NULL,
        FechaCreacion DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
        UsuarioCreacion INT NULL,
        FechaModificacion DATETIME2 NULL,
        UsuarioModificacion INT NULL,
        FechaEliminacion DATETIME2 NULL,
        UsuarioEliminacion INT NULL,
        EstaEliminado BIT NOT NULL DEFAULT(0),
        CONSTRAINT CK_Retiros_Monto CHECK (Monto > 0),
        CONSTRAINT FK_Retiros_Usuarios FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UsuarioId),
        CONSTRAINT FK_Retiros_UsuarioCreacion FOREIGN KEY (UsuarioCreacion) REFERENCES Usuarios(UsuarioId),
        CONSTRAINT FK_Retiros_UsuarioModificacion FOREIGN KEY (UsuarioModificacion) REFERENCES Usuarios(UsuarioId),
        CONSTRAINT FK_Retiros_UsuarioEliminacion FOREIGN KEY (UsuarioEliminacion) REFERENCES Usuarios(UsuarioId)
    );
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_Facturas_FechaFactura'
      AND object_id = OBJECT_ID('Facturas')
)
BEGIN
    CREATE INDEX IX_Facturas_FechaFactura
    ON Facturas(FechaFactura);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_Retiros_FechaRetiro'
      AND object_id = OBJECT_ID('Retiros')
)
BEGIN
    CREATE INDEX IX_Retiros_FechaRetiro
    ON Retiros(FechaRetiro);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_Retiros_UsuarioId'
      AND object_id = OBJECT_ID('Retiros')
)
BEGIN
    CREATE INDEX IX_Retiros_UsuarioId
    ON Retiros(UsuarioId);
END
GO

CREATE OR ALTER PROCEDURE Dashboard_ObtenerResumen
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        FacturadoHoy =
            ISNULL(
                (
                    SELECT SUM(Total + ISNULL(Propina, 0))
                    FROM Facturas
                    WHERE EstadoFactura = 'Activa'
                    AND CAST(FechaFactura AS DATE) = CAST(GETDATE() AS DATE)
                ),0),

        FacturadoSemana =
            ISNULL(
                (
                    SELECT SUM(Total + ISNULL(Propina, 0))
                    FROM Facturas
                    WHERE EstadoFactura = 'Activa'
                    AND DATEPART(YEAR, FechaFactura) = DATEPART(YEAR, GETDATE())
                    AND DATEPART(WEEK, FechaFactura) = DATEPART(WEEK, GETDATE())
                ),0),

        FacturadoMes =
            ISNULL(
                (
                    SELECT SUM(Total + ISNULL(Propina, 0))
                    FROM Facturas
                    WHERE EstadoFactura = 'Activa'
                    AND YEAR(FechaFactura) = YEAR(GETDATE())
                    AND MONTH(FechaFactura) = MONTH(GETDATE())
                ),0),

        PropinasMes =
            ISNULL(
                (
                    SELECT SUM(ISNULL(Propina, 0))
                    FROM Facturas
                    WHERE EstadoFactura = 'Activa'
                    AND YEAR(FechaFactura) = YEAR(GETDATE())
                    AND MONTH(FechaFactura) = MONTH(GETDATE())
                ),0),

        RetirosMes =
            ISNULL(
                (
                    SELECT SUM(Monto)
                    FROM Retiros
                    WHERE EstaEliminado = 0
                    AND YEAR(FechaRetiro) = YEAR(GETDATE())
                    AND MONTH(FechaRetiro) = MONTH(GETDATE())
                ),0),

        NetoMes =
            ISNULL(
                (
                    SELECT SUM(Total + ISNULL(Propina, 0))
                    FROM Facturas
                    WHERE EstadoFactura = 'Activa'
                    AND YEAR(FechaFactura) = YEAR(GETDATE())
                    AND MONTH(FechaFactura) = MONTH(GETDATE())
                ),0)
            -
            ISNULL(
                (
                    SELECT SUM(Monto)
                    FROM Retiros
                    WHERE EstaEliminado = 0
                    AND YEAR(FechaRetiro) = YEAR(GETDATE())
                    AND MONTH(FechaRetiro) = MONTH(GETDATE())
                ),0),

        ServiciosMes =
            ISNULL(
                (
                    SELECT SUM(fd.Cantidad)
                    FROM DetalleFacturas fd
                    INNER JOIN Facturas f
                        ON fd.FacturaId = f.FacturaId
                    WHERE f.EstadoFactura = 'Activa'
                    AND YEAR(f.FechaFactura) = YEAR(GETDATE())
                    AND MONTH(f.FechaFactura) = MONTH(GETDATE())
                ),0);
END
GO

CREATE OR ALTER PROCEDURE Dashboard_ObtenerIngresosSemana
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Dia = DATENAME(WEEKDAY, FechaFactura),
        Total = SUM(Total + ISNULL(Propina, 0))
    FROM Facturas
    WHERE EstaEliminado = 0
        AND EstadoFactura = 'Activa'
        AND FechaFactura >= DATEADD(DAY, -7, GETDATE())
    GROUP BY
        DATENAME(WEEKDAY, FechaFactura),
        DATEPART(WEEKDAY, FechaFactura)
    ORDER BY DATEPART(WEEKDAY, FechaFactura);
END
GO

CREATE OR ALTER PROCEDURE Dashboard_ObtenerIngresosMeses
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Mes = DATENAME(MONTH, FechaFactura),
        Total = SUM(Total + ISNULL(Propina, 0))
    FROM Facturas
    WHERE EstaEliminado = 0
        AND EstadoFactura = 'Activa'
        AND YEAR(FechaFactura) = YEAR(GETDATE())
    GROUP BY
        DATENAME(MONTH, FechaFactura),
        MONTH(FechaFactura)
    ORDER BY MONTH(FechaFactura);
END
GO

