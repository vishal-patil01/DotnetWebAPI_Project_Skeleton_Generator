@echo off
setlocal enabledelayedexpansion

REM Define the target runtimes
set runtimes=win-x64 win-x86 linux-x64

REM Loop through each runtime and publish
for %%r in (%runtimes%) do (
    set publishDir=Files\%%r
    set arguments=publish -c Release -r %%r --self-contained true -p:PublishSingleFile=true -p:PublishReadyToRun=true -p:PublishTrimmed=true -o !publishDir!

    echo Publishing for runtime: %%r
    dotnet !arguments!
)

echo Publishing completed for all specified platforms.
endlocal
pause
