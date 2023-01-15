module Escape

open Expecto

open System.Text.RegularExpressions
open Regx

//Escape
let tests =
    [ test "Escaping characters" {
          let pattern =
              regex {
                  mayHave { capture { oneOrMore { any } } }
                  tab
              }
              |> Regx.make

          let input = @"Something	Something"
          let result = Regex.Match(input, pattern)

          Expect.equal
              result.Success
              true
              $"'{input}' should match generated pattern {pattern}. Expected pattern (.*)?\t"
      }

      ]
    |> testList "Escape"
