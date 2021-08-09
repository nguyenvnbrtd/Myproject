
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
    public class HomeVM : BaseViewModel
    {
        #region initialize TextBlock
        private string _Ex1;
        public string Ex1 { get => _Ex1; set { _Ex1 = value; OnPropertyChanged(); } }

        private string _Ex2;
        public string Ex2 { get => _Ex2; set { _Ex2 = value; OnPropertyChanged(); } }

        private string _Ex3;
        public string Ex3 { get => _Ex3; set { _Ex3 = value; OnPropertyChanged(); } }

        private string _Ex4;
        public string Ex4 { get => _Ex4; set { _Ex4 = value; OnPropertyChanged(); } }

        private string _Ex5;
        public string Ex5 { get => _Ex5; set { _Ex5 = value; OnPropertyChanged(); } }

        private string _Ex6;
        public string Ex6 { get => _Ex6; set { _Ex6 = value; OnPropertyChanged(); } }

        private ObservableCollection<QuizList> a;
        #endregion

        #region public static
        public static QuizList exam_info;
        public static bool isEntry;
        #endregion

        #region ICommand
        public ICommand LoadWindowCommand { get; set; }
        public ICommand OpenExamCommand { get; set; }
     
        #endregion
        public HomeVM() {
            LoadWindowCommand = new RelayCommand<Grid>((p) => { return true; }, (p) => {

                if (p == null) return;

                foreach (FrameworkElement i in p.Children)
                {
                    if (i.Name == "gr1" && String.IsNullOrEmpty(Ex1)) i.Visibility = Visibility.Visible;
                    if (i.Name == "gr2" && String.IsNullOrEmpty(Ex2)) i.Visibility = Visibility.Visible;
                    if (i.Name == "gr3" && String.IsNullOrEmpty(Ex3)) i.Visibility = Visibility.Visible;
                    if (i.Name == "gr4" && String.IsNullOrEmpty(Ex4)) i.Visibility = Visibility.Visible;
                    if (i.Name == "gr5" && String.IsNullOrEmpty(Ex5)) i.Visibility = Visibility.Visible;
                    if (i.Name == "gr6" && String.IsNullOrEmpty(Ex5)) i.Visibility = Visibility.Collapsed;
                }
                var b = DataProvider.Ins.DB.QuizLists.Where(x => x.ExamInfoes.FirstOrDefault().DayOpen <= DateTime.Now && x.ExamInfoes.FirstOrDefault().DayEnd.Value > DateTime.Now);
                a = new ObservableCollection<QuizList>( b);
                Ex6 = "Tất cả";
                try {
                    int i = a.Count();
                    Ex1 = i != 0 ? a[i - 1].NameList : "";
                    Ex2 = i > 1 ? a[i - 2].NameList : "";
                    Ex3 = i > 2 ? a[i - 3].NameList : "";
                    Ex4 = i > 3 ? a[i - 4].NameList : "";
                    Ex5 = i > 4 ? a[i - 5].NameList : "";
                }
                catch { }

                foreach (FrameworkElement i in p.Children)
                {
                    if (i.Name == "gr1" && String.IsNullOrEmpty(Ex1)) Ex1 = "Không có bài thi nào";
                    if (i.Name == "gr2" && String.IsNullOrEmpty(Ex2)) i.Visibility = Visibility.Collapsed;
                    if (i.Name == "gr3" && String.IsNullOrEmpty(Ex3)) i.Visibility = Visibility.Collapsed;
                    if (i.Name == "gr4" && String.IsNullOrEmpty(Ex4)) i.Visibility = Visibility.Collapsed;
                    if (i.Name == "gr5" && String.IsNullOrEmpty(Ex5)) i.Visibility = Visibility.Collapsed;
                    if (i.Name == "gr6" && a.Count()>=6) i.Visibility = Visibility.Visible;
                }

            });

            OpenExamCommand = new RelayCommand<Grid>((p) => { return true; }, (p) => {
                if (p == null) return;
               
                switch (p.Name) {
                    case "gr1":
                        if (Ex1 == "Không có bài thi nào") return;
                        exam_info = a[a.Count() - 1];
                        break;
                    case "gr2":
                        exam_info = a[a.Count() - 2];
                        break;
                    case "gr3":
                        exam_info = a[a.Count() - 3];
                        break;
                    case "gr4":
                        exam_info = a[a.Count() - 4];
                        break;
                    case "gr5":
                        exam_info = a[a.Count() - 5];
                        break;
                    case "gr6":
                        if (Ex6 == "Tất cả") { AllExamWindow all = new AllExamWindow(); all.ShowDialog(); return; }
                        exam_info = a[a.Count() - 6];
                        break;
                }

                isEntry = true;
                ExamInfo ex = new ExamInfo();
                ex.ShowDialog();
                isEntry = false;
            });

        }
    }
}
