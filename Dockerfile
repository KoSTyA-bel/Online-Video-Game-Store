# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY GameStore/*.csproj ./GameStore/
COPY GameStore.Services/*.csproj ./GameStore.Services/
COPY GameStore.Test/*.csproj ./GameStore.Test/
RUN dotnet restore

# copy everything else and build app
COPY GameStore/. ./GameStore/
COPY GameStore.Services/. ./GameStore.Services/
COPY GameStore.Test/. ./GameStore.Test/
WORKDIR /source/GameStore
RUN dotnet publish -c release -o /app 

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "GameStore.dll"]
