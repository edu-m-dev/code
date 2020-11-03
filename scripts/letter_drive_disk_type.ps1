$partitions = Get-CimInstance Win32_DiskPartition
$physDisc = get-physicaldisk
$arr = @()
foreach ($partition in $partitions){
    $cims = Get-CimInstance -Query "ASSOCIATORS OF `
                          {Win32_DiskPartition.DeviceID='$($partition.DeviceID)'} `
                          WHERE AssocClass=Win32_LogicalDiskToPartition"
    $regex = $partition.name -match "(\d+)"
    $physDiscNr = $matches[0]
    foreach ($cim in $cims){
        $arr += [PSCustomObject]@{
            Drive = $cim.deviceID
            Partition = $partition.name
            MediaType = $($physDisc | ? {$_.DeviceID -eq $physDiscNr} | select -expand MediaType)
        }
    }
}

$arr