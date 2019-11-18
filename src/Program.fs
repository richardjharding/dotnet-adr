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
                        let title = t.TryGetResult Title
                        let super = t.TryGetResult Supercedes                
                        match (title, super) with
                        | Some t, Some s -> 
                                    printfn "got a title and supercedes"
                                    printfn "title: %s superceds %i" t s
                        | Some t, None ->
                                    printfn "Got a title, no supercedes"
                                    printfn "title: %s" t
                        | _,_ -> printfn "%s" (parser.PrintUsage()) //TODO how to print usage for subcommand
                        
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
                | Some (Init i) -> 
                                    match i.TryGetResult Path with
                                    | Some p -> printfn "Initialising at %s" p
                                    | None -> printfn "Initialising in default location"
                | None -> printfn "%s" (parser.PrintUsage())

        
    with e ->
        printfn "%s" e.Message
    0
