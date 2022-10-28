using System;
using System.Collections.Generic;
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
        public string doublePattern = @"^[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)$";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InputTxt.Text += (sender as Button).Content.ToString();
        }

        private void InputTxt_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextBox? txt = sender as TextBox;
            if (!Regex.IsMatch(txt!.Text, doublePattern))
                txt.Text = txt.Text.Trim(txt.Text[txt.Text.Length - 1]);
        }
    }
}
