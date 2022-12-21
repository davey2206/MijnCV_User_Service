#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MijnCV_User_Service/MijnCV_User_Service.csproj", "MijnCV_User_Service/"]
RUN dotnet restore "MijnCV_User_Service/MijnCV_User_Service.csproj"
COPY . .
WORKDIR "/src/MijnCV_User_Service"
RUN dotnet build "MijnCV_User_Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MijnCV_User_Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MijnCV_User_Service.dll"]