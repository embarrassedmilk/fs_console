open System
open Fsconsole
open OptionalMath

[<EntryPoint>]
let main argv =
    Console.WriteLine "start"

    let safeRootReciprocal = safeRoot >=> safeReciprocal

    Console.WriteLine (safeReciprocal 25.0)

    0 // return an integer exit code
