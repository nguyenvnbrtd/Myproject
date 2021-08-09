using ExamManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExamManager.ViewModel
{
    public class Result_Exam:BaseViewModel
    {
        #region initialize
        private string _ExamName;
        public string ExamName { get => _ExamName; set { _ExamName = value; OnPropertyChanged(); } }

        private string _Subject;
        public string Subject { get => _Subject; set { _Subject = value; OnPropertyChanged(); } }

        private string _Point;
        public string Point { get => _Point; set { _Point = value; OnPropertyChanged(); } }

        #endregion

    }
    public class ResultVM : BaseViewModel
    {
        #region initialize
        private ObservableCollection<Result_Exam> _List;
        public ObservableCollection<Result_Exam> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        #endregion

        #region ICommand
        public ICommand LoadWindowCommand { get; set; }
        #endregion

        public ResultVM(){
            LoadWindowCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {

                List = new ObservableCollection<Result_Exam>();

                List<UserExam> ue = new List<UserExam>(DataProvider.Ins.DB.UserExams.Where(x=>x.UserId == LoginWindowVM.idUser));

                foreach (UserExam i in ue ) {
                    List.Add(new Result_Exam() { ExamName = i.ExamInfo.QuizList.NameList, Subject = i.ExamInfo.QuizList.Subject.SubjectName, Point = String.Format("{0:0.#}" ,i.ExamPoint) });
                }
              
            });

        }
    }
}
