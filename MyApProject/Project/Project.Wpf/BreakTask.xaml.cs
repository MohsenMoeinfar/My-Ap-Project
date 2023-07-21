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
    public partial class BreakTaskWindow : Window
    {
        [ObservableProperty]
          private string brName ; 
        [ObservableProperty]
           private int min;
        private Example.swaggerClient SwCli;
        public HubConnection connection;
        private List<Example.ToDo> goals = new List<Example.ToDo>();

        public BreakTaskWindow(Example.swaggerClient Esa , HubConnection Hco)
        {
            this.DataContext = this;
            connection = Hco;
            SwCli = Esa;
            InitializeComponent();
        }
        private async void BreakTaskButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                goals = (await SwCli.BreakTaskAsync(BrName, Min)).ToList();
                tasksListView.ItemsSource = goals;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private async void SaveTasksButton_Click(object sender, RoutedEventArgs e)
       {
            if (goals.Count == 0)
            {
                              MessageBox.Show("No tasks to save.");
                              return;
            }

           try
            {
                List<Example.ToDo> newTasks = new List<Example.ToDo>();

                foreach (var task in goals)
              {
                     var existingTask = await SwCli.SearchTaskByIdAsync(task.Id);
                    if (existingTask == null)
                    {
                                  await SwCli.AddTaskAsync(task);
                                  newTasks.Add(task);  
                    }
                    else
                    {
                         MessageBox.Show("Task already exists.");
                         break;
                    }
               }

                if (newTasks.Count > 0)
                {
                        await connection.SendAsync("AddItem", newTasks); 
               }

                MessageBox.Show("Tasks saved successfully.");
            }
            catch (Exception ex)
            {
                 MessageBox.Show($"Error: {ex.Message}");
            }
     }
}


}