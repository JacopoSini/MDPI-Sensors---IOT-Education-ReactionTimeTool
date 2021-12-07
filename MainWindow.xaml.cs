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
        /* Settings (the compiled version contains different values w.r.t. these ones */
        private uint[] _ElapsedSeconds = { 10*60u, 20*60u, 30*60u, 40*60u, 47*60u };

        private DateTime _MainTimerStartTime;
        private int _LastElapsedIndex = 0;
        private bool _ISHourGlassSecondColorSet = false;
        private System.IO.StreamWriter? _LogSW;
        public MainWindow()
        {
            InitializeComponent();
            _LogSW = null;
        }
        private void OpenLogFile()
        {
            bool FileOpened = false;
            try
            {
                Microsoft.Win32.SaveFileDialog SFD = new Microsoft.Win32.SaveFileDialog();
                SFD.Filter = "Log File|*.log";
                SFD.Title = "Choose where save the log file...";
                string LogFilePath;
                do
                {
                    bool? save = SFD.ShowDialog();
                    if (save == true)
                    {
                        LogFilePath = SFD.FileName;
                        _LogSW = new System.IO.StreamWriter(LogFilePath);
                        FileOpened = true;
                        _LogSW.Write("EventName, TimeStamp, Delta [ms]\n");
                        _LogSW.Write("FileChosen, " + DateTime.Now.ToUniversalTime().ToString() + " UTC, 0\n");
                    }
                    else
                    {
                        MessageBox.Show("Unable to save the log file.\nPlease chose another path.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); ;
                    }
                } while (!FileOpened);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to save the log file.\n Error: " + ex.ToString() + " \nPlease restart this software.","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                this.Close();
            }
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.Visibility = Visibility.Hidden;
            HourGlass.Visibility = Visibility.Visible;
            OpenLogFile();
            StartMainTimer();
        }
        System.Windows.Threading.DispatcherTimer MainTimer = new System.Windows.Threading.DispatcherTimer();
        private void StartMainTimer()
        {
            MainTimer.Tick += MainTimer_Tick;
            MainTimer.Interval = new TimeSpan(0, 0, 1);
            MainTimer.Start();
            _MainTimerStartTime = DateTime.Now;
            if (_LogSW != null)
            {
                _LogSW.Write("FileOpened, " + _MainTimerStartTime.ToUniversalTime().ToString() + " UTC, 0\n");
                _LogSW.FlushAsync();
            }
            else
            {
                //MessageBox.Show("Unable to save into the log file.\nFatal error.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); ;
                this.Close();
            }
        }
        private void MainTimer_Tick(object? sender, EventArgs e)
        {
            bool IndexFound = false;

            _ISHourGlassSecondColorSet = !(_ISHourGlassSecondColorSet);
            if(_ISHourGlassSecondColorSet)
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
            for (int i = _LastElapsedIndex; i < _ElapsedSeconds.Length && !IndexFound; i++)
            {
                uint ElapsedSecondsFromStart = (uint)Math.Floor((DateTime.Now - _MainTimerStartTime).TotalSeconds);
                if (ElapsedSecondsFromStart > _ElapsedSeconds[i])
                {
                    ShowMessageWindow();
                    _LastElapsedIndex = ++i;   
                    IndexFound = true;
                }
                else
                {
                    ;
                }
            }
        }
        private void ShowMessageWindow()
        {
            MessageWindow MW = new MessageWindow();
            MW.ShowDialog();
            if (_LogSW != null)
            {
                _LogSW.Write("MessageBoxOpening, " + MW.StartTime.ToUniversalTime().ToString() + " UTC, 0\n");
                _LogSW.Write("AnswerByTheUser:" + (MW.YesAnswer?"Yes":"No") + ", " + MW.AnswerTime.ToUniversalTime().ToString() + " UTC, "+ (MW.AnswerTime - MW.StartTime).TotalMilliseconds + "\n");
                _LogSW.FlushAsync();
            }
            else
            {
                //MessageBox.Show("Unable to save into the log file.\nFatal error.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); ;
                this.Close();
            }
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_LogSW != null)
            {
                _LogSW.Write("FileClosed, " + DateTime.Now.ToUniversalTime().ToString() + " UTC, 0\n");
                _LogSW.Flush();
                _LogSW.Close();
            }
            else
            {
                MessageBox.Show("Unable to save into the log file.\nFatal error.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); ;
            }
        }
    }
}

/* [EOF] */
