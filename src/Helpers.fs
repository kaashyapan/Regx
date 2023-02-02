namespace Regx

module Helpers =
    ///Matches an actual word - Not a regex word
    let word = group { oneOrMore { wordChar } }

    ///Matches an English domain
    let domain =
        oneOrMore { inList [ inRange 'a' 'z'; inRange 'A' 'Z'; inRange '0' '9'; verbatimString @"-" ] }

    let emailId =
        oneOrMore {
            inList
                [ inRange 'a' 'z'
                  inRange 'A' 'Z'
                  inRange '0' '9'
                  verbatimString """.!#$%&’*+/=?^_`{|}~-""" ]
        }

    ///Matches an English email
    //(?:^(?<emailId>[a-zA-Z0-9\.!#\$%&’\*\+\/=\?\^_`\{\|\}~-]+)@(?<domain>[a-zA-Z0-9-]+)(?:\.(?<ending>[a-zA-Z0-9-]+))*$)
    let email =
        group {
            beginsWith { captureAs "emailId" { emailId } }
            verbatimString @"@"
            captureAs "domain" { domain }

            endsWith {
                zeroOrMore {
                    group {
                        verbatimString "."
                        captureAs "ending" { domain }
                    }
                }
            }
        }
