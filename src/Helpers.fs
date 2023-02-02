namespace Regx

module Helpers =
    ///Matches an actual word - Not a regex word
    let word = group { oneOrMore { wordChar } }
