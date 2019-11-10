// Learn more about F# at http://fsharp.org

open System
open Argu
open Cmds

let parser = ArgumentParser.Create<AdrArgs>(programName = "dotnet-adr.exe")

[<EntryPoint>]
let main argv =
    try        
        let parseResults = parser.ParseCommandLine(inputs = argv, raiseOnUsage = true) 
        
        //let x = parseResults.GetResult <@ New @>
        //parseResults.
        let r = parseResults.GetAllResults()

        let f = parseResults.TryGetResult Version
        match parseResults.TryGetResult Version with
        | Some v -> printfn "Version 1.2.3.4"
        | None ->        
                match parseResults.TryGetSubCommand() with
                | Some (New t)  ->                 
                        let z = t.GetResult Title                
                        match (t.Contains Title) with
                        | true -> 
                                    printfn "got a title"
                                    printfn "title: %s" z
                        | false -> printfn "no title supplied"
                        
                | Some (List _) -> printfn "list called"        
                | Some (Version _) -> printfn "version"
                | Some (Generate g) -> 
                                    printfn "generate summary..."
                                    match g.TryGetResult <@TOC@> with
                                    | Some _ -> printfn "toc requested"
                                    | None -> ()
                                    match g.TryGetResult <@Graph@> with
                                    | Some _ -> printfn "graph requested"
                                    | None -> ()
                | Some (Init i) -> printfn "init called "
                | None -> printfn "%s" (parser.PrintUsage())

        
    with e ->
        printfn "%s" e.Message
    0
