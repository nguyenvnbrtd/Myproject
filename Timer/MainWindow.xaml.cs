using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace Timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        #region innitialize
        private int _Mil;
        public int Mil { get => _Mil; set { _Mil = value; OnPropertyChanged(); } }

        private int _Sec;
        public int Sec { get => _Sec; set { _Sec = value; OnPropertyChanged(); } }


        readonly DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);

        }

        #region play
        private void PlayIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Play();  
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Play();
        } 
        void Play() {
            if (PlayIcon.Visibility == Visibility.Visible)
            {
                dispatcherTimer.Start();
                PlayIcon.Visibility = Visibility.Hidden;
                PauseIcon.Visibility = Visibility.Visible;
            }
            else
            {
                dispatcherTimer.Stop();
                PauseIcon.Visibility = Visibility.Hidden;
                PlayIcon.Visibility = Visibility.Visible;
            }

        }
        #endregion
         
        #region stop
        private void StopIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Stop();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Stop();
        }
        void Stop() {
            dispatcherTimer.Stop();
            TimerBar.Value = 0;
            Mil = 0;
            Sec = 0;
            if (PlayIcon.Visibility == Visibility.Visible) return;
            PauseIcon.Visibility = Visibility.Hidden;
            PlayIcon.Visibility = Visibility.Visible;
        }
        #endregion

        #region close
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PackIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
           
            TimerBar.Value+=1;
            Mil++;
            if (Mil%100 == 0) {
                Sec++;
                Mil = 0;
            }
            if (TimerBar.Value == 10000)
            {
                MessageBox.Show("Time up !");
                dispatcherTimer.Stop();
                Sec = 0;
                TimerBar.Value = 0;
                PauseIcon.Visibility = Visibility.Hidden;
                PlayIcon.Visibility = Visibility.Visible;
            }

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWD.DragMove();
        }
    }
}
