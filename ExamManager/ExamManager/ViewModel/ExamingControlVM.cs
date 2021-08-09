using ExamManager.Model;
using ExamManager.UserControlEM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ExamManager.ViewModel
{
    public class ExamingControlVM : BaseViewModel
    {
        #region initialize Question 
        #region Q1
        private string _Question1;
        public string Question1 { get => _Question1; set { _Question1 = value; OnPropertyChanged(); } }

        private string _Answer11;
        public string Answer11 { get => _Answer11; set { _Answer11 = value; OnPropertyChanged(); } }

        private string _Answer21;
        public string Answer21 { get => _Answer21; set { _Answer21 = value; OnPropertyChanged(); } }

        private string _Answer31;
        public string Answer31 { get => _Answer31; set { _Answer31 = value; OnPropertyChanged(); } }

        private string _Answer41;
        public string Answer41 { get => _Answer41; set { _Answer41 = value; OnPropertyChanged(); } }

        private bool _CbAnswer11;
        public bool CbAnswer11 { get => _CbAnswer11; set { _CbAnswer11 = value; OnPropertyChanged(); } }

        private bool _CbAnswer21;
        public bool CbAnswer21 { get => _CbAnswer21; set { _CbAnswer21 = value; OnPropertyChanged(); } }

        private bool _CbAnswer31;
        public bool CbAnswer31 { get => _CbAnswer31; set { _CbAnswer31 = value; OnPropertyChanged(); } }

        private bool _CbAnswer41;
        public bool CbAnswer41 { get => _CbAnswer41; set { _CbAnswer41 = value; OnPropertyChanged(); } }
        #endregion

        #region Q2
        private string _Question2;
        public string Question2 { get => _Question2; set { _Question2 = value; OnPropertyChanged(); } }

        private string _Answer12;
        public string Answer12 { get => _Answer12; set { _Answer12 = value; OnPropertyChanged(); } }

        private string _Answer22;
        public string Answer22 { get => _Answer22; set { _Answer22 = value; OnPropertyChanged(); } }

        private string _Answer32;
        public string Answer32 { get => _Answer32; set { _Answer32 = value; OnPropertyChanged(); } }

        private string _Answer42;
        public string Answer42 { get => _Answer42; set { _Answer42 = value; OnPropertyChanged(); } }

        private bool _CbAnswer12;
        public bool CbAnswer12 { get => _CbAnswer12; set { _CbAnswer12 = value; OnPropertyChanged(); } }

        private bool _CbAnswer22;
        public bool CbAnswer22 { get => _CbAnswer22; set { _CbAnswer22 = value; OnPropertyChanged(); } }

        private bool _CbAnswer32;
        public bool CbAnswer32 { get => _CbAnswer32; set { _CbAnswer32 = value; OnPropertyChanged(); } }

        private bool _CbAnswer42;
        public bool CbAnswer42 { get => _CbAnswer42; set { _CbAnswer42 = value; OnPropertyChanged(); } }
        #endregion

        #region Q3
        private string _Question3;
        public string Question3 { get => _Question3; set { _Question3 = value; OnPropertyChanged(); } }

        private string _Answer13;
        public string Answer13 { get => _Answer13; set { _Answer13 = value; OnPropertyChanged(); } }

        private string _Answer23;
        public string Answer23 { get => _Answer23; set { _Answer23 = value; OnPropertyChanged(); } }

        private string _Answer33;
        public string Answer33 { get => _Answer33; set { _Answer33 = value; OnPropertyChanged(); } }

        private string _Answer43;
        public string Answer43 { get => _Answer43; set { _Answer43 = value; OnPropertyChanged(); } }

        private bool _CbAnswer13;
        public bool CbAnswer13 { get => _CbAnswer13; set { _CbAnswer13 = value; OnPropertyChanged(); } }

        private bool _CbAnswer23;
        public bool CbAnswer23 { get => _CbAnswer23; set { _CbAnswer23 = value; OnPropertyChanged(); } }

        private bool _CbAnswer33;
        public bool CbAnswer33 { get => _CbAnswer33; set { _CbAnswer33 = value; OnPropertyChanged(); } }

        private bool _CbAnswer43;
        public bool CbAnswer43 { get => _CbAnswer43; set { _CbAnswer43 = value; OnPropertyChanged(); } }
        #endregion
        #endregion

        #region initialize
        public static int index = 0;
        Grid gr1;
        Grid gr2;
        Grid gr3;
        public static List<int> CbAnswer;
        #endregion

        #region ICommand
        public ICommand LoadWindowCommand { get; set; }
        public ICommand UnLoadWindowCommand { get; set; }
        public ICommand LoadGrid1Command { get; set; }
        public ICommand LoadGrid2Command { get; set; }
        public ICommand LoadGrid3Command { get; set; }
        #endregion

        public ExamingControlVM() {

            LoadWindowCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {

                #region blank
                Question1 = "";
                Question2 = "";
                Question3 = "";

                Answer11 = "";
                Answer21 = "";
                Answer31 = "";
                Answer41 = "";

                Answer12 = "";
                Answer22 = "";
                Answer32 = "";
                Answer42 = "";

                Answer13 = "";
                Answer23 = "";
                Answer33 = "";
                Answer43 = "";

                CbAnswer11 = false;
                CbAnswer21 = false;
                CbAnswer31 = false;
                CbAnswer41 = false;

                CbAnswer12 = false;
                CbAnswer22 = false;
                CbAnswer32 = false;
                CbAnswer42 = false;

                CbAnswer13 = false;
                CbAnswer23 = false;
                CbAnswer33 = false;
                CbAnswer43 = false;

                #endregion

                var a = DataProvider.Ins.DB.Quizs.Where(x => x.QuizListId == HomeVM.exam_info.Id).ToList();

                if (ExamingVM.index == index) {
                    gr1.Visibility = Visibility.Collapsed;
                    return;
                }

                Question1 = "Câu " + (index + 1) + " : " + a[index].Content;
                List<Answer> b1 = new List<Answer>(a[index].Answers);
                Answer11 = "A. "+b1[0].Content;
                Answer21 = "B. "+b1[1].Content;
                Answer31 = "C. "+b1[2].Content;
                Answer41 = "D. "+b1[3].Content;

                switch (CbAnswer[index])
                {
                    case 1:
                        CbAnswer11 = true;
                        break;
                    case 2:
                        CbAnswer21 = true;
                        break;
                    case 3:
                        CbAnswer31 = true;
                        break;
                    case 4:
                        CbAnswer41 = true;
                        break;
                }
                if(ExamingVM.wp[index].Background != Brushes.Aqua) ExamingVM.wp[index].Background = Brushes.LightGray;
                else ExamingVM.wp[index].Background = Brushes.Gray;
                index++;

                if (ExamingVM.index == index)
                {
                    gr2.Visibility = Visibility.Collapsed;
                    gr3.Visibility = Visibility.Collapsed;
                    return;
                }
                Question2 = "Câu "+(index+1)+ " : "+ a[index].Content;
                List<Answer> b2 = new List<Answer>(a[index].Answers);
                Answer12 = "A. "+b2[0].Content;
                Answer22 = "B. "+b2[1].Content;
                Answer32 = "C. "+b2[2].Content;
                Answer42 = "D. "+b2[3].Content;

              
                switch (CbAnswer[index])
                {
                    case 1:
                        CbAnswer12 = true;
                        break;
                    case 2:
                        CbAnswer22 = true;
                        break;
                    case 3:
                        CbAnswer32 = true;
                        break;
                    case 4:
                        CbAnswer43 = true;
                        break;
                }

                if (ExamingVM.wp[index].Background != Brushes.Aqua) ExamingVM.wp[index].Background = Brushes.LightGray;
                else ExamingVM.wp[index].Background = Brushes.Gray;
                index++;

                if (ExamingVM.index == index)
                {
                    gr3.Visibility = Visibility.Collapsed;
                    return;
                }
                Question3 = "Câu " + (index + 1) + " : " + a[index].Content;
                List<Answer> b3 = new List<Answer>(a[index].Answers);
                Answer13 = "A. "+b3[0].Content;
                Answer23 = "B. "+b3[1].Content;
                Answer33 = "C. "+b3[2].Content;
                Answer43 = "D. "+b3[3].Content;

                
                switch (CbAnswer[index])
                {
                    case 1:
                        CbAnswer13 = true;
                        break;
                    case 2:
                        CbAnswer23 = true;
                        break;
                    case 3:
                        CbAnswer33 = true;
                        break;
                    case 4:
                        CbAnswer43 = true;
                        break;
                }
                if (ExamingVM.wp[index].Background != Brushes.Aqua) ExamingVM.wp[index].Background = Brushes.LightGray;
                else ExamingVM.wp[index].Background = Brushes.Gray;
                index++;

            });

            LoadGrid1Command = new RelayCommand<Grid>((p) => { return true; }, (p) => { if (p == null) return; gr1 = p; });

            LoadGrid2Command = new RelayCommand<Grid>((p) => { return true; }, (p) => { if (p == null) return; gr2 = p; });

            LoadGrid3Command = new RelayCommand<Grid>((p) => { return true; }, (p) => { if (p == null) return; gr3 = p; });
            
        }

        public void LoadRadiobutton (){
            
            if (ExamingVM.ix < ExamingVM.index)
            {
                CbAnswer[ExamingVM.ix] = CbAnswer11 ? 1 : CbAnswer21 ? 2 : CbAnswer31 ? 3 : CbAnswer41 ? 4 : 0;
                if (CbAnswer[ExamingVM.ix] != 0) ExamingVM.wp[ExamingVM.ix].Background = Brushes.Aqua;
                ExamingVM.ix++;
            }

            if (ExamingVM.ix < ExamingVM.index)
            {
                CbAnswer[ExamingVM.ix] = CbAnswer12 ? 1 : CbAnswer22 ? 2 : CbAnswer32 ? 3 : CbAnswer42 ? 4 : 0;
                if (CbAnswer[ExamingVM.ix] != 0) ExamingVM.wp[ExamingVM.ix].Background = Brushes.Aqua;
                ExamingVM.ix++;
            }

            if (ExamingVM.ix < ExamingVM.index)
            {
                CbAnswer[ExamingVM.ix] = CbAnswer13 ? 1 : CbAnswer23 ? 2 : CbAnswer33 ? 3 : CbAnswer43 ? 4 : 0;
                if (CbAnswer[ExamingVM.ix] != 0) ExamingVM.wp[ExamingVM.ix].Background = Brushes.Aqua;
                ExamingVM.ix++;
            }

            int nnn = 0;
            foreach (TextBlock nn in ExamingVM.wp)
            {
                if (nnn >= ExamingVM.ix) break;
                if (nn.Background != Brushes.Aqua) nn.Background = Brushes.BurlyWood;
                nnn++;
            }

        }
    }
}
