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
    stage('Restore PACKAGES') {
      steps {
        bat "dotnet restore --configfile NuGet.Config"
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
    stage('Publish') {
      steps {
        bat "dotnet nuget push *\nupkgs\.nupkg -k yourApiKey -s            http://myserver/artifactory/api/nuget/nuget-internal-stable/com/sample"
      }
    }
  }
}