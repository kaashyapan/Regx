namespace Regx

[<AutoOpen>]
module Categories =
    type Category =
        | LetterUppercase
        | LetterLowercase
        | LetterTitlecase
        | LetterModifier
        | LetterOther
        | Letter
        | MarkNonspacing
        | MarkSpacing
        | MarkEnclosing
        | Mark
        | NumberDecimal
        | NumberLetter
        | NumberOther
        | Number
        | PunctuationConnector
        | PunctuationDash
        | PunctuationOpen
        | PunctuationClose
        | PunctuationQuoteInitial
        | PunctuationQuoteFinal
        | PunctuationOther
        | Punctuation
        | SymbolMath
        | SymbolCurrency
        | SymbolModifier
        | SymbolOther
        | Symbol
        | SeparatorSpace
        | SeparatorLine
        | SeparatorParagraph
        | Separator
        | OtherControl
        | OtherFormat
        | OtherSurrogate
        | OtherPrivate
        | OtherNoncharacter
        | Other
        | IsBasicLatin
        | IsLatin_1Supplement
        | IsLatinExtended_A
        | IsLatinExtended_B
        | IsIPAExtensions
        | IsSpacingModifierLetters
        | IsCombiningDiacriticalMarks
        | IsGreek
        | IsCyrillic
        | IsCyrillicSupplement
        | IsArmenian
        | IsHebrew
        | IsArabic
        | IsSyriac
        | IsThaana
        | IsDevanagari
        | IsBengali
        | IsGurmukhi
        | IsGujarati
        | IsOriya
        | IsTamil
        | IsTelugu
        | IsKannada
        | IsMalayalam
        | IsSinhala
        | IsThai
        | IsLao
        | IsTibetan
        | IsMyanmar
        | IsGeorgian
        | IsHangulJamo
        | IsEthiopic
        | IsCherokee
        | IsUnifiedCanadianAboriginalSyllabics
        | IsOgham
        | IsRunic
        | IsTagalog
        | IsHanunoo
        | IsBuhid
        | IsTagbanwa
        | IsKhmer
        | IsMongolian
        | IsLimbu
        | IsTaiLe
        | IsKhmerSymbols
        | IsPhoneticExtensions
        | IsLatinExtendedAdditional
        | IsGreekExtended
        | IsGeneralPunctuation
        | IsSuperscriptsandSubscripts
        | IsCurrencySymbols
        | IsCombiningMarksforSymbols
        | IsLetterlikeSymbols
        | IsNumberForms
        | IsArrows
        | IsMathematicalOperators
        | IsMiscellaneousTechnical
        | IsControlPictures
        | IsOpticalCharacterRecognition
        | IsEnclosedAlphanumerics
        | IsBoxDrawing
        | IsBlockElements
        | IsGeometricShapes
        | IsMiscellaneousSymbols
        | IsDingbats
        | IsMiscellaneousMathematicalSymbols_A
        | IsSupplementalArrows_A
        | IsBraillePatterns
        | IsSupplementalArrows_B
        | IsMiscellaneousMathematicalSymbols_B
        | IsSupplementalMathematicalOperators
        | IsMiscellaneousSymbolsandArrows
        | IsCJKRadicalsSupplement
        | IsKangxiRadicals
        | IsIdeographicDescriptionCharacters
        | IsCJKSymbolsandPunctuation
        | IsHiragana
        | IsKatakana
        | IsBopomofo
        | IsHangulCompatibilityJamo
        | IsKanbun
        | IsBopomofoExtended
        | IsKatakanaPhoneticExtensions
        | IsEnclosedCJKLettersandMonths
        | IsCJKCompatibility
        | IsCJKUnifiedIdeographsExtensionA
        | IsYijingHexagramSymbols
        | IsCJKUnifiedIdeographs
        | IsYiSyllables
        | IsYiRadicals
        | IsHangulSyllables
        | IsHighSurrogates
        | IsHighPrivateUseSurrogates
        | IsLowSurrogates
        | IsPrivateUse
        | IsCJKCompatibilityIdeographs
        | IsAlphabeticPresentationForms
        | IsArabicPresentationForms_A
        | IsVariationSelectors
        | IsCombiningHalfMarks
        | IsCJKCompatibilityForms
        | IsSmallFormVariants
        | IsArabicPresentationForms_B
        | IsHalfwidthandFullwidthForms
        | IsSpecials

        member this.toString =
            match this with
            | LetterUppercase -> @"Lu"
            | LetterLowercase -> @"Ll"
            | LetterTitlecase -> @"Lt"
            | LetterModifier -> @"Lm"
            | LetterOther -> @"Lo"
            | Letter -> @"L"
            | MarkNonspacing -> @"Mn"
            | MarkSpacing -> @"Mc"
            | MarkEnclosing -> @"Me"
            | Mark -> @"M"
            | NumberDecimal -> @"Nd"
            | NumberLetter -> @"Nl"
            | NumberOther -> @"No"
            | Number -> @"N"
            | PunctuationConnector -> @"Pc"
            | PunctuationDash -> @"Pd"
            | PunctuationOpen -> @"Ps"
            | PunctuationClose -> @"Pe"
            | PunctuationQuoteInitial -> @"Pi"
            | PunctuationQuoteFinal -> @"Pf"
            | PunctuationOther -> @"Po"
            | Punctuation -> @"P"
            | SymbolMath -> @"Sm"
            | SymbolCurrency -> @"Sc"
            | SymbolModifier -> @"Sk"
            | SymbolOther -> @"So"
            | Symbol -> @"S"
            | SeparatorSpace -> @"Zs"
            | SeparatorLine -> @"Zl"
            | SeparatorParagraph -> @"Zp"
            | Separator -> @"Z"
            | OtherControl -> @"Cc"
            | OtherFormat -> @"Cf"
            | OtherSurrogate -> @"Cs"
            | OtherPrivate -> @"Co"
            | OtherNoncharacter -> @"Cn"
            | Other -> @"C"
            | IsBasicLatin -> "IsBasicLatin"
            | IsLatin_1Supplement -> "IsLatin-1Supplement"
            | IsLatinExtended_A -> "IsLatinExtended_A"
            | IsLatinExtended_B -> "IsLatinExtended_B"
            | IsIPAExtensions -> "IsIPAExtensions"
            | IsSpacingModifierLetters -> "IsSpacingModifierLetters"
            | IsCombiningDiacriticalMarks -> "IsCombiningDiacriticalMarks"
            | IsGreek -> "IsGreek"
            | IsCyrillic -> "IsCyrillic"
            | IsCyrillicSupplement -> "IsCyrillicSupplement"
            | IsArmenian -> "IsArmenian"
            | IsHebrew -> "IsHebrew"
            | IsArabic -> "IsArabic"
            | IsSyriac -> "IsSyriac"
            | IsThaana -> "IsThaana"
            | IsDevanagari -> "IsDevanagari"
            | IsBengali -> "IsBengali"
            | IsGurmukhi -> "IsGurmukhi"
            | IsGujarati -> "IsGujarati"
            | IsOriya -> "IsOriya"
            | IsTamil -> "IsTamil"
            | IsTelugu -> "IsTelugu"
            | IsKannada -> "IsKannada"
            | IsMalayalam -> "IsMalayalam"
            | IsSinhala -> "IsSinhala"
            | IsThai -> "IsThai"
            | IsLao -> "IsLao"
            | IsTibetan -> "IsTibetan"
            | IsMyanmar -> "IsMyanmar"
            | IsGeorgian -> "IsGeorgian"
            | IsHangulJamo -> "IsHangulJamo"
            | IsEthiopic -> "IsEthiopic"
            | IsCherokee -> "IsCherokee"
            | IsUnifiedCanadianAboriginalSyllabics -> "IsUnifiedCanadianAboriginalSyllabics"
            | IsOgham -> "IsOgham"
            | IsRunic -> "IsRunic"
            | IsTagalog -> "IsTagalog"
            | IsHanunoo -> "IsHanunoo"
            | IsBuhid -> "IsBuhid"
            | IsTagbanwa -> "IsTagbanwa"
            | IsKhmer -> "IsKhmer"
            | IsMongolian -> "IsMongolian"
            | IsLimbu -> "IsLimbu"
            | IsTaiLe -> "IsTaiLe"
            | IsKhmerSymbols -> "IsKhmerSymbols"
            | IsPhoneticExtensions -> "IsPhoneticExtensions"
            | IsLatinExtendedAdditional -> "IsLatinExtendedAdditional"
            | IsGreekExtended -> "IsGreekExtended"
            | IsGeneralPunctuation -> "IsGeneralPunctuation"
            | IsSuperscriptsandSubscripts -> "IsSuperscriptsandSubscripts"
            | IsCurrencySymbols -> "IsCurrencySymbols"
            | IsCombiningMarksforSymbols -> "IsCombiningMarksforSymbols"
            | IsLetterlikeSymbols -> "IsLetterlikeSymbols"
            | IsNumberForms -> "IsNumberForms"
            | IsArrows -> "IsArrows"
            | IsMathematicalOperators -> "IsMathematicalOperators"
            | IsMiscellaneousTechnical -> "IsMiscellaneousTechnical"
            | IsControlPictures -> "IsControlPictures"
            | IsOpticalCharacterRecognition -> "IsOpticalCharacterRecognition"
            | IsEnclosedAlphanumerics -> "IsEnclosedAlphanumerics"
            | IsBoxDrawing -> "IsBoxDrawing"
            | IsBlockElements -> "IsBlockElements"
            | IsGeometricShapes -> "IsGeometricShapes"
            | IsMiscellaneousSymbols -> "IsMiscellaneousSymbols"
            | IsDingbats -> "IsDingbats"
            | IsMiscellaneousMathematicalSymbols_A -> "IsMiscellaneousMathematicalSymbols_A"
            | IsSupplementalArrows_A -> "IsSupplementalArrows_A"
            | IsBraillePatterns -> "IsBraillePatterns"
            | IsSupplementalArrows_B -> "IsSupplementalArrows_B"
            | IsMiscellaneousMathematicalSymbols_B -> "IsMiscellaneousMathematicalSymbols_B"
            | IsSupplementalMathematicalOperators -> "IsSupplementalMathematicalOperators"
            | IsMiscellaneousSymbolsandArrows -> "IsMiscellaneousSymbolsandArrows"
            | IsCJKRadicalsSupplement -> "IsCJKRadicalsSupplement"
            | IsKangxiRadicals -> "IsKangxiRadicals"
            | IsIdeographicDescriptionCharacters -> "IsIdeographicDescriptionCharacters"
            | IsCJKSymbolsandPunctuation -> "IsCJKSymbolsandPunctuation"
            | IsHiragana -> "IsHiragana"
            | IsKatakana -> "IsKatakana"
            | IsBopomofo -> "IsBopomofo"
            | IsHangulCompatibilityJamo -> "IsHangulCompatibilityJamo"
            | IsKanbun -> "IsKanbun"
            | IsBopomofoExtended -> "IsBopomofoExtended"
            | IsKatakanaPhoneticExtensions -> "IsKatakanaPhoneticExtensions"
            | IsEnclosedCJKLettersandMonths -> "IsEnclosedCJKLettersandMonths"
            | IsCJKCompatibility -> "IsCJKCompatibility"
            | IsCJKUnifiedIdeographsExtensionA -> "IsCJKUnifiedIdeographsExtensionA"
            | IsYijingHexagramSymbols -> "IsYijingHexagramSymbols"
            | IsCJKUnifiedIdeographs -> "IsCJKUnifiedIdeographs"
            | IsYiSyllables -> "IsYiSyllables"
            | IsYiRadicals -> "IsYiRadicals"
            | IsHangulSyllables -> "IsHangulSyllables"
            | IsHighSurrogates -> "IsHighSurrogates"
            | IsHighPrivateUseSurrogates -> "IsHighPrivateUseSurrogates"
            | IsLowSurrogates -> "IsLowSurrogates"
            | IsPrivateUse -> "IsPrivateUse"
            | IsCJKCompatibilityIdeographs -> "IsCJKCompatibilityIdeographs"
            | IsAlphabeticPresentationForms -> "IsAlphabeticPresentationForms"
            | IsArabicPresentationForms_A -> "IsArabicPresentationForms_A"
            | IsVariationSelectors -> "IsVariationSelectors"
            | IsCombiningHalfMarks -> "IsCombiningHalfMarks"
            | IsCJKCompatibilityForms -> "IsCJKCompatibilityForms"
            | IsSmallFormVariants -> "IsSmallFormVariants"
            | IsArabicPresentationForms_B -> "IsArabicPresentationForms_B"
            | IsHalfwidthandFullwidthForms -> "IsHalfwidthandFullwidthForms"
            | IsSpecials -> "IsSpecials"

    type GroupIdentifier =
        | GroupId of int
        | GroupName of string
        | Group of string
