using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace FeatureHub
{
    public partial class Feature2Control : UserControl
    {
        private readonly DispatcherTimer _timer;
        private TimeZoneInfo _selectedTimeZone;

        public Feature2Control()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();

            _selectedTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"); // Default timezone to Brussels
            TimeZoneComboBox.SelectedIndex = 0; // No timezone selected by default
            DisplayTime();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DisplayTime();
        }

        private void DisplayTime()
        {
            DateTime currentTime = DateTime.UtcNow;
            if (_selectedTimeZone != null)
            {
                currentTime = TimeZoneInfo.ConvertTimeFromUtc(currentTime, _selectedTimeZone);
            }
            TimeDisplay.Text = currentTime.ToString("HH:mm:ss");
        }

        private void TimeZoneComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TimeZoneComboBox.SelectedIndex == 0) // Brussels
            {
                _selectedTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"); // Default timezone to Brussels
            }
            else if (TimeZoneComboBox.SelectedIndex == 1) // UTC
            {
                _selectedTimeZone = TimeZoneInfo.FindSystemTimeZoneById("UTC");
            }
            else if (TimeZoneComboBox.SelectedIndex == 2) // CEST
            {
                _selectedTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            }
        }
    }
}
