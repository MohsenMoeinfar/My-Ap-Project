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
    public partial class NewNewWindow : Window
    {
        [ObservableProperty]
        private string newTaskName;
        private int taskId;
        [ObservableProperty]
        private DateTimeOffset newTaskDueDate;
        [ObservableProperty]
        private string  newTaskDescription;
        [ObservableProperty]
        private bool newTaskIsComplete ;
        private Example.ToDo TargetTask ;
        private Example.swaggerClient  SwCli;
        public HubConnection connection;

        public void Edit_Click(object sender, RoutedEventArgs e)
        {
           
                
                SwCli.EditTaskAsync(new Example.ToDo
                {
                   Name =  NewTaskName , 
                    Id = taskId , 
                    Description =NewTaskDescription , 
                    IsComplete = NewTaskIsComplete , 
                    DueDate = NewTaskDueDate
                }).Wait();
                connection.SendAsync("UpdateItem", new ToDo{
                        Name = NewTaskName , 
                        Id = taskId , 
                        Description =NewTaskDescription , 
                        IsComplete = NewTaskIsComplete  , 
                        DueDate =  NewTaskDueDate
                    }).Wait();
                MessageBox.Show("Task Edited successfully.");
            

           
        }

        public  NewNewWindow(Example.swaggerClient Esa , int InputId , Example.ToDo Target , HubConnection  HbCo)
        {
            this.DataContext = this;
            TargetTask = Target;
            taskId = InputId;
            SwCli = Esa;
            connection = HbCo;
            newTaskName = TargetTask.Name;
            newTaskDueDate = TargetTask.DueDate;
            newTaskDescription = TargetTask.Description;
            newTaskIsComplete = TargetTask.IsComplete;
            InitializeComponent();
            
            
        }
    }


}