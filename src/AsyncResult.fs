namespace Fsconsole
open Result

module AsyncResult = 
    type AsyncResult<'a> = Async<Result<'a>>

    let map f = 
        f |> Result.map |> Async.map

    let retn x =
        x |> Result.retn |> Async.retn

    //Async<Result<('a->'b)>> -> Async<Result<'a>> -> Async<Result<'b>>
    let apply fAsyncResult xAsyncResult =
        fAsyncResult |> Async.bind (fun fResult -> 
        xAsyncResult |> Async.map (fun xResult ->
        Result.apply fResult xResult))

    let bind f xAsyncResult = async {
        let! xResult = xAsyncResult
        match xResult with
            | Success x -> return! f x
            | Failure err -> return (Failure err)
        }