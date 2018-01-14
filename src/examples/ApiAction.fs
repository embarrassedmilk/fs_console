namespace Fsconsole

open Api

module ApiAction =
    type ApiAction<'a> = ApiAction of (ApiClient -> 'a)

    let run api (ApiAction x) = 
        let xResult = x api
        xResult

    let map f x = 
        let newAction api = 
            let xResult = run api x
            f xResult
        
        ApiAction newAction
        
    let retn x = 
        let newAction api = 
            x
        
        ApiAction newAction

    let apply fAction xAction =
        let newAction api =
            let xResult = run api xAction
            let fResult = run api fAction
            fResult xResult

        ApiAction newAction

    let bind f xAction = 
        let newAction api = 
            let xResult = run api xAction
            run api (f xResult)

        ApiAction newAction

    let execute action = 
        use api = new ApiClient()
        api.Open()
        let result = run api action
        api.Close()
        result