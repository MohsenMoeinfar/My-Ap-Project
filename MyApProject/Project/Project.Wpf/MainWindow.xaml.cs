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
using System.Net.Http;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.SignalR.Client;

namespace Project.Wpf 
{
    public partial class MainWindow : Window
    {
        
        public static HttpClient httpClient2 = new HttpClient();
        public Example.swaggerClient client = new Example.swaggerClient("http://localhost:5000/", httpClient2);
        
        public  HubConnection connection;
        public  HubConnection connection2;
        public  HubConnection connection3;
        public ObservableCollection<Example.ToDo> TaskView { get; set; } = new ObservableCollection<Example.ToDo>();
        
        public MainWindow()
        {
            InitializeComponent();
            connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5000/Dhbs")
            .Build();
            connection2 = new HubConnectionBuilder()
            .WithUrl("http://localhost:5000/Dhbs2")
            .Build();
            connection3 = new HubConnectionBuilder()
            .WithUrl("http://localhost:5000/Dhbs3")
            .Build();
        connection.On("OnItemAdded", (Example.ToDo item) =>
        {
        Application.Current.Dispatcher.Invoke(() =>
         {
                    TaskView.Add(item);
                    TaskView = new ObservableCollection<Example.ToDo>(TaskView.OrderBy(x => x.Id));
                    TasksListView.ItemsSource = TaskView;
        
        });
           
        });
        connection.On("OnItemUpdated", (Example.ToDo item) =>
        {
        Application.Current.Dispatcher.Invoke(() =>
         {
        
                TaskView = new ObservableCollection<Example.ToDo>(TaskView.Where(i => i.Id != item.Id));
                TaskView.Add(item);
                TaskView = new ObservableCollection<Example.ToDo>(TaskView.OrderBy(x => x.Id));
                TasksListView.ItemsSource = TaskView;
        
        });
         });
        connection2.On("OnItemRemoved", (int  RemovedItemId) =>
        {
        var ItemToRemove = TaskView.Where(item => item.Id == RemovedItemId).ToList()[0];
        Application.Current.Dispatcher.Invoke(() =>
        {
        
            TaskView.Remove(ItemToRemove);
           
        });
        
        });
        connection3.On("OnItemAdded", (List<Example.ToDo> items) =>
        {
            Application.Current.Dispatcher.Invoke(() =>
         {
               foreach (var item in items)
        {
            if (!TaskView.Any(task => task.Id == item.Id))
            {
                TaskView.Add(item);
            }
        }        
                
                 
        
        });
            
           
        });
         connection3.On("OnItemRemoved", (List<Example.ToDo> items) =>
        {
              Application.Current.Dispatcher.Invoke(() =>
         {
                  TaskView.Clear();
        });
           
        });
        
        connection.StartAsync().Wait();
        connection2.StartAsync().Wait();
        connection3.StartAsync().Wait();

            
            this.DataContext = this;
            foreach(var task in client.GetAllTasksAsync().Result.ToList()) 
          {
                TaskView.Add(task);
           }
            TasksListView.ItemsSource = TaskView;
            
        }

        public void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            AddTaskWindow newWindow = new AddTaskWindow(client,connection);
            newWindow.Show();

           
        }
        public void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            DeleteTaskWindow DeleteWindow = new DeleteTaskWindow(client,connection2);
            DeleteWindow.Show();
        }
        public void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditTaskWindow  EditWindow = new EditTaskWindow(client,connection);
            EditWindow.Show();
        }
        public void btnSearchByID_Click(object sender, RoutedEventArgs e)
        {
            SearchByIDTaskWindow  SearchByIDWindow = new SearchByIDTaskWindow(client);
            SearchByIDWindow.Show();
        }
        public void  btnSearchByName_Click(object sender, RoutedEventArgs e)
        {
            SearchByNameTaskWindow  SearchByNameWindow = new SearchByNameTaskWindow(client);
            SearchByNameWindow.Show();
        }
        public void  btnSetStatus_Click(object sender, RoutedEventArgs e)
        {
            SetStatusTaskWindow  SetStatusWindow = new SetStatusTaskWindow(client,connection);
            SetStatusWindow.Show();
        }
        public void  btnProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileTaskWindow  ProfileWindow = new ProfileTaskWindow(client);
            ProfileWindow.Show();
        }
        public void   btnAI_Click(object sender  , RoutedEventArgs e)
        {
            AiTaskWindow  AiWindow = new AiTaskWindow(client , connection3);
            AiWindow.Show();
        }
        public void  DeleteAll_Click(object sender  , RoutedEventArgs e)
        {
                var Goals2  =  client.GetAllTasksAsync().Result.ToList();
                client.DeleteAllTasksAsync().Wait();
                connection3.SendAsync("RemoveItem",Goals2).Wait();
                        
        }
        
        
        

    }
}
