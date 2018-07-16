module Cmds

open Argu

type Config =
    | Help

and Generate =
    | Help

and Help =
    | Command of string

and Init =
    | Help

and Link=
    | Help

and List =
    | Help

and New =    
    | [<Mandatory>] TitleText of string
with 
    interface IArgParserTemplate with
        member this.Usage = 
            match this with            
            | TitleText _ -> "new title...."

and UpgradeRepository=
    | Help

and AdrArgs = 
    | Version
    | [<CliPrefix(CliPrefix.None)>] New of ParseResults<New>
with 
    interface IArgParserTemplate with
        member this.Usage = 
            match this with
            | Version -> "Prints the version number"
            | New _ -> "Create a new adr"

