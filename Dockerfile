
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app/lunch-backend

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /src
COPY ["lunch-backend/Dinner/Dinner.csproj", "lunch-backend/Dinner/"]
COPY ["lunch-backend/BLL/BLL.csproj", "lunch-backend/BLL/"]
COPY ["lunch-backend/DAL/DAL.csproj", "lunch-backend/DAL/"]
RUN dotnet restore "lunch-backend/Dinner/Dinner.csproj"
COPY . .
WORKDIR "/src/lunch-backend/Dinner"
RUN dotnet build "Dinner.csproj" -c Release -o /app/lunch-backend

FROM build-env AS publish
RUN dotnet publish "Dinner.csproj" -c Release -o /app/lunch-backend


EXPOSE 80
FROM base AS final
WORKDIR /app/lunch-backend
COPY --from=publish /app/lunch-backend .
ENTRYPOINT ["dotnet", "Dinner.dll"]