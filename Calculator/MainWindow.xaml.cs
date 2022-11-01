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
        public string operation = "";
        public string doublePattern = @"^[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)$";
        public MainWindow()
        {
            InitializeComponent();
        }

        public double Answer { get; private set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button? btn = sender as Button;
            if (InputTxt.Text.StartsWith('0') && InputTxt.Text.Length == 1)
            {
                if (int.TryParse(btn!.Content.ToString(), out int result))
                {
                    InputTxt.Text = result.ToString();
                    return;
                }
            }

            InputTxt!.Text += btn!.Content.ToString();
        }

        private void InputTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox? txt = sender as TextBox;
            if (!Regex.IsMatch(txt!.Text, doublePattern) && txt.Text.Length > 0)
                txt.Text = txt.Text.Trim(txt.Text[txt.Text.Length - 1]);
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            Button? btn = sender as Button;

            if (!Regex.IsMatch(InputTxt.Text, "[0-9]\\d+\\s[+|\\-|\\*|/]"))
                operation = InputTxt.Text + " " + (string)btn!.Tag + " ";
            else operation += InputTxt.Text + " " + (string)btn!.Tag;

            OperationTxt.Text = operation;
        }

        private void TogetherBtn_Click(object sender, RoutedEventArgs e)
        {
            operation += InputTxt.Text;

            if (!Regex.IsMatch(operation, "[0-9]\\d+\\s[+|\\-|\\*|/]\\s\\d+\\s\\=\\s\\d+"))
                Answer = double.Parse(new DataTable().Compute(operation, null).ToString()!);
            else Answer = double.Parse(new DataTable().Compute(operation.Split(' ')[3], null).ToString()!);
                


            OperationTxt.Text = operation + (sender as Button)!.Tag.ToString();
            InputTxt.Text = Answer.ToString();
        }
    }
}
