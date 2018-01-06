namespace Fsconsole
open Result
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

    let showContentResult result = 
        match result with
            | Success (UriContent (uri, html)) ->
                printfn "SUCCESS: [%s] First 100 chars: %s" uri.Host (html.Substring(0,100))
            | Failure errs ->
                printfn "FAILURE: %A" errs