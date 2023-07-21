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
    public partial class AddTaskWindow : Window
    {
        [ObservableProperty]
        private string taskName = "type the name of task";
        [ObservableProperty]
        private int taskId;
        [ObservableProperty]
        private DateTime taskDueDate = DateTime.Now;
        [ObservableProperty]
        private string  taskDescription = " write your Description";
        [ObservableProperty]
        private bool taskIsComplete;
        private Example.swaggerClient  SwCli;
        public HubConnection connection;
        

        public void Add_Click(object sender, RoutedEventArgs e)
        {
            try
        {
            Example.ToDo ExistingTask =  SwCli.SearchTaskByIdAsync(TaskId).Result;
            if (ExistingTask != null)
            {
                MessageBox.Show("Task ID already exists. Please choose a different ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

             SwCli.AddTaskAsync(new Example.ToDo
            {
                Name = TaskName,
                Id = TaskId,
                Description = TaskDescription,
                IsComplete = TaskIsComplete,
                DueDate = TaskDueDate
            }).Wait();
            connection.SendAsync("AddItem", new ToDo{
                Name = TaskName,
                Id = TaskId,
                Description = TaskDescription,
                IsComplete = TaskIsComplete,
                DueDate = TaskDueDate
            }).Wait();
         

     
            MessageBox.Show("Task added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

           
        }

        public AddTaskWindow(Example.swaggerClient Esa , HubConnection Hconnection)
        {
            this.DataContext = this;
            SwCli = Esa;
            this.connection = Hconnection;
            InitializeComponent();
            
            
        }
    }


}