# RedisWebApi

Example of .NET6 web app with Redis cache and PostgreSQL database.
Source youtube: <https://www.youtube.com/watch?v=6HZVu3kGOrg&t=790s&ab_channel=MohamadLawand>

## Commands

dotnet new webapi -n "RedisWebApi"
cd RedisWebApi
dotnet build
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package StackExchange.Redis
dotnet add package Microsoft.Extensions.Caching.StackExchangeRedis

>update the code, add DbContext

dotnet ef migrations add "initial_migration"
dotnet ef database update
