using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ExamManager.Model;
using ExamManager.UserControlEM;
using ExamManager.ViewModel;

namespace ExamManager.ViewModel
{
    public class MainWindowVM : BaseViewModel
    {
        #region ICommand
        public ICommand CloseApplicationCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand LoadGridSeclectedCommand { get; set; }
        public ICommand LoadMainGridCommand { get; set; }
        public ICommand LoadMenuCommand { get; set; }
        #endregion

        #region initialize
        public Grid gridSelected;
        public Grid mainGrid;
        public static Window MainWin;

       
        #endregion

        public MainWindowVM()
        {

            DataProvider.Ins.DB.Users.First().ToString();

            CloseApplicationCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                if (p == null)
                    return;
                p.Close();
            });

            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                if (p == null)
                    return;
                MainWin = p;
                Load();
               
            });

            LoadGridSeclectedCommand = new RelayCommand<Grid>((p) => { return true; }, (p) => {
                if (p == null)
                    return;
                gridSelected = p;
            });

            LoadMainGridCommand = new RelayCommand<Grid>((p) => { return true; }, (p) => {
                if (p == null)
                    return;
                mainGrid = p;
                mainGrid.Children.Add(new Home());
            });

            SelectionChangedCommand = new RelayCommand<ListView>((p) => { return true; }, (p) => {
                if (p == null)
                    return;
                int i = p.SelectedIndex;
                Changescreen(i);
                MoveSelect(i);
            });

            LoadMenuCommand = new RelayCommand<ListView>((p) => { return true; }, (p) => {
                if (p == null)
                    return;
                var a = DataProvider.Ins.DB.Users.Where(x=>x.Id == LoginWindowVM.idUser).FirstOrDefault();

                if (a == null) return;

                try
                {
                    if (a.Permission == null)
                    {
                        a.Permission = 3;
                        DataProvider.Ins.DB.SaveChanges();
                    }
                }
                catch { }

                

                int? permistion = a.Permission;

                if (permistion == 2)
                {
                    foreach (FrameworkElement i in p.Items)
                    {
                        if (i.Name == "item3") i.Visibility = Visibility.Collapsed;
                        else if (i.Name == "item4") i.Visibility = Visibility.Collapsed;
                    }

                }
                else if(permistion == 3)
                {
                    foreach (FrameworkElement i in p.Items)
                    {
                        if (i.Name == "item4") i.Visibility = Visibility.Collapsed;
                        else if (i.Name == "item5") i.Visibility = Visibility.Collapsed;
                        else if (i.Name == "item6") i.Visibility = Visibility.Collapsed;
                    }
                }

            });

        }

        private void Changescreen(int i)
        {
            switch (i)
            {
                case 0:
                    mainGrid.Children.Clear();
                    mainGrid.Children.Add(new Home());
                    break;
                case 1:
                    mainGrid.Children.Clear();
                    mainGrid.Children.Add(new Candidate());
                    break;
                case 2:
                    mainGrid.Children.Clear();
                    mainGrid.Children.Add(new Result());
                    break;
                case 3:
                    mainGrid.Children.Clear();
                    mainGrid.Children.Add(new ListCandidate());

                    break;
                case 4:

                    mainGrid.Children.Clear();
                    mainGrid.Children.Add(new UserControlEM.Subject());
                    break;
                case 5:
                    mainGrid.Children.Clear();
                    mainGrid.Children.Add(new AddExam());
                    break;
                case 6:

                    mainGrid.Children.Clear();
                    mainGrid.Children.Add(new OtherControl());
                    break;
                default:
                    mainGrid.Children.Clear();
                    break;
            }
        }

        private void MoveSelect(int index)
        {
            gridSelected.Margin = new Thickness(0, 66 * index, 0, 0);
        }

        void Load() {
         
            MainWin.Hide();
            LoginWindow lg = new LoginWindow();
            lg.ShowDialog();
           
            if (LoginWindowVM.IsLogin == true)
            {
                MainWin.Show();
            }
            else
            {
                MainWin.Close();
            }
        }



    }
}
