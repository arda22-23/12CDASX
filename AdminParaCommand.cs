name: RPEkonomi Build

on:
  push:
    branches: [ main, master ]
  workflow_dispatch:

jobs:
  build:
    runs-on: windows-latest

    steps:
      - name: Kodu İndir
        uses: actions/checkout@v4

      - name: .NET Framework 4.6.1 Kur
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '4.8.x'

      - name: NuGet Paketlerini Yükle ve Derle
        shell: powershell
        run: |
          cd RPEkonomi
          dotnet restore RPEkonomi.csproj
          dotnet build RPEkonomi.csproj -c Release

      - name: DLL'yi Artifact Olarak Yükle
        uses: actions/upload-artifact@v4
        with:
          name: RPEkonomi
          path: RPEkonomi/bin/Release/net461/RPEkonomi.dll
          if-no-files-found: error
