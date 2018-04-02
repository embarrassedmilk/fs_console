namespace Fsconsole

open System.Collections.Generic

module e1_1 =
    let id a =
        a

    let compose (f: 'a -> 'b) (g: 'b -> 'c) : 'a -> 'c = (fun a -> g (f a)) //didn't use chevron on purpose

    let memoize f =
        let cache = ref Map.empty
        fun args ->
            match (!cache).TryFind(args) with
            | Some res -> res
            | None     -> 
                let res = f args
                cache := (!cache).Add(args,res)
                res

    //preorder: id and compose <=