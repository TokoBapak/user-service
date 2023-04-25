FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["Server/UserService.csproj", "Server/UserService/"]

RUN dotnet restore "Server/UserService.csproj"

COPY . .
# Initialize and update Git submodules
RUN git submodule update --init --recursive

WORKDIR "/src/Server"
RUN dotnet build "UserService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserService.csproj" -c Release -o /app/publish

FROM base AS final
RUN apt-get update && apt-get upgrade -y && apt-get install -y curl
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.dll"]
