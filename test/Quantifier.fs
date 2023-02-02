module Quantifier

open Expecto

open System.Text.RegularExpressions
open Regx

//Tests are from https://regex101.com/
//Quantifiers
let tests =
    [ test "Optional character" {
          let pattern =
              regex {
                  verbatimString "ba"
                  mayHave { verbatimString "a" }
              }
              |> Regx.make

          let input = "ba b a"
          let result = Regex.Matches(input, pattern) |> Seq.head
          Expect.equal result.Value "ba" $"'{input}' should match generated pattern {pattern}. Expected pattern ba?"
      }
      test "Zero or more" {
          let pattern = regex { zeroOrMore { verbatimString "ba" } } |> Regx.make

          let input = "a ba baa aaa ba b"
          let result = Regex.Matches(input, pattern) |> Seq.head
          Expect.equal result.Value "ba" $"'{input}' should match generated pattern {pattern}. Expected pattern ba*"
      }
      test "One or more" {
          let pattern = regex { oneOrMore { verbatimString "a" } } |> Regx.make

          let input = "a aa aaa aaaa bab baab"
          let result = Regex.Matches(input, pattern) |> Seq.length
          Expect.equal result 6 $"'{input}' should match generated pattern {pattern}. Expected pattern ba*"
      }
      test "Exactly 3 of" {
          let pattern = regex { occurs 3 { verbatimString "a" } } |> Regx.make

          let input = "a aa aaa aaaa bab baab"
          let result = Regex.Matches(input, pattern) |> Seq.length
          Expect.equal result 2 $"'{input}' should match generated pattern {pattern}. Expected pattern a{3}"
      }
      test "3 or more of" {
          let pattern = regex { occursMoreThan 3 { verbatimString "a" } } |> Regx.make

          let input = "a aa aaa aaaa aaaaaa"
          let result = Regex.Matches(input, pattern) |> Seq.length
          Expect.equal result 3 $"'{input}' should match generated pattern {pattern}."
      }
      test "between 3 and 6 of a" {
          let pattern = regex { occursBetween 3 6 { verbatimString "a" } } |> Regx.make

          let input = "a aa aaa aaaa aaaaaaaaaa"
          let result = Regex.Matches(input, pattern) |> Seq.length
          Expect.equal result 4 $"'{input}' should match generated pattern {pattern}."
      }
      test "match greedy" {
          let pattern =
              regex {
                  longest {
                      verbatimString "r"
                      wordChar
                  }
              }
              |> Regx.make

          let input = "r re regex"
          let result = Regex.Matches(input, pattern) |> Seq.length
          Expect.equal result 2 $"'{input}' should match generated pattern {pattern}."
      }
      test "match lazy" {
          let pattern =
              regex {
                  fewest {
                      verbatimString "r"
                      wordChar
                  }
              }
              |> Regx.make

          let input = "r re regex"
          let result = Regex.Matches(input, pattern) |> Seq.length
          Expect.equal result 3 $"'{input}' should match generated pattern {pattern}."
      } ]
    |> testList "Quantifiers"
