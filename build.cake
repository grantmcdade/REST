#addin nuget:?package=Cake.Curl&version=3.0.0

var target = Argument("target", "Default");
var packageOutputDirectory = Argument("PackageOutputDirectory", "package");
var configuration = Argument("Configuration", "Release");
var version = "0.1.0";

Task("Build")
  .Does(() =>
{
  DotNetCoreBuild("./Sample.sln",
    new DotNetCoreBuildSettings {
      Configuration = configuration
    });
});

Task("Test")
  .IsDependentOn("Build")
  .Does(() =>
{
  DotNetCoreTest("./tests/API.Tests/API.Tests.csproj",
    new DotNetCoreTestSettings {
      Configuration = configuration
    });
});

Task("Publish")
  .IsDependentOn("Build")
  .IsDependentOn("Version")
  .IsDependentOn("Test")
  .Does(() =>
{
  if (DirectoryExists(packageOutputDirectory))
  {
    DeleteDirectory(packageOutputDirectory, new DeleteDirectorySettings {
      Recursive = true
    });
  }

  EnsureDirectoryExists(packageOutputDirectory);

  DotNetCorePublish("./src/API/API.csproj",
    new DotNetCorePublishSettings {
      Configuration = configuration
    });

  Information($"Creating { packageOutputDirectory }/API.{ version }.zip");
  Zip($"./src/API/bin/{ configuration }/netcoreapp2.1/publish", $"{ packageOutputDirectory }/API.{ version }.zip");
});

Task("Version")
  .Does(() =>
{
  version = XmlPeek("./src/API/API.csproj", "/Project/PropertyGroup/Version/text()");
  Information($"Read version number { version }");
});

Task("Deploy")
  .IsDependentOn("Publish")
  .Does(() =>
{
  var username = EnvironmentVariable("DeployUsername");
  var password = EnvironmentVariable("DeployPassword");

  if (String.IsNullOrEmpty(password))
  {
      Information("Password is not empty!");
  }

  Information($"Deploying as user: { username }");

  CurlUploadFile(
    $"{ packageOutputDirectory }/API.{ version }.zip",
    new Uri("https://rest-demo.scm.azurewebsites.net/api/zipdeploy"),
    new CurlSettings {
      RequestCommand = "POST",
      Username = username,
      Password = password,
      ArgumentCustomization = args => {
        return args.Append("--fail");
      }
    });
});

Task("Default")
  .IsDependentOn("Build");

RunTarget(target);