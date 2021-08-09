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
    public class Question : BaseViewModel
    {
        #region initialize
        private string _Question;
        public string question { get => _Question; set { _Question = value; OnPropertyChanged(); } }

        private string _Answer1;
        public string Answer1 { get => _Answer1; set { _Answer1 = value; OnPropertyChanged(); } }

        private string _Answer2;
        public string Answer2 { get => _Answer2; set { _Answer2 = value; OnPropertyChanged(); } }

        private string _Answer3;
        public string Answer3 { get => _Answer3; set { _Answer3 = value; OnPropertyChanged(); } }

        private string _Answer4;
        public string Answer4 { get => _Answer4; set { _Answer4 = value; OnPropertyChanged(); } }

        private bool _CbAnswer1;
        public bool CbAnswer1 { get => _CbAnswer1; set { _CbAnswer1 = value; OnPropertyChanged(); } }

        private bool _CbAnswer2;
        public bool CbAnswer2 { get => _CbAnswer2; set { _CbAnswer2 = value; OnPropertyChanged(); } }

        private bool _CbAnswer3;
        public bool CbAnswer3 { get => _CbAnswer3; set { _CbAnswer3 = value; OnPropertyChanged(); } }

        private bool _CbAnswer4;
        public bool CbAnswer4 { get => _CbAnswer4; set { _CbAnswer4 = value; OnPropertyChanged(); } }

        #endregion
    }
    public class AddingExamVM : BaseViewModel
    {
        #region initialize header
        private string _ExamName;
        public string ExamName { get => _ExamName; set { _ExamName = value; OnPropertyChanged(); } }

        private DateTime _DayOpen;
        public DateTime DayOpen { get => _DayOpen; set { _DayOpen = value; OnPropertyChanged(); } }

        private DateTime _DayEnd;
        public DateTime DayEnd { get => _DayEnd; set { _DayEnd = value; OnPropertyChanged(); } }

        private string _Duration;
        public string Duration { get => _Duration; set { _Duration = value; OnPropertyChanged(); } }

        private Subject _Subject;
        public Subject Subject { get => _Subject; set { _Subject = value; OnPropertyChanged(); } }

        private ObservableCollection<Subject> _SubjectList;
        public ObservableCollection<Subject> SubjectList { get => _SubjectList; set { _SubjectList = value; OnPropertyChanged(); } }

      
        #endregion

        #region initialize list quiz

        private string _Question;
        public string Question { get => _Question; set { _Question = value; OnPropertyChanged(); } }

        private string _Answer1;
        public string Answer1 { get => _Answer1; set { _Answer1 = value; OnPropertyChanged(); } }

        private string _Answer2;
        public string Answer2 { get => _Answer2; set { _Answer2 = value; OnPropertyChanged(); } }

        private string _Answer3;
        public string Answer3 { get => _Answer3; set { _Answer3 = value; OnPropertyChanged(); } }

        private string _Answer4;
        public string Answer4 { get => _Answer4; set { _Answer4 = value; OnPropertyChanged(); } }

        private bool _CbAnswer1;
        public bool CbAnswer1 { get => _CbAnswer1; set { _CbAnswer1 = value; OnPropertyChanged(); } }

        private bool _CbAnswer2;
        public bool CbAnswer2 { get => _CbAnswer2; set { _CbAnswer2 = value; OnPropertyChanged(); } }

        private bool _CbAnswer3;
        public bool CbAnswer3 { get => _CbAnswer3; set { _CbAnswer3 = value; OnPropertyChanged(); } }

        private bool _CbAnswer4;
        public bool CbAnswer4 { get => _CbAnswer4; set { _CbAnswer4 = value; OnPropertyChanged(); } }

        private ObservableCollection<Question> _List;
        public ObservableCollection<Question> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Question _SelectedQuiz;
        public Question SelectedQuiz { get => _SelectedQuiz; set { _SelectedQuiz = value; OnPropertyChanged(); } }

        #endregion

        #region static
        public static bool isBack;
        #endregion

        #region ICommand
        public ICommand CloseWindowCommand { get; set; }
        public ICommand AddQuestionCommand { get; set; }
        public ICommand LoadWindowCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand CompletedCommand { get; set; }
        public ICommand DeleteQuestionCommand { get; set; }
        public ICommand EditQuestionCommand { get; set; }
        #endregion
        public AddingExamVM() {

            LoadWindowCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {

                ExamName = ExamVM.exname;
                DayOpen = ExamVM.dayopen;
                DayEnd = ExamVM.dayend;
                Duration = ExamVM.duration;
                Subject = ExamVM.subject;
               

                Question = "";
                Answer1 = "";
                Answer2 = "";
                Answer3 = "";
                Answer4 = "";
                CbAnswer1 = false;
                CbAnswer2 = false;
                CbAnswer3 = false;
                CbAnswer4 = false;

                isBack = false;

                SubjectList = new ObservableCollection<Subject>(DataProvider.Ins.DB.Subjects);

                List = new ObservableCollection<Question>();
                if(AddExamVM.isEdit)
                {
                    var a = DataProvider.Ins.DB.Quizs.Where(x => x.QuizListId == ExamVM.quizlist.Id).ToList();
                    foreach(Quiz i in a)
                    {
                        var b = i.Answers.ToList();
                        List.Add(new Question()
                        {
                            question = i.Content,
                            Answer1 = b[0].Content,
                            Answer2 = b[1].Content,
                            Answer3 = b[2].Content,
                            Answer4 = b[3].Content,
                            CbAnswer1 = b[0].IsCorrect == 1 ? true : false,
                            CbAnswer2 = b[1].IsCorrect == 1 ? true : false,
                            CbAnswer3 = b[2].IsCorrect == 1 ? true : false,
                            CbAnswer4 = b[3].IsCorrect == 1 ? true : false,
                        }); 
                    }
                    if (List.Count() < 0) return;
                    SelectedQuiz = List.FirstOrDefault();

                    Question = SelectedQuiz.question;

                    Answer1 = SelectedQuiz.Answer1;
                    Answer2 = SelectedQuiz.Answer2;
                    Answer3 = SelectedQuiz.Answer3;
                    Answer4 = SelectedQuiz.Answer4;

                    CbAnswer1 = SelectedQuiz.CbAnswer1 ? true : false;
                    CbAnswer2 = SelectedQuiz.CbAnswer2 ? true : false;
                    CbAnswer3 = SelectedQuiz.CbAnswer3 ? true : false;
                    CbAnswer4 = SelectedQuiz.CbAnswer4 ? true : false;
                }
            
            });

            CloseWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                if (p == null)
                    return;

                isBack = true;
                if(AddExamVM.isEdit) { p.Close(); return; }

                DataProvider.Ins.DB.ExamInfoes.Remove(ExamVM.quizlist.ExamInfoes.FirstOrDefault());
                DataProvider.Ins.DB.QuizLists.Remove(ExamVM.quizlist);

                DataProvider.Ins.DB.SaveChanges();
                p.Close();

            });

            SelectionChangedCommand = new RelayCommand<Object>((p) => { if (SelectedQuiz == null) return false;  return true; }, (p) => {
                Question = SelectedQuiz.question;

                Answer1 = SelectedQuiz.Answer1;
                Answer2 = SelectedQuiz.Answer2;
                Answer3 = SelectedQuiz.Answer3;
                Answer4 = SelectedQuiz.Answer4;

                CbAnswer1 = SelectedQuiz.CbAnswer1 ? true : false;
                CbAnswer2 = SelectedQuiz.CbAnswer2 ? true : false;
                CbAnswer3 = SelectedQuiz.CbAnswer3 ? true : false;
                CbAnswer4 = SelectedQuiz.CbAnswer4 ? true : false;

            });

            AddQuestionCommand = new RelayCommand<Object>((p) => {
                if (String.IsNullOrEmpty(Question) ||
                String.IsNullOrEmpty(Answer1) ||
                String.IsNullOrEmpty(Answer2)||
                String.IsNullOrEmpty(Answer3)||
                String.IsNullOrEmpty(Answer4)||
                !CbAnswer1 && !CbAnswer2 && !CbAnswer3 && !CbAnswer4)return false;
                else return true;
            }, (p) => {

                List.Add(new Question()
                    { question = Question 
                    , Answer1 = Answer1, Answer2 = Answer2, Answer3 = Answer3 , Answer4 = Answer4
                    , CbAnswer1 = CbAnswer1, CbAnswer2 = CbAnswer2, CbAnswer3 = CbAnswer3, CbAnswer4 = CbAnswer4});
               
                Question = "";
                Answer1 = "";
                Answer2 = "";
                Answer3 = "";
                Answer4 = "";
                CbAnswer1 = false;
                CbAnswer2 = false;
                CbAnswer3 = false;
                CbAnswer4 = false;
            });

            CompletedCommand = new RelayCommand<Window>((p) => {
                if (List.Count() <=0)
                    return false;
                return true;
            }, (p) => {
                if (p == null)
                    return;

                foreach (Quiz i in ExamVM.quizlist.Quizs)
                {

                    var b =  DataProvider.Ins.DB.Answers.Where(x => x.QuizId == i.Id).ToList();
                    DataProvider.Ins.DB.Answers.Remove(b[0]);
                    DataProvider.Ins.DB.Answers.Remove(b[1]);
                    DataProvider.Ins.DB.Answers.Remove(b[2]);
                    DataProvider.Ins.DB.Answers.Remove(b[3]);

                }
                var qui = DataProvider.Ins.DB.Quizs.Where(x => x.QuizListId == ExamVM.quizlist.Id).ToList();
                foreach (Quiz i in qui)
                {
                    DataProvider.Ins.DB.Quizs.Remove(i);

                }

                DataProvider.Ins.DB.SaveChanges();



                foreach (Question i in List)
                {
                    var quiz = new Quiz() { QuizListId = ExamVM.quizlist.Id, Content = i.question };

                    var a1 = new Answer() { QuizId = quiz.Id, Content = i.Answer1, IsCorrect = i.CbAnswer1 ? 1 : 0 };
                    var a2 = new Answer() { QuizId = quiz.Id, Content = i.Answer2, IsCorrect = i.CbAnswer2 ? 1 : 0 };
                    var a3 = new Answer() { QuizId = quiz.Id, Content = i.Answer3, IsCorrect = i.CbAnswer3 ? 1 : 0 };
                    var a4 = new Answer() { QuizId = quiz.Id, Content = i.Answer4, IsCorrect = i.CbAnswer4 ? 1 : 0 };

                    DataProvider.Ins.DB.Quizs.Add(quiz);
                    DataProvider.Ins.DB.Answers.Add(a1);
                    DataProvider.Ins.DB.Answers.Add(a2);
                    DataProvider.Ins.DB.Answers.Add(a3);
                    DataProvider.Ins.DB.Answers.Add(a4);
                    DataProvider.Ins.DB.SaveChanges();
                }
                var a = DataProvider.Ins.DB.QuizLists.Where(x => x.Id == ExamVM.quizlist.Id).FirstOrDefault();
                a.SubjectsId = Subject.Id;
                a.NameList = ExamName;
                a.ExamInfoes.FirstOrDefault().Duration = Convert.ToInt32(Duration);
                a.ExamInfoes.FirstOrDefault().DayOpen = DayOpen;
                a.ExamInfoes.FirstOrDefault().DayEnd = DayEnd;

                DataProvider.Ins.DB.SaveChanges();

                isBack = false;
                p.Close();
            });

            EditQuestionCommand = new RelayCommand<Object>((p) => {
                if (SelectedQuiz == null)
                    return false;
                return true;
            }, (p) => {
                SelectedQuiz.question = Question;

                SelectedQuiz.Answer1 = Answer1;
                SelectedQuiz.Answer2 = Answer2;
                SelectedQuiz.Answer3 = Answer3;
                SelectedQuiz.Answer4 = Answer4;

                SelectedQuiz.CbAnswer1 = CbAnswer1;
                SelectedQuiz.CbAnswer2 = CbAnswer2;
                SelectedQuiz.CbAnswer3 = CbAnswer3;
                SelectedQuiz.CbAnswer4 = CbAnswer4;

            });

            DeleteQuestionCommand = new RelayCommand<Object>((p) => {
                if (SelectedQuiz == null)
                    return false;
                return true;
            }, (p) => {
                List.Remove(SelectedQuiz);
                if (List.Count() <= 0) {
                    Question = "";
                    Answer1 = "";
                    Answer2 = "";
                    Answer3 = "";
                    Answer4 = "";
                    CbAnswer1 = false;
                    CbAnswer2 = false;
                    CbAnswer3 = false;
                    CbAnswer4 = false;
                    return;
                }
                SelectedQuiz = List.First();
            });

        }
    }
}
