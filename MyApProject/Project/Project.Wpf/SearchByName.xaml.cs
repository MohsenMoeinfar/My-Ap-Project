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
    public partial class SearchByNameTaskWindow : Window
    {
        [ObservableProperty]
        private string taskName;
        private Example.swaggerClient  SwCli;
        public void SearchByName_Click(object sender, RoutedEventArgs e)
        {
            var TasksFound  = SwCli.SearchTasksByNameAsync(TaskName).Result.ToList();
            if(TasksFound != null)
            {
                    ResultSearchByNameTaskWindow  ResSearchByNameWindow = new ResultSearchByNameTaskWindow(TasksFound);
                    ResSearchByNameWindow.Show();
                    
            }
            else
            {
                    MessageBox.Show("Task not found.");
            }

            
        }
        public SearchByNameTaskWindow(Example.swaggerClient Esa)
        {
            this.DataContext = this;
            SwCli = Esa;
            InitializeComponent();
            
            
        }
    }


}