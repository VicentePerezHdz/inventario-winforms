# Inventario (Windows Forms)

Aplicación de escritorio en C# y Windows Forms (.NET Framework 4.7.2) para administrar un inventario con SQL Server.

## Funciones
- Inicio de sesión de usuarios.
- Alta y consulta de artículos, empleados y almacenes.
- Registro de compras.
- Alta y modificación de usuarios.
- Instalador con un proyecto de Visual Studio Installer (`SetupInventario`).

## Cómo correrlo
1. Crea la base de datos `Inventario` en SQL Server con los scripts de la carpeta `scr/`.
2. Revisa la cadena de conexión en `App.config`. Por defecto usa `.\SQLEXPRESS` con autenticación de Windows.
3. Abre `Inventario.sln` en Visual Studio y ejecuta el proyecto `Inventario`.
