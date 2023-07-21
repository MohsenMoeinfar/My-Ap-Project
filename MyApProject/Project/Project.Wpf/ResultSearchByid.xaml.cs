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
    
    public partial class ResultSearchByIDTaskWindow : Window
    {
       
        public ObservableCollection<Example.ToDo> TaskSearchView { get; set; } = new ObservableCollection<Example.ToDo>();


        public ResultSearchByIDTaskWindow(Example.ToDo Founded)
        {
            this.DataContext = this;
            TaskSearchView.Add(Founded);
            InitializeComponent();
            
            
        }
    }
}

