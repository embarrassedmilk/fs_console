namespace Fsconsole

open Fsconsole.Result

module Customers = 
    type CustomerId = CustomerId of int
    type EmailAddress = EmailAddress of string
    type CustomerInfo = {
        id: CustomerId
        email: EmailAddress
    }

    let createCustomerId id = 
        if id > 0 then
            Success(CustomerId id)
        else
            Failure ["Customer id must be positive"]

    let createEmailAddress str = 
        if System.String.IsNullOrEmpty(str) then
            Failure ["Email must not be empty"]
        elif str.Contains("@") then
            Success (EmailAddress str)
        else
            Failure ["Email must contain @ sign"]

    let createCustomer customerId email = 
        { id = customerId; email=email }

    let (<!>) = Result.map
    let (<*>) = Result.apply

    //applicative version
    let createCustomerResultA id email = 
        let idResult = createCustomerId id
        let emailResult = createEmailAddress email
        createCustomer <!> idResult <*> emailResult

    let (>>=) x f = Result.bind f x

    //monadic version
    let createCustomerResultM id email = 
        createCustomerId id >>= (fun customerId -> 
        createEmailAddress email >>= (fun customerEmail ->
        let customer = createCustomer customerId customerEmail
        Success customer
        ))
    
