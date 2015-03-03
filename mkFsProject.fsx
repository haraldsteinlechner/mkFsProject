
open System.IO
open System.Net
open System.Diagnostics

printfn "creating directory structure"
Directory.CreateDirectory "packages"
Directory.CreateDirectory "tools"

printfn "downloading nuget.exe"
let client = new WebClient ()
client.DownloadFile("https://github.com/vrvis/aardvark/tree/master/bin/nuget.exe", @".\tools");

printfn "got nuget"

printfn "bootstrapping (download)"
client.DownloadFile("https://raw.githubusercontent.com/haraldsteinlechner/mkFsProject/master/build.sh","build2.sh");

printfn "build file (download)"
client.DownloadFile("https://raw.githubusercontent.com/haraldsteinlechner/mkFsProject/master/build.fsx","build2.fsx");

let p = new Process()
p.StartInfo.UseShellExecute <- false
p.StartInfo.RedirectStandardError <- true
p.StartInfo.RedirectStandardOutput <- true
p.StartInfo.FileName <- "/bin/sh"
p.StartInfo.Arguments <- "./build.sh"
p.Start()
p.WaitForExit()
if p.HasExited then 
    let s = p.StandardOutput.ReadToEnd()
    if s.Length > 0 then printfn "%s" s
if p.ExitCode <> 0 then
    printfn "%s" <| p.StandardError.ReadToEnd()
printfn "boostrap returned: %d" p.ExitCode