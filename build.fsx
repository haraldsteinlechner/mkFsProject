#I @"packages/FAKE/tools/"
#r @"FakeLib.dll"

open Fake
open System
open System.IO

let projects = ["src/**/*.fsproj"; ];

Target "Restore" (fun () ->

    let packageConfigs = !!"src/**/packages.config" |> Seq.toList

    let defaultNuGetSources = RestorePackageHelper.RestorePackageDefaults.Sources
    for pc in packageConfigs do
        RestorePackage (fun p -> { p with OutputPath = "Packages" }) pc


)

Target "Clean" (fun () ->
    CleanDir "Bin"
)

Target "Projects" (fun () ->
    MSBuildRelease "Bin/Release" "Build" projects |> ignore
)


// start build
RunTargetOrDefault "Projects"

