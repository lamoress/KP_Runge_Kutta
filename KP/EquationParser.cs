using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using DynamicExpresso;
using System.Text.RegularExpressions;

namespace KP
{
    public static class EquationParser
    {
        public static Func<double, double[], double> ParseEquation(string equationText, int numVariables)
        {
            try
            {
                equationText = ReplacePowerOperator(equationText);
                equationText = equationText.Replace(",", ".");

                var interpreter = new Interpreter();

                interpreter.SetFunction("sin", (Func<double, double>)Math.Sin);
                interpreter.SetFunction("cos", (Func<double, double>)Math.Cos);
                interpreter.SetFunction("tan", (Func<double, double>)Math.Tan);
                interpreter.SetFunction("exp", (Func<double, double>)Math.Exp);
                interpreter.SetFunction("log", (Func<double, double>)Math.Log);
                interpreter.SetFunction("sqrt", (Func<double, double>)Math.Sqrt);
                interpreter.SetFunction("abs", (Func<double, double>)Math.Abs);
                interpreter.SetFunction("Pow", (Func<double, double, double>)Math.Pow);

                interpreter.SetVariable("pi", Math.PI);
                interpreter.SetVariable("e", Math.E);

                var parameters = new List<Parameter> { new Parameter("t", typeof(double)) };

                for (int i = 0; i < numVariables; i++)
                {
                    parameters.Add(new Parameter($"y{i + 1}", typeof(double)));
                }

                var lambda = interpreter.Parse(equationText, parameters.ToArray());

                if (lambda.ReturnType != typeof(double))
                {
                    throw new Exception("Уравнение должно возвращать числовое значение.");
                }

                return (t, y) =>
                {
                    try
                    {
                        var arguments = new object[parameters.Count];
                        arguments[0] = t;
                        for (int i = 0; i < numVariables; i++)
                        {
                            arguments[i + 1] = y[i];
                        }
                        var result = lambda.Invoke(arguments);
                        return Convert.ToDouble(result);
                    }
                    catch (Exception ex)
                    {
                        ExceptionLogger.LogException(ex);
                        throw new Exception("Ошибка при вычислении выражения.", ex);
                    }
                };
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                throw new Exception($"Ошибка при парсинге уравнения: {ex.Message}", ex);
            }
        }

        private static string ReplacePowerOperator(string equation)
        {
            try
            {
                string pattern = @"(\w+|\))\s*\^\s*(\w+|\()";
                string replacement = "Pow($1,$2)";
                string result = equation;

                while (Regex.IsMatch(result, pattern))
                {
                    result = Regex.Replace(result, pattern, replacement);
                }

                return result;
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                throw new Exception("Ошибка при обработке оператора возведения в степень.", ex);
            }
        }
    }
}
