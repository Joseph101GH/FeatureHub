using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FeatureHub
{
    public partial class Feature1Control : UserControl
    {
        public Feature1Control()
        {
            InitializeComponent();
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsGuid(InputTextBox.Text))
            {
                this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#059547"));
            }
            else
            {
                this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e0443c"));
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            InputTextBox.Clear();
            this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fdfcfa"));
        }

        private bool IsGuid(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            Regex guidPattern = new Regex(@"^(\{0[xX][0-9a-fA-F]{8}\,(\s)?[xX][0-9a-fA-F]{4}\,(\s)?[xX][0-9a-fA-F]{4}\,(\s)?\{[0-9a-fA-F]{2}(\s)?,[0-9a-fA-F]{2}(\s)?,[0-9a-fA-F]{2}(\s)?,[0-9a-fA-F]{2}(\s)?,[0-9a-fA-F]{2}(\s)?,[0-9a-fA-F]{2}(\s)?,[0-9a-fA-F]{2}(\s)?,[0-9a-fA-F]{2}(\s)?\}\})$|^(?i)[0-9a-fA-F]{8}[-][0-9a-fA-F]{4}[-][0-9a-fA-F]{4}[-][0-9a-fA-F]{4}[-][0-9a-fA-F]{12}$");
            return guidPattern.IsMatch(input);
        }
    }
}
