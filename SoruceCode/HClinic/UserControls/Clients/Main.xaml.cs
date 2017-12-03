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

namespace HClinic.UserControls.Clients
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        public Windows.Main parent;
        Manager manager;
        Dates dates;
        Sessions sessions;
        private Main() { }
        public Main(Windows.Main parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void ClientsManager_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ClientMainContent.Children.Clear();
            if (manager == null)
            {
                manager = new Manager(this);
            }
            ClientMainContent.Children.Add(manager);
        }

        private void ClientsDates_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ClientMainContent.Children.Clear();
            if (dates == null)
            {
                dates = new Dates(this);
            }
            ClientMainContent.Children.Add(dates);
        }

        private void ClientsSessions_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ClientMainContent.Children.Clear();
            if (sessions == null)
            {
                sessions = new Sessions(this);
            }
            ClientMainContent.Children.Add(sessions);
        }
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
