#load "../src/Category.fs"
#load "../src/Builder.fs"
#load "../src/Regx.fs"
#load "../src/Helpers.fs"

open Regx
open Regx.Helpers
// Compose a regex for url
let protocol =
    group {
        capture {
            oneOf {
                verbatimString "http"
                verbatimString "https"
            }
        }

        verbatimString "://"
    }

let subdomain =
    group {
        word
        verbatimString "."
    }

let domain =
    group {
        captureAs "domain" { occursBetween 2 256 { wordChar } }
        verbatimString "."
    }

let ending = group { occursBetween 1 8 { wordChar } }

let port =
    group {
        verbatimString ":"
        occursMoreThan 2 { digit }
    }

let path =
    zeroOrMore {
        capture {
            verbatimString "/"
            oneOrMore { wordChar }
        }
    }

let pattern =
    regex {
        mayHave { protocol }
        mayHave { subdomain }
        domain
        ending
        mayHave { port }
        path
    }
    |> Regx.make

// Generates - (?:(http|https):\/\/)?(?:\w+\.)?(?:(?<domain>\w{2,256})\.)(?:\w{1,8})(?::\d{2,})?(\/\w+)*

printfn "Pattern - %A" pattern
let url = """http://www.regex.dev:3000/path1/path2"""

System.Text.RegularExpressions.Regex.Match(url, pattern).Value
|> printfn "Matches - %A"
