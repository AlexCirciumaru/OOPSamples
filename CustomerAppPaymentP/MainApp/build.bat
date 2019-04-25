
echo "Building Implementations ..."
cd ..\Implementations\PayPalPlugin
dotnet build
cd ..\CardPlugin
dotnet build
echo "Done building implementations"

echo "Building main app"
cd ..\..\MainApp
dotnet build
echo "Main App Build done"

copy "..\Implementations\PayPalPlugin\bin\Debug\netstandard2.0\*.dll" ".\bin\Debug\netcoreapp2.0\Plugins\"
copy "..\Implementations\CardPlugin\bin\Debug\netstandard2.0\*.dll" ".\bin\Debug\netcoreapp2.0\Plugins\"