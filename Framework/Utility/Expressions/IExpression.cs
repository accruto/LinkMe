using System.Collections;

namespace LinkMe.Framework.Utility.Expressions
{
    public interface IExpression
    {
        /// <summary>
        /// Returns the expression as a string with all sub-expressions in brackets to show
        /// operator precedence. Currently used only for debugging purposes.
        /// </summary>
        string GetRawExpression();
        /// <summary>
        /// Returns the expression as a string that can be displayed to the user, without unnecessary
        /// operators or brackets. If the string is parsed again it should result in an equivalent expression.
        /// </summary>
        string GetUserExpression(BinaryOperator defaultOperator);
        /// <summary>
        /// Returns the expression as a string that can be displayed to the user, without unnecessary
        /// operators or brackets. If the string is parsed again it should result in an equivalent expression.
        /// Uses AND as the default operator.
        /// </summary>
        string GetUserExpression();
        /// <summary>
        /// Returns an array of all the terms that would be matched by the expression, filtering out duplicates.
        /// Negated terms are not returned.
        /// </summary>
        string[] GetUniqueLiterals(bool originalOnly);
        /// <summary>
        /// Returns true if the input text matches the expression, otherwise false.
        /// </summary>
        /// <param name="text">The text to check against the expression. Must not be null.</param>
        bool IsMatch(string text);
        /// <summary>
        /// Returns true if the expression is equivalent (not necessarily identical) to another expression,
        /// ie. a query using one expression should always return the same results as a query using the other.
        /// </summary>
        /// <param name="obj">The expression to compare to.</param>
        /// <param name="ignoreCase">True to perform case-insensitive comparisons on string literlas, false
        /// to perform case-sensitive comparisons.</param>
        /// <returns>True if the expressions are equivalent, otherwise false.</returns>
        bool Equals(IExpression obj, bool ignoreCase);
    }
}