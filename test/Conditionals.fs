module Conditionals

open Expecto

open System.Text.RegularExpressions
open Regx

//Conditionals
let tests =
    [ test "If true" {
          let pattern =
              regex {
                  capture { verbatimString "A candy" }
                  if' (GroupId 1) { verbatimString " is true" }
                  else' { verbatimString " is false" }
              }
              |> Regx.make

          let input = "A candy is true is false."
          let result = Regex.Match(input, pattern)

          Expect.equal
              result.Value
              "A candy is true"
              $"'{input}' should match generated pattern {pattern}. Expected pattern /(A candy)?(?(1) is true| is false)/"
      }

      test "If named true" {
          let pattern =
              regex {
                  captureAs "candy" { verbatimString "A candy" }
                  if' (GroupName "candy") { verbatimString " is true" }
                  else' { verbatimString " is false" }
              }
              |> Regx.make

          let input = "A candy is true is false."
          let result = Regex.Match(input, pattern)

          Expect.equal
              result.Value
              "A candy is true"
              $"'{input}' should match generated pattern {pattern}. Expected pattern /(A candy)?(?(1) is true| is false)/"
      }


      test "If false" {
          let pattern =
              regex {
                  capture { verbatimString "A candy" }
                  if' (GroupId 1) { verbatimString " is true" }
                  else' { verbatimString " is false" }
              }
              |> Regx.make

          let input = "A not candy is true is false."
          let result = Regex.Match(input, pattern)

          Expect.equal
              result.Value
              " is false"
              $"'{input}' should match generated pattern {pattern}. Expected pattern /(A candy)?(?(1) is true| is false)/"
      }

      test "If named false" {
          let pattern =
              regex {
                  captureAs "candy" { verbatimString "A candy" }
                  if' (GroupName "candy") { verbatimString " is true" }
                  else' { verbatimString " is false" }
              }
              |> Regx.make

          let input = "A not candy is true is false."
          let result = Regex.Match(input, pattern)

          Expect.equal
              result.Value
              " is false"
              $"'{input}' should match generated pattern {pattern}. Expected pattern /(A candy)?(?(1) is true| is false)/"
      }

      test "If lookahead" {
          let conditional = regex { positiveLookAhead { verbatimString "is" } } |> Regx.make

          let pattern =
              regex {
                  if' (Group conditional) { capture { verbatimString "is delicious" } }
                  else' { capture { verbatimString "disgusting" } }
              }
              |> Regx.make

          let input = "Candy is delicious or disgusting."
          let result = Regex.Match(input, pattern)

          Expect.equal
              result.Value
              "is delicious"
              $"'{input}' should match generated pattern {pattern}. Expected pattern /(?(?=is)(is delicious)|(disgusting))/"
      }

      test "If lookbehind" {
          let conditional = regex { positiveLookBehind { whiteSpace } } |> Regx.make

          let pattern =
              regex {
                  if' (Group conditional) { capture { verbatimString "delish" } }
                  else' { capture { verbatimString "ew" } }
              }
              |> Regx.make

          let input = "Is candy delish or ew?"
          let result = Regex.Match(input, pattern)

          Expect.equal
              result.Value
              "delish"
              $"'{input}' should match generated pattern {pattern}. Expected pattern /(?(?=is)(is delicious)|(disgusting))/"
      } ]
    |> testList "Conditionals"
