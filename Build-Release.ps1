Set-StrictMode -Version Latest
$ErrorActionPreference="Continue"
$ProgressPreference="SilentlyContinue"

# capture the start time 
$start = [DateTime]::UtcNow

[String]$thisScriptFullPath = $script:MyInvocation.MyCommand.Path 
[String]$thisScriptDirectory = Split-Path $thisScriptFullPath -Parent

# build path arguments must end with '\'
# [String]$objPath = Join-Path $thisScriptDirectory 'obj\release\'
# [String]$binPath = Join-Path $thisScriptDirectory 'bin\release\'
# dotnet build Lrc.sln -c release -v diagnostic /p:IntermediateOutputPath=$objPath /p:OutputPath=$binPath
dotnet build Lrc.sln -c release -v diagnostic

# capture the completed time (with or without errors)
$end = [DateTime]::UtcNow; 

""
"Build-Release"
"Start:=    $start (UTC)"
"Completed:=$end (UTC)"
"Elapsed:=  $($end-$start)"