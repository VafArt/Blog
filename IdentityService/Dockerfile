FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["IdentityService/Blog.IdentityService.csproj", "IdentityService/"]
COPY ["Blog.Common/Blog.Common.csproj", "Blog.Common/"]
COPY ["Blog.Contracts/Blog.Contracts.csproj", "Blog.Contracts/"]
RUN dotnet restore "IdentityService/Blog.IdentityService.csproj"
COPY . .
WORKDIR "/src/IdentityService"
RUN dotnet build "Blog.IdentityService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blog.IdentityService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blog.IdentityService.dll"]