# Один Dockerfile для всех хостов: docker build --build-arg HOST=Api|Worker|PublicGateway
ARG DOTNET_VERSION=10.0

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
ARG HOST=Api
WORKDIR /src
COPY . .
RUN dotnet publish src/Hosts/SoftCollection.${HOST}/SoftCollection.${HOST}.csproj -c Release -o /app /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
ARG HOST=Api
ENV HOST_DLL=SoftCollection.${HOST}.dll \
    ASPNETCORE_HTTP_PORTS=8080 \
    TZ=Asia/Bishkek
WORKDIR /app
COPY --from=build /app .
USER $APP_UID
EXPOSE 8080
ENTRYPOINT ["sh", "-c", "exec dotnet $HOST_DLL"]
