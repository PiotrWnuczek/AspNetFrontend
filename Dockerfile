#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Client.BlazorServer/Client.BlazorServer.csproj", "Client.BlazorServer/"]
COPY ["Client.Model/Client.Model.csproj", "Client.Model/"]
COPY ["Client.Controller/Client.Controller.csproj", "Client.Controller/"]
RUN dotnet restore "Client.BlazorServer/Client.BlazorServer.csproj"
COPY . .
WORKDIR "/src/Client.BlazorServer"
RUN dotnet build "Client.BlazorServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Client.BlazorServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Client.BlazorServer.dll"]