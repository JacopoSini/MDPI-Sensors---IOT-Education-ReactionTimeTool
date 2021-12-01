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
using System.Windows.Shapes;

namespace MDPISensors
{
    /// <summary>
    /// Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        private DateTime _StartTime;
        private bool _YesAnswer = false;
        private bool _UnlockClosing = false;
        private DateTime _AnswerTime;
        public bool YesAnswer
        {
            get
            {
                return _YesAnswer;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return _StartTime;
            }
        }
        public DateTime AnswerTime
        {
            get
            {
                return _AnswerTime;
            }
        }
        public MessageWindow()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            this.Closing += new System.ComponentModel.CancelEventHandler(MessageWindow_Closing);
            System.Media.SoundPlayer soundPlayer = new System.Media.SoundPlayer("Belligerent.wav");
            soundPlayer.Play(); // Play is run in a separate thread
            InitializeComponent();

            /* tested also with different scaling factors: PrimaryScreenHeight and Width scales subsequentially */
            this.Top = rand.NextDouble() * (System.Windows.SystemParameters.PrimaryScreenHeight - this.Height);
            this.Left = rand.NextDouble() * (System.Windows.SystemParameters.PrimaryScreenWidth - this.Width);

            this._StartTime = DateTime.Now;
            this.Topmost = true;
            StartTopWindowTimer();
        }
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            _AnswerTime = DateTime.Now;
            _YesAnswer = true;
            _UnlockClosing = true;
            this.Close();
        }
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            _AnswerTime = DateTime.Now;
            _UnlockClosing = true;
            this.Close();
        }

        void MessageWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_UnlockClosing)
            {
                e.Cancel = true;
            }
            else
            {
                ;
            }
        }


        System.Windows.Threading.DispatcherTimer TopWindowTimer = new System.Windows.Threading.DispatcherTimer();
        private void StartTopWindowTimer()
        {
            TopWindowTimer.Tick += MainTimer_Tick;
            TopWindowTimer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            TopWindowTimer.Start();
        }
        private void MainTimer_Tick(object? sender, EventArgs e)
        {
            this.Topmost = true;
        }
    }
}

/* [EOF] */
