using Avalonia.Controls;

namespace Calculator;

public partial class MainWindow : Window
{
    private bool ZeroNum = true;
    private bool LastExp = false;
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Number_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            if (!ZeroNum)
            {
                string expressionContent = (string)expression.Content;
                string buttonNumber = (string)button.Content;
                expression.Content = expressionContent + buttonNumber;
            }
            else
            {
                string bufExpressionContent = (string)expression.Content;
                string expressionContent = "";
                for (int i = 0; i < bufExpressionContent.Length - 1; i++)
                {
                    expressionContent += bufExpressionContent[i];
                }
                string buttonNumber = (string)button.Content;
                expression.Content = expressionContent + buttonNumber;
                ZeroNum = false;
            }
        }
    }

    private void Zero_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            if (!ZeroNum)
            {
                string expressionContent = (string)expression.Content;
                string buttonNumber = (string)button.Content;
                expression.Content = expressionContent + buttonNumber;
            }
        }
    }

    private void Erase_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            if (expression.Content != "0")
            {
                string bufExpressionContent = (string)expression.Content;
                if (bufExpressionContent.Length != 1)
                {
                    string expressionContent = "";
                    for (int i = 0; i < bufExpressionContent.Length - 1; i++)
                    {
                        expressionContent += bufExpressionContent[i];
                    }
                    expression.Content = expressionContent;
                }
                else
                {
                    expression.Content = "0";
                    ZeroNum = true;
                }
            }
        }
    }
}