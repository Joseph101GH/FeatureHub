using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace FeatureHub
{
    public partial class Feature3Control : UserControl
    {
        private readonly ObservableCollection<TimerItem> _timerItems;
        private readonly DispatcherTimer _timer;

        public Feature3Control()
        {
            InitializeComponent();
            _timerItems = new ObservableCollection<TimerItem>();
            TimersListView.ItemsSource = _timerItems;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(60);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (var timerItem in _timerItems)
            {
                UpdateEndTimeAndDuration(timerItem);
            }
            TimersListView.Items.Refresh();
        }


        private void StartTimeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            var timerItem = ((FrameworkElement)sender).DataContext as TimerItem;
            if (timerItem != null)
            {
                timerItem.StartTime = textBox.Text;
                UpdateEndTimeAndDuration(timerItem);
            }
        }

        private void EndTimeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            TimerItem timerItem = ((FrameworkElement)sender).DataContext as TimerItem;
            if (timerItem != null && !timerItem.IsActive)
            {
                timerItem.EndTime = textBox.Text;
                UpdateEndTimeAndDuration(timerItem);
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            TimerItem newItem = new TimerItem
            {
                Description = DescriptionTextBox.Text,
                StartTime = DateTime.Now.ToString("HH:mm"),
                Duration = "00:00",
                IsActive = true // Add this line to set IsActive to true when a new timer is started
            };
            _timerItems.Add(newItem);
            DescriptionTextBox.Clear();
            DescriptionTextBox.Focus(); // Set focus to the TextBox after the Start button is clicked
        }


        private void DescriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            StartButton.IsEnabled = !string.IsNullOrEmpty(DescriptionTextBox.Text);
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).Tag is TimerItem timerItem && timerItem.IsActive)
            {
                timerItem.IsActive = false;
                UpdateEndTimeAndDuration(timerItem);

                // Calculate total duration of all stopped timers
                var totalDuration = _timerItems.Where(ti => !ti.IsActive).Sum(ti => ti.TotalMinutes);
                TotalTimeTextBlock.Text = $"Total: {TimeSpan.FromMinutes(totalDuration):hh\\:mm}";
            }
        }



        private TimeSpan RoundUpToNearest(TimeSpan value, TimeSpan roundingInterval)
        {
            var ticks = (value.Ticks + roundingInterval.Ticks - 1) / roundingInterval.Ticks;
            return new TimeSpan(ticks * roundingInterval.Ticks);
        }


        private void UpdateEndTimeAndDuration(TimerItem item)
        {
            if (item.IsActive)
            {
                item.EndTime = DateTime.Now.ToString("HH:mm");
                DateTime startTime = DateTime.ParseExact(item.StartTime, "HH:mm", CultureInfo.InvariantCulture);
                DateTime endTime = DateTime.ParseExact(item.EndTime, "HH:mm", CultureInfo.InvariantCulture);
                TimeSpan duration = endTime - startTime;
                TimeSpan roundedDuration = RoundUpToNearest(duration, TimeSpan.FromMinutes(15));
                item.Duration = $"{roundedDuration:h\\:mm}";
                item.TotalMinutes = (int)roundedDuration.TotalMinutes;
            }
        }


    }
}
