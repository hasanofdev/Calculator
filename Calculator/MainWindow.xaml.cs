using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private double _result;

        private void ButtonNumber_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (InputTxt.Text == "0")
                    InputTxt.Text = string.Empty;

                InputTxt.Text += btn.Content.ToString();
            }
        }

        private void ButtonSymbol_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(InputTxt.Text))
                return;

            if (sender is Button btn)
            {
                if (btn.Content.ToString() == "C" || btn.Content.ToString() == "CE")
                {
                    InputTxt.Text = "0";
                    return;
                }

                if (char.IsDigit(InputTxt.Text[InputTxt.Text.Length - 1]) || InputTxt.Text[InputTxt.Text.Length - 1] == '.' && btn.Content.ToString() != "." || InputTxt.Text[InputTxt.Text.Length - 1] == ',' && btn.Content.ToString() != ".")
                    InputTxt.Text += btn.Content.ToString();
            }
        }

        private void ButtonResult_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(InputTxt.Text))
                return;

            if (!char.IsDigit(InputTxt.Text[InputTxt.Text.Length - 1]) && InputTxt.Text[InputTxt.Text.Length - 1] != '.')
                return;

            CalculateResult();
            InputTxt.Text = _result.ToString();
        }

        private void ButtonOperation_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(InputTxt.Text))
                return;

            if (sender is Button btn)
            {
                if (InputTxt.Text.Contains('+') || InputTxt.Text.Contains('-') || InputTxt.Text.Contains('*') || InputTxt.Text.Contains('/'))
                    CalculateResult();

                if (_result == 0)
                {
                    if (!double.TryParse(InputTxt.Text, out _result))
                        return;
                }

                _result = btn.Content.ToString() switch
                {
                    "%" => _result / 100,
                    "+/-" => _result * -1,
                    "1/x" => 1 / _result,
                    "x²" => Math.Pow(_result, 2),
                    "√x" => Math.Sqrt(_result),
                    _ => _result
                };

                if (_result.ToString() == "∞")
                {
                    MessageBox.Show("Cannot Divide by zero");
                    InputTxt.Text = "0";
                    return;
                }

                InputTxt.Text = _result.ToString();
            }
        }


        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(InputTxt.Text))
                return;

            InputTxt.Text = InputTxt.Text.Remove(InputTxt.Text.Length - 1);
        }

        private void ButtonUnidentified_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Unidentified");

        private void CalculateResult()
        {
            string formattedCalculation = InputTxt.Text;
            try
            {
                _result = double.Parse(new DataTable().Compute(formattedCalculation, null).ToString()!);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                _result = 0;
            }
        }
    }
}
