  
# Stage 1: Construir la imagen
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# Moverme al directorio /app dentro del contenedor
WORKDIR /app
# Copiar todo dentro del contenedor
COPY . .
# Mpverme dentro del contenedor a la carpeta donde estan los assets de la aplicacion web
WORKDIR /app/Movies.WebApp
# Ejecutar los comando para construir la aplicacion
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Stage 2: Construir la imagen
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/Movies.WebApp/out ./

# Esta variable de entorno la utiliza el framework para leer el archivo de configuracion
ENV ASPNETCORE_ENVIRONMENT Development
# Con esta instruccion se ejecuta la aplicacion dentro del contenedor
ENTRYPOINT ["dotnet", "Movies.WebApp.dll"]