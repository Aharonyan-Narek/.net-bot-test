FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AspNetTelegramBotExample/AspNetTelegramBotExample.csproj", "AspNetTelegramBotExample/"]
RUN dotnet restore "AspNetTelegramBotExample/AspNetTelegramBotExample.csproj"
COPY . .
WORKDIR "/src/AspNetTelegramBotExample"
RUN dotnet build "AspNetTelegramBotExample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspNetTelegramBotExample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNetTelegramBotExample.dll"]
