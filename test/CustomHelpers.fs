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
      test "Emails" {
          let pattern = regex { email } |> Regx.make
          let input = @"elon.musk_108@space-x.com"
          let result = Regex.Match(input, pattern)

          Expect.equal result.Success true $"'{input}' should match generated pattern {pattern}"
      //(?:^(?<emailId>[a-zA-Z0-9\.!#\$%&’\*\+\/=\?\^_`\{\|\}~-]+)@(?<domain>[a-zA-Z0-9-]+)(?:\.(?<ending>[a-zA-Z0-9-]+))*$)
      } ]
    |> testList "Custom word helper"
