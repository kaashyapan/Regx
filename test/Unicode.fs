module Unicodes

open Expecto

open System.Text.RegularExpressions
open Regx

//Unicodes
let tests =
    [ test "Unicode character" {
          let pattern = regex { unicode Punctuation } |> Regx.make

          let input =
              """there are many unicode categories, including "P", which finds all punctuation!"""

          let result = Regex.Matches(input, pattern) |> Seq.length
          Expect.equal result 5 $"'{input}' should match generated pattern {pattern}."
      } ]
    |> testList "Unicodes"
