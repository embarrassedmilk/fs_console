namespace Fsconsole


module OptionList = 
    let (<*>) = Option.apply
    let retn = Some

    let rec mapOption f list = 
        let cons head tail = head :: tail
        match list with
            | [] ->
                retn []
            | head::tail ->
                retn cons <*> (f head) <*> (mapOption f tail)

    let parseInt str = 
        match (System.Int32.TryParse str) with
        | true, i -> 
            Some i
        | false, _ -> 
            None

    

    