namespace Regx

[<AutoOpen>]
module Builder =
    type RegxElement =
        //Character classes
        | WordChar
        | NotWordChar
        | In of char seq
        | NotIn of char seq
        | InRange of char * char
        | InList of RegexBuilder list
        | Any
        | AnyChar
        | Digit
        | NotDigit
        | WhiteSpace
        | NotWhiteSpace
        | Unicode of Category
        | NonUnicode of Category
        //Anchors
        | BeginsWith of RegxElement
        | EndsWith of RegxElement
        | BeginsWithString of RegxElement
        | EndsWithString of RegxElement
        | WordBoundary
        | NotWordBoundary
        //Quantifiers
        | Occurs of int * RegxElement
        | OccursMoreThan of int * RegxElement
        | OccursBetween of int * int * RegxElement
        | OneOrMore of RegxElement
        | ZeroOrMore of RegxElement
        | Optional of RegxElement
        | Lazy of RegxElement
        | Greedy of RegxElement
        | OneOf of RegxElement
        //Capture group
        | Capture of RegxElement
        | CaptureAs of string * RegxElement
        | NoCapture of RegxElement
        | Between of string * string * RegxElement
        | NumRef of int
        | NameRef of string
        //Look around
        | PositiveLookAhead of RegxElement
        | NegativeLookAhead of RegxElement
        | PositiveLookBehind of RegxElement
        | NegativeLookBehind of RegxElement
        //String literals
        | VerbatimString of string
        | EscapedString of string
        //Conditionals
        | If of GroupIdentifier * RegxElement
        | Else of RegxElement
        //Convenience helpers
        | Tab
        | LineFeed
        | VerticalTab
        | FormFeed
        | CarriageReturn
        | NullCharacter
        //Internal
        | Empty
        | ElementList of seq<RegxElement>
        | RegEx of RegxElement

    and RegexBuilder(el: RegxElement) =
        member _.Yield(n: RegexBuilder) = n

        member _.Combine(x1: RegexBuilder, x2: RegexBuilder) =
            match x1.get, x2.get with
            | ElementList ls1, ElementList ls2 -> ElementList(Seq.append ls1 ls2)
            | el1, ElementList ls2 -> ElementList(Seq.append (Seq.singleton el1) ls2)
            | ElementList ls1, el2 -> ElementList(Seq.append ls1 (Seq.singleton el2))
            | el1, el2 ->
                ElementList(
                    seq {
                        yield el1
                        yield el2
                    }
                )
            |> RegexBuilder

        member internal this.get = el

        member this.Run(n: RegexBuilder) =
            match this.get with
            | RegEx _ -> RegEx n.get
            | BeginsWith _ -> BeginsWith n.get
            | EndsWith _ -> EndsWith n.get
            | BeginsWithString _ -> BeginsWithString n.get
            | EndsWithString _ -> EndsWithString n.get
            | Occurs(times, _) -> Occurs(times, n.get)
            | OccursMoreThan(times, _) -> OccursMoreThan(times, n.get)
            | OccursBetween(minTimes, maxTimes, _) -> OccursBetween(minTimes, maxTimes, n.get)
            | OneOrMore _ -> OneOrMore n.get
            | ZeroOrMore _ -> ZeroOrMore n.get
            | Optional _ -> Optional n.get
            | Lazy _ -> Lazy n.get
            | Greedy _ -> Greedy n.get
            | OneOf _ -> OneOf n.get
            | Capture _ -> Capture n.get
            | CaptureAs(name, _) -> CaptureAs(name, n.get)
            | NoCapture _ -> NoCapture n.get
            | Between(open_, close, _) -> Between(open_, close, n.get)
            | NumRef index -> NumRef index
            | NameRef name -> NameRef name
            | PositiveLookAhead _ -> PositiveLookAhead n.get
            | NegativeLookAhead _ -> NegativeLookAhead n.get
            | PositiveLookBehind _ -> PositiveLookBehind n.get
            | NegativeLookBehind _ -> NegativeLookBehind n.get
            | If(group, _) -> If(group, n.get)
            | Else _ -> Else n.get
            | _ -> this.get

            |> RegexBuilder

        member this.Delay(f) = f ()

    ///Dot - Matches any character except linebreaks. Equivalent to [^\n\r].
    let any = RegexBuilder(Any)
    ///Match any - A character set that can be used to match any character, including line breaks, without the dotall flag. Represents [\s\S]
    let anyChar = RegexBuilder(AnyChar)
    ///Matches any word character (alphanumeric & underscore). Only matches low-ascii characters (no accented or non-roman characters). Equivalent to [A-Za-z0-9_]
    let wordChar = RegexBuilder(WordChar)
    ///Matches any character that is not a word character (alphanumeric & underscore). Equivalent to [^A-Za-z0-9_]
    let notWordChar = RegexBuilder(NotWordChar)
    ///Digit - Matches any digit character (0-9). Equivalent to [0-9].
    let digit = RegexBuilder(Digit)
    ///Non-Digit - Matches any character that is not a digit character (0-9). Equivalent to [^0-9].
    let notDigit = RegexBuilder(NotDigit)
    ///Whitespace - Matches any whitespace character (spaces, tabs, line breaks).
    let whiteSpace = RegexBuilder(WhiteSpace)
    ///Non-whitespace - Matches any character that is not a whitespace character (spaces, tabs, line breaks).
    let notWhiteSpace = RegexBuilder(NotWhiteSpace)
    ///Character set - Match any character in the set
    ///e.g charIn (seq{'a'.. 'z'})
    let charIn charSet = RegexBuilder(In charSet)
    ///Negate character set - Match any character that is not in the set.
    ///e.g charNotIn (seq{'a'.. 'z'})
    let charNotIn charSet = RegexBuilder(NotIn charSet)

    ///Matches characters in list.
    let inList (set: RegexBuilder list) = RegexBuilder(InList set)

    ///Range - Matches a character having a character code between the two specified characters inclusive.
    ///e.g inRange 'a' 'z'
    let inRange startChar endChar = RegexBuilder(InRange(startChar, endChar))

    ///^ - Enclosed expression occurs in the beginning of the fragment
    let beginsWith = RegexBuilder(BeginsWith Empty)
    ///$ - Enclosed expression occurs in the end of the fragment
    let endsWith = RegexBuilder(EndsWith Empty)
    ///\A - Enclosed expression occurs in the beginning of the fragment
    let beginsWithString = RegexBuilder(BeginsWith Empty)
    ///\z - Enclosed expression occurs in the end of the fragment
    let endsWithString = RegexBuilder(EndsWith Empty)
    ///Matches a word boundary position between a word character and non-word character or position (start / end of string).
    let wordBoundary = RegexBuilder(WordBoundary)
    ///Matches any position that is not a word boundary. This matches a position, not a character.
    let notWordBoundary = RegexBuilder(NotWordBoundary)

    ///Optional - Matches 0 or 1 of the enclosed fragment, effectively making it optional.
    let mayHave = RegexBuilder(Optional Empty)
    ///Lazy - Makes the enclosed fragment lazy, causing it to match as few characters as possible. By default, quantifiers are greedy, and will match as many characters as possible.
    let fewest = RegexBuilder(Lazy Empty)
    ///Greedy - Makes the enclosed fragment Greedily, causing it to match as many characters as possible. By default, quantifiers are greedy, and will match as many characters as possible.
    let longest = RegexBuilder(Greedy Empty)

    ///Match unicode block/category -/ \p{IsGreek}+/ or \p{Pd}
    let unicode category = RegexBuilder(Unicode category)
    ///Negative unicode block/category -/ \P{IsGreek}+/ or \P{Pd}
    let nonUnicode category = RegexBuilder(NonUnicode category)

    ///Acts like a boolean OR. Matches the expression before or after the |.
    let oneOf = RegexBuilder(OneOf Empty)
    ///Plus - Matches 1 or more of the enclosed fragment.
    let oneOrMore = RegexBuilder(OneOrMore Empty)
    ///Star - Matches 0 or more of the enclosed fragment.
    let zeroOrMore = RegexBuilder(ZeroOrMore Empty)

    ///Matches the exact quantity of the enclosed token.
    /// e.g occurs 3 { word }
    let occurs (times: int) = RegexBuilder(Occurs(times, Empty))

    ///Matches the specified quantity or more of the enclosed token.
    /// e.g occursMoreThan 3 { word }
    let occursMoreThan (times: int) = RegexBuilder(OccursMoreThan(times, Empty))

    ///Matches a quantity of the enclosed toke in the specified range.
    /// e.g occursBetween 3 { word }
    let occursBetween (minTimes: int) (maxTimes: int) = RegexBuilder(OccursBetween(minTimes, maxTimes, Empty))

    ///(ABC) - Capturing group - Groups multiple tokens together and creates a capture group for extracting a substring or using a backreference.
    let between open_ close = RegexBuilder(Between(open_, close, Empty))

    ///(ABC) - Capturing group - Groups multiple tokens together and creates a capture group for extracting a substring or using a backreference.
    let capture = RegexBuilder(Capture Empty)
    ///(?<name>ABC) - Named capturing group - Creates a capturing group that can be referenced via the specified name.
    let captureAs name = RegexBuilder(CaptureAs(name, Empty))
    ///(?:ha) - Non-capturing group - Groups multiple tokens together without creating a capture group.
    let group = RegexBuilder(NoCapture Empty)
    ///\1 - Numeric reference - Matches the results of a capture group.
    let refGroupNo index = RegexBuilder(NumRef index)
    ///\k<name> - Group name reference - Matches the results of a named capture group.
    let refGroupName name = RegexBuilder(NameRef name)

    let if' (group: GroupIdentifier) = RegexBuilder(If(group, Empty))
    let else' = RegexBuilder(Else Empty)
    ///Positive lookahead - \d(?=px) - Matches a group after the main expression without including it in the result.
    let positiveLookAhead = RegexBuilder(PositiveLookAhead Empty)
    ///Negative lookahead - \d(?!px) - Specifies a group that can not match after the main expression (if it matches, the result is discarded).
    let negativeLookAhead = RegexBuilder(NegativeLookAhead Empty)
    ///Positive lookbehind - (?<=ABC) - Matches a group before the main expression without including it in the result.
    let positiveLookBehind = RegexBuilder(PositiveLookBehind Empty)
    ///Negative lookbehind - (?<!ABC) - Specifies a group that can not match before the main expression (if it matches, the result is discarded).
    let negativeLookBehind = RegexBuilder(NegativeLookBehind Empty)

    ///Unescaped string literal - Reserve characters will be automatically escaped +*?^$\.[]{}()|/
    let verbatimString str = RegexBuilder(VerbatimString str)
    ///Escaped string literal
    let escapedString str = RegexBuilder(EscapedString str)
    ///Matches a TAB character (char code 9). \t
    let tab = RegexBuilder(Tab)
    ///Matches a LINE FEED character (char code 10). \n
    let lineFeed = RegexBuilder(LineFeed)
    ///Matches a VERTICAL TAB character (char code 11). \v
    let verticalTab = RegexBuilder(VerticalTab)
    ///Matches a FORM FEED character (char code 12). \f
    let formFeed = RegexBuilder(FormFeed)
    ///Matches a CARRIAGE RETURN character (char code 13). \r
    let carriageReturn = RegexBuilder(CarriageReturn)
    ///Matches a NULL character (char code 0). \0
    let nullCharacter = RegexBuilder(NullCharacter)

    let regex = RegexBuilder(RegEx Empty)
