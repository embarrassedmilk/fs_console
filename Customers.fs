namespace Fsconsole

open Fsconsole.Result

module Customers = 
    type CustomerId = CustomerId of int
    type CustomerStrangeId = CustomerStrangeId of int
    type EmailAddress = EmailAddress of string
    type CustomerInfo = {
        id: CustomerId
        strangeId: CustomerStrangeId
        email: EmailAddress
    }

    let createCustomerId id = 
        if id > 0 then
            Success(CustomerId id)
        else
            Failure ["Customer id must be positive"]

    let createCustomerStrangeId id = 
        if id = 666 then
            Success(CustomerStrangeId id)
        else
            Failure ["Strange id must be 666"]

    let createEmailAddress str = 
        if System.String.IsNullOrEmpty(str) then
            Failure ["Email must not be empty"]
        elif str.Contains("@") then
            Success (EmailAddress str)
        else
            Failure ["Email must contain @ sign"]

    let createCustomer customerId customerStrangeId email = 
        { id = customerId; strangeId=customerStrangeId; email=email }

    let (<!>) = Result.map
    let (<*>) = Result.apply

    //applicative version
    let createCustomerResultA id strangeId email = 
        let idResult = createCustomerId id
        let strangeIdResult = createCustomerStrangeId strangeId
        let emailResult = createEmailAddress email
        
        let testMap1 = createCustomer <!> idResult
        let testMap2 = Result.map createCustomer idResult

        Result.apply (Result.apply (Result.map createCustomer idResult) strangeIdResult) emailResult
        //createCustomer <!> idResult <*> strangeIdResult <*> emailResult

    let (>>=) x f = Result.bind f x

    //monadic version
    let createCustomerResultM id email = 
        let a = CustomerStrangeId 666
        createCustomerId id >>= (fun customerId -> 
        createEmailAddress email >>= (fun customerEmail ->
        let customer = createCustomer customerId a customerEmail
        Success customer
        ))
    
