using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ExamManager;
using ExamManager.Model;

namespace ExamManager.ViewModel
{
    public class AllExamWindowVM : BaseViewModel
    {
        #region initialize
        private ObservableCollection<Ex> _ExamList;
        public ObservableCollection<Ex> ExamList { get => _ExamList; set { _ExamList = value; OnPropertyChanged(); } }

        private ObservableCollection<QuizList> _List;
        public ObservableCollection<QuizList> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Ex _SelectedExam;
        public Ex SelectedExam { get => _SelectedExam; set { _SelectedExam = value; OnPropertyChanged(); } }

        private string _ExamName;
        public string ExamName { get => _ExamName; set { _ExamName = value; OnPropertyChanged(); } }

        private int _ExamIndex;
        public int ExamIndex { get => _ExamIndex; set { _ExamIndex = value; OnPropertyChanged(); } }
        #endregion

        #region ICommand
        public ICommand LoadWindowCommand { get; set; }
        public ICommand ExitWindowCommand { get; set; }
        public ICommand NextWindowCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        #endregion
        public AllExamWindowVM(){
            LoadWindowCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                List = new ObservableCollection<QuizList>(DataProvider.Ins.DB.QuizLists);
                ExamList = new ObservableCollection<Ex>();

                foreach (QuizList i in List)
                {
                    ExamList.Add(new Ex()
                    {
                        Subject = DataProvider.Ins.DB.Subjects.Where(x => x.Id == i.SubjectsId).SingleOrDefault().SubjectName,
                        ExamName = i.NameList,
                        DayOpen = DataProvider.Ins.DB.ExamInfoes.Where(x => x.QuizListId == i.Id).SingleOrDefault().DayOpen.Value,
                        DayEnd = DataProvider.Ins.DB.ExamInfoes.Where(x => x.QuizListId == i.Id).SingleOrDefault().DayEnd.Value
                    });

                }

                SelectedExam = ExamList[0];
                ExamName = SelectedExam.ExamName;
            });

            SelectionChangedCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                if (SelectedExam == null) return;
                ExamName = SelectedExam.ExamName;
            });

            ExitWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                if (p == null) return;
                p.Close();
            });

            NextWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                if (p == null) return;

                int y = 0;
                foreach(QuizList i in List)
                {
                    if (y == ExamIndex) { HomeVM.exam_info = i; break; }
                    y++;
                }
                p.Close();
                HomeVM.isEntry = true;
                ExamInfo ex = new ExamInfo();
                ex.ShowDialog();
                HomeVM.isEntry = false;

                
            });

        }
    }
}
