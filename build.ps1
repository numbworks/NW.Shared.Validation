<#

    Title: build.ps1
    Author: numbworks@gmail.com
    Last Update: 10.02.2024
    Description: 

        This script automates the process of building:
            - this library as NuGet package
        
        Pre-requisites:
            - The script expects the project source to be stored into the '{solution_folder}\src\{project_name}' folder
            - The script requires 'dotnet.exe' to be installed on the system.

        Features
            - The script can be run both in VSCode and in a shell/Windows Terminal            
            - If the '{solution_folder}\artifacts' folder is not found, '{desktop}\artifacts' will be used.
                - The user can even force the second option by enabling the $forcePublishToDesktop flag.
            - The script supports timestamped logging
            - The library artifact will be something like:
                - "NW.UnivariateForecasting.2.5.0.nupkg"
            - The script is a wrapper around the following 'dotnet' commands:
                - dotnet pack
            
#>

# Variables
[bool]$forcePublishToDesktop = $true

# Classes
class Logger {

    # Fields
    # Properties
    # Constructors()
    hidden Logger() { }
    
    # Methods
    static [void] Log([string]$message) {

        Write-Host $("{0} {1}" -f $([Logger]::GetTimeStamp()), $message)
    
    }
    static hidden [string] GetTimeStamp() {

        return $(Get-Date -Format "[yyyy-MM-dd HH:mm]")

    }

}

