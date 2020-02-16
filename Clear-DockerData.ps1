Get-ChildItem -Path (Join-Path $PSScriptRoot "docker\data") -Directory | ForEach-Object {
    Get-ChildItem -Path $_.FullName -Recurse | Remove-Item -Exclude ".gitkeep" -Force -Recurse
}
