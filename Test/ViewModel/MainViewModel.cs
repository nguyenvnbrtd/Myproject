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

namespace Test.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
        #region initialize

        private bool _IsCheck;
        public bool isCheck { get => _IsCheck; set { _IsCheck = value; OnPropertyChanged(); } }

        private int  _Timer;
        public int Timer { get => _Timer; set { _Timer = value; OnPropertyChanged(); } }

        bool pause = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand LoadedStackCommand { get; set; }
        public ICommand ClickCommand { get; set; }
        public ICommand ResetCommand { get; set; }


        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
       
        #endregion

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Timer++;
        }
        public MainViewModel(){
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                Timer = 0;
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0,0,0,0,1);
                p.Show();
          
            }
            );

            ClickCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
               
                if (!pause)
                {
                    dispatcherTimer.Start();
                    pause = true;
                }
                else
                {
                    dispatcherTimer.Stop();
                    pause = false;
                }

            });

            ResetCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                Timer = 0;
                dispatcherTimer.Stop();
                pause = false;
            }
            );
            LoadedStackCommand = new RelayCommand<WrapPanel>((p) => { return true; }, (p) => {
                Button bt = new Button();
                Button bt1 = new Button();
                TextBlock t = new TextBlock();
                
                if (p == null) return;
                p.Children.Add(bt);
                p.Children.Add(bt1);
                p.Children.Add(t);

                int innnn = 0;
                foreach (var i in p.Children) {
                    if(i.GetType() == new Button().GetType())
                    innnn++;
                }
                MessageBox.Show(""+innnn);



            }
            );
        }
        

    }
}
