using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Example;
// using Project.Wpf;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Net.Http;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.SignalR.Client;
namespace Project.Wpf 
{
    [ObservableObject]
    public partial class SearchByIDTaskWindow : Window
    {
        [ObservableProperty]
        private int taskId;
        private Example.swaggerClient  SwCli;
        public void SearchByID_Click(object sender, RoutedEventArgs e)
        {
            var TaskFound  = SwCli.SearchTaskByIdAsync(TaskId).Result;
            if(TaskFound != null)
            {
                    ResultSearchByIDTaskWindow  ResSearchByIDWindow = new ResultSearchByIDTaskWindow(TaskFound);
                    ResSearchByIDWindow.Show();
                    

            }
            else
            {
                    MessageBox.Show("Task not found.");
            }

            
        }
        public SearchByIDTaskWindow(Example.swaggerClient Esa)
        {
            this.DataContext = this;
            SwCli = Esa;
            InitializeComponent();
            
            
        }
    }


}