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
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : UserControl
    {
        Main parent;
        ManagerItem addClient;
        List<ManagerItem> clients;
        private Manager() { }
        public Manager(Main parent)
        {
            InitializeComponent();
            this.parent = parent;
            clients = new List<ManagerItem>();
            addClient = new ManagerItem(this,new Client());
            loadClients();
        }
        public void loadClients(string query = "")
        {
            ClientsManagerContent.Children.Clear();
            ClientsManagerContent.Children.Add(addClient);
            clients.Clear();
            System.Data.DataTable dt = App.databasceConnection.query(string.Format("select * from tbl_clients {0}",query == "" ? "" : "where id like '%"+ query + "%' or name like '%" + query + "%' or phone like '%" + query + "%' order by id asc"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                clients.Add(new ManagerItem(
                    this,
                    new Classes.Clients.Client(
                        int.Parse(dt.Rows[i]["id"].ToString()),
                        dt.Rows[i]["name"].ToString(),
                        dt.Rows[i]["phone"].ToString(),
                        dt.Rows[i]["job"].ToString(),
                        dt.Rows[i]["address"].ToString(),
                        DateTime.Parse(dt.Rows[i]["birthday"].ToString()),
                        bool.Parse(dt.Rows[i]["is_active"].ToString()),
                        DateTime.Parse(dt.Rows[i]["creation"].ToString()),
                        App.currentUser
                        )
                    ));
                ClientsManagerContent.Children.Add(clients[i]);
            }
        }
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.loadClients(txtSearch.Text);
        }
    }
}
