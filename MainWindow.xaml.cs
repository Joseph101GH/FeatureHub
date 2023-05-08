using System.Windows;
using System.Windows.Controls;

namespace FeatureHub
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Feature1Button_Click(object sender, RoutedEventArgs e)
        {
            LoadFeature1();
        }

        private void Feature2Button_Click(object sender, RoutedEventArgs e)
        {
            LoadFeature2();
        }

        private void Feature3Button_Click(object sender, RoutedEventArgs e)
        {
            LoadFeature3();
        }

        private void LoadFeature1()
        {
            var content = new Feature1Control();
            MainContent.Children.Clear();
            MainContent.Children.Add(content);
        }

        private void LoadFeature2()
        {
            var content = new Feature2Control();
            MainContent.Children.Clear();
            MainContent.Children.Add(content);
        }

        private void LoadFeature3()
        {
            var content = new Feature3Control();
            MainContent.Children.Clear();
            MainContent.Children.Add(content);
        }

        private void Feature4Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Feature5Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
