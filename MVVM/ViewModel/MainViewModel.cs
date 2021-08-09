using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace MVVM.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
        // mọi thứ xử lý sẽ nằm trong này
        public bool Isloaded = false;
        //public ICommand LoadedWindowCommand { get; set; }

        public ICommand Abc { get; set; }
        public ICommand Bcd { get; set; }
        public MainViewModel()
        {
            Abc = new RelayCommand<Rectangle>((p) => { return true; }, (p) => {
                
                if (p != null)
                {
                    
                        MessageBox.Show("Started");
                        p.Height = 500;
                        p.Width = 500;
                   
                }
                else {
                    MessageBox.Show("p=null");

                }

            }
            );

            Bcd = new RelayCommand<Button>((p) => { return true; }, (p) => {
                if (p != null)
                {
                    MessageBox.Show("button");
                    p.Height = 200;
                    p.Width = 300;
                }
                else { MessageBox.Show("cant"); }
            }
            );


        }
    }
}
