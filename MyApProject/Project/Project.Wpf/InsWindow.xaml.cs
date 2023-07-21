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
    public partial class InspiretaskWindow : Window
    {
        [ObservableProperty]
        private string res;
        [ObservableProperty]
        private string inp;
        [ObservableProperty]
        private string goalInterest;
        
        private Example.swaggerClient  SwCli;
         private async void InspireTask_Click(object sender, RoutedEventArgs e)
    {
        try{
        string interest = ((ComboBoxItem)InterestComboBox.SelectedItem).Content.ToString();
     

        string movieInterest = (await SwCli.GetInterestByIdAsync(1)).MovieInterest;
        string musicInterest = (await SwCli.GetInterestByIdAsync(1)).MusicInterest;
        string sportInterest = (await SwCli.GetInterestByIdAsync(1)).SportInterest;
        
       
        switch (interest.ToLower())
        {
            case "movie":
                Res = (await SwCli.InspireTaskAsync(movieInterest, Inp)).ToList()[0];
                break;
            case "music":
                Res = (await SwCli.InspireTaskAsync(musicInterest, Inp)).ToList()[0];
                break;
            case "sport":
                Res = (await SwCli.InspireTaskAsync(sportInterest, Inp)).ToList()[0];
                break;
            default:
                MessageBox.Show("Please choose one of the three options: music, film, or sports.");
                break;
        }
       }
       catch(Exception ex)
       {
                   MessageBox.Show(@$"Please choose one of the three options: music, film, or sports.
                                          {ex.Message}");
       }
    }
        

        
        
        public InspiretaskWindow(Example.swaggerClient Esa)
        {
            this.DataContext = this;
            SwCli = Esa;
            InitializeComponent();
            
            
        }
    }


}