
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app/api

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /src
COPY ["api/Dinner/Dinner.csproj", "api/Dinner/"]
COPY ["api/BLL/BLL.csproj", "api/BLL/"]
COPY ["api/DAL/DAL.csproj", "api/DAL/"]
RUN dotnet restore "api/Dinner/Dinner.csproj"
COPY . .
WORKDIR "/src/api/Dinner"
RUN dotnet build "Dinner.csproj" -c Release -o /app/api

FROM build-env AS publish
RUN dotnet publish "Dinner.csproj" -c Release -o /app/api


EXPOSE 80
FROM base AS final
WORKDIR /app/api
COPY --from=publish /app/api .
ENTRYPOINT ["dotnet", "Dinner.dll"]