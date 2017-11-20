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
using System.Windows.Shapes;

namespace HClinic.Windows
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        UserControls.Clients clients;
        UserControls.Home home;
        UserControls.Settings settings;
        public Main()
        {
            InitializeComponent();
            home = new UserControls.Home();
            GridContent.Children.Add(home);
        }
        private void GridRowTop_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (new SubWindows.Confirm("الخروج من النظام", "هل تود بالفعل الخروج من النظام؟", "errorBrush").ShowDialog() == true)
            {
                this.Close();
            }
        }
        private void btnMinmize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void btnHome_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GridContent.Children.Clear();
            GridContent.Children.Add(home);
        }
        private void btnMaximize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }
        private void btnClients_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GridContent.Children.Clear();
            if (clients == null)
            {
                clients = new UserControls.Clients();
                clients.Height = Double.NaN;
                clients.Width = Double.NaN;
            }
            GridContent.Children.Add(clients);
        }
        private void btnSettings_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GridContent.Children.Clear();
            if (settings == null)
            {
                settings = new UserControls.Settings();
            }
            GridContent.Children.Add(settings);
        }
    }
}
