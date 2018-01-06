namespace Fsconsole

open Result

module ResultList = 
    let (<*>) = Result.apply
    let retn = Success

    let rec mapResult f list = 
        let cons head tail = head :: tail
        match list with
            | [] ->
                retn []
            | head::tail ->
                retn cons <*> (f head) <*> (mapResult f tail)

    let parseInt str = 
        match (System.Int32.TryParse str) with
        | true, i -> 
            Success i
        | false, _ -> 
            Failure [str + " is not an integer"]