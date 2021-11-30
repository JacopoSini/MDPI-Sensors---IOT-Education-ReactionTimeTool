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
        private DateTime _AnswerTime;
        public bool YesAnswer
        {
            get { return _YesAnswer; }
        }

        public DateTime StartTime
        {
            get { return _StartTime; }
        }
        public DateTime AnswerTime
        {
            get { return _AnswerTime; } 
        }

        public MessageWindow()
        {
            InitializeComponent();
            this._StartTime = DateTime.Now;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            _AnswerTime = DateTime.Now;
            _YesAnswer = true;
            this.Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            _AnswerTime = DateTime.Now;
            this.Close();
        }
    }
}
