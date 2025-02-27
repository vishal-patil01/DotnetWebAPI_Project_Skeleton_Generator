#!/bin/bash

# Define the target runtimes
runtimes=("win-x64" "win-x86" "linux-x64" "osx-arm64" "linux-arm")

# Loop through each runtime and publish
for runtime in "${runtimes[@]}"
do
    publishDir="Files/$runtime"
    arguments="publish -c Release -r $runtime --self-contained true -p:PublishSingleFile=true -p:PublishReadyToRun=true -p:PublishTrimmed=true -o $publishDir"

    echo "Publishing for runtime: $runtime"
    echo "dotnet $arguments"
    dotnet publish -c Release -r "$runtime" --self-contained true -p:PublishSingleFile=true -p:PublishReadyToRun=true -p:PublishTrimmed=true -o "$publishDir"
done

echo "Publishing completed for all specified platforms."
