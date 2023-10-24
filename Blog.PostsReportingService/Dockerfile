#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Blog.PostsReportingService/Blog.PostsReportingService.csproj", "Blog.PostsReportingService/"]
COPY ["../Blog.Contracts/Blog.Contracts.csproj", "../Blog.Contracts/"]
COPY ["Blog.Common/Blog.Common.csproj", "Blog.Common/"]
RUN dotnet restore "Blog.PostsReportingService/Blog.PostsReportingService.csproj"
COPY . .
WORKDIR "/src/Blog.PostsReportingService"
RUN dotnet build "Blog.PostsReportingService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blog.PostsReportingService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blog.PostsReportingService.dll"]