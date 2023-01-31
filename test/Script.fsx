#load "../src/Category.fs"
#load "../src/Builder.fs"
#load "../src/Regx.fs"

open Regx
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
        oneOrMore { word }
        verbatimString "."
    }

let domain =
    group {
        captureAs "domain" { occursBetween 2 256 { word } }
        verbatimString "."
    }

let ending = group { occursBetween 1 8 { word } }

let port =
    group {
        verbatimString ":"
        occursMoreThan 2 { digit }
    }

let path =
    zeroOrMore {
        capture {
            verbatimString "/"
            oneOrMore { word }
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
