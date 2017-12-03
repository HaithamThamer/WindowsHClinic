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
using HClinic.Classes.Clients;
namespace HClinic.UserControls.Clients
{
    /// <summary>
    /// Interaction logic for ManagerItem.xaml
    /// </summary>
    public partial class ManagerItem : UserControl
    {
        public Manager parent;
        public Client client;
        public ManagerItem(Manager parent,Client client)
        {
            InitializeComponent();
            this.parent = parent;
            this.client = client;
            if (client.id == 0)
            {
                lblName.Content = "أضافة";
                ClientTools.Children.Clear();
                ClientPhone.Children.Clear();
                ClientBirthday.Children.Clear();
                lblId.MouseLeftButtonUp += ManagerItemMain_MouseLeftButtonUp;
            }
            else
            {
                lblId.Content = client.id;
                lblId.FontSize = 12;
                lblName.Content = client.name;
                lblBirthday.Content = client.birthday.ToString(App.Constants.DateFormat);
                lblPhone.Content = client.phone;
                lblId.MouseLeftButtonUp += ManagerItemMain_MouseLeftButtonUp1;
            }
        }
        /// <summary>
        /// Update client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManagerItemMain_MouseLeftButtonUp1(object sender, MouseButtonEventArgs e)
        {
            new Windows.Clients.Add(this,int.Parse((sender as Label).Content.ToString())).ShowDialog();
        }
        /// <summary>
        /// Add new client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManagerItemMain_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            new Windows.Clients.Add(this).ShowDialog();
        }

        private void btnRemoveClient_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (new Windows.Confirm("","").ShowDialog() == true)
            {
                App.databasceConnection.query(string.Format("delete from tbl_clients where id = '{0}'", lblId.Content));
                parent.loadClients();
            }
        }
    }
}
