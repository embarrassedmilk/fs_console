namespace Fsconsole

module MonodialValidation = 

    type ValidationResult = 
        | Success
        | Failure of string list

    let fail str = 
        Failure [str]

    let validateBadWord badWord (name:string) =
        if name.Contains(badWord) then
            fail ("string contains a bad word: " + badWord)
        else 
            Success 

    let validateLength maxLength name =
        if String.length name > maxLength then
            fail "string is too long"
        else 
            Success

    let zero = Success

    let add r1 r2 = 
        match r1, r2 with 
        | Success, Success -> Success
        | Failure f1, Success -> Failure f1
        | Success, Failure f2 -> Failure f2
        | Failure f1, Failure f2 -> Failure (f1 @ f2)
