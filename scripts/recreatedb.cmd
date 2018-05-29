pushd .\src\API
dotnet run dropdb migratedb stop
dotnet run createuser stop
popd
