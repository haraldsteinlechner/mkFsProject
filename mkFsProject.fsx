
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
client.DownloadFile("https://github.com/haraldsteinlechner/mkFsProject/blob/master/build.sh","build.sh");

printfn "build file (download)"
client.DownloadFile("https://github.com/haraldsteinlechner/mkFsProject/blob/master/build.fsx","build.fsx");

let p = new Process()
p.StartInfo.UseShellExecute <- false
p.StartInfo.RedirectStandardOutput <- true
p.StartInfo.FileName = "build.sh"
p.Start()
p.WaitForExit()