module CustomHelpers

open Expecto

open System.Text.RegularExpressions
open Regx
open Regx.Helpers

//Escape
let tests =
    [ test "Words" {
          let pattern = regex { oneOrMore { word } } |> Regx.make

          let input = @"This is a sentence."
          let result = Regex.Matches(input, pattern) |> Seq.length

          Expect.equal result 4 $"'{input}' should match generated pattern {pattern}. Expected pattern (?:\w+)+"
      }

      ]
    |> testList "Custom word helper"
