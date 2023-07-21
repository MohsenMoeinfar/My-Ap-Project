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
    public partial class ProfileTaskWindow : Window
    {
        [ObservableProperty]
        private int taskId;
        private Example.swaggerClient  SwCli;
         public void  Add_Click(object sender, RoutedEventArgs e)
        {
            AddInterestTaskWindow  AddInterestWindow = new AddInterestTaskWindow(SwCli);
            AddInterestWindow.Show();
        }
         public void  Edit_Click(object sender, RoutedEventArgs e)
        {
            EditInterestTaskWindow  EditInterestWindow = new EditInterestTaskWindow(SwCli);
            EditInterestWindow.Show();
        }
        public ProfileTaskWindow(Example.swaggerClient Esa)
        {
            this.DataContext = this;
            SwCli = Esa;
            InitializeComponent();
            
            
        }
    }


}