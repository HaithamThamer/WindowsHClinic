using HClinic.Classes.Clients;
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
    /// Interaction logic for ClientViewerItem.xaml
    /// </summary>
    public partial class ClientViewerItem : UserControl
    {
        public Dates parentDates;
        public Sessions parentSessions;
        public Client client;
        public Classes.Users.User user;
        private ClientViewerItem()
        {
            InitializeComponent();
        }
        public ClientViewerItem(Dates parent,Client client):this()
        {
            this.parentDates = parent;
            this.client = client;
            lblId.Content = client.id.ToString();
            lblName.Content = client.name.ToString();
            lblPhone.Content = client.phone;
        }
        
        public ClientViewerItem(Dates parent,Classes.Users.User user) : this()
        {
            this.parentDates = parent;
            this.user = user;
            lblId.Content = user.id;
            lblName.Content = user.name;
            lblPhone.Content = user.phone;
        }
        public ClientViewerItem(Sessions parent, Client client) : this()
        {
            this.parentSessions = parent;
            this.client = client;
            lblId.Content = client.id.ToString();
            lblName.Content = client.name.ToString();
            lblPhone.Content = client.phone;
        }

        public ClientViewerItem(Sessions parent, Classes.Users.User user) : this()
        {
            this.parentSessions = parent;
            this.user = user;
            lblId.Content = user.id;
            lblName.Content = user.name;
            lblPhone.Content = user.phone;
        }
        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (client != null)
            {
                if (parentSessions == null)
                {
                    this.parentDates.selectedClient = client;
                    this.parentDates.txtClient.Text = client.name;
                    this.parentDates.ClientViewerStack.Children.Clear();
                    this.parentDates.btnAddDate.Background = (Brush)new BrushConverter().ConvertFromString(FindResource("foregroundColor").ToString());
                    this.parentDates.btnAddDate.Cursor = Cursors.Hand;
                    this.parentDates.btnAddDateName.Content = HClinic.Assets.Languages.Default.lblAddDate;
                    this.parentDates.btnAddDateIcon.Content = "\uf067";
                }
                else
                {
                    this.parentSessions.selectedClient = client;
                    this.parentSessions.txtClient.Text = client.name;
                    this.parentSessions.ClientViewerStack.Children.Clear();
                    this.parentSessions.btnSessionNew.Background = (Brush)new BrushConverter().ConvertFromString(FindResource("foregroundColor").ToString());
                    this.parentSessions.btnSessionNew.Cursor = Cursors.Hand;
                }
            }
            else
            {
                if (parentSessions == null)
                {
                    this.parentDates.selectedUser = user;
                    this.parentDates.txtUser.Text = user.name;
                    this.parentDates.UserViewerStack.Children.Clear();

                }
                else
                {
                    this.parentSessions.selectedUser = user;
                    this.parentSessions.txtUser.Text = user.name;
                    this.parentSessions.UserViewerStack.Children.Clear();
                }
            }
        }
    }
}
