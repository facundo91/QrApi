# https://www.softwaredeveloper.blog/multi-project-dotnet-core-solution-in-docker-image#dockerfile-from-instruction
# run docker build from solution directory
# docker build -f qrAPI/Dockerfile -t IMAGE_NAME .
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet publish qrAPI -c Release -o qrAPI/out

# Copy everything else and build
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/qrAPI/out .

ENTRYPOINT ["dotnet", "qrAPI.dll"]