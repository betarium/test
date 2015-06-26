function LoadIni($filename)
{
    $result = @{}
    $lines = get-content $filename
    foreach($line in $lines)
    {
        if($line -match "^;")
        {
            continue
        }
        $param = $line.split("=",2)
        $result[$param[0]] = $param[1]
    }
    return $result
}

function SendMail($conf, $mailSubject, $mailBody)
{
    if($conf["MailTo"] -eq "0"){
        return
    }

    $password = ConvertTo-SecureString $conf["SmtpPassword"] -AsPlainText -Force
    $credential = New-Object System.Management.Automation.PSCredential($conf["SmtpUser"], $password)

    Send-MailMessage -To $conf["MailTo"] -From $conf["MailFrom"] `
        -Subject $mailSubject -Body $mailBody `
        -SmtpServer $conf["SmtpServer"] -Port $conf["SmtpPort"] `
        -Credential $credential -Encoding UTF8
}

$INI_PATH = $MyInvocation.MyCommand.Path + ".ini"
$BASE_PATH = $MyInvocation.MyCommand.Path
$BASE_PATH = Split-Path -Parent $BASE_PATH

start-transcript ($MyInvocation.MyCommand.Path + ".log")

$message = "Start Windows update check."
Write-Output $message

$INI = LoadIni $INI_PATH

$MailFooter = "`n`n" + "Host:" + $Env:COMPUTERNAME + "`n"

$Session = New-Object -com "Microsoft.Update.Session"
$Search = $Session.CreateUpdateSearcher()
$SearchResults = $Search.Search("IsInstalled=0 and IsHidden=0 and AutoSelectOnWebSites=1")

$AvailableUpdates = $SearchResults.Updates

if($AvailableUpdates.count -eq 0){
    Write-Output "No update patch."
    Exit
}

$message = "Windows Update start."
Write-Output $message
SendMail $INI "Update Check - WindowsUpdate.ps1" ($message + $MailFooter)

$UpdateCount = $AvailableUpdates.count
Write-Output "$UpdateCount patch found(s)."

Write-Output "Start download."

$DownloadCollection = New-Object -com "Microsoft.Update.UpdateColl"

$AvailableUpdates | ForEach-Object {
    Write-Output "  Patch: " $_.Title

    if ($_.InstallationBehavior.CanRequestUserInput -ne $TRUE) {
        $DownloadCollection.Add($_) | Out-Null
    }
    else {
        Write-Output "  Required user input."
    }
    if ($_.InstallationBehavior.RebootBehavior -gt 0) {
        Write-Output "  Required reboot."
    }
}

$Downloader = $Session.CreateUpdateDownloader()
$Downloader.Updates = $DownloadCollection
$Downloader.Download()

Write-Output "Finish download."

Write-Output "Start update."

$InstallCollection = New-Object -com "Microsoft.Update.UpdateColl"
$AvailableUpdates | ForEach-Object {
    if ($_.IsDownloaded) {
        $InstallCollection.Add($_) | Out-Null
    }
}

$Installer = $Session.CreateUpdateInstaller()
$Installer.Updates = $InstallCollection
$Results = $Installer.Install()


$message = "Windows Update complete."
Write-Output $message
SendMail $INI "Update complete - WindowsUpdate.ps1" ($message + $MailFooter)


if ($Results.RebootRequired) {
    Write-Output "Reboot."
    SendMail $INI "Reboot - WindowsUpdate.ps1" ("Reboot." + $MailFooter)
    stop-transcript
    Restart-Computer -F
}

stop-transcript

