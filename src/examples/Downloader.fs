namespace Fsconsole
open Result
open Async
open TraverseListAsync
open TraverseListResult
open System
open System.Net

module Downloader =
    type [<Measure>] ms

    type WebClientWithTimeout(timeout:int<ms>) =
        inherit System.Net.WebClient()

        override this.GetWebRequest(address) =
            let result = base.GetWebRequest(address)
            result.Timeout <- int timeout 
            result

    type UriContent = 
        UriContent of System.Uri * string

    type UriContentSize = 
        UriContentSize of System.Uri * int

    let getUriContent (uri:System.Uri) =
        async {
            use client = new WebClientWithTimeout(1000<ms>)
            try
                printfn " [%s] Started ..." uri.Host
                client.DownloadStringAsync(uri)
                let! html = Async.AwaitEvent(client.DownloadStringCompleted)
                printfn " [%s] Finished ..." uri.Host
                let uriContent = UriContent (uri, html.Result)
                return (Result.Success uriContent)
            with
            | ex ->
                printfn " [%s] ... exception" uri.Host
                let err = sprintf "[%s] %A" uri.Host ex.Message
                return Result.Failure [err ]
            }

    let makeContentSize (UriContent(uri, html)) = 
        if System.String.IsNullOrEmpty(html) then
            Result.Failure ["empty page"]
        else
            let uriContentSize = UriContentSize (uri, html.Length)
            Result.Success uriContentSize 
    
    let getUriContentSize uri =
        getUriContent uri
        |> Async.map (Result.bind makeContentSize)

    let maxContentSize list = 
        let contentSize (UriContentSize (_, len)) = len

        list |> List.maxBy contentSize

    let doTheStuff list =
        (List.map (System.Uri >> getUriContentSize) list)
        |> TraverseListAsync.sequenceAsyncA
        |> Async.map sequenceResultA
        |> Async.map (Result.map maxContentSize)

    let showContentSizeResult result =
        match result with
        | Success (UriContentSize (uri, len)) -> 
            printfn "SUCCESS: [%s] Content size is %i" uri.Host len 
        | Failure errs -> 
            printfn "FAILURE: %A" errs