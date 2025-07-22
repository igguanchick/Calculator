using System;
using System.Threading;
using Avalonia.Controls;
using Avalonia.Input;

namespace Calculator;

public partial class MainWindow : Window
{
    private bool zeroNum = true; //Обозначает, что у нас введён ноль в начале или после действия
    private bool lastExp = false; //Обозначает, что у нас введён знак выражения
    private bool afterPlusOrMinus = false; //Обозначает, что мы пишем число после плюса или минуса
    private bool haveComma = false; //Обозначает, что в нашем числе есть запятая

    public MainWindow()
    {
        InitializeComponent();
    }

    private void NumberClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e) //Нажатие на 1-9
    {
        if (sender is Button button)
        {
            lastExp = false;
            if (!zeroNum)
            {
                expression.Content = (string)expression.Content + (string)button.Content;
            }
            else
            {
                string expressionContent = "";
                for (int i = 0; i < ((string)expression.Content).Length - 1; i++)
                {
                    expressionContent += ((string)expression.Content)[i];
                }
                expression.Content = expressionContent + (string)button.Content;
                zeroNum = false;
            }
        }
    }

    private void ZeroClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e) //Нажатие на 0
    {
        if (sender is Button button)
        {
            if (!zeroNum)
            {
                expression.Content = (string)expression.Content + "0";
                if (lastExp)
                {
                    zeroNum = true;
                    lastExp = false;
                }
            }
        }
    }

    private void CommaClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e) //Нажатие на ,
    {
        if (sender is Button button)
        {
            if (!haveComma)
            {
                if (lastExp)
                {
                    expression.Content = (string)expression.Content + "0";
                }
                haveComma = true;
                lastExp = true;
                zeroNum = false;
                expression.Content = (string)expression.Content + ",";
            }
        }
    }

    private void EraseClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e) //Стереть последний символ
    {
        if (sender is Button button)
        {
            if (expression.Content != "0")
            {
                if (((string)expression.Content).Length != 1)
                {
                    haveComma = false;
                    string expressionContent = "";
                    for (int i = 0; i < ((string)expression.Content).Length - 1; i++)
                    {
                        expressionContent += ((string)expression.Content)[i];
                    }
                    expression.Content = expressionContent;
                    lastExp = false;
                    switch (expressionContent[expressionContent.Length - 1])
                    {
                        case '+':
                            lastExp = true;
                            break;
                        case '-':
                            lastExp = true;
                            break;
                        case '÷':
                            lastExp = true;
                            break;
                        case '×':
                            lastExp = true;
                            break;
                        case ',':
                            lastExp = true;
                            break;
                    }
                    afterPlusOrMinus = false;
                    for (int i = expressionContent.Length - 1; i > 0; i--)
                    {
                        if (expressionContent[i] == '+' || expressionContent[i] == '-')
                        {
                            afterPlusOrMinus = true;
                            break;
                        }
                        else if (expressionContent[i] == '÷' || expressionContent[i] == '×') break;
                        else if (expressionContent[i] == ',') haveComma = true;
                    }
                }
                else
                {
                    FullEraseClick(sender, e);
                }
            }
        }
    }

    private void FullEraseClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e) //Стереть всё выражение
    {
        if (sender is Button button)
        {
            expression.Content = "0";
            zeroNum = true;
            lastExp = false;
            afterPlusOrMinus = false;
            haveComma = false;
        }
    }

    private void PlusOrMinusClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e) //Нажатие на + или -
    {
        if (sender is Button button)
        {
            zeroNum = false;
            haveComma = false;
            if (lastExp)
            {
                EraseClick(sender, e);
            }
            expression.Content = (string)expression.Content + (string)button.Content;
            lastExp = true;
            afterPlusOrMinus = true;
        }
    }

    private void DivisionOrMultiplicationClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e) //Нажатие на * или /
    {
        if (sender is Button button)
        {
            zeroNum = false;
            haveComma = false;
            if (lastExp)
            {
                EraseClick(sender, e);
            }
            expression.Content = (string)expression.Content + (string)button.Content;
            lastExp = true;
            afterPlusOrMinus = false;
        }
    }

    private void PercentClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e) //Нажатие на %
    {
        if (sender is Button button)
        {
            if (!lastExp)
            {
                if (afterPlusOrMinus)
                {
                    int start = 0;
                    string reverseNumPercent = "";
                    for (int i = ((string)expression.Content).Length - 1; i > -1 && start == 0; i--)
                    {
                        switch (((string)expression.Content)[i])
                        {
                            case '-':
                                start = i;
                                break;
                            case '+':
                                start = i;
                                break;
                            default:
                                reverseNumPercent += ((string)expression.Content)[i];
                                break;
                        }
                    }
                    string numPercent = "";
                    for (int i = reverseNumPercent.Length - 1; i > -1; i--)
                    {
                        numPercent += reverseNumPercent[i];
                    }
                    string percentExpression = "";
                    string newExpressionContent = "";
                    for (int i = 0; i < start; i++)
                    {
                        percentExpression += ((string)expression.Content)[i];
                        newExpressionContent += ((string)expression.Content)[i];
                    }
                    percentExpression = Math.Round(Convert.ToDouble(MainLogic.CalculateExpression(percentExpression)) / 100 * Convert.ToDouble(numPercent), 6).ToString();
                    newExpressionContent += ((string)expression.Content)[start];
                    newExpressionContent += percentExpression;
                    expression.Content = newExpressionContent;
                }
                else
                {
                    int start = 0;
                    string numPercent = "", reverseNumPercent = "";
                    for (int i = ((string)expression.Content).Length - 1; i > -1 && start == 0; i--)
                    {
                        switch (((string)expression.Content)[i])
                        {
                            case '÷':
                                start = i + 1;
                                break;
                            case '×':
                                start = i + 1;
                                break;
                            default:
                                reverseNumPercent += ((string)expression.Content)[i];
                                break;
                        }
                    }
                    for (int i = reverseNumPercent.Length - 1; i > -1; i--)
                    {
                        numPercent += reverseNumPercent[i];
                    }
                    numPercent = Math.Round(Convert.ToDouble(numPercent) / 100, 6).ToString();
                    string newExpressionContent = "";
                    for (int i = 0; i < start; i++)
                    {
                        newExpressionContent += ((string)expression.Content)[i];
                    }
                    newExpressionContent += numPercent;
                    expression.Content = newExpressionContent;
                }
                for (int i = ((string)expression.Content).Length - 1; i > 0; i--)
                {
                    switch (((string)expression.Content)[i])
                    {
                        case '+':
                            i = -1;
                            break;
                        case '-':
                            i = -1;
                            break;
                        case '÷':
                            i = -1;
                            break;
                        case '×':
                            i = -1;
                            break;
                        case ',':
                            haveComma = true;
                            i = -1;
                            break;
                    }
                }
            }
        }
    }

    private void EqualClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e) //Нажатие на =
    {
        if (sender is Button button)
        {
            expression.Content = MainLogic.CalculateExpression((string)expression.Content);
            lastExp = false;
            afterPlusOrMinus = false;
            haveComma = false;
            if ((string)expression.Content == "0")
            {
                zeroNum = true;
            }
            else for (int i = 0; i < ((string)expression.Content).Length; i++)
            {
                if (((string)expression.Content)[i] == ',')
                {
                    haveComma = true;
                }
            }
        }
    }
}