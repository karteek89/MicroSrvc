FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["LRMS/LRMS.csproj", "LRMS/"]
RUN dotnet restore "LRMS/LRMS.csproj"
COPY . .
WORKDIR "/src/LRMS"
RUN dotnet build "LRMS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LRMS.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LRMS.dll"]