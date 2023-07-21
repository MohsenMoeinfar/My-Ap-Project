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
    public partial class DeleteTaskWindow : Window
    {
        [ObservableProperty]
        private int taskId;
        private Example.swaggerClient  SwCli;
        public HubConnection connection;
        public void Delete_Click(object sender, RoutedEventArgs e)
        {
            var TaskFound  = SwCli.SearchTaskByIdAsync(TaskId).Result;
            if(TaskFound != null)
            {
                    SwCli.DeleteTaskAsync(TaskId).Wait();
                    connection.SendAsync("RemoveItem" , TaskId).Wait();
                    MessageBox.Show("Task deleted successfully.");

            }
            else
            {
                     MessageBox.Show("Task not found.");
            }

            
        }
        public DeleteTaskWindow(Example.swaggerClient Esa , HubConnection Hconnection)
        {
            this.DataContext = this;
            SwCli = Esa;
            connection = Hconnection;
            InitializeComponent();
            
            
        }
    }


}