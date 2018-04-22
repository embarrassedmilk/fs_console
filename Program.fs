open System
open Fsconsole
open Either

[<EntryPoint>]
let main argv =
    Console.WriteLine "start"
    let safeRootReciprocal = safeRoot >=> safeReciprocal

    Console.WriteLine (safeRootReciprocal 0.0)

    0 // return an integer exit code
