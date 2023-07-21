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
    public partial class AddInterestTaskWindow : Window
    {
        [ObservableProperty]
        private string interestMovie;
        [ObservableProperty]
        private string interestMusic;
        [ObservableProperty]
        private string interestSport;
        private Example.swaggerClient  SwCli;
        public void  Button_Click(object sender, RoutedEventArgs e)
        {
               int Count  = SwCli.GetAllInterestsAsync().Result.Count;
               if(Count == 0)
               {
                    SwCli.AddInterestAsync(new Example.UserInterest{
                            Id = 1,
                            MovieInterest = InterestMovie,
                            MusicInterest = InterestMusic,
                            SportInterest = InterestSport
                            }).Wait();
                            Count++;
                            MessageBox.Show("Interest added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
               }
               else
               {
                        MessageBox.Show("An interest has already been registered. Please go to the edit section.");
               }
        }
         
        public AddInterestTaskWindow(Example.swaggerClient Esa)
        {
            this.DataContext = this;
            SwCli = Esa;
            InitializeComponent();
            
            
        }
    }


}