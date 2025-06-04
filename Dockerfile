FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

COPY FlashcardsAPI/FlashcardsAPI.csproj ./FlashcardsAPI/

RUN dotnet restore FlashcardsAPI/FlashcardsAPI.csproj

COPY FlashcardsAPI/ ./FlashcardsAPI/

RUN dotnet publish FlashcardsAPI/FlashcardsAPI.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build /app/publish .

RUN ls -l /app

ENV ASPNETCORE_URLS=http://+:${PORT}

ENTRYPOINT ["dotnet", "FlashcardsAPI.dll"]
