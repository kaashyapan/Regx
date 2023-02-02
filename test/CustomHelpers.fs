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
      }
      test "Url" {
          let pattern = regex { url } |> Regx.make

          let input =
              [ @"https://google.com"
                @"http://www.google.com"
                @"google.com/path1/path2"
                @"www.google.com/path1" ]

          let result = input |> List.map (fun i -> (Regex.Match(i, pattern)).Value)

          Expect.equal result input $"'{input}' should match generated pattern {pattern}"
      }
      test "ip4" {
          let pattern = regex { ip4Address } |> Regx.make
          let input = [ "1.2.3.4"; "01.102.103.104"; "255.255.255.255"; "256.2.3.4" ]
          let result = input |> List.map (fun i -> (Regex.Match(i, pattern)).Success)

          let expected = [ true; true; true; false ]
          Expect.equal result expected $"'{input}' should match generated pattern {pattern}"
      }
      test "ip6" {
          let pattern = regex { ip6Address } |> Regx.make

          let input =
              [ "2001:db8:3333:4444:5555:6666:7777:8888"
                "2001:db8::"
                "::1234:5678"
                "2001:db8::1234:5678"
                "2001:0db8:0001:0000:0000:0ab9:C0A8:0102"
                "2001:db8:1::ab9:C0A8:102"
                "2001:0tb8:0001:0000:0000:0ab9:C0A8:0102" ]

          let result = input |> List.map (fun i -> (Regex.Match(i, pattern)).Success)

          let expected = [ true; true; true; true; true; true; false ]
          Expect.equal result expected $"'{input}' should match generated pattern {pattern}"
      }
      test "Guid" {
          let pattern = regex { guid } |> Regx.make

          let input =
              [ @"8DEC0FE3-2C80-4CB5-8D8C-F7F9A0904757"
                @"{43C7CF7D-6E08-4485-B4E0-F733CE173763}"
                @"18b87272-0141-4136-8ede-b543891f3cfb"
                @"{d8f40e6b-9cb8-42d4-a81d-848900c77da9}" ]

          let result = input |> List.map (fun i -> Regex.Match(i, pattern).Value)

          Expect.equal result input $"'{input}' should match generated pattern {pattern}"

      }
      test "Uuid" {
          let pattern = regex { uuid } |> Regx.make
          let input = [ @"123e4567-e89b-12d3-a456-426655440000" ]

          let result = input |> List.map (fun i -> Regex.Match(i, pattern).Value)

          Expect.equal result input $"'{input}' should match generated pattern {pattern}"
      } ]
    |> testList "Custom word helper"
