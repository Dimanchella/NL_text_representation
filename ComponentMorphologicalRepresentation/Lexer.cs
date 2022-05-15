using NL_text_representation.ComponentMorphologicalRepresentation.Entities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NL_text_representation.ComponentMorphologicalRepresentation
{
    public class Lexer
    {
        private const short DATE_GROUP = 2;
        private const short TIME_GROUP = 16;
        private const short NUMBER_GROUP = 21;

        private const string DATE_PATTERN = @"^(((3[01]|[1-2][0-9]|0?[1-9])\.(01|03|05|07|08|10|12)|(30|[1-2][0-9]|0?[1-9])\.(04|06|09|11)|([1-2][0-9]|0?[1-9])\.02)\.|((3[01]|[0-2][1-9])\/(01|03|05|07|08|10|12)|(30|[0-2][1-9])\/(04|06|09|11)|([0-2][1-9])\/02)\/)[0-9]+$";
        private const string TIME_PATTERN = @"^([0-9]+)\:([0-5][0-9])\:([0-5][0-9]([.,][0-9]+)?)$";
        private const string NUMBER_PATTERN = @"^\-?[0-9]+([.,][0-9]+)?$";
        private static readonly Regex regex = new(@$"(({DATE_PATTERN})|({TIME_PATTERN})|({NUMBER_PATTERN}))");

        private static readonly Dictionary<int, string> states = CreateStates();
        private string researchedString = "";
        private int lastPos = 0;
        private List<Token> allTokens = null;
        

        private static Dictionary<int, string> CreateStates()
        {
            Dictionary<int, string> states = new();
            states.Add(0, null);
            states.Add(1, "word");
            states.Add(2, null);
            states.Add(3, null);
            states.Add(4, null);
            states.Add(5, "alias");
            states.Add(6, "punctuation");
            states.Add(7, null);
            states.Add(8, "regex");
            states.Add(9, null);
            states.Add(10, "regex");
            return states;
        }


        public string ResearchedString
        {
            set
            {
                researchedString = value;
                lastPos = 0;
            }
        }
        public IEnumerable<Token> AllTokens { get => allTokens; }


        public Token GetNextToken()
        {
            int state = 0;
            int lastAccepting = -1;
            int nextLastPos = lastPos;

            for (int pos = lastPos; state >= 0; pos++)
            {
                if (IsAcceptingState(state))
                {
                    lastAccepting = state;
                    nextLastPos = pos;
                }

                if (pos < researchedString.Length)
                {
                    state = DFATransition(researchedString[pos], state);
                }
                else
                {
                    state = -1;
                }
            }

            if (lastAccepting >= 0)
            {
                string name = TokenName(lastAccepting);
                string lexeme = TokenLexeme(nextLastPos);
                if (name.Equals("regex"))
                {
                    var match = regex.Match(lexeme);
                    if (match.Groups[DATE_GROUP].Success)
                        return new Token("date", lexeme);
                    else if (match.Groups[TIME_GROUP].Success)
                        return new Token("time", lexeme);
                    else if (match.Groups[NUMBER_GROUP].Success)
                        return new Token("number", lexeme);
                    else
                        throw new FormatException($"Uncorrect token [{lexeme}]");
                }
                else
                {
                    return new Token(name, lexeme);
                }
            }
            else if (lastPos >= researchedString.Length)
                return new Token("eos", "#nil#");
            else if (IsSpaceSeparator(researchedString[lastPos]) && SkipSpaceSeparator())
                return GetNextToken();
            else
                throw new FormatException($"Uncorrect token [{TokenLexeme(nextLastPos)}]");
        }
        public void FindAllTokens()
        {
            allTokens = new();
            lastPos = 0;
            Token token;
            do
            {
                token = GetNextToken();
                allTokens.Add(token);
            } while (!token.Name.Equals("eos"));
        }

        private string TokenName(int state) { return states.GetValueOrDefault(state); }
        private string TokenLexeme(int nextLastPos)
        {
            string lexeme = researchedString.Substring(lastPos, nextLastPos - lastPos);
            lastPos = nextLastPos;
            return lexeme;
        }

        private bool IsSpaceSeparator(char ch) { return ch == ' ' || ch == '\n' || ch == '\r'; }
        private bool SkipSpaceSeparator()
        {
            while (lastPos < researchedString.Length)
            {
                if (IsSpaceSeparator(researchedString[lastPos]))
                    lastPos++;
                else
                    return true;
            }
            return false;
        }

        private int DFATransition(char ch, int state)
        {
            switch (state)
            {
                case 0:
                    if (char.IsLetter(ch))
                        return 1;
                    else if (ch == '"')
                        return 3;
                    else if (ch == '-')
                        return 7;
                    else if (char.IsPunctuation(ch))
                        return 6;
                    else if (char.IsDigit(ch))
                        return 8;
                    else
                        return -1;
                case 1:
                    if (char.IsLetterOrDigit(ch))
                        return 1;
                    else if (ch == '-')
                        return 2;
                    else
                        return -1;
                case 2:
                    if (char.IsLetterOrDigit(ch))
                        return 1;
                    else
                        return -1;
                case 3:
                    if (char.IsLetterOrDigit(ch) || char.IsPunctuation(ch) || ch == ' ')
                        return 4;
                    else
                        return -1;
                case 4:
                    if (ch == '"')
                        return 5;
                    else if (char.IsLetterOrDigit(ch) || char.IsPunctuation(ch) || ch == ' ')
                        return 4;
                    else 
                        return -1;
                case 7:
                    if (char.IsDigit(ch))
                        return 8;
                    else
                        return -1;
                case 8:
                    if (char.IsLetter(ch))
                        return 1;
                    else if (char.IsDigit(ch))
                        return 8;
                    else if (ch == '.' || ch == ',' || ch == '/' || ch == ':')
                        return 9;
                    else
                        return -1;
                case 9:
                    if (char.IsDigit(ch))
                        return 10;
                    else
                        return -1;
                case 10:
                    if (ch == '.' || ch == ',' || ch == '/' || ch == ':')
                        return 9;
                    else if (char.IsDigit(ch))
                        return 10;
                    else
                        return -1;
                default:
                    return -1;
            }
        }
        private bool IsAcceptingState(int state) { return states.GetValueOrDefault(state) != null; }
    }
}
