pipeline{
    agent any

    environment {
        dotnet ='C:\\Program Files\\dotnet'
    }

    triggers {
        pollSCM 'H * * * *'
    }
    
    stage ('Clean workspace') {
        steps {
            cleanWs()
        }
    }

    stage ('Git Checkout') {
        steps {
            git branch: 'master', credentialsId: 'netdeveloper', url: 'https://github.com/sigiandre/PalMayor-API-Resful-Backend.git'
        }
    }

    stage('Restore packages') {
        steps {
            bat "dotnet restore ${workspace}\\PalMayor-API-Resful-Backend\\ApiVP.sln"
        }
    }

    stage('Clean') {
        steps {
            bat "msbuild.exe ${workspace}\\PalMayor-API-Resful-Backend\\ApiVP.sln" /nologo /nr:false /p:platform=\"x64\" /p:configuration=\"release\" /t:clean"
        }
    }

    stage('Increase version') {
        steps {
            echo "${env.BUILD_NUMBER}"
            powershell '''
            $xmlFileName = "PalMayor-API-Resful-Backend\\ApiVP\\Package.appxmanifest"     
            [xml]$xmlDoc = Get-Content $xmlFileName
            $version = $xmlDoc.Package.Identity.Version
            $trimmedVersion = $version -replace '.[0-9]+$', '.'
            $xmlDoc.Package.Identity.Version = $trimmedVersion + ${env:BUILD_NUMBER}
            echo 'New version:' $xmlDoc.Package.Identity.Version
            $xmlDoc.Save($xmlFileName)
            '''
        }
    }

    stage('Build') {
        steps {
            bat "msbuild.exe ${workspace}\\PalMayor-API-Resful-Backend\\ApiVP.sln /nologo /nr:false  /p:platform=\"x64\" /p:configuration=\"release\" /p:PackageCertificateKeyFile=<path-to-certificate-file>.pfx /t:clean;restore;rebuild"
        }
    }

    stage('Running unit tests') {
        steps {
            bat "dotnet add ${workspace}/PalMayor-API-Resful-Backend/ApiVP.Tests/ApiVP.Tests.csproj package JUnitTestLogger --version 1.1.0"
            bat "dotnet test ${workspace}/PalMayor-API-Resful-Backend/ApiVP.Tests/ApiVP.Tests.csproj --logger \"junit;LogFilePath=\"${WORKSPACE}\"/TestResults/1.0.0.\"${env.BUILD_NUMBER}\"/results.xml\" --configuration release --collect \"Code coverage\""
            powershell '''
            $destinationFolder = \"$env:WORKSPACE/TestResults\"
            if (!(Test-Path -path $destinationFolder)) {New-Item $destinationFolder -Type Directory}
            $file = Get-ChildItem -Path \"$env:WORKSPACE/PalMayor-API-Resful-Backend/ApiVP.Tests/TestResults/*/*.coverage\"
            $file | Rename-Item -NewName testcoverage.coverage
            $renamedFile = Get-ChildItem -Path \"$env:WORKSPACE/PalMayor-API-Resful-Backend/ApiVP.Tests/TestResults/*/*.coverage\"
            Copy-Item $renamedFile -Destination $destinationFolder
            '''
        }        
    }
}