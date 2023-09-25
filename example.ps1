$p = Start-Process dotnet run -PassThru
Write-Host "Waiting 10 seconds for dotnet run to build and start"
Start-Sleep 10

function TestUrl($url) {
    Write-Host "--Multiple get's"
    (Invoke-WebRequest $url).Content | Write-Host
    Start-Sleep 1
    (Invoke-WebRequest $url).Content | Write-Host
    Start-Sleep 1
    (Invoke-WebRequest $url).Content | Write-Host
    
    Write-Host "--Long get override"
    Start-Sleep 1
    $job1 = Start-Job -ScriptBlock { Invoke-WebRequest "$($using:url)/long" | Write-Output }
    Start-Sleep 1
    $job2 = Start-Job -ScriptBlock { Invoke-WebRequest "$($using:url)/long" | Write-Output }
    $job1 | Wait-Job | Out-Null
    $job2 | Wait-Job | Out-Null
    Write-Host "--Job1"
    ($job1 | Receive-Job).Content | write-host
    Write-Host "--Job2"
    ($job2 | Receive-Job).Content | write-host
    
}

Write-Host "=====Standard options"
TestUrl -url "http://localhost:5240/StandardOptions"

Write-Host "=====Scoped options"
TestUrl -url "http://localhost:5240/Scoped"

Write-Host "=====Snapshot options"
TestUrl -url "http://localhost:5240/Snapshot"

$p.Kill()