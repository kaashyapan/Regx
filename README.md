# Regx - Declarative Regex for humans
## F# DSL for Regex [nuget](https://www.nuget.org/packages/Regx)
```fsharp
#r "nuget: Regx"
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
        oneOrMore { wordChar }
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
 ```


More examples in tests


|DSL  |What it means  |
|--|--|
| `any` |Dot - Matches any character except linebreaks. Equivalent to [^\n\r].  |
| `anyChar` |Match any - A character set that can be used to match any character, including line breaks, without the dotall flag. Represents [\s\S]  |
| `wordChar` |Matches any word character (alphanumeric & underscore). Only matches low-ascii characters (no accented or non-roman characters). Equivalent to [A-Za-z0-9_]  |
| `notWordChar` |Matches any character that is not a word character (alphanumeric & underscore). Equivalent to [^A-Za-z0-9_]  |
| `digit` |Matches any digit character (0-9). Equivalent to [0-9].  |
| `notDigit` |Matches any character that is not a digit character (0-9). Equivalent to [^0-9].  |
| `whiteSpace` |Matches any whitespace character (spaces, tabs, line breaks).  |
| `notWhiteSpace` |Matches any character that is not a whitespace character (spaces, tabs, line breaks).  |
| `charIn` |Match any character in the set  |
| `charNotIn` |Negate character set - Match any character that is not in the set.  |
| `inRange` |Matches a character having a character code between the two specified characters inclusive. |
| `inList` |Matches characters in list|
| `beginsWith` |Enclosed expression occurs in the beginning of the fragment |
| `endsWith` |Enclosed expression occurs in the end of the fragment |
| `beginsWithString` |Enclosed expression occurs in the beginning of the fragment  |
| `endsWithString` |Enclosed expression occurs in the end of the fragment  |
|`wordBoundary` |Matches a word boundary position between a word character and non-word character or position (start / end of string).|
|`notWordBoundary`|Matches any position that is not a word boundary. This matches a position, not a character.|
|`mayHave`|Matches 0 or 1 of the enclosed fragment, effectively making it optional|
|`fewest`|Makes the enclosed fragment lazy, causing it to match as few characters as possible.|
|`longest`|Makes the enclosed fragment Greedily, causing it to match as many characters as possible.|
|`oneOf`|Acts like a boolean OR. Matches the expression before or after the|
|`oneOrMore`|Matches 1 or more of the enclosed fragment|
|`zeroOrMore`|Matches 0 or more of the enclosed fragment|
|`occurs`|Matches the exact quantity of the enclosed token.|
|`occursMoreThan`|Matches the specified quantity or more of the enclosed token.|
|`occursBetween`|Matches a quantity of the enclosed toke in the specified range.|
|`between`|Match between an open and a closing tag- for balanced captures|
|`capture`| Grouped capture|
|`captureAs`|Named group capture|
|`group`|Non-capturing group|
|`refGroupNo`|\1 - Numeric reference - Matches the results of a capture group.|
|`refGroupName`|\k<name> - Group name reference - Matches the results of a named capture group.|
|`positiveLookAhead`| \d(?=px) - Matches a group after the main expression without including it in the result.|
|`negativeLookAhead`| \d(?!px) - Specifies a group that can not match after the main expression (if it matches, the result is discarded).|
|`positiveLookBehind`| (?<=ABC) - Matches a group before the main expression without including it in the result.|
|`negativeLookBehind`| (?<!ABC) - Specifies a group that can not match before the main expression (if it matches, the result is discarded).|
|`verbatimString`|Unescaped string literal|
|`tab`||
|`lineFeed`||
|`verticalTab`||
|`formFeed`||
|`carriageReturn`||
|`nullCharacter`||

### Additional custom builders

|DSL  |What it means  |
|--|--|
| `word` | Represents an actual word. Equivalent to /(?:\w+)/  |
| `email` | English email |
| `url` | English url |
| `guid` | GUID |
| `uuid` | UUID |
| `ip4Address` | IPv4 |
| `ip6Address` | IPv6 |
