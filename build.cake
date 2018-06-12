#addin nuget:?package=Cake.Curl&version=3.0.0

var target = Argument("target", "Default");
var packageOutputDirectory = Argument("PackageOutputDirectory", "package");
var version = "0.1.0";

Task("Build")
  .Does(() =>
{
  DotNetCoreBuild("./Sample.sln");
});

Task("Test")
  .IsDependentOn("Build")
  .Does(() =>
{
  DotNetCoreTest("./tests/API.Tests/API.Tests.csproj");
});

Task("Publish")
  .IsDependentOn("Build")
  .IsDependentOn("Version")
  .Does(() =>
{
  DeleteDirectory(packageOutputDirectory, new DeleteDirectorySettings {
    Recursive = true
  });

  EnsureDirectoryExists(packageOutputDirectory);

  DotNetCorePublish("./src/API/API.csproj",
    new DotNetCorePublishSettings {
      OutputDirectory = packageOutputDirectory
    });

  Information($"Creating { packageOutputDirectory }/API.{ version }.zip");
  Zip(packageOutputDirectory, $"{ packageOutputDirectory }/API.{ version }.zip");
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
  CurlUploadFile(
    $"{ packageOutputDirectory }/API.{ version }.zip",
    new Uri("https://rest-demo.scm.azurewebsites.net/api/zipdeploy"),
    new CurlSettings {
      RequestCommand = "POST",
      Username = EnvironmentVariable("DeployUsername"),
      Password = EnvironmentVariable("DeployPassword"),
      ArgumentCustomization = args => {
        return args.Append("--fail");
      }
    });
});

Task("Default")
  .IsDependentOn("Build");

RunTarget(target);