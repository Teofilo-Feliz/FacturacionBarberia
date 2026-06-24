💈 Facturación Barbería

Sistema de facturación para barberías desarrollado con ASP.NET Core MVC, permitiendo gestionar servicios, facturas y usuarios mediante roles de acceso.

Características
Inicio de sesión con autenticación por Cookies y Claims.
Gestión de usuarios.
Gestión de servicios.
Facturación de servicios.
Anulación de facturas.
Dashboard administrativo con métricas del negocio.
Control de acceso por roles (Administrador y Cajero).

Tecnologías
ASP.NET Core MVC
C#
SQL Server
Entity Framework Core
Bootstrap 5
Razor Views

Arquitectura del Proyecto

El proyecto se encuentra organizado en capas siguiendo una estructura inspirada en Clean Architecture:

FacturacionBarberia
│
├── Application
│   ├── Controllers
│   ├── Dependency
│   ├── DTO
│   ├── Helpers
│   ├── Interfaces
│   ├── Services
│   └── ViewModel
│
├── Domain
│   └── Models
│       ├── Entities
│       └── Enum
│
├── Infrastructure
│   ├── Audit
│   ├── Data
│   ├── Dependency
│   ├── PatronRepository
│   │   ├── DashboardRepository
│   │   ├── FacturaRepository
│   │   └── GenericRepository
│   └── UnitOfWork
│
├── Scripts
│
├── Views
│   ├── Cliente
│   ├── Dashboard
│   ├── Factura
│   ├── Login
│   ├── Servicio
│   ├── Usuario
│   └── Shared
│
└── wwwroot

Base de Datos

El repositorio incluye:

Script de creación de la base de datos SQL Server.
Procedimientos almacenados utilizados por el Dashboard.
Estructura completa de tablas y relaciones.

Roles
Administrador
Acceso al Dashboard.
Gestión de usuarios.
Gestión de servicios.
Facturación.
Consulta de métricas del negocio.

Cajero
Facturación.
Gestión de servicios.
Acceso restringido a funcionalidades administrativas.

Funcionalidades Implementadas
Autenticación y autorización.
Gestión de usuarios.
Gestión de servicios.
Facturación.
Anulación de facturas.
Dashboard con indicadores del negocio.
Estado del Proyecto

Proyecto en desarrollo continuo con nuevas funcionalidades y mejoras en proceso.

Desarrollado con ASP.NET Core MVC y SQL Server.

