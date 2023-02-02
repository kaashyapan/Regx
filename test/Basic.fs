module Basic

open Expecto

open System.Text.RegularExpressions
open Regx

//Tests are from https://regexr.com/
//Character classes
let tests =
    [ test "Character set" {
          let pattern =
              regex {
                  charIn (
                      seq {
                          'a'
                          'e'
                          'i'
                          'o'
                          'u'
                      }
                  )
              }
              |> Regx.make

          let input = "glib jocks vex dwarves!"
          let result = (Regex.Matches(input, pattern)) |> Seq.length
          Expect.equal result 5 $"{input} should match generated pattern {pattern}. Expected pattern [aeiou]"
      }
      test "Negated set" {
          let pattern =
              regex {
                  charNotIn (
                      seq {
                          'a'
                          'e'
                          'i'
                          'o'
                          'u'
                      }
                  )
              }
              |> Regx.make

          let input = "glib jocks vex dwarves!"
          let result = (Regex.Matches(input, pattern)) |> Seq.length
          Expect.equal result 18 $"{input} should match generated pattern {pattern}. Expected pattern [^aeiou]"
      }
      test "Range" {
          let pattern = regex { inRange 'g' 's' } |> Regx.make

          let input = "abcdefghijklmnopqrstuvwxyz"
          let result = (Regex.Matches(input, pattern)) |> Seq.length
          Expect.equal result 13 $"{input} should match generated pattern {pattern}. Expected pattern [g-s]"
      }
      test "Dot" {
          let pattern = regex { any } |> Regx.make

          let input = "glib jocks vex dwarves!"
          let result = (Regex.Matches(input, pattern)) |> Seq.length
          Expect.equal result 23 $"{input} should match generated pattern {pattern}. Expected pattern ."
      }
      test "Any Char" {
          let pattern = regex { anyChar } |> Regx.make

          let input = "glib jocks vex dwarves!"
          let result = (Regex.Matches(input, pattern)) |> Seq.length
          Expect.equal result 23 $"{input} should match generated pattern {pattern}. Expected pattern [\s\S]"
      }
      test "Word character" {
          let pattern = regex { wordChar } |> Regx.make

          let input = "bonjour, mon frère"
          let result = (Regex.Matches(input, pattern)) |> Seq.length
          Expect.equal result 15 $"{input} should match generated pattern {pattern}. Expected pattern \w"
      }
      test "Not word character" {
          let pattern = regex { notWordChar } |> Regx.make

          let input = "bonjour, mon frère"
          let result = (Regex.Matches(input, pattern)) |> Seq.length
          Expect.equal result 3 $"{input} should match generated pattern {pattern}. Expected pattern \W"
      }
      test "Digit" {
          let pattern = regex { digit } |> Regx.make

          let input = "+1-(444)-555-1234"
          let result = (Regex.Matches(input, pattern)) |> Seq.length
          Expect.equal result 11 $"{input} should match generated pattern {pattern}. Expected pattern \d"
      }
      test "Non Digit" {
          let pattern = regex { notDigit } |> Regx.make

          let input = "+1-(444)-555-1234"
          let result = (Regex.Matches(input, pattern)) |> Seq.length
          Expect.equal result 6 $"{input} should match generated pattern {pattern}. Expected pattern \D"
      }
      test "WhiteSpace" {
          let pattern = regex { whiteSpace } |> Regx.make

          let input = "glib jocks vex dwarves!"
          let result = (Regex.Matches(input, pattern)) |> Seq.length
          Expect.equal result 3 $"{input} should match generated pattern {pattern}. Expected pattern \s"
      }
      test "Non WhiteSpace" {
          let pattern = regex { notWhiteSpace } |> Regx.make

          let input = "glib jocks vex dwarves!"
          let result = (Regex.Matches(input, pattern)) |> Seq.length
          Expect.equal result 20 $"{input} should match generated pattern {pattern}. Expected pattern \S"
      }

      ]
    |> testList "Basic"
