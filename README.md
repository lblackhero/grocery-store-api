<div align="center">

# grocery-store-api

Esta API te permitira gestionar un catalogo de alimentos como usuario administrador, y como usuario estándar te permitirá realizar ordenes sobre el catalogo que gestione el usuario administrador.

[Diseño de la base de datos](https://dbdiagram.io/d/grocery-store-api-database-6620805b03593b6b61486402) · [Prueba la API](https://grocery-store-api.azurewebsites.net/swagger/index.html) · [Crea los objetos de base de datos](https://github.com/lblackhero/grocery-store-api/blob/main/GroceryStore.Database/DDL/SCHEMAS/DBO/TABLES/create_all_tables_grocery_api.sql)
  
</div>

## ☑️ Características de la aplicación

- **Sistema de autenticación**: Te podrás registrar como un usuario administrador o estándar.
- **Gestión del catalogo de alimentos**: Como usuario administrador, podras realizar operaciones CRUD para gestionar un catálogo de alimentos.
- **Creación de ordenes sobre el catálogo**: Como un usuario registrado, podrás realizar ordenes sobre el catálogo de alimentos existente. Una confirmación de la orden te llegará a tu correo electrónico, siempre y cuando exista. 

## 💻 Tecnologías utilizadas

- .NET
- .ASP .NET Identity
- C#
- Azure Services - Azure Email Services, Azure SQL Database & Azure App Services
  
