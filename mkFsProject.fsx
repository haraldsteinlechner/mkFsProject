
open System.IO
open System.Net
open System.Diagnostics

printfn "creating directory structure"
Directory.CreateDirectory "packages"
Directory.CreateDirectory "bin"

printfn "downloading nuget.exe"
let client = new WebClient ()
client.CachePolicy <- new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
client.DownloadFile("https://github.com/haraldsteinlechner/mkFsProject/raw/master/bin/nuget.exe", @"bin/nuget.exe");

printfn "got nuget"

printfn "bootstrapping (download)"
client.DownloadFile("https://raw.githubusercontent.com/haraldsteinlechner/mkFsProject/master/build.sh","build.sh");
client.DownloadFile("https://raw.githubusercontent.com/haraldsteinlechner/mkFsProject/master/build.bat","build.bat");

printfn "build file (download)"
client.DownloadFile("https://raw.githubusercontent.com/haraldsteinlechner/mkFsProject/master/build.fsx","build.fsx");

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

if not <| System.IO.File.Exists ".gitignore" ||  not <| System.IO.File.ReadAllText(".gitignore").Contains("packages") then
    System.IO.File.AppendAllText (".gitignore" , "packages\nbin\n!/bin/nuget.exe\n*.swp\n")
