namespace Fsconsole

module Api =
    type ApiClient() =
        static let mutable data = Map.empty<string, obj>

        member private this.TryCast<'a> key (value:obj) = 
            match value with
            | :? 'a as a ->
                Result.Success a
            | _ ->
                let typeName = typeof<'a>.Name
                Result.Failure [sprintf "Can't cast value at %s to %s" key typeName]

        member this.Get<'a> (id:obj) = 
            let key = sprintf "%A" id
            printfn "[API] Get %s" key
            match Map.tryFind key data with
            | Some o ->
                this.TryCast<'a> key o
            | None ->
                Result.Failure [sprintf "Key %s not found" key]

        member this.Set (id:obj) (value:obj) =
            let key = sprintf "%A" id
            printfn "[API] Set %s" key
            data <- Map.add key value data
            Result.Success ()

        member this.Open() =
            printfn "[API] Opening"

        member this.Close() =
            printfn "[API] Closing"

        interface System.IDisposable with
            member this.Dispose() =
                printfn "[API] Disposing"