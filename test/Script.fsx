#load "../src/Category.fs"
#load "../src/Builder.fs"
#load "../src/Regx.fs"

open Regx
// Compose a regex for url 
let protocol = 
  mayHave {
    group {
      capture {
        oneOf { 
          verbatimString "http" 
          verbatimString "https" 
        }
      }
      verbatimString ":\/\/" 
    }
  }

let subdomain = 
  mayHave {
    group {
      oneOrMore { word }
      verbatimString "\."
    }
  }

let domain = 
  group {
    captureAs "domain" {
      occursBetween 2 256 {
        word
      }
    }
    verbatimString "\."
  }

let ending = 
  group {
    occursBetween 1 8 {
      word
    }
  }

let port = 
  mayHave {
    oneOf {
      group {
        verbatimString ":"
        occursMoreThan 2 { digit } 
      }
      group {
        verbatimString "\/:"
        occursMoreThan 2 { digit } 
      }
    }

  }

let path =
  zeroOrMore {
    capture {
      verbatimString "\/"
      oneOrMore { word } 
    }
  }

regex {
  protocol
  subdomain
  domain
  ending
  port
  path
}
|> Regx.make
|> printfn "Regex %A"
