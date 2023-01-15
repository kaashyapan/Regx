module Program

open Expecto

[<EntryPoint>]
let main _ =
    let tests =
        [ Basic.tests
          Anchor.tests
          Quantifier.tests
          Groups.tests
          Lookaround.tests
          Conditionals.tests
          Unicodes.tests
          Escape.tests ]
        |> testList "Tests"

    (runTestsWithCLIArgs [] [||] tests) |> ignore

    0
