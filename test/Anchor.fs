module Anchor

open Expecto

open System.Text.RegularExpressions
open Regx

//Tests are from https://regexr.com/
//Anchors
let tests =
    [ test "Starts with" {
          let pattern = regex { beginsWith { oneOrMore { wordChar } } } |> Regx.make

          let input = "she sells seashells"
          let result = Regex.Matches(input, pattern) |> Seq.head
          Expect.equal result.Value "she" $"'{input}' should match generated pattern {pattern}. Expected pattern ^\w+"
      }
      test "Ends with" {
          let pattern = regex { endsWith { oneOrMore { wordChar } } } |> Regx.make

          let input = "she sells seashells"
          let result = Regex.Matches(input, pattern) |> Seq.head

          Expect.equal
              result.Value
              "seashells"
              $"'{input}' should match generated pattern {pattern}. Expected pattern \w+$"
      }
      test "Starts with string" {
          let pattern = regex { beginsWithString { oneOrMore { wordChar } } } |> Regx.make

          let input = "she sells seashells"
          let result = Regex.Matches(input, pattern) |> Seq.head
          Expect.equal result.Value "she" $"'{input}' should match generated pattern {pattern}. Expected pattern /A\w+"
      }
      test "Ends with string" {
          let pattern = regex { endsWithString { oneOrMore { wordChar } } } |> Regx.make

          let input = "she sells seashells"
          let result = Regex.Matches(input, pattern) |> Seq.head

          Expect.equal
              result.Value
              "seashells"
              $"'{input}' should match generated pattern {pattern}. Expected pattern \w+/z"
      }
      test "Boundary" {
          let pattern =
              regex {
                  verbatimString @"d"
                  wordBoundary
              }
              |> Regx.make

          let input = "word boundaries are odd"
          let result = Regex.Matches(input, pattern) |> Seq.head
          Expect.equal result.Value "d" $"'{input}' should match generated pattern {pattern}. Expected pattern d\b"
      }
      test "Not a Boundary" {
          let pattern =
              regex {
                  verbatimString @"r"
                  notWordBoundary
              }
              |> Regx.make

          let input = "regex is really cool"
          let result = Regex.Matches(input, pattern) |> Seq.head
          Expect.equal result.Value "r" $"'{input}' should match generated pattern {pattern}. Expected pattern r\B"
      }




      ]
    |> testList "Anchors"
