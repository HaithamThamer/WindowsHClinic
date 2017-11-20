using System;
using System.Collections.Generic;
using System.Data;
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

namespace HClinic.Windows.UserControls.ClientsControls
{
    /// <summary>
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : UserControl
    {
        List<ClientBox> clients;
        UIElement addClient;
        public Manager()
        {
            InitializeComponent();
            clients = new List<ClientBox>();
            addClient = ClientsContent.Children[0];
            loadClients();
            
            //clients.Add(new ClientBox("احمد احمد", 11, DateTime.Now, DateTime.Now));
            
        }
        private void btnClientAdd_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            new SubWindows.ClientsWindows.Add().ShowDialog();
            loadClients();
        }
        public void loadClients()
        {
            clients.Clear();
            ClientsContent.Children.Clear();
            ClientsContent.Children.Add(addClient);
            DataTable dt = App.databasceConnection.query(string.Format(string.Format("SELECT * FROM tbl_clients")));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                clients.Add(new ClientBox(
                    dt.Rows[i]["name"].ToString(),
                    int.Parse(dt.Rows[i]["id"].ToString()),
                    DateTime.Parse(dt.Rows[i]["birthday"].ToString()),
                    DateTime.Parse(dt.Rows[i]["birthday"].ToString())
                    ));
                ClientsContent.Children.Add(clients[i]);
            }
        }
    }
}
