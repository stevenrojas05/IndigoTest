# IndigoTest

Proyecto base orientado a arquitectura Onion con .NET 8, enfocado en el dominio de Productos y Ventas.

## Descripción

Este repositorio contiene la base de un backend organizado por capas siguiendo la arquitectura Onion. Incluye:
- Estructura de capas: Domain, Application, Infrastructure, API, y carpeta separada para el Client.
- Entidades principales: Customer, Product, Sale, SaleItem.
- Repositorios e interfaces genéricos y específicos.
- Implementación de servicios CRUD en la capa Application.
- Exposición de endpoints en la API Web.
- Migraciones y contexto EF Core para la base de datos.
- Scripts SQL para la creación de las tablas principales.

## Estructura del Proyecto

```
Layers/
├── Domain
│   ├── Entities
│   └── Interfaces
├── Application
│   ├── Interfaces
│   └── Services
├── Infrastructure
│   ├── Data
│   └── Repositories
├── API
│   ├── Controllers
│   └── appsettings.json
└── Client (pendiente)
```

## Tablas en la Base de Datos

El proyecto incluye scripts para:
- Customers
- Products
- Sales
- SaleItems

**Ejemplo de creación de tabla:**

```sql
CREATE TABLE Customers (
    CustomerId INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);
```

## Ejecución del Proyecto

1. Clona el repositorio:
    ```
    git clone https://github.com/tuusuario/PruebaIndigo.git
    ```

2. Instala dependencias:
    - .NET 8 SDK
    - Paquetes NuGet indicados en cada proyecto (especialmente para EF Core y JWT)

3. Configura la cadena de conexión en `API/appsettings.json` según tu entorno.

4. Realiza la migración de la base de datos:
    ```
    dotnet ef database update --project Infrastructure
    ```

5. Ejecuta la API:
    ```
    dotnet run --project API
    ```

## Estado del Proyecto

- Se encuentran implementadas las capas principales y los servicios básicos CRUD de cada entidad.
- No se desarrolló aún el cliente front-end ni toda la lógica de autenticación JWT.
- El proyecto sirve de referencia para la organización y estructuración profesional con Onion Architecture.

## Autor

- Steven Rojas

