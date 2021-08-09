using ExamManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ExamManager.ViewModel
{
    public class AddSubjectVM:BaseViewModel
    {
        #region initialize
        private string _Subject;
        public string Subject { get => _Subject; set { _Subject = value; OnPropertyChanged(); } }

        private string _Descriptions;
        public string Descriptions { get => _Descriptions; set { _Descriptions = value; OnPropertyChanged(); } }

        public static bool change;
        #endregion

        #region ICommand
        public ICommand CloseWindowCommand { get; set; }
        public ICommand AddSubjectCommand { get; set; }
        public ICommand LoadWindowCommand { get; set; }
        #endregion

        public AddSubjectVM() {

            LoadWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                if (SubjectVM.isEdit)
                {
                    Descriptions = SubjectVM.Subject.Descriptions;
                    Subject = SubjectVM.Subject.SubjectName;
                }
                else
                {
                    Descriptions = "Không có mô tả";
                    Subject = "";
                }
                change = true;
            });

            CloseWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                if (p == null) return;
                change = false;
                p.Close();
            });

            AddSubjectCommand = new RelayCommand<Window>((p) => {
                if (String.IsNullOrEmpty(Subject)) return false;
                else return true;
            }, 
            (p) => {
                if (p == null) return;
                if (SubjectVM.isEdit)
                {
                    SubjectVM.Subject.SubjectName = Subject;
                    SubjectVM.Subject.Descriptions = Descriptions;
                }
                else
                {
                    Model.Subject a = new Model.Subject() { SubjectName = Subject, Descriptions = Descriptions };
                    DataProvider.Ins.DB.Subjects.Add(a);
                }
                DataProvider.Ins.DB.SaveChanges();

                p.Close();
            });
        }
    }
}