# Functions
function Assert-ThatDotnetIsInstalled {

    [OutputType([bool])]
    param()

    try 
    {

        . dotnet | Out-Null

        [Logger]::Log("'dotnet.exe' is installed.")

        return $true 

    } 
    catch { 
        
        [Logger]::Log("'dotnet.exe' is not installed.")
        return $false 
    
    }

}
function Get-CurrentDirectory() {

    [OutputType([System.IO.DirectoryInfo])]
    param()

    [string]$currentDir = $null
    if ($PSISE) { 
        $currentDir = (Split-Path -Path $psISE.CurrentFile.FullPath) 
    }
    elseif ($profile.Contains("VSCode")) {
        $currentDir = (Split-Path $PSEditor.GetEditorContext().CurrentFile.Path) 
    }
    elseif (-not $PSScriptRoot) {
        $currentDir = (Get-ChildItem | ForEach-Object { $_.DirectoryName } | Select-Object -Unique)
    }
    else { 
        $currentDir = $PSScriptRoot 
    }

    return [System.IO.DirectoryInfo]::new($currentDir)

}
function Get-ArtifactsFolder {

    [OutputType([System.IO.DirectoryInfo])]
    param(
	    [parameter(Mandatory=$true)] [System.IO.DirectoryInfo]$SolutionFolder,
        [parameter(Mandatory=$true)] [bool]$ForcePublishToDesktop
    )

    # TO-DO: validation    

    [System.IO.DirectoryInfo]$desktopartifactFolder = [System.IO.Path]::Combine([Environment]::GetFolderPath('Desktop'), "artifacts")
    if($ForcePublishToDesktop.Equals($true)) {
       
        return $desktopartifactFolder
    
    }

    [System.IO.DirectoryInfo]$artifactsFolder = [System.IO.Path]::Combine($SolutionFolder, "artifacts")
    if ($artifactsFolder.Exists.Equals($false)) {

        [Logger]::Log("'artifacts' folder doesn't exist: '$artifactsFolder'.")
        
        $artifactsFolder = $desktopartifactFolder

        [Logger]::Log("'desktop\artifacts' folder will be used instead: '$artifactsFolder'.")

    }
    else {

        [Logger]::Log("'artifacts' folder: '$artifactsFolder'.")

    }

    return $artifactsFolder

}
function Get-ProjectFile {

    [OutputType([System.IO.FileInfo])]
    param(
	    [parameter(Mandatory=$true)] [System.IO.DirectoryInfo]$SolutionFolder,
        [parameter(Mandatory=$true)] [string]$ProjectName
    )

    # TO-DO: validation    

    [System.IO.DirectoryInfo]$projectFolder = [System.IO.Path]::Combine($SolutionFolder, "src")    
    if ($projectFolder.Exists.Equals($false)) {

        [Logger]::Log("'src' folder doesn't exist: '$projectFolder'.")
        return $null

    }

    $projectFolder = [System.IO.Path]::Combine($projectFolder, $ProjectName)
    if ($projectFolder.Exists.Equals($false)) {

        [Logger]::Log("'$ProjectName' folder doesn't exist: '$projectFolder'.")
        return $null
        
    }

    [string]$projectFileName = "$($projectFolder)\$($ProjectName).csproj"
    [System.IO.FileInfo]$projectFile = [System.IO.FileInfo]::new($projectFileName)

    [Logger]::Log("Project's file: '$projectFile'.")

    return $projectFile

}
function Publish-Project {

    [OutputType([System.IO.FileInfo])]
    param(
	    [parameter(Mandatory=$true)] [System.IO.FileInfo]$ProjectFile,
        [parameter(Mandatory=$true)] [System.IO.DirectoryInfo]$ArtifactsFolder,
        [parameter(Mandatory=$true)] [string]$Runtime
    )

    # TO-DO: validation    

    [Logger]::Log("Publishing project for runtime '$Runtime'...")
    [Logger]::Log("")

    dotnet publish $ProjectFile -r $Runtime -c Release /p:PublishSingleFile=true /p:PublishReadyToRun=false /p:DebugSymbols=false /p:DebugType=None -o $ArtifactsFolder | Out-Host

    [Logger]::Log("")

    if ($LASTEXITCODE.Equals(1)) {

        [Logger]::Log("The project has failed to publish.")
        return $null

    }

    [System.IO.FileInfo]$publishedFile = $(Get-ChildItem $ArtifactsFolder | Sort-Object -Descending -Property LastWriteTime -Top 1)

    [Logger]::Log("The project has been successfully published: '$publishedFile'.")

    return $publishedFile

}
function Get-ProjectFileAssemblyName {

    [OutputType([string])]
    param(
	    [parameter(Mandatory=$true)] [System.IO.FileInfo]$ProjectFile
    )

    # TO-DO: validation    

    [xml]$projectFileContent = $(Get-Content -Path $ProjectFile)

    return $projectFileContent.Project.PropertyGroup.AssemblyName

}
function Get-ProjectFileVersion {

    [OutputType([string])]
    param(
	    [parameter(Mandatory=$true)] [System.IO.FileInfo]$ProjectFile
    )

    # TO-DO: validation

    [xml]$projectFileContent = $(Get-Content -Path $ProjectFile)

    return $projectFileContent.Project.PropertyGroup.Version

}
function Get-NuGetPackages {

    [OutputType([string[]])]
    param(
	    [parameter(Mandatory=$true)] [System.IO.DirectoryInfo]$ArtifactsFolder
    )    

    # TO-DO: validation

    [string[]]$packageNames = (Get-ChildItem -Path "$ArtifactsFolder\*" -Include @("*.nupkg") | ForEach-Object { $_.Name} )
    
    [Logger]::Log("'$($packageNames.Count)' NuGet packages have been found in the provided artifacts folder.")

    return $packageNames

}
function Build-Library {

    <#
    
        By default, this function will create a .nupkg package out of every project found in the .sln file.

        If you want to exclude one or more projects, please add the <IsPackable>false</IsPackable> field to the .csproj as shown below:

            <Project>
                <PropertyGroup>
                    <IsPackable>false</IsPackable>
                </PropertyGroup>
            </Project>
    
    #>

    [OutputType([string[]])]
    param(
	    [parameter(Mandatory=$true)] [System.IO.DirectoryInfo]$SolutionFolder,
	    [parameter(Mandatory=$true)] [System.IO.DirectoryInfo]$ArtifactsFolder
    )

    # TO-DO: validation
    
    try {

        [Logger]::Log("The following solution directory has been provided: '$($SolutionFolder.FullName)'.")
        [Logger]::Log("The following artifacts directory has been provided: '$($ArtifactsFolder.FullName)'.")

        [System.IO.FileInfo[]]$solutionFiles = (Get-ChildItem -Path "$SolutionFolder\*" -Include @("*.sln"))
        if ($solutionFiles.Count.Equals(1).Equals($false))
        {
            [Logger]::Log("An unexpected amount of solution files have been found in the current directory.")
            return $null
        }

        [System.IO.FileInfo]$solutionFileName = $solutionFiles[0]
        [Logger]::Log("The following solution file has been found: '$($solutionFileName.Name)'.")
        [Logger]::Log("Creating NuGet package(s) using 'dotnet pack'...")
        
        dotnet pack $solutionFileName --output $ArtifactsFolder
        
        [Logger]::Log("Exit code is: '$($LASTEXITCODE)'.")

        if ($LASTEXITCODE.Equals(0)) {

            [string[]]$packageNames = (Get-NuGetPackages -ArtifactsFolder $ArtifactsFolder)
            if ($packageNames) {

                [Logger]::Log("The NuGet packages are: '$packageNames'")

            }
            else {

                [Logger]::Log("No NuGet packages found in the artifacts folder (?).")

            } 

            return $packageNames

        }
        else {
            return $null
        }

    }
    catch {
        
        [Logger]::Log($_.Exception.Message)
        return $null

    }  

}
function Invoke-BuildScript {

    [OutputType([System.Void])]
    param()

    $ErrorActionPreference = "Stop"

    try { 

        if ($(Assert-ThatDotnetIsInstalled).Equals($false)) {
            
            [Logger]::Log("Aborting.")
            break
        
        }
        
        [System.IO.DirectoryInfo]$solutionFolder = Get-CurrentDirectory
        [System.IO.DirectoryInfo]$artifactsFolder = $(Get-ArtifactsFolder -SolutionFolder $solutionFolder -ForcePublishToDesktop $forcePublishToDesktop)
      
        [string[]]$packageNames = (Build-Library -SolutionFolder $solutionFolder -ArtifactsFolder $artifactsFolder)
     
    }
    catch {

        [Logger]::Log($_.Exception.Message)
    
    }

}

# Main
Clear-Host
Invoke-BuildScript