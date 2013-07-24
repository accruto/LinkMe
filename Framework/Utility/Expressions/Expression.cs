using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LinkMe.Framework.Utility.Expressions
{
    /// <summary>
    /// Parses expressions using the following grammar:
    /// 
    /// < expression > ::= { term [ [ operator ] term ] [,...n] }
    /// < term > ::= { word | "quoted_text" | (expression) }
    /// < operator > ::= { AND | OR | AND NOT  }
    /// < word > ::= sequence of characters other than whitespace, double quotes and brackets
    /// < quoted_text > ::= sequence of characters other than double quotes (")
    /// 
    /// Binding order: brackets, AND NOT, AND, OR. If no operator is specified between terms the default is AND.
    /// </summary>
    public static class Expression
    {
        #region Nested types

        private enum Token
        {
            Literal,
            And,
            Or,
            Not,
            OpenBracket,
            CloseBracket
        }

        #endregion

        public const string SymbolsValidInFullTextSearch = "#.-+/";
        public const char NoCachePrefix = '^';
        public const char NoSynonymsPrefix = '=';

        internal const BinaryOperator DefaultBinaryOperator = BinaryOperator.And;
        internal const string DebuggerDisplayValue = "{GetRawExpression()}";

        private const string TokenizeRegexPattern = @"(?<token>\(|\)|(\""[^\""]+\"")" + @"|(?<=^|\s+|\(|\)|\"")[^\s\""\(\)]+(?=$|\s+|\(|\)|\""))";
        public static readonly char[] Prefixes = new[] { NoCachePrefix, NoSynonymsPrefix };

        private static readonly Regex TokenizeRegex = new Regex(TokenizeRegexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly string[] WordsToStrip = new[] { "AND", "OR", "NOT", "(", ")" };

        /// <summary>
        /// Parses a string expression into an IExpression object using "AND" as the default operator.
        /// </summary>
        public static IExpression Parse(string expression)
        {
            return Parse(expression, DefaultBinaryOperator, ModificationFlags.None);
        }

        /// <summary>
        /// Parses a string expression into an IExpression object using "NONE" as the default modifier.
        /// </summary>
        public static IExpression Parse(string expression, BinaryOperator op)
        {
            return Parse(expression, op, ModificationFlags.None);
        }

        /// <summary>
        /// Parses a string expression into an IExpression object using "AND" as the default operator.
        /// </summary>
        public static IExpression Parse(string expression, ModificationFlags flags)
        {
            return Parse(expression, DefaultBinaryOperator, flags);
        }

        /// <summary>
        /// Parses a string expression into an IExpression object using the specified operator as default
        /// (ie. where two terms appear next to each other without an operator in-between).
        /// </summary>
        public static IExpression Parse(string expression, BinaryOperator defaultOp, ModificationFlags flags)
        {
            if (expression == null)
                return null;

            // Trim and replace MS Word quotes with regular quotes.
            expression = expression.Trim().Replace('\x201C', '\"').Replace('\x201D', '\"');

            if (expression.Trim().Length == 0)
                return null;

            var matches = TokenizeRegex.Matches(expression);
            if (matches.Count == 0)
            {
                // How can this happen?
                Debug.Assert(expression.Trim().Trim(LiteralTerm.Quote).Length == 0,
                             "Expression '" + expression + "' contained no tokens - is this valid?");
                return null;
            }

            var currentTokens = Tokenise(matches, defaultOp, flags);
            var parsed = ParseExpression(currentTokens, defaultOp);

            if (IsNotExpression(parsed))
                throw new NotQueryNotSupportedException();

            // Don't apply flags to exact searches
            return parsed;
            // return (flags == ModificationFlags.None || (parsed is LiteralTerm && ((LiteralTerm)parsed).IsExact) ? parsed : new ModifierExpression(parsed, flags));
        }

        public static IExpression ParseExactPhrase(string expression)
        {
            return ParseExactPhrase(expression, ModificationFlags.None);
        }

        public static IExpression ParseExactPhrase(string expression, ModificationFlags flags)
        {
            if (expression == null)
                return null;

            expression = expression.Trim();

            if (expression.Trim().Length == 0)
                return null;

            if (expression.IndexOfAny(TextUtil.WhitespaceChars) != -1)
            {
                // Add quotes to ensure exact phrase
                if (!expression.StartsWith(LiteralTerm.Quote.ToString()))
                    expression = LiteralTerm.Quote + expression;

                if (!expression.EndsWith(LiteralTerm.Quote.ToString()))
                    expression = expression + LiteralTerm.Quote;
            }

            IExpression parsed = new LiteralTerm(expression);

            return (flags == ModificationFlags.None ? parsed : new ModifierExpression(parsed, flags));
        }

        /// <summary>
        /// Combines an array of expressions into a single expression using the specified operators.
        /// If any of the expressions are modifiers (ModifierExpression objects) their flags are also
        /// combined.
        /// </summary>
        public static IExpression Combine(BinaryOperator op, params IExpression[] expressions)
        {
            if (expressions == null || expressions.Length == 0)
                return null;

            var list = new List<IExpression>(expressions.Length);
            var flags = ModificationFlags.None;

            foreach (IExpression exp in expressions)
            {
                if (exp != null)
                {
                    var modifier = exp as ModifierExpression;
                    if (modifier != null)
                    {
                        flags |= modifier.Flags;
                        list.Add(modifier.Expression);
                    }
                    else
                    {
                        list.Add(exp);
                    }
                }
            }

            if (list.Count == 0)
                return null;

            IExpression combined = (list.Count == 1 ? list[0] : new CommutativeExpression(op, list));

            return (flags == 0 ? combined : new ModifierExpression(combined, flags));
        }

        /// <summary>
        /// Splits an expression into simple parts that can be presented to the user: all words, exact phrase,
        /// "at least one of" words and excluded ("NOT") words. Returns true on success, false if the
        /// expression is too complex to be presented in this way.
        /// </summary>
        public static bool SplitIntoSimplifiedParts(IExpression expression, out string allWords,
                                                    out string exactPhrase, out string anyWord, out string withoutWords)
        {
            allWords = null;
            exactPhrase = null;
            anyWord = null;
            withoutWords = null;

            if (expression == null)
                return false;

            var modifierExpression = expression as ModifierExpression;
            if (modifierExpression != null)
            {
                expression = modifierExpression.Expression;
            }

            // Try to split it.

            if (!TrySplitLiterals(expression, ref allWords, ref exactPhrase, ref anyWord)
                && !TrySplitSubExpressions(expression, ref allWords, ref exactPhrase, ref anyWord,
                                           ref withoutWords))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns an expression containing all the terms in the specified expression joined by specified
        /// binary operator. Duplicate terms are filtered out. Eg. if the input expression is
        /// "(this AND that) OR (that AND other)" and the input operator is "OR" the return value is
        /// "this OR that OR other".
        /// </summary>
        public static IExpression Flatten(IExpression expression, BinaryOperator op)
        {
            if (expression == null)
                return null;

            string[] uniqueLiterals = expression.GetUniqueLiterals(true);
            Debug.Assert(!uniqueLiterals.IsNullOrEmpty(), "!MiscUtils.IsNullOrEmpty(uniqueLiterals)");

            if (uniqueLiterals.Length == 1)
                return new LiteralTerm(uniqueLiterals[0]);

            var terms = uniqueLiterals.Select(s => new LiteralTerm(s)).ToArray();

            return new CommutativeExpression(op, terms);
        }

        /// <summary>
        /// Removes all operators and brackets from the supplied text.
        /// </summary>
        public static string StripOperatorsAndBrackets(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            return string.Join(" ", TextUtil.SplitIntoWords(text, WordsToStrip));
        }

        internal static string OperatorToString(UnaryOperator op)
        {
            switch (op)
            {
                case UnaryOperator.Not:
                    return "NOT";

                default:
                    throw new InvalidEnumArgumentException("op", (int)op, typeof(UnaryOperator));
            }
        }

        internal static string OperatorToString(BinaryOperator op)
        {
            switch (op)
            {
                case BinaryOperator.And:
                    return "AND";

                case BinaryOperator.Or:
                    return "OR";

                default:
                    throw new InvalidEnumArgumentException("op", (int)op, typeof(BinaryOperator));
            }
        }

        internal static string OperatorToString(object op)
        {
            if (op == null)
                throw new ArgumentNullException("op");

            if (op is BinaryOperator)
                return OperatorToString((BinaryOperator)op);
            if (op is UnaryOperator)
                return OperatorToString((UnaryOperator)op);

            throw new ArgumentException("Unexpected type of operator: " + op.GetType().FullName, "op");
        }

        internal static int CompareOperatorPrecedence(BinaryOperator a, BinaryOperator b)
        {
            if (a == b)
                return 0;
            if (a == BinaryOperator.Or)
                return -1;

            Debug.Assert(a == BinaryOperator.And && b == BinaryOperator.Or,
                         "a == BinaryOperator.And && b == BinaryOperator.Or");

            return 1;
        }

        private static List<object> Tokenise(MatchCollection matches, BinaryOperator defaultOp, ModificationFlags flags)
        {
            var contexts = new Stack<List<object>>();
            var currentTokens = new List<object>();
            var allowShingling = flags.IsFlagSet(ModificationFlags.AllowShingling);

            for (var i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                Token token = GetToken(match.Value);

                switch (token)
                {
                    case Token.Literal:
                        var matchValue = match.Value;
                        
                        if (allowShingling && i+1 < matches.Count)
                        {
                            //get succesive literal tokens

                            while (i+1 < matches.Count)
                            {
                                var nextToken = GetToken(matches[i + 1].Value);

                                if (nextToken != Token.Literal)
                                    break;

                                matchValue = matchValue + " " + matches[i + 1].Value;
                                i++;
                            }
                            currentTokens.Add(new ModifierExpression(new LiteralTerm(matchValue), flags));
                        }
                        else
                        {
                            currentTokens.Add(new LiteralTerm(matchValue));
                        }
                        break;

                    case Token.And:
                        currentTokens.Add(BinaryOperator.And);
                        break;

                    case Token.Or:
                        currentTokens.Add(BinaryOperator.Or);
                        break;

                    case Token.Not:
                        currentTokens.Add(UnaryOperator.Not);
                        break;

                    case Token.OpenBracket:
                        // Start of a sub-expression.

                        contexts.Push(currentTokens);
                        currentTokens = new List<object>();
                        break;

                    case Token.CloseBracket:
                        // End of sub-expression - parse it.

                        if (contexts.Count == 0)
                        {
                            throw new InvalidQueryException(string.Format("The closing bracket at position"
                                                                          + " {0} does not match any opening bracket.", match.Index));
                        }

                        IExpression subQuery = ParseExpression(currentTokens, defaultOp);
                        currentTokens = contexts.Pop();

                        if (subQuery == null)
                        {
                            throw new InvalidQueryException("The expression contains an empty sub-expression (brackets"
                                                            + " with nothing inside them).");
                        }

                        currentTokens.Add(subQuery);
                        break;

                    default:
                        Debug.Fail("Unexpected token: " + token);
                        break;
                }
            }

            if (contexts.Count > 0)
            {
                throw new InvalidQueryException("An opening bracket does not have a matching"
                                                + " closing bracket.");
            }

            return currentTokens;
        }

        private static bool IsNotExpression(IExpression expression)
        {
            UnaryExpression unary;
            return ((unary = expression as UnaryExpression) != null && unary.Operator == UnaryOperator.Not);
        }

        private static bool TrySplitSubExpressions(IExpression expression, ref string allWords,
                                                   ref string exactPhrase, ref string anyWord, ref string withoutWords)
        {
            var commutative = expression as CommutativeExpression;
            if (commutative == null || commutative.Operator != BinaryOperator.And)
                return false;

            // We have some ANDed sub-expressions. See if we can still split them.

            UnaryExpression not = null;
            var andedLiterals = new List<IExpression>();
            LiteralTerm exact = null;
            CommutativeExpression oredLiterals = null;

            foreach (IExpression commTerm in commutative.Terms)
            {
                IExpression term = commTerm;

                // Is it a literal?

                var literal = term as LiteralTerm;
                if (literal != null)
                {
                    if (exact == null && literal.IsExact)
                    {
                        exact = literal;
                    }
                    else
                    {
                        andedLiterals.Add(literal);
                    }
                    continue;
                }

                // Is it a NOT?

                var unary = term as UnaryExpression;
                if (unary != null)
                {
                    if (not != null || unary.Operator != UnaryOperator.Not)
                        return false; // More than one NOT or something other than NOT - cannot be split.

                    if (unary.Term is LiteralTerm || (unary.Term is CommutativeExpression &&
                                                      ((CommutativeExpression)unary.Term).Operator == BinaryOperator.Or))
                    {
                        not = unary;
                        continue;
                    }

                    return false;
                }

                var innerAndOr = term as CommutativeExpression;
                if (innerAndOr != null)
                {
                    if (innerAndOr.Operator != BinaryOperator.Or || !AreAllLiterals(innerAndOr.Terms))
                        return false; // A sub-expression - cannot be split.

                    if (oredLiterals != null)
                        return false; // More than one OR - cannot be split.

                    oredLiterals = innerAndOr;
                    continue;
                }

                return false;
            }

            if (andedLiterals.Count > 0)
            {
                allWords = andedLiterals.Count == 1 ? andedLiterals[0].GetUserExpression() : JoinLiterals(andedLiterals);
            }

            if (not != null)
            {
                if (not.Term is LiteralTerm)
                {
                    withoutWords = not.Term.GetUserExpression();
                }
                else
                {
                    Debug.Assert(not.Term is CommutativeExpression, "not.Term is CommutativeExpression");
                    withoutWords = JoinLiterals(((CommutativeExpression)not.Term).Terms);
                }
            }
            if (exact != null)
            {
                exactPhrase = exact.Value.Trim(LiteralTerm.Quote);
            }

            if (oredLiterals != null)
            {
                anyWord = JoinLiterals(oredLiterals.Terms);
            }

            return true;
        }

        private static bool TrySplitLiterals(IExpression expression, ref string allWords,
                                             ref string exactPhrase, ref string anyWord)
        {
            // Is it a single literal?

            var literal = expression as LiteralTerm;
            if (literal != null)
            {
                if (literal.IsExact)
                {
                    exactPhrase = literal.Value.Trim(LiteralTerm.Quote);
                }
                else
                {
                    allWords = literal.Value;
                }

                return true;
            }

            // Is it ORs or ANDs?

            var commutative = expression as CommutativeExpression;
            if (commutative != null)
            {
                // Are all the terms literals?

                if (AreAllLiterals(commutative.Terms))
                {
                    if (commutative.Operator == BinaryOperator.Or)
                    {
                        anyWord = JoinLiterals(commutative.Terms);
                    }
                    else if (commutative.Operator == BinaryOperator.And)
                    {
                        allWords = JoinLiterals(commutative.Terms);
                    }
                    else
                    {
                        Debug.Fail("Unexpected binary operator: " + commutative.Operator);
                    }

                    return true;
                }
            }

            return false;
        }

        private static bool AreAllLiterals(IEnumerable<IExpression> terms)
        {
            return terms.All(term => term is LiteralTerm || term is ModifierExpression);
        }

        private static string JoinLiterals(IList<IExpression> literals)
        {
            Debug.Assert(literals.Count > 0, "literals.Count > 0");

            var sb = new StringBuilder(literals[0].GetUserExpression());

            for (int i = 1; i < literals.Count; i++)
            {
                sb.Append(" ");
                sb.Append(literals[i].GetUserExpression());
            }

            return sb.ToString();
        }

        private static IExpression ParseExpression(List<object> tokens, BinaryOperator defaultOp)
        {
            if (tokens.Count == 0)
                return null;

            if (tokens.Count == 1)
            {
                var expression = tokens[0] as IExpression;
                if (expression != null)
                    return expression;

                Debug.Assert(tokens[0] is BinaryOperator || tokens[0] is UnaryOperator,
                             "tokens[0] is BinaryOperator || tokens[0] is UnaryOperator");
                throw new InvalidQueryException(string.Format("The entire query (or a sub-query inside it) is"
                                                              + " an operator: \"{0}\".", OperatorToString(tokens[0])));
            }

            CheckTokensAndInsertDefaultOperator(tokens, defaultOp);

            // Make multiple passes over the list of tokens, replacing "term [ operator ] term" with
            // "expression". Process operators in binding order, tightest first.
            // Not particularly efficient, but should be good enough. Note that at this stage the token
            // list should be "term [operator term] [operator term...]" - there should be no operators or
            // operands right next to each other.

            ReduceTermsForUnaryOperator(tokens, UnaryOperator.Not);
            ReduceTermsForCommutativeOperator(tokens, BinaryOperator.And);
            ReduceTermsForCommutativeOperator(tokens, BinaryOperator.Or);

            Debug.Assert(tokens.Count == 1, tokens.Count + " tokens are left after processing operators.");
            Debug.Assert(tokens[0] is IExpression, "The token left after processing operators is a "
                                                   + tokens[0].GetType().FullName);

            return (IExpression)tokens[0];
        }

        private static void CheckTokensAndInsertDefaultOperator(IList<object> tokens, BinaryOperator defaultOp)
        {
            Debug.Assert(tokens.Count > 1, "tokens.Count > 1");

            var lastTokenIsTerm = (tokens[0] is IExpression);

            if (tokens[0] is BinaryOperator)
            {
                throw new InvalidQueryException(string.Format("The query (or a sub-query inside it)"
                                                              + " begins with an operator: \"{0}\".", OperatorToString(tokens[0])));
            }

            // To make reducing terms easier replace "expression expression" with
            // "expression default_operator expression". At the same time check for invalid conditions, such
            // as two operators in a row.

            for (int index = 1; index < tokens.Count; index++)
            {
                object token = tokens[index];

                if (lastTokenIsTerm)
                {
                    if (token is IExpression || token is UnaryOperator)
                    {
                        tokens.Insert(index, defaultOp); // Two operands in a row - insert default operator so long as we're not shingling
                    }
                    else
                    {
                        Debug.Assert(token is BinaryOperator, "Unexpected type of token: " + token.GetType().FullName);
                    }

                    lastTokenIsTerm = false;
                }
                else
                {
                    // Last token was an operator, expect an operand (IExpression) or a NOT.

                    if (token is BinaryOperator)
                    {
                        throw new InvalidQueryException(string.Format("The query (or a sub-query inside it)"
                                                                      + " contains two operators next to each other: \"{0} {1}\".",
                                                                      OperatorToString((BinaryOperator)tokens[index - 1]),
                                                                      OperatorToString((BinaryOperator)token)));
                    }
                    if (token is UnaryOperator)
                    {
                        Debug.Assert((UnaryOperator)token == UnaryOperator.Not,
                                     "Unexpected unary operator: " + token);

                        object lastToken = tokens[index - 1];
                        if (!(lastToken is BinaryOperator && (BinaryOperator)lastToken == BinaryOperator.And))
                        {
                            throw new InvalidQueryException(string.Format("The query (or a sub-query inside it)"
                                                                          + " contains an invalid operator combination: \"{0} {1}\".  {1} is only"
                                                                          + " supported as part of \"{2} {1}\".",
                                                                          OperatorToString(lastToken), OperatorToString((UnaryOperator)token),
                                                                          OperatorToString(BinaryOperator.And)));
                        }
                    }
                    else
                    {
                        Debug.Assert(token is IExpression, "Unexpected type of token: " + token.GetType().FullName);
                        lastTokenIsTerm = true;
                    }
                }
            }

            if (!lastTokenIsTerm)
            {
                throw new InvalidQueryException(string.Format("The query (or a sub-query inside it) ends with an operator: \"{0}\".", OperatorToString((BinaryOperator)tokens[tokens.Count - 1])));
            }
        }

        private static void ReduceTermsForUnaryOperator(List<object> tokens, UnaryOperator op)
        {
            Debug.Assert(tokens.IndexOf(null) == -1, "The tokens array contains a null.");

            int index = tokens.IndexOf(op);
            while (index != -1)
            {
                Debug.Assert(index != tokens.Count - 1, "index != tokens.Count - 1");

                object right = tokens[index + 1];

                Debug.Assert(right is IExpression, "What the? Right is " + right.GetType());

                // Replace "op, IExpression" with "UnaryExpression".

                tokens[index] = new UnaryExpression(op, (IExpression)right);
                tokens.RemoveAt(index + 1);

                if (index == tokens.Count)
                    break;

                index = tokens.IndexOf(op, index);
            }
        }

        private static void ReduceTermsForCommutativeOperator(List<object> tokens, BinaryOperator op)
        {
            Debug.Assert(tokens.IndexOf(null) == -1, "The tokens array contains a null.");

            int index = tokens.IndexOf(op);
            while (index != -1)
            {
                Debug.Assert(index != 0 && index != tokens.Count - 1, "index != 0 && index != tokens.Count - 1");

                var terms = new List<IExpression>();

                int endIndex = AddOperands(tokens, op, terms, index - 1);
                Debug.Assert(endIndex > index, "endIndex > index");

                // Replace all the tokens from the start index to the end index with a CommutativeExpression.

                var commutative = new CommutativeExpression(op, terms);
                tokens[index - 1] = commutative.Simplify();
                tokens.RemoveRange(index, endIndex - index + 1);

                if (index == tokens.Count)
                    break;

                index = tokens.IndexOf(op, index + 1);
            }
        }

        private static int AddOperands(IList<object> tokens, BinaryOperator op, ICollection<IExpression> operands, int startIndex)
        {
            // Iterate over the list of tokens, starting at startIndex, looking for operands (IExpression
            // objects) separated by the specified operator (op) and adding them to the operands array. The
            // expression ends when we reach a different operator or the end of the token list.

            bool expectOperand = true; // false to expect operator

            int index = startIndex;
            while (index < tokens.Count)
            {
                object token = tokens[index];

                if (expectOperand)
                {
                    var expression = token as IExpression;
                    if (expression != null)
                    {
                        operands.Add(expression);
                    }
                    else
                    {
                        Debug.Assert(token is BinaryOperator, "Unexpected type of token: " + token.GetType().FullName);
                        throw new InvalidQueryException(string.Format("The query (or a sub-query inside it)"
                                                                      + " contains two operators next to each other: \"{0} {1}\".",
                                                                      OperatorToString((BinaryOperator)token), OperatorToString(op)));
                    }
                }
                else
                {
                    // Should never have two operands in a row if CheckTokensAndInsertDefaultOperator() has been called.
                    Debug.Assert(token is BinaryOperator, "Unexpected type of token: " + token.GetType().FullName);

                    if ((BinaryOperator)token != op)
                        return index - 1; // Found a different operator, so that's the end of the expression.
                }

                expectOperand = !expectOperand;
                index++;
            }

            if (expectOperand)
            {
                throw new InvalidQueryException(string.Format("The query (or a sub-query inside it) ends with an operator: \"{0}\".", OperatorToString(op)));
            }

            return tokens.Count - 1;
        }

        private static Token GetToken(string str)
        {
            Debug.Assert(!string.IsNullOrEmpty(str), "!string.IsNullOrEmpty(str)");

            switch (str.Length)
            {
                case 1:
                    if (str == "(")
                        return Token.OpenBracket;
                    if (str == ")")
                        return Token.CloseBracket;
                    break;

                case 2:
                    if (string.Compare(str, "OR", true) == 0)
                        return Token.Or;
                    break;

                case 3:
                    if (string.Compare(str, "AND", true) == 0)
                        return Token.And;
                    if (string.Compare(str, "NOT", true) == 0)
                        return Token.Not;
                    break;
            }

            return Token.Literal;
        }
    }
}