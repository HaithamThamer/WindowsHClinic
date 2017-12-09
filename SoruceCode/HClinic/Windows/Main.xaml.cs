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
        UserControls.Clients.Main clientMain;
        UserControls.Home.Main homeMain;
        UserControls.Settings.Main settingsMain;
        UserControls.AssistantDoctor.Main assistantDoctorMain;
        public Main()
        {
            InitializeComponent();

            //set time and date every 1 sec. 
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            //set home as main for specfic user
            

        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            lblDate.Content = DateTime.Now.ToString(App.Constants.DateTimeFormat);
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (new Confirm("","").ShowDialog() == true)
            {
                this.DialogResult = true;
                this.Close();
            }
        }
        private void btnMaximize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }
        private void btnMinimize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void lblTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void btnShortcutOne_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnShortcutTwo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnShortcutThree_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnShortcutFour_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //set user name in footer
            lblUsername.Content = App.currentUser.name;
        }

        private void btnHome_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainContent.Children.Clear();
            if (homeMain == null)
            {
                homeMain = new UserControls.Home.Main(this);
            }
            MainContent.Children.Add(homeMain);
        }

        private void btnClients_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainContent.Children.Clear();
            if (clientMain == null)
            {
                clientMain = new UserControls.Clients.Main(this);
            }
            MainContent.Children.Add(clientMain);
        }

        private void btnSettings_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainContent.Children.Clear();
            if (settingsMain == null)
            {
                settingsMain = new UserControls.Settings.Main(this);
            }
            MainContent.Children.Add(settingsMain);
        }

        private void btnAssistantDoctor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainContent.Children.Clear();
            if (assistantDoctorMain == null)
            {
                assistantDoctorMain = new UserControls.AssistantDoctor.Main(this);
            }
            MainContent.Children.Add(assistantDoctorMain);
        }

        private void btnDoctor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
        bool isFirst = true;
        private void Window_Activated(object sender, EventArgs e)
        {
            if (isFirst)
            {
                switch (App.currentUser.type)
                {
                    case App.Tables.Users.UserTypes.Guest:
                        break;
                    case App.Tables.Users.UserTypes.Admin:

                        break;
                    case App.Tables.Users.UserTypes.Assistant:
                        btnAssistantDoctor_MouseLeftButtonUp(null, null);
                        break;
                    case App.Tables.Users.UserTypes.Doctor:
                        btnDoctor_MouseLeftButtonUp(null, null);
                        break;
                    case App.Tables.Users.UserTypes.Reception:
                        btnHome_MouseLeftButtonUp(null, null);
                        break;
                    default:
                        break;
                }
                isFirst = false;
            }
        }
    }
}
