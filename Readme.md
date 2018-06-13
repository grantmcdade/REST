# REST

## Overview

<!-- https://rulkosh.visualstudio.com/Rulkosh/_apis/build/repos/git/badge?api-version=4.1-preview.1 -->
<!-- https://rulkosh.visualstudio.com/Rulkosh/_apis/build/repos/git/badge?repoId=BradsApp&branchName=cleanarchitecture&api-version=4.1-preview.1 -->

This is a sample REST API implemented with C# and ASP.NET Core WebAPI.

A sample of the API is hosted at [https://rest-demo.azurewebsites.net/swagger/index.html](https://rest-demo.azurewebsites.net/swagger/index.html)

It makes use of the following NuGet libraries.

+ [AutoMapper](https://automapper.org/) - for mapping the report temlates from the database objects
+ [Mediator](https://github.com/jbogard/MediatR) - for the command/query implementation
+ [FluentValidation](https://github.com/JeremySkinner/FluentValidation) - for validation of API input
+ [OpenIddict](https://github.com/openiddict/openiddict-core) - for generating JWT access tokens
+ [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) - for documenting and exploring the API

## Notes
+ This project was created using Microsoft Visual Studio Community 2017 15.7.2
+ The test folder contains a [Post Man](https://www.getpostman.com/) collection export to test the API with.
+ This project is built and deployed to Azure using [VSTS](https://www.visualstudio.com/team-services/)
