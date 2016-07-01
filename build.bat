@echo off

pushd src\IrcBot
dotnet restore
dotnet build
popd

pushd test\IrcBot.Tests
dotnet restore
dotnet build
popd
