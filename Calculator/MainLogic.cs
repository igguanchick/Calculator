using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Security.Principal;

namespace Calculator;

public class MainLogic
{
    public static string CalculateExpression(string expression) // Вычисление выражения
    {
        bool endCycle = false;
        while (!endCycle)
        {
            endCycle = true;
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '×' || expression[i] == '÷')
                {
                    double number1 = 0, number2 = 0;
                    int start = 0, end = expression.Length - 1;
                    endCycle = false;
                    string reverseStringNumber = "";
                    for (int j = i - 1; j > -1; j--)
                    {
                        switch (expression[j])
                        {
                            case '+':
                                start = j + 1;
                                j = 0;
                                break;
                            case '-':
                                start = j + 1;
                                j = 0;
                                break;
                            case '÷':
                                start = j + 1;
                                j = 0;
                                break;
                            case '×':
                                start = j + 1;
                                j = 0;
                                break;
                            default:
                                reverseStringNumber += expression[j];
                                break;
                        }
                    }
                    string stringNumber = "";
                    for (int j = reverseStringNumber.Length - 1; j > -1; j--)
                    {
                        stringNumber += reverseStringNumber[j];
                    }
                    number1 = Convert.ToDouble(stringNumber);
                    stringNumber = "";
                    for (int j = i + 1; j < expression.Length; j++)
                    {
                        switch (expression[j])
                        {
                            case '+':
                                end = j - 1;
                                j = expression.Length;
                                break;
                            case '-':
                                end = j - 1;
                                j = expression.Length;
                                break;
                            case '÷':
                                end = j - 1;
                                j = expression.Length;
                                break;
                            case '×':
                                end = j - 1;
                                j = expression.Length;
                                break;
                            default:
                                stringNumber += expression[j];
                                break;
                        }
                    }
                    number2 = Convert.ToDouble(stringNumber);
                    string newExpression = "";
                    for (int j = 0; j < expression.Length; j++)
                    {
                        if (j == start)
                        {
                            if (expression[i] == '×')
                            {
                                newExpression += (number1 * number2).ToString();
                            }
                            else
                            {
                                newExpression += Math.Round((number1 / number2), 12).ToString();
                            }
                            j = end;
                        }
                        else
                        {
                            newExpression += expression[j];
                        }
                    }
                    expression = newExpression;
                    break;
                }
            }
        }
        endCycle = false;
        while (!endCycle)
        {
            endCycle = true;
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '+' || expression[i] == '-')
                {
                    double number1 = 0, number2 = 0;
                    int start = 0, end = expression.Length - 1;
                    endCycle = false;
                    string reverseStringNumber = "";
                    for (int j = i - 1; j > -1; j--)
                    {
                        switch (expression[j])
                        {
                            case '+':
                                start = j + 1;
                                j = 0;
                                break;
                            case '-':
                                start = j + 1;
                                j = 0;
                                break;
                            default:
                                reverseStringNumber += expression[j];
                                break;
                        }
                    }
                    string stringNumber = "";
                    for (int j = reverseStringNumber.Length - 1; j > -1; j--)
                    {
                        stringNumber += reverseStringNumber[j];
                    }
                    number1 = Convert.ToDouble(stringNumber);
                    stringNumber = "";
                    for (int j = i + 1; j < expression.Length; j++)
                    {
                        switch (expression[j])
                        {
                            case '+':
                                end = j - 1;
                                j = expression.Length;
                                break;
                            case '-':
                                end = j - 1;
                                j = expression.Length;
                                break;
                            default:
                                stringNumber += expression[j];
                                break;
                        }
                    }
                    number2 = Convert.ToDouble(stringNumber);
                    string newExpression = "";
                    for (int j = 0; j < expression.Length; j++)
                    {
                        if (j == start)
                        {
                            if (expression[i] == '+')
                            {
                                newExpression += (number1 + number2).ToString();
                            }
                            else
                            {
                                newExpression += (number1 - number2).ToString();
                            }
                            j = end;
                        }
                        else
                        {
                            newExpression += expression[j];
                        }
                    }
                    expression = newExpression;
                }
            }
        }
        return Math.Round(Convert.ToDouble(expression), 6).ToString();
    }
}