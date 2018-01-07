namespace Fsconsole
open Result

module TraverseListResult =
    let traverseResultA f list = 
        let (<*>) = Result.apply
        let retn = Result.Success
        
        let cons head tail = head :: tail

        let initState = retn []
        
        let folder head tail = 
            retn cons <*> (f head) <*> tail

        List.foldBack folder list initState

    let sequenceResultA x = traverseResultA id x

    let rec traverseResultM f list =
        let (>>=) x f = Result.bind f x
        let retn = Result.Success

        let cons head tail = head :: tail

        let initState = retn []

        let folder head tail = 
            f head >>= (fun h ->
            tail >>= (fun t -> 
            retn (cons h t)))

        List.foldBack folder list initState