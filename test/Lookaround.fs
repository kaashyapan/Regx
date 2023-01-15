module Lookaround

open Expecto

open System.Text.RegularExpressions
open Regx

//Tests are from https://regexr.com/
//Anchors
let tests =
    [ test "Positive Lookahead" {
          let pattern =
              regex {
                  verbatimString "foo"
                  positiveLookAhead { verbatimString "bar" }
              }
              |> Regx.make

          let input = "foobar foobaz"
          let result = Regex.Matches(input, pattern) |> Seq.length
          Expect.equal result 1 $"'{input}' should match generated pattern {pattern}. Expected pattern foo(?=bar)"
      }
      test "Negative Lookahead" {
          let pattern =
              regex {
                  verbatimString "foo"
                  negativeLookAhead { verbatimString "bar" }
              }
              |> Regx.make

          let input = "foobar foobaz"
          let result = Regex.Matches(input, pattern) |> Seq.length
          Expect.equal result 1 $"'{input}' should match generated pattern {pattern}. Expected pattern foo(?!bar)"
      }
      test "Positive Lookbehind" {
          let pattern =
              regex {
                  positiveLookBehind { verbatimString "foo" }
                  verbatimString "bar"
              }
              |> Regx.make

          let input = "foobar fuubaz"
          let result = Regex.Matches(input, pattern) |> Seq.length
          Expect.equal result 1 $"'{input}' should match generated pattern {pattern}. Expected pattern (?<=foo)bar"
      }
      test "Negative Lookbehind" {
          let pattern =
              regex {
                  negativeLookBehind { verbatimString "not " }
                  verbatimString "foo"
              }
              |> Regx.make

          let input = "not foo but foo"
          let result = Regex.Matches(input, pattern) |> Seq.length
          Expect.equal result 1 $"'{input}' should match generated pattern {pattern}. Expected pattern (?<!not)foo"
      } ]
    |> testList "Lookaround"
