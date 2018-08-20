Set-StrictMode -Version Latest
$ErrorActionPreference="Continue"
$ProgressPreference="SilentlyContinue"

#maintain in parallel with Build-Debug.ps1
#maintain in parallel with Build-Release.ps1

# capture the start time 
$start = [DateTime]::UtcNow

# some files are left behind as of .NET Command Line Tools (2.1.400)
# in addition .\bin\*.* and .\obj\*.* can be output and not cleaned up by clean
#TODO: try kill any directory and subdirectories\files named .\bin\ or .\obj\

dotnet clean Lrc.sln -v quiet > $null
dotnet clean Lrc.sln -c debug -v quiet > $null
dotnet clean Lrc.sln -c release -v quiet > $null

# capture the completed time (with or without errors)
$end = [DateTime]::UtcNow; 
""
"Build-Clean"
"Start:=    $start (UTC)"
"Completed:=$end (UTC)"
"Elapsed:=  $($end-$start)"
