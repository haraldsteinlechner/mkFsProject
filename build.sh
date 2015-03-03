#!/bin/bash

if [ ! -d "Packages/FAKE" ]; then
	echo "downloading FAKE"
	mono --runtime=v4.0 Packages/nuget.exe install FAKE -OutputDirectory packages -ExcludeVersion
	mono --runtime=v4.0 Packages/nuget.exe install FSharp.Formatting.CommandTool -OutputDirectory packages -ExcludeVersion -Prerelease 
	mono --runtime=v4.0 Packages/nuget.exe install SourceLink.Fake -OutputDirectory packages -ExcludeVersion 
fi


mono Packages/FAKE/tools/FAKE.exe "build.fsx"  $@
