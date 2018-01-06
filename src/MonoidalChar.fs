namespace Fsconsole

module MonoidalChar = 
    open System

    type Mchar = Mchar of Char list

    let toMchar ch = Mchar [ch]

    let addChar (Mchar m1) (Mchar m2) = 
        Mchar(m1 @ m2)

    let (++) = addChar

    let toString (Mchar m) = 
        System.String(List.toArray m)