namespace Regx

module Helpers =
    ///Matches an actual word - Not a regex word
    let word = group { oneOrMore { wordChar } }

    ///Matches an English domain
    let domain =
        oneOrMore { inList [ inRange 'a' 'z'; inRange 'A' 'Z'; inRange '0' '9'; verbatimString @"-" ] }

    ///Matches an English email
    //(?:^(?<emailId>[a-zA-Z0-9\.!#\$%&’\*\+\/=\?\^_`\{\|\}~-]+)@(?<domain>[a-zA-Z0-9-]+)(?:\.(?<ending>[a-zA-Z0-9-]+))*$)
    let email =
        let emailId =
            oneOrMore {
                inList
                    [ inRange 'a' 'z'
                      inRange 'A' 'Z'
                      inRange '0' '9'
                      verbatimString """.!#$%&’*+/=?^_`{|}~-""" ]
            }

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

    let ip6Address =
        let quad =
            group {
                wordBoundary
                occursBetween 1 4 { group { inList [ inRange '0' '9'; inRange 'a' 'f'; inRange 'A' 'F' ] } }
                wordBoundary
            }

        group {
            beginsWith {
                endsWith {
                    mayHave { quad }
                    verbatimString ":"
                    mayHave { quad }
                    mayHave { verbatimString ":" }
                    mayHave { quad }
                    mayHave { verbatimString ":" }
                    mayHave { quad }
                    mayHave { verbatimString ":" }
                    mayHave { quad }
                    mayHave { verbatimString ":" }
                    mayHave { quad }
                    mayHave { verbatimString ":" }
                    mayHave { quad }
                    mayHave { verbatimString ":" }
                    verbatimString ":"
                    mayHave { quad }
                }
            }
        }

    let ip4Address =
        let octet =
            group {
                wordBoundary

                group {
                    oneOf {
                        group {
                            verbatimString "25"
                            inRange '0' '5'
                        }

                        group {
                            verbatimString "2"
                            inRange '0' '4'
                            inRange '0' '9'
                        }

                        group {
                            mayHave { inRange '0' '1' }
                            inRange '0' '9'
                            mayHave { inRange '0' '9' }
                        }
                    }
                }

                wordBoundary
            }

        beginsWith {
            endsWith {
                group {
                    octet
                    verbatimString "."
                    octet
                    verbatimString "."
                    octet
                    verbatimString "."
                    octet
                }
            }
        }

    let ipAddress =
        group {
            oneOf {
                ip4Address
                ip6Address
            }
        }

    let url =
        let protocol =
            group {
                captureAs "protocol" {
                    oneOf {
                        verbatimString "http"
                        verbatimString "https"
                    }
                }

                verbatimString "://"
            }

        let subDomain =
            zeroOrMore {
                group {
                    captureAs "subdomain" {
                        occursBetween 1 256 {
                            inList
                                [ inRange 'a' 'z'
                                  inRange 'A' 'Z'
                                  inRange '0' '9'
                                  verbatimString """@:%._\+~#=""" ]
                        }
                    }

                    verbatimString "."
                }
            }

        let domain =
            group {
                captureAs "domain" {
                    occursBetween 1 256 {
                        inList
                            [ inRange 'a' 'z'
                              inRange 'A' 'Z'
                              inRange '0' '9'
                              verbatimString """@:%._\+~#=""" ]
                    }
                }

                verbatimString "."
            }

        let ending =
            captureAs "ending" {
                oneOrMore {
                    group { occursBetween 1 6 { inList [ inRange 'a' 'z'; inRange 'A' 'Z'; inRange '0' '9' ] } }
                }
            }

        let path =
            zeroOrMore {
                group {
                    oneOf {
                        group {
                            verbatimString "/"
                            captureAs "path" { word }
                        }

                        verbatimString "/"
                        verbatimString "#"
                    }
                }
            }

        let query =
            zeroOrMore {
                group {
                    oneOf {
                        verbatimString "&"
                        verbatimString "?"
                        notWhiteSpace
                    }

                    captureAs "key" { word }
                    verbatimString "="
                    captureAs "value" { word }
                }
            }

        group {
            mayHave { protocol }
            mayHave { subDomain }
            domain
            ending
            path
            query
        }

    let guid =
        let hex = inList [ inRange '0' '9'; inRange 'a' 'f'; inRange 'A' 'F' ]

        group {
            beginsWith {
                mayHave { verbatimString "{" }
                occurs 8 { hex }
            }

            verbatimString "-"
            occurs 4 { hex }
            verbatimString "-"
            occurs 4 { hex }
            verbatimString "-"
            occurs 4 { hex }
            verbatimString "-"

            endsWith {
                occurs 12 { hex }
                mayHave { verbatimString "}" }
            }
        }

    let uuid =
        let hex = inList [ inRange '0' '9'; inRange 'a' 'f' ]

        group {
            beginsWith { occurs 8 { hex } }
            escapedString "-"
            occurs 4 { hex }
            escapedString "-"
            occurs 4 { hex }
            escapedString "-"
            occurs 4 { hex }
            escapedString "-"
            endsWith { occurs 12 { hex } }
        }
