FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ./app/MyApp.sln ./
COPY ./app/MyApp.WebApi/* ./MyApp.WebApi/
COPY ./app/MyApp.Core/* ./MyApp.Core/

RUN dotnet restore
COPY . .
WORKDIR /src/MyApp.WebApi/
RUN dotnet build -c Release -o /app

# Copy everything else and build
FROM build-env AS publish
RUN dotnet publish -c Release -o /app

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
RUN apt-get update \
    && apt-get install -y libldap2-dev 
WORKDIR /app
COPY --from=build-env /app/ .
COPY ./web.keytab /app/

EXPOSE 80

ENTRYPOINT ["dotnet", "MyApp.WebApi.dll"]