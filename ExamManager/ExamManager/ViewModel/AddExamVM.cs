using ExamManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ExamManager.ViewModel
{
    public class Ex : BaseViewModel
    {
        #region initialize 

        private string _ExamName;
        public string ExamName { get => _ExamName; set { _ExamName = value; OnPropertyChanged(); } }

        private DateTime _DayOpen;
        public DateTime DayOpen { get => _DayOpen; set { _DayOpen = value; OnPropertyChanged(); } }

        private DateTime _DayEnd;
        public DateTime DayEnd { get => _DayEnd; set { _DayEnd = value; OnPropertyChanged(); } }

        private string _Duration;
        public string Duration { get => _Duration; set { _Duration = value; OnPropertyChanged(); } }

        private string _Subject;
        public string Subject { get => _Subject; set { _Subject = value; OnPropertyChanged(); } }

        #endregion
    }
    public class AddExamVM : BaseViewModel
    {
        #region initialize 
        private ObservableCollection<Ex> _ExamList;
        public ObservableCollection<Ex> ExamList { get => _ExamList; set { _ExamList = value; OnPropertyChanged(); } }

        private Ex _SelectedExam;
        public Ex SelectedExam { get => _SelectedExam; set { _SelectedExam = value; OnPropertyChanged(); } }

        private ObservableCollection<QuizList> _List;
        public ObservableCollection<QuizList> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        public static bool isEdit;
        public static QuizList a;
        #endregion

        #region ICommand
        public ICommand LoadWindowCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion
        public AddExamVM() {
           
            LoadWindowCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                isEdit = false;

                UpdateList();

                if (ExamList.Count() <= 0) return;
                SelectedExam = ExamList.First();
            });

            AddCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                ExamInfo ex = new ExamInfo();
                
                ex.ShowDialog();
                if (ExamVM.IsBack == false)
                {
                    AddingWindow ad = new AddingWindow();
                    ad.ShowDialog();
                    if (AddingExamVM.isBack == true) return;

                    UpdateList();

                    if (ExamList.Count() <= 0) return;
                    SelectedExam = ExamList.First();

                }
                
            });

            EditCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                a = DataProvider.Ins.DB.QuizLists.Where(x=>x.NameList == SelectedExam.ExamName && x.Subject.SubjectName == SelectedExam.Subject).FirstOrDefault();
                isEdit = true;

                ExamInfo ex = new ExamInfo();
                ex.ShowDialog();
                if (ExamVM.IsBack == false)
                {
                    AddingWindow ad = new AddingWindow();
                    ad.ShowDialog();
                    if (AddingExamVM.isBack == true) return;

                    UpdateList();
                }
                isEdit = false;
            });

            DeleteCommand = new RelayCommand<Object>((p) => {
                MessageBoxResult r = MessageBox.Show("Bạn sẽ xóa toàn bộ các thôn tin liên quan đến bài thi này bao gồm các kết quả thi.", "Bạn có muốn tiếp tục thao tác ?", MessageBoxButton.OKCancel);
                if(r == MessageBoxResult.OK) return true;
                else return false;
            }, (p) => {
                var b = DataProvider.Ins.DB.QuizLists.Where(x => x.NameList == SelectedExam.ExamName && x.Subject.SubjectName == SelectedExam.Subject).FirstOrDefault();
                var c = DataProvider.Ins.DB.Quizs.Where(x=>x.QuizListId == b.Id).ToList();

                foreach(Quiz i in c)
                {
                    var answer = i.Answers.ToList();
                    DataProvider.Ins.DB.Answers.Remove(answer[0]);
                    DataProvider.Ins.DB.Answers.Remove(answer[1]);
                    DataProvider.Ins.DB.Answers.Remove(answer[2]);
                    DataProvider.Ins.DB.Answers.Remove(answer[3]);
                    
                }
                DataProvider.Ins.DB.SaveChanges();

                foreach (Quiz i in c)
                {
                    DataProvider.Ins.DB.Quizs.Remove(i);
                }
                DataProvider.Ins.DB.SaveChanges();

                var n = b.ExamInfoes.FirstOrDefault().UserExams.ToList();
                foreach(UserExam i in n)
                {
                    DataProvider.Ins.DB.UserExams.Remove(i);
                }
                DataProvider.Ins.DB.SaveChanges();

                DataProvider.Ins.DB.ExamInfoes.Remove(b.ExamInfoes.FirstOrDefault());
                DataProvider.Ins.DB.SaveChanges();

                DataProvider.Ins.DB.QuizLists.Remove(b);
                DataProvider.Ins.DB.SaveChanges();

                UpdateList();

                if (ExamList.Count() <= 0) return;
                SelectedExam = ExamList.First();
            });

        }

        void UpdateList() {
            List = new ObservableCollection<QuizList>(DataProvider.Ins.DB.QuizLists);
            ExamList = new ObservableCollection<Ex>();
            Stack<Ex> stack = new Stack<Ex>();

            foreach (QuizList i in List)
            {
                stack.Push(new Ex()
                {
                    Subject = DataProvider.Ins.DB.Subjects.Where(x => x.Id == i.SubjectsId).SingleOrDefault().SubjectName,
                    ExamName = i.NameList,
                    DayOpen = DataProvider.Ins.DB.ExamInfoes.Where(x => x.QuizListId == i.Id).SingleOrDefault().DayOpen.Value,
                    DayEnd = DataProvider.Ins.DB.ExamInfoes.Where(x => x.QuizListId == i.Id).SingleOrDefault().DayEnd.Value
                });

            }
            while (stack.Count > 0)
                ExamList.Add(stack.Pop());

        }
    }
}
