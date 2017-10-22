using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public static class MathParser
{
    public static void Main() { }

    static string powerMatcher = @"pow\([^\)]+\)|pow\[[^\)]+\]|pow\{[^\)]+\}";

    public static float  Evaluate(float x, float y, string expression)
    {
        return Single.Parse(Evaluate(expression.Replace("x", Math.Round(x,6).ToString()).Replace("y", Math.Round(y,6).ToString())));
    }

    static string Evaluate(string expression)
    {
        Match match = Regex.Match(expression, powerMatcher);
        if (match.Success)
        {
            string power = match.Value;
            string[] powerArguments = power.Substring(4,power.Length - 5).Split(',');
            return Evaluate(expression.Replace(power,Math.Pow(Double.Parse(Evaluate(powerArguments[0])),Double.Parse(Evaluate(powerArguments[1]))).ToString()));
        }
        if (expression.IndexOf('N') > -1) { return "-2000000"; }
        return new System.Data.DataTable().Compute(expression,null).ToString();
    }
}