pipeline {
  agent any
  environment {
    dotnet = 'C:\\Program Files\\dotnet\\dotnet.exe'
  }

  stages {
    stage('Checkout') {
      steps {
        git credentialsId: 'netdeveloper', url: 'https://github.com/sigiandre/PalMayor-API-Resful-Backend', branch: 'master'
      }
    }
    stage('Restore Packages') {
      steps {
        bat "dotnet restore"
        }
    }
    stage('Clean') {
      steps {
        bat 'dotnet clean'
      }
    }
    stage('Build') {
      steps {
        bat 'dotnet build --configuration Release'
      }
    }
    stage('Pack') {
      steps {
        bat 'dotnet pack --no-build --output nupkgs'
      }
    }
    stage('Running unit tests') {
    steps {
      bat "dotnet add ${workspace}/ApiVP.Tests/ApiVP.Tests.csproj package JUnitTestLogger --version 1.1.0"
      bat "dotnet test ${workspace}/ApiVP.Tests/ApiVP.Tests.csproj --logger \"junit;LogFilePath=\"${WORKSPACE}\"/TestResults/1.0.0.\"${env.BUILD_NUMBER}\"/results.xml\" --configuration release --collect \"Code coverage\""
      powershell '''
      $destinationFolder = \"$env:WORKSPACE/TestResults\"
      if (!(Test-Path -path $destinationFolder)) {New-Item $destinationFolder -Type Directory}
      $file = Get-ChildItem -Path \"$env:WORKSPACE/ApiVP.Tests/TestResults/*/*.coverage\"
      $file | Rename-Item -NewName testcoverage.coverage
      $renamedFile = Get-ChildItem -Path \"$env:WORKSPACE/ApiVP.Tests/TestResults/*/*.coverage\"
      Copy-Item $renamedFile -Destination $destinationFolder
      '''
      }        
    }
    stage('Publish'){
      steps{
        bat "dotnet publish ApiVP.Api\\ApiVP.Api.csproj"
      }
    }
  }
}