module Groups

open Expecto

open System.Text.RegularExpressions
open Regx

//Tests are from https://regexr.com/
//Groups
let tests =
    [ test "capture group" {
          let pattern =
              regex {
                  verbatimString @"match and "
                  oneOrMore { capture { verbatimString @"capture " } }
              }
              |> Regx.make

          let input = @"match and capture capture ?"

          let result =
              Regex.Match(input, pattern) |> (fun match' -> match'.Groups |> Seq.length)

          Expect.equal
              result
              2
              $"'{input}' should match generated pattern {pattern}. Expected pattern - /match and (capture )+/g"
      }

      test "ignore group" {
          let pattern =
              regex {
                  group {
                      occurs 2 {
                          group {
                              oneOrMore { wordChar }
                              notWordChar
                          }
                      }
                  }

                  group {
                      capture { oneOrMore { wordChar } }
                      notWordChar
                  }
              }
              |> Regx.make

          let input = @"Call me Sally."

          let result = Regex.Match(input, pattern) |> (fun match' -> match'.Groups.[1].Value)

          Expect.equal
              result
              "Sally"
              $"'{input}' should match generated pattern {pattern}. Expected pattern - /(?:\w+\W){2}(\w+\W)/"
      }

      test "Named group" {
          let pattern =
              regex {
                  group {
                      occurs 2 {
                          group {
                              oneOrMore { wordChar }
                              notWordChar
                          }
                      }
                  }

                  group {
                      captureAs "FirstName" { oneOrMore { wordChar } }
                      notWordChar
                  }
              }
              |> Regx.make

          let input = @"Call me Sally."

          let result =
              Regex.Match(input, pattern)
              |> (fun match' -> match'.Groups |> Seq.find (fun t -> t.Name = "FirstName"))
              |> (fun k -> k.Value)

          Expect.equal
              result
              "Sally"
              $"'{input}' should match generated pattern {pattern}. Expected pattern - /(?:\w+\W){2}(\w+\W)/"
      }

      test "Balanced Named group" {
          let pattern =
              regex { between "<span>" "</span>" { captureAs "FirstName" { oneOrMore { wordChar } } } }
              |> Regx.make

          let input = @"<span>Sally</span>"

          let result =
              Regex.Match(input, pattern)
              |> (fun match' -> match'.Groups |> Seq.find (fun t -> t.Name = "FirstName"))
              |> (fun k -> k.Value)

          Expect.equal
              result
              "Sally"
              $"'{input}' should match generated pattern {pattern}. Expected pattern - /(?<span><span>)\w+(?<FirstName-span></span>)/"
      }

      test "Balanced UnNamed group" {
          let pattern =
              regex { between "<span>" "</span>" { capture { oneOrMore { wordChar } } } }
              |> Regx.make

          let input = @"<span>Sally</span>"

          let result =
              Regex.Match(input, pattern) |> (fun match' -> match') |> (fun k -> k.Value)

          Expect.equal
              result
              input
              $"'{input}' should match generated pattern {pattern}. Expected pattern - /(?<span><span>)\w+(?<-span></span>)/"
      }

      test "Ref group index" {
          let pattern =
              regex {
                  capture { oneOrMore { wordChar } }
                  whiteSpace
                  capture { oneOrMore { wordChar } }
                  whiteSpace
                  refGroupNo 1
                  whiteSpace
                  refGroupNo 2
              }
              |> Regx.make

          let input = @"Hello again Hello again."
          let result = Regex.Match(input, pattern)

          Expect.equal
              result.Success
              true
              $"'{input}' should match generated pattern {pattern}. Expected pattern - /(\w+)\s(\w+)\s\1\s\2./"
      }

      test "Ref group name" {
          let pattern =
              regex {
                  captureAs "Greeting" { oneOrMore { wordChar } }
                  whiteSpace
                  captureAs "Name" { oneOrMore { wordChar } }
                  whiteSpace
                  refGroupName "Greeting"
                  whiteSpace
                  refGroupName "Name"
              }
              |> Regx.make

          let input = @"Hello again Hello again."
          let result = Regex.Match(input, pattern)

          Expect.equal
              result.Success
              true
              $"'{input}' should match generated pattern {pattern}. Expected pattern - /(\w+)\s(\w+)\s\1\s\2./"
      }

      ]
    |> testList "Groups"
