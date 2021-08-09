using ExamManager.Model;
using ExamManager.UserControlEM;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace ExamManager.ViewModel
{
    
    public class ExamingVM:BaseViewModel
    {
        #region initialize
        private string _ExamName;
        public string ExamName { get => _ExamName; set { _ExamName = value; OnPropertyChanged(); } }

        private string _Duration;
        public string Duration { get => _Duration; set { _Duration = value; OnPropertyChanged(); } }

        private string _Subject;
        public string Subject { get => _Subject; set { _Subject = value; OnPropertyChanged(); } }


        private int _Min;
        public int Min { get => _Min; set { _Min = value; OnPropertyChanged(); } }

        private int _Sec;
        public int Sec { get => _Sec; set { _Sec = value; OnPropertyChanged(); } }

        public static int index;

        public static int ix;

        ExamingControlVM examingRight;

        DateTime TimeEntry;

        QuizList QuizList;

        Window ex;

        readonly DispatcherTimer dispatcherTimer = new DispatcherTimer();

        ScrollViewer sr;

        public static List<TextBlock> wp;
        #endregion

        #region ICommand
        public ICommand LoadHintCommand { get; set; }
        public ICommand LoadWindowCommand { get; set; }
        public ICommand LoadMainWindowCommand { get; set; }
        public ICommand CompletedCommand { get; set; }
        public ICommand OpenRightCommand { get; set; }
        public ICommand OpenLeftCommand { get; set; }
        public ICommand ResetScrollCommand { get; set; }
        #endregion

        public ExamingVM() {
            LoadMainWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                if (p == null)
                    return;
                ex = p;
            });

            LoadHintCommand = new RelayCommand<WrapPanel>((p) => { return true; }, (p) => {
                if (p == null)
                    return;
                index = DataProvider.Ins.DB.Quizs.Where(x => x.QuizListId == QuizList.Id).Count();
                wp = new List<TextBlock>();
                for (int i = 0; i < index; i++)
                {
                    TextBlock t = new TextBlock
                    {
                        Text = (i + 1).ToString(),
                    };
                    p.Children.Add(t);
                    wp.Add(t);
                }
                
            });

            LoadWindowCommand = new RelayCommand<Grid>((p) => { return true; }, (p) => {
                if (p == null)
                    return;

                #region timer
                Min = 0;
                Sec = 0;
                dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();
                #endregion


                ExamingRight ExamingRight = new ExamingRight();
                examingRight = ExamingRight.DataContext as ExamingControlVM;

                QuizList = HomeVM.exam_info;
                ExamName = QuizList.NameList;
                Duration = DataProvider.Ins.DB.ExamInfoes.Where(x => x.QuizListId == QuizList.Id).SingleOrDefault().Duration.ToString();
                Subject = DataProvider.Ins.DB.Subjects.Where(x => x.Id == QuizList.SubjectsId).SingleOrDefault().SubjectName;
                
                index = DataProvider.Ins.DB.Quizs.Where(x => x.QuizListId == QuizList.Id).Count();
                ExamingControlVM.CbAnswer = new List<int>();

                for (int i =0; i<index; i++) { ExamingControlVM.CbAnswer.Add(0); }
               
                ExamingControlVM.index = 0;
                ix = 0;
                p.Children.Clear();
                p.Children.Add(ExamingRight);


                TimeEntry = DateTime.Now;
                });

            CompletedCommand = new RelayCommand<Window>((p) => {
               
                MessageBoxResult r =  MessageBox.Show("Bạn muốn dừng làm bài ? ","Bạn có muốn tiếp tục ?", MessageBoxButton.OKCancel);
                if (r == MessageBoxResult.OK)
                    return true;
                else
                    return false;
            }, (p) => {
                if (p == null)
                    return;

                examingRight.LoadRadiobutton();

                float point = 10*((float)Point()/index);

                var b = DataProvider.Ins.DB.ExamInfoes.Where(x=>x.QuizListId == QuizList.Id).SingleOrDefault();

                var check = DataProvider.Ins.DB.UserExams.Where(x=>x.UserId == LoginWindowVM.idUser &&
                    x.ExamInfoId == b.Id).SingleOrDefault();

                if (check == null)
                {
                    var userExam = new UserExam()
                    {
                        ExamInfoId = b.Id,
                        UserId = LoginWindowVM.idUser,
                        ExamPoint = point,
                        TimeEntry = TimeEntry
                    }; 
                    DataProvider.Ins.DB.UserExams.Add(userExam);
                }
                else
                {
                    if (check.ExamPoint < point)
                       check.ExamPoint = point;
                }
                dispatcherTimer.Stop();
                DataProvider.Ins.DB.SaveChanges();
                p.Close();
            });

            OpenRightCommand = new RelayCommand<Grid>((p) => { return ExamingControlVM.index<index?true:false; }, (p) => {
                if (p == null)
                    return;

                p.Children.Clear();
                
                examingRight.LoadRadiobutton();

                if (sr != null) sr.ScrollToTop();

                p.Children.Add(new ExamingRight());


            });

            OpenLeftCommand = new RelayCommand<Grid>((p) => { return ExamingControlVM.index>3?true:false; }, (p) => {
                if (p == null)
                    return;
                
                ExamingControlVM.index = (int)((ExamingControlVM.index-1) / 3 - 1) * 3;
                if (ExamingControlVM.index < 0) ExamingControlVM.index = 0;


                examingRight.LoadRadiobutton();

                
                ix = (int)((ix-1) / 3 - 1) * 3;
                if (ix < 0) ix = 0;


                p.Children.Clear();
                if (sr != null) sr.ScrollToTop();
                p.Children.Add(new ExamingLeft());

            });
            
            ResetScrollCommand = new RelayCommand<ScrollViewer>((p) => { return  true; }, (p) => {
                if (p == null)
                    return;
                sr = p;
            });

        }

        private int Point()
        {
            int point=0;

            var a = DataProvider.Ins.DB.Quizs.Where(x => x.QuizListId == QuizList.Id).ToList();

            for(int i = 0; i < index; i++)
            {
                int y = ExamingControlVM.CbAnswer[i];
                if (y == 0) { continue; }
                if (a[i].Answers.ToList()[y-1].IsCorrect == 1) {
                    point++;
                }
            }
            return point;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Sec++;
            if(Sec == 60)
            {
                Min++;
                Sec = 0;
            }
            if(Min == Convert.ToInt32(Duration))
            {
                MessageBox.Show("Bạn hết thời gian làm bài");
                dispatcherTimer.Stop();
                CompletedCommand.Execute(ex);
                ex.Close();
            }
        }
    }

}
