using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExamManager.ViewModel
{


    public class OtherControlVM : BaseViewModel
    {
        #region ICommand
        public ICommand ContactTwitter { get; set; }
        public ICommand ContactFacebook { get; set; }
        public ICommand ContactInstagram { get; set; }

        #endregion

        public OtherControlVM(){
            ContactTwitter = new RelayCommand<Object>((p) => { return true; }, (p) => {
                Contact("https://twitter.com/NguynHu99191053");
            });

            ContactFacebook = new RelayCommand<Object>((p) => { return true; }, (p) => {
                Contact("https://www.facebook.com/nguyen.leanddd");
            });

            ContactInstagram = new RelayCommand<Object>((p) => { return true; }, (p) => {
                Contact("https://www.instagram.com/leanddd3");
            });

        }

        void Contact(string path) {
            try{ System.Diagnostics.Process.Start(path); }
            catch { }
        }
    }
}
