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
    public partial class EditTaskWindow : Window
    {
        [ObservableProperty]
        private int taskId;
        private Example.swaggerClient  SwCli;
        public HubConnection connection;

        public void Edit_Click(object sender, RoutedEventArgs e)
        {
            var TaskFound  = SwCli.SearchTaskByIdAsync(TaskId).Result;
            if(TaskFound != null)
            {
                NewNewWindow newWindow = new NewNewWindow(SwCli,TaskId ,TaskFound , connection);
                newWindow.Show();
            }
            else
            {
                MessageBox.Show("Task not found.");

            }


           
        }

        public EditTaskWindow(Example.swaggerClient Esa , HubConnection Hconnection)
        {
            this.DataContext = this;
            connection = Hconnection;
            SwCli = Esa;
            InitializeComponent();
            
            
        }
    }


}