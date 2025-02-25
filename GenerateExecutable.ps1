# Define the target runtimes
$runtimes = @("win-x64", "win-x86", "linux-x64")

# Loop through each runtime and publish
foreach ($runtime in $runtimes) {
    $publishDir = "Files/$runtime"
    $arguments = "publish -c Release -r $runtime --self-contained true -p:PublishSingleFile=true -p:PublishReadyToRun=true -p:PublishTrimmed=true -o $publishDir"

    Write-Output "Publishing for runtime: $runtime"
    Write-Output "dotnet $arguments"
    & dotnet publish -c Release -r $runtime --self-contained true -p:PublishSingleFile=true -p:PublishReadyToRun=true -p:PublishTrimmed=true -o $publishDir
}

Write-Output "Publishing completed for all specified platforms."
