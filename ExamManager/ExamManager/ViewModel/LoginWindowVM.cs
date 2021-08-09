using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ExamManager.Model;
using ExamManager.ViewModel;

namespace ExamManager.ViewModel
{
    public class LoginWindowVM:BaseViewModel
    {
        #region initialize
        public static string UserLogin="";

        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }

        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        private Visibility _TbWarning;
        public Visibility TbWarning { get => _TbWarning; set { _TbWarning = value; OnPropertyChanged(); } }


        public static bool IsLogin { get; set; }
        public static int idUser;
        #endregion

        #region ICommand
        public ICommand CloseApplicationCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand LoadWarningCommand { get; set; }

        #endregion


        public LoginWindowVM()
        {
            TbWarning = Visibility.Collapsed;
            IsLogin = false;
            Password = "";
            UserName = "";

            CloseApplicationCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                if(p!= null)
                {
                    IsLogin = false;
                    p.Close();
                }
            });

            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                if (p != null)
                {

                    Login(p);
                   
                }
            });

            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => {
                if (p != null)
                { Password = p.Password; }
            });

            LoadWarningCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                TbWarning = Visibility.Collapsed;
            });
        }

        private void Login(Window p)
        {
            var accCount = DataProvider.Ins.DB.Users.Where(x => x.UserName == UserName && x.PassWord == Password).SingleOrDefault();
            
            if (accCount != null)
            {
                IsLogin = true;
                UserLogin = UserName;
                idUser = accCount.Id;
                p.Close();
            }
            else
            {
                IsLogin = false;
                TbWarning = Visibility.Visible;
            }
         
        }
    }
}
