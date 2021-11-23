#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Payment.Application.WebApi/Payment.Application.WebApi.csproj", "Payment.Application.WebApi/"]
COPY ["Payment.Domain.Mediator/Payment.Domain.Mediator.csproj", "Payment.Domain.Mediator/"]
COPY ["Payment.Domain/Payment.Domain.csproj", "Payment.Domain/"]
COPY ["Payment.Domain.Service/Payment.Domain.Service.csproj", "Payment.Domain.Service/"]
COPY ["Payment.Infra.DataBase/Payment.Infra.DataBase.csproj", "Payment.Infra.DataBase/"]
RUN dotnet restore "Payment.Application.WebApi/Payment.Application.WebApi.csproj"
COPY . .
WORKDIR "/src/Payment.Application.WebApi"
RUN dotnet build "Payment.Application.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Payment.Application.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment.Application.WebApi.dll"]
