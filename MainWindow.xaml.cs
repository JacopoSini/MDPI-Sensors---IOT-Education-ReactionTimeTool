using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace MDPISensors
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool ISHourGlassSecondColorSet = false;    
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.Visibility = Visibility.Hidden;
            HourGlass.Visibility = Visibility.Visible;
            StartMainTimer();
        }

        System.Windows.Threading.DispatcherTimer MainTimer = new System.Windows.Threading.DispatcherTimer();
        private void StartMainTimer()
        {
            MainTimer.Tick += MainTimer_Tick;
            MainTimer.Interval = new TimeSpan(0, 0, 1);
            MainTimer.Start();
        }

        private void MainTimer_Tick(object? sender, EventArgs e)
        {
            ISHourGlassSecondColorSet = !(ISHourGlassSecondColorSet);
            if(ISHourGlassSecondColorSet)
            {
                HourGlassLine1.Stroke = new SolidColorBrush(Colors.LightGray);
                HourGlassLine2.Stroke = new SolidColorBrush(Colors.LightGray);
                HourGlassLine3.Stroke = new SolidColorBrush(Colors.LightGray);
                HourGlassLine4.Stroke = new SolidColorBrush(Colors.LightGray);
            }
            else
            {
                HourGlassLine1.Stroke = new SolidColorBrush(Colors.Black);
                HourGlassLine2.Stroke = new SolidColorBrush(Colors.Black);
                HourGlassLine3.Stroke = new SolidColorBrush(Colors.Black);
                HourGlassLine4.Stroke = new SolidColorBrush(Colors.Black);
            }
        }
    }
}
