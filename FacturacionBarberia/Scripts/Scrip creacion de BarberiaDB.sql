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