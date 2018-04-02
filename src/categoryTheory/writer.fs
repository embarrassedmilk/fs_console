namespace Fsconsole

module Writer =
    type Writer<'a> = ('a*string)

    let inline retn (x:'a) : Writer<'a> =
        (x,"")

    let inline (>=>) (f1: 'a -> Writer<'b>) (f2: 'b -> Writer<'c>) : 'a -> Writer<'c> =
        fun (x:'a) ->
            let (y,s1) = f1 x
            let (z,s2) = f2 y
            (z, s1+s2)

module OptionalMath =
    type OptionalMath<'a> = ('a*bool)

    let id (x: 'a): 'a =
        x

    let retn (x:'a): OptionalMath<'a> =
        (x,true)
    
    let inline (>=>) (f1: 'a -> OptionalMath<'b>) (f2: 'b -> OptionalMath<'c>) : 'a -> OptionalMath<'c> =
        fun (x: 'a) ->
            let y, b1 = f1 x
            let z, b2 = 
                match b1 with
                | true  -> f2 y
                | false -> (Unchecked.defaultof<'c>, false)
            (z, b1 && b2)
    
    let safeRoot (a:double) : OptionalMath<double> =
        match a > 0.0 with
        | true  -> ((sqrt a), true)
        | false -> (0.0, false)

    let safeReciprocal (a:double) : OptionalMath<double> =
        match a <> 0.0 with
        | true  -> retn (1.0/a)
        | false -> (0.0, false)

    