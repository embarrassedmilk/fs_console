namespace Fsconsole

open Api
open ApiAction
open Result
open Fsconsole.List

module ReaderExample = 
    type CustId = CustId of string
    type ProductId = ProductId of string
    type ProductInfo = {ProductName: string}

    let getPurchaseId (custId:CustId) =
        let action (api:ApiClient) =
            api.Get<ProductId list> custId

        ApiAction action

    let getProductInfo (productId:ProductId) = 
        let action (api:ApiClient) = 
            api.Get<ProductInfo> productId
        
        ApiAction action

    let getPurchaseInfo =
        let getProductInfoLifted =
            getProductInfo
            |> traverse
            |> ApiActionResult.bind
        getPurchaseId >> getProductInfoLifted

    //helpers:
    let showResult result =
        match result with
        | Success (productInfoList) -> 
            printfn "SUCCESS: %A" productInfoList
        | Failure errs -> 
            printfn "FAILURE: %A" errs

    let setupTestData (api:ApiClient) =
        api.Set (CustId "C1") [ProductId "P1"; ProductId "P2"] |> ignore
        api.Set (CustId "C2") [ProductId "PX"; ProductId "P2"] |> ignore

        api.Set (ProductId "P1") {ProductName="P1-Name"} |> ignore
        api.Set (ProductId "P2") {ProductName="P2-Name"} |> ignore

    let setupAction = ApiAction setupTestData
    ApiAction.execute setupAction