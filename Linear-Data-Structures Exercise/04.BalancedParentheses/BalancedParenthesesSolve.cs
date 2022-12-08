namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            var stack = new Stack<char>(parentheses.Length / 2);

            if (parentheses.Length % 2 != 0)
            {
                return false;
            }

            foreach (var singleParenthese in parentheses)
            {
                char expectedParenthese = default;

                switch (singleParenthese)
                {
                    case ')':
                        expectedParenthese = '(';
                        break;
                    case '}':
                        expectedParenthese = '{';
                        break;
                    case ']':
                        expectedParenthese = '[';
                        break;
                    default:
                        stack.Push(singleParenthese);
                        break;
                }

                if (expectedParenthese == default)
                {
                    continue;
                }

                if (stack.Pop() != expectedParenthese)
                {
                    return false;
                }
            }

            return stack.Count == 0;
        }
    }
}
