using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EngLearning.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
        // mọi thứ xử lý sẽ nằm trong này
        //public ICommand LoadedWindowCommand { get; set; }
        //public bool Isloaded = false;
        //public ICommand LoadedWindowCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
       
        public MainViewModel()
        {

            CloseWindowCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => {
                FrameworkElement window = MainWindow.GetWindow(p);
                var w = window as Window;
                if (w != null)
                {
                    w.Close();
                }
            }
            );
        }
      
        
    }
}
