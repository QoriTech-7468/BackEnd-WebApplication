FROM mcr.microsoft.com/dotnet/sdk:9.0 AS builder
WORKDIR /app
COPY Rutana.API/*.csproj Rutana.API/
RUN dotnet restore ./Rutana.API
COPY . .
RUN dotnet publish ./Rutana.API -c Release -o out

# Final stage: runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production
COPY --from=builder /app/out .
EXPOSE 80
# The application listens on port 80 by default
ENTRYPOINT ["dotnet", "Rutana.API.dll"]

