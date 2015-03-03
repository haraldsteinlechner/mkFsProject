
open System.IO
open System.Net

printfn "creating directory structure"
Directory.CreateDirectory "packages"
Directory.CreateDirectory "tools"

printfn "downloading nuget.exe"
let client = new WebClient ()
client.DownloadFile("https://github.com/vrvis/aardvark/tree/master/bin/nuget.exe", "tools");

printfn "got nuget"

