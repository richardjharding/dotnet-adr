
module Cmds
open Argu
open System.CodeDom.Compiler


type Config =
    | Help


and Help =
    | Command of string

and Init =
    | [<MainCommand>] Path of path:string
    
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | Path _ -> "Folder to create records in defaults to doc/adr"
            

and List  =
    | [<Hidden>]Foo
    // class end
with    
    interface IArgParserTemplate with
        member this.Usage = "List decision records"
    
and Generate = 
    | TOC
    | Graph
with 
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | TOC _ -> "Generate table of contents"
            | Graph _ -> "Graph"

and Link =
    | Help

and Version = 
    | [<Hidden>]Foo
with interface IArgParserTemplate with  
        member this.Usage = "Display the tool version"
    


and New =    
    | [<Mandatory>][<MainCommand>] Title of title:string
    | [<CustomCommandLine("-s")>] Supercedes of adrnumber:int
with 
    interface IArgParserTemplate with
        member this.Usage = 
            match this with            
            | Title _ -> "Title of the new decision record"
            | Supercedes _ -> "The number of the record to superced"

and UpgradeRepository=
    | Help

and AdrArgs = 
    | [<CliPrefix(CliPrefix.None)>] Version of ParseResults<Version>  
    | [<CliPrefix(CliPrefix.None)>] List of ParseResults<List>
    | [<CliPrefix(CliPrefix.None)>] Init of ParseResults<Init>
    | [<CliPrefix(CliPrefix.None)>] New of ParseResults<New>
    | [<CliPrefix(CliPrefix.None)>] Generate of ParseResults<Generate>
with 
    interface IArgParserTemplate with
        member this.Usage = 
            match this with
            | Version _ -> "Prints the version number"            
            | New _ -> "Create a new adr"
            | Init _ -> "Init a new set of records" 
            | List _ -> "List the decision records"
            | Generate _ -> "Generate summary documentation"

