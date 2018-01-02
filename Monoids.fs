namespace Fsconsole

module OrdersUsingImperativeLoop = 
    type ProductLine = {
        ProductCode: string
        Qty: int
        Price: float
        LineTotal: float
    }

    type TotalLine = {
        Qty: int
        OrderTotal: float
    }

    type OrderLine = 
        | Product of ProductLine
        | Total of TotalLine
        | EmptyOrder

    let addLine orderLine1 orderLine2 = 
        match orderLine1, orderLine2 with
        | Product p1, Product p2 ->
            Total { Qty = p1.Qty + p2.Qty; OrderTotal = p1.LineTotal + p2.LineTotal }
        | Product p, Total t -> 
            Total { Qty = p.Qty + t.Qty; OrderTotal = p.LineTotal + t.OrderTotal }
        | Total t, Product p ->
            Total  { Qty = t.Qty + p.Qty; OrderTotal = t.OrderTotal + p.LineTotal }
        | Total t1, Total t2 ->
            Total { Qty = t1.Qty + t2.Qty; OrderTotal = t1.OrderTotal + t2.OrderTotal }
        | EmptyOrder, _ -> 
            orderLine2
        | _, EmptyOrder -> 
            orderLine1

    let printLine = function
        | Product { ProductCode=p; Qty=q; Price=pr; LineTotal=t } ->
            printfn "%-10s %5i %4g each %6g" p q pr t
        | Total { Qty=q; OrderTotal=t } ->
            printfn "%-10s %5i %6g" "TOTAL" q t

    // let addLine orderLine1 orderLine2 =
    //     {
    //         ProductCode = "TOTAL"
    //         Qty = orderLine1.Qty + orderLine2.Qty
    //         Total = orderLine1.Total + orderLine2.Total
    //     }

    // let printLine {ProductCode=p;Qty=q;Total=t} = 
    //     printfn "%-10s %5i %6g" p q t 

    // let calculateOrderTotal lines = 
    //     let accumulateTotal total line =
    //         total + line.Total
    //     lines
    //     |> List.fold accumulateTotal 0.0

    // let orderLines = [
    //     {ProductCode="AAA"; Qty=2; Total=19.98}
    //     {ProductCode="BBB"; Qty=1; Total=1.99}
    //     {ProductCode="CCC"; Qty=3; Total=3.99}
    // ]
    
    // let printTotal lines = 
    //     lines
    //     |> calculateOrderTotal 
    //     |> printfn "Total is %g"

    // let printSummary lines = 
    //     lines
    //     |> List.reduce addLine
    //     |> printLine

    // let (++) a b = addLine a b

    // let printReceipt = 
    //     orderLines
    //     |> List.iter printLine
    //     printfn "-----------------------"
        
    //     orderLines
    //     |> printSummary