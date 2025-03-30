FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY RapidPay.Api/RapidPay.Api.csproj RapidPay.Api/
RUN dotnet restore RapidPay.Api/RapidPay.Api.csproj

COPY . .
RUN dotnet publish RapidPay.Api/RapidPay.Api.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

RUN apt-get update && apt-get install -y coreutils
CMD ["sh", "-c", "dotnet RapidPay.Api.dll"]

EXPOSE 80