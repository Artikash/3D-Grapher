using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public static class MathParser
{
    public static void Main() { }

    static string powerMatcher = @"pow\(.+";

    public static float  Evaluate(float x, float y, string expression)
    {
        return Single.Parse(Evaluate(expression.Replace("x", Math.Round(x,6).ToString()).Replace("y", Math.Round(y,6).ToString())));
    }

    static string Evaluate(string expression)
    {
        //System.Diagnostics.Debug.WriteLine(expression + "expression");
        Match match = Regex.Match(expression, powerMatcher);
        if (match.Success)
        {
            string power = "(";
            int open = 1;
            for (int i = 4; open > 0;i++)
            {
                if (match.Value[i] == ',' && open == 1) { power += '|'; continue; }
                power += match.Value[i];
                if (match.Value[i] == '(') { open++; }
                if (match.Value[i] == ')') { open--; }
            }
            string[] powerArguments = power.Substring(1,power.Length - 2).Split('|');
            //System.Diagnostics.Debug.WriteLine(powerArguments[0] +"arg0");
            //System.Diagnostics.Debug.WriteLine(powerArguments[1] + "arg1");
            //System.Diagnostics.Debug.WriteLine(expression.Replace("pow" + power, Math.Pow(Double.Parse(Evaluate(powerArguments[0])), Double.Parse(Evaluate(powerArguments[1]))).ToString()) + "replaced");
            return Evaluate(expression.Replace("pow" + power.Replace('|',','),Math.Pow(Double.Parse(Evaluate(powerArguments[0])),Double.Parse(Evaluate(powerArguments[1]))).ToString()));
        }
        if (expression.IndexOf('N') > -1) { return "-2000000"; }
        return new System.Data.DataTable().Compute(expression,null).ToString();
    }
}