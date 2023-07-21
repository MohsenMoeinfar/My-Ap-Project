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
    public partial class AiTaskWindow : Window
    {
        
        private Example.swaggerClient  SwCli;
        public HubConnection connection;
         public void  btnBreaktask_Click(object sender, RoutedEventArgs e)
        {
            BreakTaskWindow  BrWindow = new BreakTaskWindow(SwCli , connection);
            BrWindow.Show();
        }
         public void  btnInspiretask_Click(object sender, RoutedEventArgs e)
        {
            InspiretaskWindow  InsWindow = new InspiretaskWindow(SwCli);
            InsWindow.Show();
        }
        public void  btnIdontknowwhattodo_Click(object sender, RoutedEventArgs e)
        {
            IdontknowwhattodoTaskWindow  IdontknowwhattodoWindow = new IdontknowwhattodoTaskWindow (SwCli);
            IdontknowwhattodoWindow.Show();
        }
         public void  btnAskStartingGuide_Click(object sender, RoutedEventArgs e)
        {
            AskStartingGuideTaskWindow  AskStartingGuideWindow = new AskStartingGuideTaskWindow(SwCli);
            AskStartingGuideWindow.Show();
        }
        public void  btnCategorization_Click(object sender, RoutedEventArgs e)
        {
            CategorizationTaskWindow  CategorizationWindow = new CategorizationTaskWindow(SwCli);
            CategorizationWindow.Show();
        }

        public AiTaskWindow(Example.swaggerClient Esa , HubConnection Hc)
        {
            this.DataContext = this;
            connection = Hc;
            SwCli = Esa;
            InitializeComponent();
            
            
        }
    }


}