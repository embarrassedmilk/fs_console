// Learn more about F# at http://fsharp.org

open System
// open Fsconsole.OrdersUsingImperativeLoop
// open Fsconsole.MonoidalChar
// open Fsconsole.MonodialValidation
open Fsconsole.Result
open Fsconsole.Customers

[<EntryPoint>]
let main argv =
    // let orderLine1 = Product {ProductCode="AAA"; Qty=2; Price=9.99; LineTotal=19.98}
    // let orderLine2 = Product {ProductCode="BBB"; Qty=1; Price=1.99; LineTotal=1.99}
    // let orderLine3 = addLine orderLine1 orderLine2

    // let zero = EmptyOrder

    // let testLine = addLine orderLine1 zero
    // assert (testLine = orderLine1)
    
    // orderLine1 |> printLine
    // orderLine2 |> printLine
    // orderLine3 |> printLine

    // let a = 'a' |> toMchar
    // let b = 'b' |> toMchar
    // let c = a ++ b

    // c |> toString |> printfn "a + b = %s"

    // let mvaltest = 
    //     let validationResults str = 
    //         [
    //             validateLength 10
    //             validateBadWord "monad"
    //             validateBadWord "cobol"
    //         ]
    //         |> List.map(fun validate -> validate str)
        
    //     "cobol has native support for monads"
    //     |> validationResults
    //     |> List.reduce add
    //     |> printfn "Result is %A"

    let goodId = 1
    let badId = 0
    let goodEmail = "test@test.com"
    let badEmail = "screech.com"

    //applicative
    let goodCustomerA = createCustomerResultA goodId goodEmail
    match goodCustomerA with
        | Success info -> 
            printfn "yaaay"
        | Failure errs ->
            errs |> Seq.iter (printf "%s ,")

    let badCustomerA = createCustomerResultA badId badEmail
    match badCustomerA with
        | Success info -> 
            printfn "baaad"
        | Failure errs ->
            errs |> Seq.iter (printfn "%s ")

    //monadic
    let goodCustomerM = createCustomerResultM goodId goodEmail
    match goodCustomerM with
        | Success info -> 
            printfn "yaaay m"
        | Failure errs ->
            errs |> Seq.iter (printf "%s ,")

    let badCustomerM = createCustomerResultM badId badEmail
    match badCustomerM with
        | Success info -> 
            printfn "baaad m"
        | Failure errs ->
            errs |> Seq.iter (printfn "%s ")
        
    0 // return an integer exit code
