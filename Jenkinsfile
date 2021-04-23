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
    stage('Publish'){
      steps{
        bat "dotnet publish PalMayor\\ApiVP.csproj"
      }
    }
  }
}