namespace Fsconsole

open ApiAction
open Result

module ApiActionResult = 
    let map f =
        ApiAction.map (Result.map f)

    let retn x = 
        ApiAction.retn (Result.retn x)

    let apply fActionResult xActionResult = 
        let newAction api =
            let fResult = ApiAction.run api fActionResult 
            let xResult = ApiAction.run api xActionResult 
            Result.apply fResult xResult 
        ApiAction newAction

    let bind f xActionResult = 
        let newAction api = 
            let xResult = ApiAction.run api xActionResult
            match xResult with
            | Success x ->
                ApiAction.run api (f x)
            | Failure errs ->
                (Failure errs)

        ApiAction newAction