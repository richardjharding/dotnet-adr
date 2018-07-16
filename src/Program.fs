// Learn more about F# at http://fsharp.org

open System
open Argu
open Cmds

let parser = ArgumentParser.Create<AdrArgs>(programName = "dotnet-adr.exe")

[<EntryPoint>]
let main argv =
    try
        parser.ParseCommandLine(inputs = argv, raiseOnUsage = true) |> ignore
    with e ->
        printfn "%s" e.Message
    0
