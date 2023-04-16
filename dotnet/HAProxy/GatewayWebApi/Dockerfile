FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy all the files
COPY . .

# Build and publish the code
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
# ENV ASPNETCORE_URLS=http://*:5011
WORKDIR /app
COPY --from=build-env /app/out .
# EXPOSE 5011
ENTRYPOINT ["dotnet", "GatewayWebApi.dll"]