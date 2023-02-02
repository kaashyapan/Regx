namespace Regx

type Regx =
    static member translate(el: RegexElement) =
        let translate el = Regx.translate el

        let intersperse sep ls =
            Seq.foldBack
                (fun x ->
                    function
                    | [] -> [ x ]
                    | xs -> x :: sep :: xs)
                ls
                []

        let join ls = Seq.fold (fun acc x -> acc + x) "" ls

        match el with
        | WordChar -> """\w"""
        | NotWordChar -> """\W"""
        | In set -> set |> Seq.fold (fun acc x -> acc + x.ToString()) "" |> sprintf """[%s]"""
        | NotIn set -> set |> Seq.fold (fun acc x -> acc + x.ToString()) "" |> sprintf """[^%s]"""
        | InRange(start, end') -> sprintf """[%c-%c]""" start end'
        | Any -> """."""
        | AnyChar -> """[\s\S]"""
        | Digit -> """\d"""
        | NotDigit -> """\D"""
        | WhiteSpace -> """\s"""
        | NotWhiteSpace -> """\S"""
        | WordBoundary _ -> sprintf """\b"""
        | NotWordBoundary _ -> sprintf """\B"""
        | Unicode category -> sprintf """\p{%s}""" (category.toString)
        | NonUnicode category -> sprintf """\P{%s}""" (category.toString)
        | BeginsWith el -> el |> translate |> sprintf """^%s"""
        | EndsWith el -> el |> translate |> sprintf """%s$"""
        | BeginsWithString el -> el |> translate |> sprintf """\A%s"""
        | EndsWithString el -> el |> translate |> sprintf """%s\z"""
        | Occurs(times, el) -> sprintf """%s{%d}""" (translate el) times
        | OccursMoreThan(times, el) -> sprintf """%s{%d,}""" (translate el) times
        | OccursBetween(minTimes, maxTimes, el) -> sprintf """%s{%d,%d}""" (translate el) minTimes maxTimes
        | OneOrMore el -> sprintf """%s+""" (translate el)
        | ZeroOrMore el -> sprintf """%s*""" (translate el)
        | Optional el -> el |> translate |> sprintf """%s?"""
        | Lazy el -> el |> translate |> sprintf """%s*?"""
        | Greedy el -> el |> translate |> sprintf """%s+?"""
        | OneOf(ElementList elements) -> elements |> Seq.map translate |> intersperse "|" |> join
        | OneOf invalidExpression ->
            failwith (sprintf "OneOf regex expression needs multiple options. %A" invalidExpression)
        | Capture el -> el |> translate |> sprintf """(%s)"""
        | CaptureAs(name, el) -> el |> translate |> sprintf """(?<%s>%s)""" name
        | NoCapture el -> el |> translate |> sprintf """(?:%s)"""
        | Between(open_, close, el_) ->
            let tag =
                open_.ToLowerInvariant().ToCharArray()
                |> Seq.filter (fun c -> seq { 'a' .. 'z' } |> Seq.contains c)
                |> Seq.toArray
                |> function
                    | [||] -> "tag"
                    | arr -> arr |> System.String

            match el_ with
            | Capture el ->
                let child = el |> translate
                sprintf """(?<%s>%s)%s(?<-%s>%s)""" tag open_ child tag close
            | CaptureAs(name, el) ->
                let child = el |> translate
                sprintf """(?<%s>%s)%s(?<%s-%s>%s)""" tag open_ child name tag close
            | _ -> failwith "Between should enclose a captured or non-captured group to form a balanced group"

        | NumRef index -> sprintf """\%d""" index
        | NameRef name -> sprintf """\k<%s>""" name
        | If(groupIdentifier, el) ->
            let child = el |> translate

            match groupIdentifier with
            | GroupId index -> sprintf """?(?(%d)%s""" index child
            | GroupName name -> sprintf """?(?(%s)%s""" name child
            | Group el -> sprintf """(?%s%s""" el child

        | Else el -> sprintf """|%s)""" (translate el)
        | PositiveLookAhead el -> el |> translate |> sprintf """(?=%s)"""
        | NegativeLookAhead el -> el |> translate |> sprintf """(?!%s)"""
        | PositiveLookBehind el -> el |> translate |> sprintf """(?<=%s)"""
        | NegativeLookBehind el -> el |> translate |> sprintf """(?<!%s)"""
        | VerbatimString str ->
            let pattern = """([\+\*\?\^\$\.\[\]\{\}\(\)\|\/\\])"""
            let replace = """\$1"""
            System.Text.RegularExpressions.Regex.Replace(str, pattern, replace)
        | EscapedString str -> str
        | Tab -> """\t"""
        | LineFeed -> """\n"""
        | VerticalTab -> """\v"""
        | FormFeed -> """\f"""
        | CarriageReturn -> """\r"""
        | NullCharacter -> """\0"""
        | ElementList elements -> elements |> Seq.map translate |> join
        | invalidExpression -> failwith (sprintf "Unexpected regex expression. %A" invalidExpression)

    static member make(builder: RegexBuilder) =
        match builder.get with
        | RegEx tree -> tree |> Regx.translate
        | el -> failwith (sprintf "Expected a regex expression tree. Received a fragment of %A" el)
