namespace Fsconsole
open Async

module TraverseListAsync =
    let rec traverseAsyncA f list = 
        let (<*>) = Async.apply
        let retn = Async.retn
        
        let cons head tail = head :: tail

        let initState = retn []
        
        let folder head tail = 
            retn cons <*> (f head) <*> tail

        List.foldBack folder list initState

    let sequenceAsyncA x = traverseAsyncA id x

    // let rec traverseResultM f list =
    //     let (>>=) x f = Result.bind f x
    //     let retn = Result.Success

    //     let cons head tail = head :: tail

    //     let initState = retn []

    //     let folder head tail = 
    //         f head >>= (fun h ->
    //         tail >>= (fun t -> 
    //         retn (cons h t)))

    //     List.foldBack folder list initState