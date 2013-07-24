using System;
using System.Linq;
using LinkMe.Framework.Utility.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test
{
    [TestClass]
    public class ExpressionTests
    {
        [TestMethod]
        public void GetUserExpressionBugs()
        {
            string allWords, exactPhrase, anyWord, withoutWords;

            // Case 6917 - parse and split "not for profit" (without quotes).

            var exp = Expression.Parse("not for profit");
            Assert.AreEqual("((NOT for) AND profit)", exp.GetRawExpression());
            Assert.AreEqual("NOT for profit", exp.GetUserExpression());
            Assert.IsTrue(Expression.SplitIntoSimplifiedParts(exp, out allWords, out exactPhrase, out anyWord, out withoutWords));
            Assert.AreEqual("profit", allWords);
            Assert.IsNull(exactPhrase);
            Assert.IsNull(anyWord);
            Assert.AreEqual("for", withoutWords);

            // OR NOT

            exp = Expression.Parse("((NOT for) OR profit)");
            Assert.AreEqual("NOT for OR profit", exp.GetUserExpression());
        }

        [TestMethod]
        public void TestStripOperatorsAndBrackets()
        {
            Assert.AreEqual(null, Expression.StripOperatorsAndBrackets(null));
            Assert.AreEqual("", Expression.StripOperatorsAndBrackets(""));
            Assert.AreEqual("", Expression.StripOperatorsAndBrackets("and"));
            Assert.AreEqual("whatever", Expression.StripOperatorsAndBrackets("whatever"));
            Assert.AreEqual("anything everything", Expression.StripOperatorsAndBrackets("anything aNd everything"));
            Assert.AreEqual("THAT", Expression.StripOperatorsAndBrackets("NOT THAT"));
            Assert.AreEqual("something else", Expression.StripOperatorsAndBrackets("something (or else)"));
            Assert.AreEqual("invalid bracket opened", Expression.StripOperatorsAndBrackets("invalid bracket (opened"));
            Assert.AreEqual("with extra spaces", Expression.StripOperatorsAndBrackets("with extra ( spaces )"));
            Assert.AreEqual("", Expression.StripOperatorsAndBrackets("and or"));
            Assert.AreEqual("brackets anything stuff", Expression.StripOperatorsAndBrackets("brackets or (not anything) and stuff"));
        }

        [TestMethod]
        public void TestParseSuccess()
        {
            // Empty.

            var parsed = Expression.Parse(null);
            Assert.IsNull(parsed);

            parsed = Expression.Parse("");
            Assert.IsNull(parsed);

            parsed = Expression.Parse("\"\"");
            Assert.IsNull(parsed);

            parsed = Expression.Parse("\"\"\"");
            Assert.IsNull(parsed);

            // Single word.

            parsed = Expression.Parse("word");
            var literal = parsed as LiteralTerm;
            Assert.IsNotNull(literal);
            Assert.AreEqual("word", literal.Value);
            Assert.AreEqual("word", parsed.GetRawExpression());
            Assert.AreEqual("word", parsed.GetUserExpression());

            parsed = Expression.Parse("\"exact phrase\"");
            literal = parsed as LiteralTerm;
            Assert.IsNotNull(literal);
            Assert.AreEqual("\"exact phrase\"", literal.Value);
            Assert.AreEqual("\"exact phrase\"", parsed.GetRawExpression());
            Assert.AreEqual("\"exact phrase\"", parsed.GetUserExpression());
            Assert.AreEqual(parsed, Expression.Parse(parsed.GetUserExpression()));

            // Sumbols.

            parsed = Expression.Parse("the-test");
            literal = parsed as LiteralTerm;
            Assert.IsNotNull(literal);
            Assert.AreEqual("the-test", literal.Value);

            parsed = Expression.Parse("test/ing");
            literal = parsed as LiteralTerm;
            Assert.IsNotNull(literal);
            Assert.AreEqual("test/ing", literal.Value);

            // One explicit operator.

            parsed = Expression.Parse("explicit or disjunction");
            var commutative = parsed as CommutativeExpression;
            Assert.IsNotNull(commutative);
            Assert.AreEqual(BinaryOperator.Or, commutative.Operator);
            Assert.AreEqual(2, commutative.Terms.Count);
            Assert.AreEqual("explicit", ((LiteralTerm)commutative.Terms[0]).Value);
            Assert.AreEqual("disjunction", ((LiteralTerm)commutative.Terms[1]).Value);
            Assert.AreEqual("(explicit OR disjunction)", parsed.GetRawExpression());
            Assert.AreEqual("explicit OR disjunction", parsed.GetUserExpression());
            Assert.AreEqual(parsed, Expression.Parse(parsed.GetUserExpression()));

            // One implicit operator.

            parsed = Expression.Parse("implicit conjunction");
            commutative = parsed as CommutativeExpression;
            Assert.IsNotNull(commutative);
            Assert.AreEqual(BinaryOperator.And, commutative.Operator);
            Assert.AreEqual(2, commutative.Terms.Count);
            Assert.AreEqual("implicit", ((LiteralTerm)commutative.Terms[0]).Value);
            Assert.AreEqual("conjunction", ((LiteralTerm)commutative.Terms[1]).Value);
            Assert.AreEqual("(implicit AND conjunction)", parsed.GetRawExpression());
            Assert.AreEqual("implicit conjunction", parsed.GetUserExpression());
            Assert.AreEqual(parsed, Expression.Parse(parsed.GetUserExpression()));

            // Implicit operator and explicit operators. AND should bind tighter than OR.

            parsed = Expression.Parse("implicit conjunctions Or an explicit and one");
            Assert.AreEqual("((implicit AND conjunctions) OR (an AND explicit AND one))", parsed.GetRawExpression());
            Assert.AreEqual("implicit conjunctions OR an explicit one", parsed.GetUserExpression());
            Assert.AreEqual(parsed, Expression.Parse(parsed.GetUserExpression()));

            parsed = Expression.Parse("(explicit OR disjunctions) AND (another or one)");
            Assert.AreEqual("((explicit OR disjunctions) AND (another OR one))", parsed.GetRawExpression());
            Assert.AreEqual("(explicit OR disjunctions) (another OR one)", parsed.GetUserExpression());
            Assert.AreEqual(parsed, Expression.Parse(parsed.GetUserExpression()));

            // Operator precedence: AND NOT should bind tighter than AND.

            parsed = Expression.Parse("\"match this\" AND NOT \"don't match this\" AND \"but match this\"");
            commutative = parsed as CommutativeExpression;
            Assert.IsNotNull(commutative);
            Assert.AreEqual(BinaryOperator.And, commutative.Operator);
            Assert.AreEqual(3, commutative.Terms.Count);
            Assert.AreEqual("\"match this\"", ((LiteralTerm)commutative.Terms[0]).Value);
            Assert.AreEqual("\"but match this\"", ((LiteralTerm)commutative.Terms[2]).Value);
            var unary = (UnaryExpression)commutative.Terms[1];
            Assert.AreEqual(UnaryOperator.Not, unary.Operator);
            Assert.AreEqual("\"don't match this\"", ((LiteralTerm)unary.Term).Value);
            Assert.AreEqual("(\"match this\" AND (NOT \"don't match this\") AND \"but match this\")", parsed.GetRawExpression());
            Assert.AreEqual("\"match this\" AND NOT \"don't match this\" \"but match this\"", parsed.GetUserExpression());
            Assert.AreEqual(parsed, Expression.Parse(parsed.GetUserExpression()));

            // Expressions in brackets inlined.

            parsed = Expression.Parse("one two (three (four five six seven) eight) OR nine OR (ten OR eleven)");
            Assert.AreEqual("((one AND two AND three AND four AND five AND six AND seven AND eight) OR nine OR ten OR eleven)", parsed.GetRawExpression());
            Assert.AreEqual("one two three four five six seven eight OR nine OR ten OR eleven", parsed.GetUserExpression());
            Assert.AreEqual(parsed, Expression.Parse(parsed.GetUserExpression()));

            // Complex expression.

            parsed = Expression.Parse(" word OR \"exact string\" AND (another expression AND NOT   (some \"other   sub\"  expression))");
            Assert.AreEqual("(word OR (\"exact string\" AND another AND expression AND (NOT (some AND \"other   sub\" AND expression))))", parsed.GetRawExpression());
            Assert.AreEqual("word OR \"exact string\" another expression AND NOT (some \"other   sub\" expression)", parsed.GetUserExpression());
            Assert.AreEqual(parsed, Expression.Parse(parsed.GetUserExpression()));

            // "NOT" in-between terms is the same as "AND NOT"

            parsed = Expression.Parse("one NOT two");
            commutative = parsed as CommutativeExpression;
            Assert.IsNotNull(commutative);
            Assert.AreEqual(BinaryOperator.And, commutative.Operator);
            Assert.AreEqual(2, commutative.Terms.Count);
            Assert.AreEqual("one", ((LiteralTerm)commutative.Terms[0]).Value);
            unary = (UnaryExpression)commutative.Terms[1];
            Assert.AreEqual("two", ((LiteralTerm)unary.Term).Value);

            // Double "AND NOT".

            parsed = Expression.Parse("one AND NOT two AND NOT three");
            commutative = parsed as CommutativeExpression;
            Assert.IsNotNull(commutative);
            Assert.AreEqual(BinaryOperator.And, commutative.Operator);
            Assert.AreEqual(3, commutative.Terms.Count);
            Assert.AreEqual("one", ((LiteralTerm)commutative.Terms[0]).Value);
            unary = (UnaryExpression)commutative.Terms[1];
            Assert.AreEqual("two", ((LiteralTerm)unary.Term).Value);
            unary = (UnaryExpression)commutative.Terms[2];
            Assert.AreEqual("three", ((LiteralTerm)unary.Term).Value);
            Assert.AreEqual("(one AND (NOT two) AND (NOT three))", parsed.GetRawExpression());
            Assert.AreEqual("one AND NOT two AND NOT three", parsed.GetUserExpression());
            Assert.AreEqual(parsed, Expression.Parse(parsed.GetUserExpression()));
        }

        [TestMethod]
        public void TestParseModifiers()
        {
            var parsed = Expression.Parse("one", ModificationFlags.AllowShingling);
            var term = parsed as LiteralTerm;
            Assert.IsNotNull(term);
            Assert.AreEqual("one", term.Value);

            parsed = Expression.Parse("one two", ModificationFlags.AllowShingling);
            var modifier = parsed as ModifierExpression;
            Assert.IsNotNull(modifier);
            Assert.AreEqual(ModificationFlags.AllowShingling, modifier.Flags);
            Assert.AreEqual(typeof(LiteralTerm), modifier.Expression.GetType());
            Assert.AreEqual("one two", parsed.GetRawExpression());

            parsed = Expression.Parse("\"something quoted\"", ModificationFlags.AllowShingling);
            modifier = parsed as ModifierExpression;
            Assert.IsNull(modifier);
            Assert.AreEqual(typeof(LiteralTerm), parsed.GetType());
            Assert.AreEqual("\"something quoted\"", parsed.GetRawExpression());
        }

        [TestMethod]
        public void TestParseExactPhrase()
        {
            // Regular

            var parsed = Expression.ParseExactPhrase("some phrase");
            var literal = parsed as LiteralTerm;
            Assert.IsNotNull(literal);
            Assert.AreEqual("\"some phrase\"", literal.Value);

            // Quotes and spaces

            parsed = Expression.ParseExactPhrase(" \"another phrase\"  ");
            literal = parsed as LiteralTerm;
            Assert.IsNotNull(literal);
            Assert.AreEqual("\"another phrase\"", literal.Value);

            // Search flags

            parsed = Expression.ParseExactPhrase("search flags");
            literal = parsed as LiteralTerm;
            Assert.IsNotNull(literal);
            Assert.AreEqual("\"search flags\"", literal.Value);

            // Query flags

            parsed = Expression.ParseExactPhrase("query flags", ModificationFlags.AllowShingling);
            var modifier = parsed as ModifierExpression;
            Assert.IsNotNull(modifier);
            Assert.AreEqual(ModificationFlags.AllowShingling, modifier.Flags);
            Assert.AreEqual("\"query flags\"", ((LiteralTerm)modifier.Expression).Value);
        }

        [TestMethod]
        public void TestParseErrors()
        {
            try
            {
                Expression.Parse("()");
                throw new AssertFailedException("No exception was thrown for an invalid query.");
            }
            catch (InvalidQueryException ex)
            {
                AssertExMessageContains(ex, "contains an empty sub-expression");
            }

            try
            {
                Expression.Parse("Invalid ( \t  ) sub-query");
                throw new AssertFailedException("No exception was thrown for an invalid query.");
            }
            catch (InvalidQueryException ex)
            {
                AssertExMessageContains(ex, "contains an empty sub-expression");
            }

            try
            {
                Expression.Parse("and something");
                throw new AssertFailedException("No exception was thrown for an invalid query.");
            }
            catch (InvalidQueryException ex)
            {
                AssertExMessageContains(ex, "begins with an operator");
            }

            try
            {
                Expression.Parse("nothing oR");
                throw new AssertFailedException("No exception was thrown for an invalid query.");
            }
            catch (InvalidQueryException ex)
            {
                AssertExMessageContains(ex, "ends with an operator");
            }

            try
            {
                Expression.Parse("one or not two");
                throw new AssertFailedException("No exception was thrown for an invalid query.");
            }
            catch (InvalidQueryException ex)
            {
                AssertExMessageContains(ex, "invalid operator combination: \"OR NOT\"");
            }

            try
            {
                Expression.Parse("something and or nothing");
                throw new AssertFailedException("No exception was thrown for an invalid query.");
            }
            catch (InvalidQueryException ex)
            {
                AssertExMessageContains(ex, "two operators next to each other: \"AND OR\"");
            }

            try
            {
                Expression.Parse("this (and not) that");
                throw new AssertFailedException("No exception was thrown for an invalid query.");
            }
            catch (InvalidQueryException ex)
            {
                AssertExMessageContains(ex, "The query (or a sub-query inside it) begins with an operator: \"AND\"");
            }

            try
            {
                Expression.Parse("not (human or resources)");
                throw new AssertFailedException("No exception was thrown for an invalid query.");
            }
            catch (InvalidQueryException ex)
            {
                AssertExMessageContains(ex, "The entire expression is a \"NOT\" expression, which is not supported.");
            }
        }

        [TestMethod]
        public void TestGetUniqueTerms()
        {
            var exp = Expression.Parse("one");
            Assert.IsTrue(new[] { "one" }.SequenceEqual(exp.GetUniqueLiterals(false)));

            exp = Expression.Parse("one two three");
            Assert.IsTrue(new[] { "one", "two", "three" }.SequenceEqual(exp.GetUniqueLiterals(false)));

            exp = Expression.Parse("one two one three");
            Assert.IsTrue(new[] { "one", "two", "three" }.SequenceEqual(exp.GetUniqueLiterals(false)));

            exp = Expression.Parse("one AND \"two one\" OR three");
            Assert.IsTrue(new[] { "one", "\"two one\"", "three" }.SequenceEqual(exp.GetUniqueLiterals(false)));

            exp = Expression.Parse("(one AND NOT \"one two three\") OR (three AND four)");
            Assert.IsTrue(new[] { "one", "three", "four" }.SequenceEqual(exp.GetUniqueLiterals(false)));
        }

        [TestMethod]
        public void TestEqualityAndHashCodes()
        {
            // Implicit AND operator.

            var one = Expression.Parse("one and two");
            var two = Expression.Parse("one two");
            Assert.AreEqual(one, two);
            Assert.AreEqual(one.GetHashCode(), two.GetHashCode());

            // Ignore case.

            two = Expression.Parse("one AND TWO");
            Assert.AreEqual(one, two);
            Assert.AreEqual(one.GetHashCode(), two.GetHashCode());

            // Different order.

            two = Expression.Parse("two ONE");
            Assert.AreEqual(one, two);
            Assert.AreEqual(one.GetHashCode(), two.GetHashCode());

            // Different operator

            two = Expression.Parse("one or two");
            Assert.IsFalse(Equals(one, two));

            // Different words

            two = Expression.Parse("one two2");
            Assert.IsFalse(Equals(one, two));

            // Subset or superset

            two = Expression.Parse("one");
            Assert.IsFalse(Equals(one, two));

            two = Expression.Parse("one one");
            Assert.IsFalse(Equals(one, two));

            two = Expression.Parse("one two three");
            Assert.IsFalse(Equals(one, two));

            // Complex expressions

            one = Expression.Parse("(one OR two) AND NOT (three OR four) AND five");
            two = Expression.Parse("five and not (four OR three) (ONE OR two)");
            Assert.AreEqual(one, two);
            Assert.AreEqual(one.GetHashCode(), two.GetHashCode());
        }

        [TestMethod]
        public void TestCombine()
        {
            // 0 expressions.

            var combined = Expression.Combine(BinaryOperator.Or);
            Assert.IsNull(combined);

            combined = Expression.Combine(BinaryOperator.Or, null);
            Assert.IsNull(combined);

            // 1 expression.

            IExpression one = new LiteralTerm("something");
            combined = Expression.Combine(BinaryOperator.Or, one);
            Assert.AreSame(one, combined);

            // Multiple expressions.

            combined = Expression.Combine(BinaryOperator.Or, one, one);
            Assert.AreEqual(typeof(CommutativeExpression), combined.GetType());
            Assert.AreEqual("something OR something", combined.GetUserExpression());

            combined = Expression.Combine(BinaryOperator.And, Expression.Parse("one or two"), Expression.Parse("three four five"), Expression.Parse("six or seven or eight"));
            Assert.AreEqual("(one OR two) three four five (six OR seven OR eight)", combined.GetUserExpression());

            // Modifiers.

            combined = Expression.Combine(BinaryOperator.Or, Expression.Parse("no synonyms", ModificationFlags.AllowShingling));
            Assert.AreEqual(typeof(ModifierExpression), combined.GetType());

            combined = Expression.Combine(BinaryOperator.Or, Expression.Parse("no"), Expression.Parse("synonyms"));
            Assert.AreEqual(typeof(CommutativeExpression), combined.GetType());
            Assert.AreEqual("no OR synonyms", combined.GetUserExpression());

            combined = Expression.Combine(BinaryOperator.And, Expression.Parse("\"no cache\""), Expression.Parse("\"or synonyms\"", ModificationFlags.AllowShingling));
            Assert.AreEqual(typeof(CommutativeExpression), combined.GetType());
            Assert.AreEqual("\"no cache\" \"or synonyms\"", combined.GetUserExpression());

            combined = Expression.Combine(BinaryOperator.And, Expression.Parse("\"no cache\""), Expression.Parse("\"or synonyms\""), Expression.Parse("\"definitely no synonyms\""));
            Assert.AreEqual(typeof(CommutativeExpression), combined.GetType());
            Assert.AreEqual("\"no cache\" \"or synonyms\" \"definitely no synonyms\"", combined.GetUserExpression());
        }

        [TestMethod]
        public void TestSplit()
        {
            string allWords;
            string exactPhrase;
            string anyWord;
            string withoutWords;

            // Success.

            var exp = Expression.Parse("oneWord");
            Assert.IsTrue(Expression.SplitIntoSimplifiedParts(exp, out allWords, out exactPhrase, out anyWord, out withoutWords));
            Assert.AreEqual("oneWord", allWords);
            AssertAllNull(exactPhrase, anyWord, withoutWords);

            exp = Expression.Parse("two words");
            Assert.IsTrue(Expression.SplitIntoSimplifiedParts(exp, out allWords, out exactPhrase, out anyWord, out withoutWords));
            Assert.AreEqual("two words", allWords);
            AssertAllNull(exactPhrase, anyWord, withoutWords);

            exp = Expression.Parse("\"exact phrase\"");
            Assert.IsTrue(Expression.SplitIntoSimplifiedParts(exp, out allWords, out exactPhrase, out anyWord, out withoutWords));
            Assert.AreEqual("exact phrase", exactPhrase);
            AssertAllNull(allWords, anyWord, withoutWords);

            exp = Expression.Parse("any or word");
            Assert.IsTrue(Expression.SplitIntoSimplifiedParts(exp, out allWords, out exactPhrase, out anyWord, out withoutWords));
            Assert.AreEqual("any word", anyWord);
            AssertAllNull(allWords, exactPhrase, withoutWords);

            exp = Expression.Parse("these words and not thisWord");
            Assert.IsTrue(Expression.SplitIntoSimplifiedParts(exp, out allWords, out exactPhrase, out anyWord, out withoutWords));
            Assert.AreEqual("these words", allWords);
            Assert.AreEqual("thisWord", withoutWords);
            AssertAllNull(exactPhrase, anyWord);

            exp = Expression.Parse("(how or about) all this \"complex stuff\" and not (any or \"of these\")");
            Assert.IsTrue(Expression.SplitIntoSimplifiedParts(exp, out allWords, out exactPhrase, out anyWord, out withoutWords));
            Assert.AreEqual("how about", anyWord);
            Assert.AreEqual("all this", allWords);
            Assert.AreEqual("complex stuff", exactPhrase);
            Assert.AreEqual("any \"of these\"", withoutWords);

            // Modifiers.

            exp = Expression.Parse("all words");
            Assert.IsTrue(Expression.SplitIntoSimplifiedParts(exp, out allWords, out exactPhrase, out anyWord, out withoutWords));
            Assert.AreEqual("all words", allWords);
            AssertAllNull(exactPhrase, anyWord, withoutWords);

            exp = Expression.Parse("any or word");
            Assert.IsTrue(Expression.SplitIntoSimplifiedParts(exp, out allWords, out exactPhrase, out anyWord, out withoutWords));
            Assert.AreEqual("any word", anyWord);
            AssertAllNull(allWords, exactPhrase, withoutWords);

            // Weighted

            //exp = InsertSynonyms(Expression.Parse("analyst"));
            //Assert.IsTrue(Expression.SplitIntoSimplifiedParts(exp, out allWords, out exactPhrase,
            //    out anyWord, out withoutWords));
            //Assert.AreEqual("analyst", allWords);
            //AssertAllNull(exactPhrase, anyWord, withoutWords);

            //exp = InsertSynonyms(Expression.Parse("business analyst"));
            //var commutative = exp as CommutativeExpression;
            //Assert.IsNotNull(commutative);
            //Assert.AreEqual(2, commutative.Terms.Count);

            //Assert.IsTrue(Expression.SplitIntoSimplifiedParts(exp, out allWords, out exactPhrase,
            //    out anyWord, out withoutWords));
            //Assert.AreEqual("business analyst", allWords);
            //AssertAllNull(exactPhrase, anyWord, withoutWords);

            // Failure.

            Assert.IsFalse(Expression.SplitIntoSimplifiedParts(null, out allWords, out exactPhrase, out anyWord, out withoutWords));

            exp = Expression.Parse("(one or two) (three or four)");
            Assert.IsFalse(Expression.SplitIntoSimplifiedParts(exp, out allWords, out exactPhrase, out anyWord, out withoutWords));

            exp = Expression.Parse("(one and two) or (three and four)");
            Assert.IsFalse(Expression.SplitIntoSimplifiedParts(exp, out allWords, out exactPhrase, out anyWord, out withoutWords));

            exp = Expression.Parse("this and not (anded insideNot)");
            Assert.IsFalse(Expression.SplitIntoSimplifiedParts(exp, out allWords, out exactPhrase, out anyWord, out withoutWords));
        }

        [TestMethod]
        public void TestFlatten()
        {
            Assert.IsNull(Expression.Flatten(null, BinaryOperator.And));
            Assert.AreEqual("one", Flatten("one", BinaryOperator.Or));
            Assert.AreEqual("\"still one\"", Flatten("\"still one\"", BinaryOperator.Or));
            Assert.AreEqual("one OR two", Flatten("one two", BinaryOperator.Or));
            Assert.AreEqual("one two", Flatten("one two", BinaryOperator.And));
            Assert.AreEqual("one, OR two OR three", Flatten("one, two\tthree", BinaryOperator.Or));
            Assert.AreEqual("one two ,, three", Flatten("one  and \t two ,, three", BinaryOperator.And));
            Assert.AreEqual("C# OR .NET", Flatten("C# .NET", BinaryOperator.Or));
            Assert.AreEqual("this OR that OR other", Flatten("(this AND that) OR (that AND other)", BinaryOperator.Or));
            // Case 6917 - strip negated terms from flattened expressions.
            Assert.AreEqual("this OR \"something quoted\"", Flatten(
                                                                "(this OR \"something quoted\") AND \"something quoted\" AND NOT \"something else\"", BinaryOperator.Or));
            Assert.AreEqual("this", Flatten("this and not that", BinaryOperator.Or));
            Assert.AreEqual("this OR that", Flatten("this and that and not \"something else\"", BinaryOperator.Or));
            Assert.AreEqual("this that", Flatten("this and that and not \"something else\"", BinaryOperator.And));
        }

        [TestMethod]
        public void TestIsMatch()
        {
            // One word.

            Assert.IsFalse(IsMatch("test", ""));
            Assert.IsFalse(IsMatch("test", "tes"));
            Assert.IsFalse(IsMatch("test", "est"));

            // Case and quotes.

            Assert.IsTrue(IsMatch("test", "test"));
            Assert.IsTrue(IsMatch("test", "TeSt"));
            Assert.IsTrue(IsMatch("test", "\"tesT\""));
            Assert.IsTrue(IsMatch("test", "\'Test\'"));

            // Repeated characeter.

            Assert.IsFalse(IsMatch("tt", "t"));
            Assert.IsFalse(IsMatch("tt", "ttt"));
            Assert.IsFalse(IsMatch("tt", "tttt"));
            Assert.IsTrue(IsMatch("tt", "ttt tt"));
            Assert.IsTrue(IsMatch("tt", "tt ttt"));

            // Whole word match only - where "word" is a sequence of [a-zA-Z_0-9] characters.

            Assert.IsTrue(IsMatch("test", "test data"));
            Assert.IsTrue(IsMatch("test", "Some test."));
            Assert.IsFalse(IsMatch("test", "testing"));
            Assert.IsFalse(IsMatch("test", "thetest"));
            Assert.IsTrue(IsMatch("test", "test/ing"));
            Assert.IsTrue(IsMatch("test", "the-test"));

            // Operators.

            Assert.IsTrue(IsMatch("this OR that", "this data"));
            Assert.IsTrue(IsMatch("this OR that", "that data"));
            Assert.IsFalse(IsMatch("this AND that", "this data"));
            Assert.IsFalse(IsMatch("this AND that", "that data"));
            Assert.IsTrue(IsMatch("this OR data", "this data"));
            Assert.IsTrue(IsMatch("this AND data", "this data"));
            Assert.IsTrue(IsMatch("(this OR that) AND data", "This data or not?"));
            Assert.IsFalse(IsMatch("(this OR that) AND data", "This thing or not?"));
            Assert.IsFalse(IsMatch("(this AND that) OR data", "This thing or not?"));
            Assert.IsTrue(IsMatch("(this AND that) OR data", "This thing or that one?"));
            Assert.IsTrue(IsMatch("(this AND that) OR data", "This data or that data."));
            Assert.IsTrue(IsMatch("(this AND NOT that) OR data", "This data or that data."));
            Assert.IsFalse(IsMatch("(this AND NOT that) OR data", "This thing or that one?"));
            Assert.IsFalse(IsMatch("this AND NOT (that OR data)", "This thing or that one?"));
            Assert.IsFalse(IsMatch("this AND NOT (that OR data)", "This data or that data."));
            Assert.IsFalse(IsMatch("this AND NOT (that OR data)", "This thing or that thing"));
            Assert.IsFalse(IsMatch("this AND NOT (that OR data)", "Some thing or that thing"));
            Assert.IsTrue(IsMatch("this AND NOT (that OR data)", "Try THIS!"));

            // Error.

            try
            {
                IsMatch("test", null);
                Assert.Fail("Expected ArgumentNullException");
            }
            catch (ArgumentNullException)
            {
            }
        }

        private static bool IsMatch(string expression, string text)
        {
            return Expression.Parse(expression).IsMatch(text);
        }

        private static string Flatten(string expression, BinaryOperator op)
        {
            return Expression.Flatten(Expression.Parse(expression), op).GetUserExpression();
        }

        private static void AssertExMessageContains(Exception ex, string stringToFind)
        {
            Assert.IsTrue(ex.Message.IndexOf(stringToFind) != -1, "Exception message '" + ex.Message + "' does not contain expected string '" + stringToFind + "'.");
        }

        private static void AssertAllNull(params object[] objects)
        {
            for (var i = 0; i < objects.Length; i++)
            {
                if (objects[i] != null)
                    Assert.Fail("The object at index " + i + " should be null, but is '" + objects[i] + "'.");
            }
        }
    }
}