namespace FishConditionExpression
{
    public static class ConditionHelper
    {
        public static ACondition ParseToConditon(string rawExpression)
        {
            var expression = TrimBracePairs(rawExpression);
            int mode = GetMode(expression);
            switch (mode)
            {
                case 0:
                    return new RawCondition() { name = expression };
                case 1:
                    {
                        And and = new And();
                        and.Conditions = new List<ACondition>(2);
                        var (left, right) = GetElements(expression, "and");
                        and.Conditions.Add(ParseToConditon(left));
                        and.Conditions.Add(ParseToConditon(right));
                        return and;
                    }
                case 2:
                    {
                        Or and = new Or();
                        and.Conditions = new List<ACondition>(2);
                        var (left, right) = GetElements(expression, "or");
                        and.Conditions.Add(ParseToConditon(left));
                        and.Conditions.Add(ParseToConditon(right));
                        return and;
                    }
                default:
                    throw new Exception($"表达式异常:{rawExpression}");
            }
        }
        public static (string, string) GetElements(string exp, string oper)
        {
            int braceDepth = 0;
            int splitIndex1 = -1;
            int splitIndex2 = -1;
            for (int i = exp.Length - 1; i >= 0; i--)
            {
                if (exp[i] == '(')
                {
                    braceDepth++;
                    continue;
                }
                if (exp[i] == ')')
                {
                    braceDepth--;
                    continue;
                }
                if (braceDepth == 0)
                {
                    if (i < exp.Length - oper.Length - 2 && exp.Substring(i, oper.Length + 2) == $" {oper} ")
                    {
                        splitIndex1 = i;
                        splitIndex2 = i + oper.Length + 2;
                        break;
                    }
                }
            }
            if (splitIndex1 < 0 || splitIndex2 < 0)
            {
                throw new Exception($"表达式异常:{exp}");
            }
            return (exp.Substring(0, splitIndex1), exp.Substring(splitIndex2));
        }
        public static int GetMode(string expression)
        {
            int braceDepth = 0;
            for (int i = expression.Length - 1; i >= 0; i--)
            {
                if (expression[i] == '(')
                {
                    braceDepth++;
                    continue;
                }
                if (expression[i] == ')')
                {
                    braceDepth--;
                    continue;
                }
                if (braceDepth == 0)
                {
                    if (i < expression.Length - 3 - 2 && expression.Substring(i, 3 + 2) == " and ")
                    {
                        return 1;
                    }
                    if (i < expression.Length - 2 - 2 && expression.Substring(i, 2 + 2) == " or ")
                    {
                        return 2;
                    }
                }
            }
            return 0;
        }
        public static string TrimBracePairs(string str)
        {
            while (str.Length > 0 && str[0] == '(')
            {
                int braceDepth = 0;
                int level1Left = -1;
                int level1Right = -1;
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == '(')
                    {
                        braceDepth++;
                        if (level1Left < 0)
                        {
                            level1Left = i;
                        }
                    }
                    if (str[i] == ')')
                    {
                        braceDepth--;
                        if (level1Right < 0 && braceDepth == 0)
                        {
                            level1Right = i;
                            break;
                        }
                    }
                }
                if (level1Left >= 0 && level1Right == str.Length - 1)
                {
                    str = str.Substring(1, str.Length - 2);
                }
                else
                {
                    break;
                }
            }
            return str;
        }
    }
}