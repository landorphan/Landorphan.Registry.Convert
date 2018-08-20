Set-StrictMode -Version Latest
$ErrorActionPreference="Continue"
$ProgressPreference="SilentlyContinue"

# capture the start time 
$start = [DateTime]::UtcNow

[String]$thisScriptFullPath = $script:MyInvocation.MyCommand.Path 
[String]$thisScriptDirectory = Split-Path $thisScriptFullPath -Parent

#build path arguments must end with '\'
#[String]$objPath = Join-Path $thisScriptDirectory 'obj\debug\'
#[String]$binPath = Join-Path $thisScriptDirectory 'bin\debug\'
#dotnet build Lrc.sln -c debug -v diagnostic /p:IntermediateOutputPath=$objPath /p:OutputPath=$binPath
dotnet build Lrc.sln -c debug -v diagnostic

# capture the completed time (with or without errors)
$end = [DateTime]::UtcNow; 

""
"Build-Debug"
"Start:=    $start (UTC)"
"Completed:=$end (UTC)"
"Elapsed:=  $($end-$start)"