# Imagem base com .NET 9.0 SDK (caso esteja disponível em preview)
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build

WORKDIR /app

# Copia o csproj e restaura dependências
COPY AbrigoGerenciamento/*.csproj ./AbrigoGerenciamento/
RUN dotnet restore ./AbrigoGerenciamento/AbrigoGerenciamento.csproj


COPY . .
RUN dotnet publish ./AbrigoGerenciamento/AbrigoGerenciamento.csproj -c Release -o /out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["dotnet", "AbrigoGerenciamento.dll"]
