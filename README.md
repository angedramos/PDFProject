# PDFSignature
Este es un proyecto que utiliza tanto el Frontend (Angular) como Backend (.NET) para la subida y manejo de archivos en formato PDF

## Requisitos
- .NET SDK 8.0 o superior
- Node.js y npm
- Angular CLI (versión 17 o superior)
- SQL Server Management Studio (para gestión de base de datos)

## Crear la base de datos
1. Ejecutar el script proporcionado (PdfSignature.sql) en SQL Server Management studio
2. Una vez creada la base de datos, abre el archivo appsettings.json ubicado en PdfSignatureAPI
3. Modifica la cadena de conexión:
`"ConnectionStrings": {
  "DatabaseConnectionOS": "Server=<tu_servidor>;Database=PDFSignatureDatabase;Trusted_Connection=True;TrustServerCertificate=True;"
}`


## Instrucciones para ejecutar el backend
1. Clona este repositorio y navega al directorio del backend (`PdfSignatureAPI`).
2. Abre una terminal y ejecuta el comando:
   ```bash
   dotnet restore
3. A continuación, ejecuta el comando: dotnet run.


## Instrucciones para ejecutar el frontend
1. Clona este repositorio y Navega al directorio del frontend.
2. Ejecutar `npm install` para instalar las dependencias.
3. Ejecutar `ng serve` para iniciar la aplicación.

## Pruebas
- Backend: Ejecutar `dotnet test` para correr las pruebas unitarias.
- Frontend: Ejecutar `ng test` para correr las pruebas de Angular.

