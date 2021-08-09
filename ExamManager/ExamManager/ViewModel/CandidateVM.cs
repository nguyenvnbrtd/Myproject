using ExamManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ExamManager.ViewModel
{


    public class CandidateVM : BaseViewModel
    {
        #region initialize
        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }

        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        private string _ConfirmPassword;
        public string ConfirmPassword { get => _ConfirmPassword; set { _ConfirmPassword = value; OnPropertyChanged(); } }

        private bool _IsCheck;
        public bool IsCheck { get => _IsCheck; set { _IsCheck = value; OnPropertyChanged(); } }

        public static bool IsLogOut = false;

        private string _Name;
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }

        private string _Gender;
        public string Gender { get => _Gender; set { _Gender = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _ListGender;
        public ObservableCollection<string> ListGender { get => _ListGender; set { _ListGender = value; OnPropertyChanged(); } }

        private DateTime _BirthDay;
        public DateTime BirthDay { get => _BirthDay; set { _BirthDay = value; OnPropertyChanged(); } }

        private string _PhoneNumber;
        public string PhoneNumber { get => _PhoneNumber; set { _PhoneNumber = value; OnPropertyChanged(); } }

        private string _Job;
        public string Job { get => _Job; set { _Job = value; OnPropertyChanged(); } }

        private string _MoreInfo;
        public string MoreInfo { get => _MoreInfo; set { _MoreInfo = value; OnPropertyChanged(); } }

        #region Textbox Warning Password
        private TextBlock _TbWarning;
        public TextBlock TbWarning { get => _TbWarning; set { _TbWarning = value; OnPropertyChanged(); } }

        private TextBlock _TbWarning1;
        public TextBlock TbWarning1 { get => _TbWarning1; set { _TbWarning1 = value; OnPropertyChanged(); } }
        #endregion

        #endregion

        #region ICommand
        public ICommand LoadWarningCommand { get; set; }
        public ICommand LoadWarningTextBlockCommand { get; set; }
        public ICommand LoadWarningTextBlock1Command { get; set; }
        public ICommand LoadWindowCommand { get; set; }
        public ICommand LoadPasswordCommand { get; set; }
        public ICommand LoadPasswordConfirmCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand PasswordChangeCommand { get; set; }
        public ICommand TouchEnterCommand { get; set; }
        public ICommand TouchEnterCommand1 { get; set; }
        public ICommand TouchEnterCommand2 { get; set; }
        public ICommand TouchEnterCommand3 { get; set; }
        public ICommand ChangeInfoCommand { get; set; }
        #endregion
        public CandidateVM() {

            LoadWindowCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                UserName = LoginWindowVM.UserLogin;
                var accCount = DataProvider.Ins.DB.Users.Where(x => x.UserName == UserName).SingleOrDefault();
                if (accCount == null) return;
                ConfirmPassword = "";
                Password = "";

                ListGender = new ObservableCollection<string>
                {
                    "Nam",
                    "Nữ",
                    "Khác",
                    ""
                };

                Name = accCount.Name;
                Gender = accCount.Gender;
                BirthDay = accCount.BirthDay.Value;
                PhoneNumber = accCount.PhoneNumber;
                Job = accCount.Job;
                MoreInfo = accCount.MoreInfo;

            });

            LoadPasswordCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => {
                if (p != null)
                {
                    Password = p.Password  ;
                }
            });

            LoadPasswordConfirmCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => {
                if (p != null)
                {
                    ConfirmPassword = p.Password;
                }
            });

            LoadWarningTextBlockCommand = new RelayCommand<TextBlock>((p) => { return true; }, (p) => {
                if (p != null)
                {
                    TbWarning = p;
                }
            });

            LoadWarningTextBlock1Command = new RelayCommand<TextBlock>((p) => { return true; }, (p) => {
                if (p != null)
                {
                    TbWarning1 = p;
                }
            });

            PasswordChangeCommand = new RelayCommand<Object>((p) => {
                if(String.IsNullOrEmpty(Password)) return false;
                return true;

            }, (p) => {
                
                if (Password == ConfirmPassword)
                {
                    var accCount = DataProvider.Ins.DB.Users.Where(x => x.UserName == UserName).SingleOrDefault();
                    
                    try {
                        accCount.PassWord = Password; DataProvider.Ins.DB.SaveChanges();
                        TbWarning.Foreground = Brushes.Green;
                        TbWarning.Text = "Đã đổi mật khẩu";

                    } catch {
                        TbWarning.Foreground = Brushes.Red;
                        TbWarning.Text = "Không thể đổi mật khẩu";
                    }
                    
                }
                else
                {
                    TbWarning.Foreground = Brushes.Red;
                    TbWarning.Text = "Kiểm tra lại mật khẩu";
                }
                TbWarning.Visibility = Visibility.Visible;
            });

            ChangeInfoCommand = new RelayCommand<Object>((p) => {
                var accCount = DataProvider.Ins.DB.Users.Where(x => x.UserName == UserName).SingleOrDefault();

                if (accCount.Name == Name &&
                    accCount.Gender == Gender &&
                    accCount.BirthDay == BirthDay &&
                    accCount.PhoneNumber == PhoneNumber &&
                    accCount.Job == Job &&
                    accCount.MoreInfo == MoreInfo) return false;
                else return true;
            }, (p) => {

                var accCount = DataProvider.Ins.DB.Users.Where(x => x.UserName == UserName).SingleOrDefault();

                if (accCount == null)
                {
                    TbWarning1.Text = "Lỗi";
                    TbWarning1.Foreground = Brushes.Red;
                    TbWarning1.Visibility = Visibility.Visible;
                    return;
                }

                accCount.Name = Name;
                accCount.Gender = Gender;
                accCount.BirthDay = BirthDay;
                accCount.PhoneNumber = PhoneNumber;
                accCount.Job = Job;
                accCount.MoreInfo = MoreInfo;
                DataProvider.Ins.DB.SaveChanges();

                TbWarning1.Text = "Đổi thông tin thành công";
                TbWarning1.Foreground = Brushes.Green;
                TbWarning1.Visibility = Visibility.Visible;


            });

            TouchEnterCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => {
                if (p != null)
                {
                    if (IsCheck == true) p.Visibility = Visibility.Collapsed;
                    else p.Visibility = Visibility.Visible;
                }
            });

            TouchEnterCommand1 = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => {
                if (p != null)
                {
                    if (IsCheck == true) p.Visibility = Visibility.Collapsed;
                    else p.Visibility = Visibility.Visible;
                }
            });

            TouchEnterCommand2 = new RelayCommand<TextBox>((p) => { return true; }, (p) => {
                if (p != null)
                {
                    if (IsCheck == true) p.Visibility = Visibility.Visible;
                    else p.Visibility = Visibility.Collapsed;
                }
            });

            TouchEnterCommand3 = new RelayCommand<TextBox>((p) => { return true; }, (p) => {
                if (p != null)
                {
                    if (IsCheck == true) p.Visibility = Visibility.Visible;
                    else p.Visibility = Visibility.Collapsed;
                }
            });

            LogOutCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                MainWindow m = new MainWindow();
                MainWindowVM.MainWin.Close();
                m.ShowDialog();
                
            });

            LoadWarningCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                TbWarning.Visibility = Visibility.Collapsed;
                TbWarning1.Visibility = Visibility.Collapsed;
            });
        }
    }
}
