namespace Fsconsole

module Either =
    type Either<'l,'r> =
        | Left of 'l
        | Right of 'r

    let retn (v: 'b) : Either<'l,'b> =
        Right v

    let inline (>=>) (f1: 'a -> Either<'l,'b>) (f2: 'b -> Either<'l,'c>) : 'a -> Either<'l,'c> =
        fun (x: 'a) ->
            let eitherB = f1 x
            match eitherB with
            | Left l -> Left l
            | Right r -> f2 r

    let safeRoot (a:double) : Either<string, double> =
        match a > 0.0 with
        | true  -> retn (sqrt a)
        | false -> Left "Input should be positive"

    let safeReciprocal (a:double) : Either<string, double> =
        match a <> 0.0 with
        | true  -> retn (1.0/a)
        | false -> Left "Cannot divide by zero"