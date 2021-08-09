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

namespace ExamManager.ViewModel
{
    public class ListCandidateVM : BaseViewModel
    {
        #region initialize
        private ObservableCollection<User> _List;
        public ObservableCollection<User> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private User _SelectedUser;
        public User SelectedUser { get => _SelectedUser; set { _SelectedUser = value; OnPropertyChanged(); } }

        private string _Name;
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }

        private string _Gender;
        public string Gender { get => _Gender; set { _Gender = value; OnPropertyChanged(); } }

        private DateTime _BirthDay;
        public DateTime BirthDay { get => _BirthDay; set { _BirthDay = value; OnPropertyChanged(); } }

        private string _PhoneNumber;
        public string PhoneNumber { get => _PhoneNumber; set { _PhoneNumber = value; OnPropertyChanged(); } }

        private string _Job;
        public string Job { get => _Job; set { _Job = value; OnPropertyChanged(); } }

        private string _MoreInfo;
        public string MoreInfo { get => _MoreInfo; set { _MoreInfo = value; OnPropertyChanged(); } }

        private int? _Permission;
        public int? Permission { get => _Permission; set { _Permission = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _ListGender;
        public ObservableCollection<string> ListGender { get => _ListGender; set { _ListGender = value; OnPropertyChanged(); } }

        private Visibility _Visibility;
        public Visibility Visibility { get => _Visibility; set { _Visibility = value; OnPropertyChanged(); } }

        private Visibility _Visibility1;
        public Visibility Visibility1 { get => _Visibility1; set { _Visibility1 = value; OnPropertyChanged(); } }


        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }

        private string _PassWord;
        public string PassWord { get => _PassWord; set { _PassWord = value; OnPropertyChanged(); } }

        #endregion
        #region ICommand
        public ICommand LoadWindowCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand EditInfoCommand { get; set; }
        public ICommand WarningCommand { get; set; }
        public ICommand AddUserCommand { get; set; }
        public ICommand AddWarningCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }
        #endregion

        public ListCandidateVM() {
            LoadWindowCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                Visibility = Visibility.Collapsed;
                Visibility1 = Visibility.Collapsed;

                ResetList();

                SelectedUser = List.First();

                ListGender = new ObservableCollection<string>
                {
                    "Nam",
                    "Nữ",
                    "Khác",
                    ""
                };

                Name = SelectedUser.Name;
                Gender = SelectedUser.Gender;
                BirthDay = SelectedUser.BirthDay.Value;
                PhoneNumber = SelectedUser.PhoneNumber;
                Job = SelectedUser.Job;
                MoreInfo = SelectedUser.MoreInfo;
                Permission = SelectedUser.Permission;
                UserName = SelectedUser.UserName;
                PassWord = SelectedUser.PassWord;
            });

            SelectionChangedCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                if (SelectedUser == null) return;
                Visibility = Visibility.Collapsed;

                Name = SelectedUser.Name;
                Gender = SelectedUser.Gender;
                BirthDay = SelectedUser.BirthDay.Value;
                PhoneNumber = SelectedUser.PhoneNumber;
                Job = SelectedUser.Job;
                MoreInfo = SelectedUser.MoreInfo;
                Permission = SelectedUser.Permission;
                UserName = SelectedUser.UserName;
                PassWord = SelectedUser.PassWord;

            });

            EditInfoCommand = new RelayCommand<Object>((p) => {
                if (SelectedUser.Name == Name &&
                    SelectedUser.Gender == Gender &&
                    SelectedUser.BirthDay == BirthDay &&
                    SelectedUser.PhoneNumber == PhoneNumber &&
                    SelectedUser.Job == Job &&
                    SelectedUser.MoreInfo == MoreInfo &&
                    SelectedUser.UserName == UserName &&
                    SelectedUser.PassWord == PassWord ) return false;
                else return true;
            }, (p) => {
                if (SelectedUser == null) return;
                if(SelectedUser.Permission != Permission) MessageBox.Show("Không thể thay đổi quyền của người dùng");
                Permission = SelectedUser.Permission;
                Visibility = Visibility.Visible;

                SelectedUser.Name = Name;
                SelectedUser.Gender = Gender;
                SelectedUser.BirthDay = BirthDay;
                SelectedUser.PhoneNumber = PhoneNumber;
                SelectedUser.Job = Job;
                SelectedUser.MoreInfo = MoreInfo;
                SelectedUser.UserName = UserName;
                SelectedUser.PassWord = PassWord;
                DataProvider.Ins.DB.SaveChanges();
                ResetList();
            });

            WarningCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                Visibility = Visibility.Collapsed;

            });

            AddWarningCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                Visibility1 = Visibility.Collapsed;

            });

            AddUserCommand = new RelayCommand<Object>((p) =>
            {
                if (String.IsNullOrEmpty(UserName) || String.IsNullOrEmpty(PassWord))
                {
                    MessageBox.Show("Không để trống UserName và PassWord");
                    return false;
                }
                else if (DataProvider.Ins.DB.Users.Where(x => x.UserName == UserName).Count() > 0)
                {
                    MessageBox.Show("Tên người dùng đã được đăng ký, vui lòng chọn tên khác");
                    return false;
                }
                else return true;
            }, (p) => {
                MessageBoxResult a = MessageBox.Show("Bạn có muốn người dùng này quyền " + Permission + ", Lưu ý không thể sửa sau này", "Chú ý !", MessageBoxButton.OKCancel);
                if (a == MessageBoxResult.Cancel) return;

                DataProvider.Ins.DB.Users.Add(new User {UserName = UserName, PassWord = PassWord, Name = Name, PhoneNumber = PhoneNumber,
                    Gender = Gender, BirthDay = BirthDay, Job = Job, Permission = Permission, MoreInfo = MoreInfo
                });
                DataProvider.Ins.DB.SaveChanges();
                Visibility1 = Visibility.Visible;
                ResetList();
                SelectedUser = List.FirstOrDefault();
            });

            DeleteUserCommand = new RelayCommand<Object>((p) => {
                MessageBoxResult a = MessageBox.Show("Bạn có muốn tiếp tục","Thao tác sẽ xóa hết tất cả dữ liệu về tài khoản này",MessageBoxButton.OKCancel);
                if (a == MessageBoxResult.Cancel) return false;
                else return true;
            }, (p) => {
                if (SelectedUser == null) return;
                var u = DataProvider.Ins.DB.Users.Where(x=>x.UserName == UserName).FirstOrDefault();
                if(u.UserName == "admin")
                {
                    MessageBox.Show("Không thể xóa admin");
                    return;
                }

                var n =DataProvider.Ins.DB.UserExams.Where(x=>x.UserId == u.Id);
                foreach (var i in n) {
                    DataProvider.Ins.DB.UserExams.Remove(i);
                    DataProvider.Ins.DB.SaveChanges();
                }
                DataProvider.Ins.DB.Users.Remove(u);
                DataProvider.Ins.DB.SaveChanges();
                ResetList();
                SelectedUser = List.FirstOrDefault();
            });

        }

        void ResetList()
        {
            var n = DataProvider.Ins.DB.Users;
            List = new ObservableCollection<User>();
            foreach (User i in n)
            {
                if (i.Permission == 1) List.Add(i);
            }
            foreach (User i in n)
            {
                if (i.Permission == 2) List.Add(i);
            }
            foreach (User i in n)
            {
                if (i.Permission == 3) List.Add(i);
            }
        }
    }
}
