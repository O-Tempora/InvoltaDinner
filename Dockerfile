FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app-source
COPY ["Dinner/Dinner.csproj", "Dinner/"]
COPY ["BLL/BLL.csproj", "BLL/"]
COPY ["DAL/DAL.csproj", "DAL/"]
RUN dotnet restore "Dinner/Dinner.csproj"
COPY . .
WORKDIR "/app-source/Dinner"
RUN dotnet build "Dinner.csproj" -c Release -o /app

FROM build-env AS publish
RUN dotnet publish "Dinner.csproj" -c Release -o /app

EXPOSE 80
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Dinner.dll"]


# docker build --no-cache \
#         -t registry.involta.pro/lunch/lunch-backend/lunch-backend:latest \
#         -t registry.involta.pro/lunch/lunch-backend/lunch-backend:0.0.5 .

# docker push registry.involta.pro/lunch/lunch-backend/lunch-backend:latest \
# && docker push registry.involta.pro/lunch/lunch-backend/lunch-backend:0.0.5
