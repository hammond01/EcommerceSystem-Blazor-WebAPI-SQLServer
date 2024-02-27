@echo off
cd /d "E:\Net Core\EcommerceSystem-Blazor-WebAPI-SQLServer\TS_ES_EcommerceSystem"

start cmd /k "dotnet run --project API.Warehouse --urls https://localhost:7275"
start cmd /k "dotnet run --project Server --urls https://localhost:7297"
start cmd /k "dotnet run --project AuthenticationAPI --urls https://localhost:7253"
