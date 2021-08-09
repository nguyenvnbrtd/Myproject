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
    public class ExamVM : BaseViewModel
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

        private Subject _Subject;
        public Subject Subject { get => _Subject; set { _Subject = value; OnPropertyChanged(); } }

        private ObservableCollection<Subject> _SubjectList;
        public ObservableCollection<Subject> SubjectList { get => _SubjectList; set { _SubjectList = value; OnPropertyChanged(); } }

        private Visibility _TbWarning;
        public Visibility TbWarning { get => _TbWarning; set { _TbWarning = value; OnPropertyChanged(); } }

        public static string exname;
        public static string duration;
        public static Subject subject;
        public static QuizList quizlist;
        public static DateTime dayopen;
        public static DateTime dayend;
        public static Model.ExamInfo examinfo;
        public static bool IsBack;
       
        #endregion

        #region ICommand
        public ICommand LoadWindowCommand { get; set; }
        public ICommand LoadWarningTBCommand { get; set; }
        public ICommand NextWindowCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand NewSubjectCommand { get; set; }
      
        #endregion
        public ExamVM(){
            LoadWindowCommand = new RelayCommand<Grid>((p) => { return true; }, (p) => {
                if (p == null) return;
                if (HomeVM.isEntry)
                {
                    foreach (FrameworkElement i in p.Children) {
                        if (i.Name == "tbExamName") ((TextBox)i).IsReadOnly = true;
                        if (i.Name == "tbDuration") ((TextBox)i).IsReadOnly = true;
                        if (i.Name == "DayOpen") i.Visibility = Visibility.Collapsed;
                        if (i.Name == "tbDayOpen") i.Visibility = Visibility.Visible;
                        if (i.Name == "DayEnd") i.Visibility = Visibility.Collapsed;
                        if (i.Name == "tbDayEnd") i.Visibility = Visibility.Visible;
                        if (i.Name == "cbSubject") ((ComboBox)i).IsReadOnly = true;
                        if (i.Name == "btNewSubject") i.Visibility = Visibility.Collapsed;
                    }
                    ExamName = HomeVM.exam_info.NameList;
                    DayOpen = HomeVM.exam_info.ExamInfoes.FirstOrDefault().DayOpen.Value;
                    DayEnd = HomeVM.exam_info.ExamInfoes.FirstOrDefault().DayEnd.Value;
                    Duration = HomeVM.exam_info.ExamInfoes.FirstOrDefault().Duration.ToString();
                    Subject = HomeVM.exam_info.Subject;
                    SubjectList = new ObservableCollection<Subject>(DataProvider.Ins.DB.Subjects);
                    IsBack = false;
                }
                else
                {
                    foreach (FrameworkElement i in p.Children)
                    {
                        if (i.Name == "tbExamName") ((TextBox)i).IsReadOnly = false;
                        if (i.Name == "tbDuration") ((TextBox)i).IsReadOnly = false;
                        if (i.Name == "DayOpen") i.Visibility = Visibility.Visible;
                        if (i.Name == "tbDayOpen") i.Visibility = Visibility.Collapsed;
                        if (i.Name == "DayEnd") i.Visibility = Visibility.Visible;
                        if (i.Name == "tbDayEnd") i.Visibility = Visibility.Collapsed;
                        if (i.Name == "cbSubject") ((ComboBox)i).IsReadOnly = false;
                        if (i.Name == "btNewSubject") i.Visibility = Visibility.Visible;
                    }

                    if (!AddExamVM.isEdit)
                    {
                        ExamName = "";
                        DayOpen = DateTime.Now;
                        DayEnd = DateTime.Now;
                        Duration = "";
                        Subject = null;
                       

                    }
                    else
                    {
                        ExamName = AddExamVM.a.NameList;
                        DayOpen = AddExamVM.a.ExamInfoes.FirstOrDefault().DayOpen.Value;
                        DayEnd = AddExamVM.a.ExamInfoes.FirstOrDefault().DayEnd.Value;
                        Duration = AddExamVM.a.ExamInfoes.FirstOrDefault().Duration.ToString();
                        Subject = AddExamVM.a.Subject;
                      
                    }
                    IsBack = false;
                    SubjectList = new ObservableCollection<Subject>(DataProvider.Ins.DB.Subjects);

                }
                TbWarning = Visibility.Collapsed;

            });

            LoadWarningTBCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                
                TbWarning = Visibility.Collapsed;
            });

            NextWindowCommand = new RelayCommand<Window>((p) => {
                if (HomeVM.isEntry) return true;
                if (String.IsNullOrEmpty(ExamName) || String.IsNullOrEmpty(Duration) || Subject ==null || !int.TryParse(Duration, out int i))
                {
                    TbWarning = Visibility.Visible;
                    return false;
                }
                else
                    return true;

            }, (p) => {
                if (p == null)
                    return;

                exname = ExamName;
                duration = Duration;
                subject = Subject;
                dayopen = DayOpen;
                dayend = DayEnd;

                p.Close();

                if (HomeVM.isEntry)
                {
                    Examing exam = new Examing();
                    exam.ShowDialog();
                }
                else
                {
                    var a = DataProvider.Ins.DB.QuizLists.Where(x => x.NameList == ExamName && x.Subject.SubjectName == Subject.SubjectName).FirstOrDefault();
                    if (DataProvider.Ins.DB.QuizLists.Where(x => x.NameList == ExamName && x.Subject.SubjectName == Subject.SubjectName).Count() > 0) {
                        MessageBoxResult r = MessageBox.Show("Bạn có muốn sửa lại bài thi ?","Bài thi " + ExamName+" của môn "+ Subject.SubjectName+" đã có sẵn" ,MessageBoxButton.OKCancel);
                        AddExamVM.isEdit = true;
                        if (r == MessageBoxResult.OK)
                        {
                            quizlist = a;
                            examinfo = a.ExamInfoes.FirstOrDefault();
                        }
                        else
                        {
                            IsBack = true;
                            return;
                        }
                    }
                    else
                    {
                        quizlist = new QuizList() { NameList = ExamName, SubjectsId = DataProvider.Ins.DB.Subjects.Where(x => x.Id == Subject.Id).SingleOrDefault().Id };
                        DataProvider.Ins.DB.QuizLists.Add(quizlist);

                        examinfo = new Model.ExamInfo() { QuizListId = quizlist.Id, DayOpen = DayOpen, DayEnd = DayEnd, Duration = Convert.ToInt32(Duration), Ispublic = 1 };
                        DataProvider.Ins.DB.ExamInfoes.Add(examinfo);
                        DataProvider.Ins.DB.SaveChanges();

                    }

                    IsBack = false;

                }

               

            });

            CloseWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                if (p == null)
                    return;
                IsBack = true;
                p.Close();
            });

            NewSubjectCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                if (p == null) return;
                p.Hide();
                AddSubject aj = new AddSubject();
                aj.ShowDialog();
                if (AddSubjectVM.change) { SubjectList = new ObservableCollection<Subject>(DataProvider.Ins.DB.Subjects); Subject = SubjectList.Last(); }

                p.ShowDialog();
                
            });
        }
    }
}
