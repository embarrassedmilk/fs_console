namespace Fsconsole

module Option = 
    let apply fOption xOption = 
        match fOption, xOption with
            | Some f, Some x ->
                Some (f x)
            | _ ->
                None