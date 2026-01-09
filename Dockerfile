# --- build stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy csproj and restore
COPY GazelServices/GazelServices.csproj ./GazelServices/
RUN dotnet restore ./GazelServices/GazelServices.csproj

# copy the rest
COPY . .

# publish
RUN dotnet publish ./GazelServices/GazelServices.csproj -c Release -o /app/publish /p:UseAppHost=false

# --- runtime stage ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "GazelServices.dll"]
