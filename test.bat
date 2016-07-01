@echo off

call build.bat

pushd test\IrcBot.Tests
dotnet test
popd