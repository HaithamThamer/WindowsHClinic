
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
using HClinic.Classes.Users;
namespace HClinic.UserControls.Clients
{
    /// <summary>
    /// Interaction logic for Dates.xaml
    /// </summary>
    public partial class Dates : UserControl
    {
        Main parent;
        DateItem header;
        List<DateItem> dates;
        public Client selectedClient = null;
        public User selectedUser = null;
        public Date selectedDate = null;
        private Dates() { }
        public Dates(Main parent)
        {
            InitializeComponent();
            this.parent = parent;
            header = new DateItem(this);
            //header = new DateItem(this, 0, false, DateTime.Now, DateTime.Now, "", "");
            dates = new List<DateItem>();
            DatesContent.Children.Add(header);
            btnSearch_MouseLeftButtonUp(null, null);
            dateFrom.SelectedDateChanged += DateFrom_SelectedDateChanged;
            dateTo.SelectedDateChanged += DateTo_SelectedDateChanged;

            chkAll.Checked += ChkAll_Checked;
            chkAll.Unchecked += ChkAll_Unchecked;
            chkDelivered.Checked += ChkDelivered_Checked;
            chkDelivered.Unchecked += ChkDelivered_Unchecked;
            chkNonDelivered.Checked += ChkNonDelivered_Checked;
            chkNonDelivered.Unchecked += ChkNonDelivered_Unchecked;
        }

        private void ChkNonDelivered_Unchecked(object sender, RoutedEventArgs e)
        {
            if ((bool)!chkAll.IsChecked)
            {
                chkDelivered.IsChecked = true;
            }
            
        }

        private void ChkNonDelivered_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)!chkAll.IsChecked)
            {
                chkDelivered.IsChecked = false;
            }
            
        }

        private void ChkDelivered_Unchecked(object sender, RoutedEventArgs e)
        {
            if ((bool)!chkAll.IsChecked)
            {
                chkNonDelivered.IsChecked = true;
            }
            
        }

        private void ChkDelivered_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)!chkAll.IsChecked)
            {
                chkNonDelivered.IsChecked = false;
            }
            
        }

        private void ChkAll_Unchecked(object sender, RoutedEventArgs e)
        {
            chkDelivered.IsChecked = chkNonDelivered.IsChecked = false;
            chkDelivered.IsEnabled = chkNonDelivered.IsEnabled = true;
            btnSearch_MouseLeftButtonUp(null, null);
        }

        private void ChkAll_Checked(object sender, RoutedEventArgs e)
        {
            chkDelivered.IsChecked = chkNonDelivered.IsChecked = true;
            chkDelivered.IsEnabled = chkNonDelivered.IsEnabled = false;
            btnSearch_MouseLeftButtonUp(null, null);
        }

        private void DateTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSearch_MouseLeftButtonUp(null, null);
        }

        private void DateFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSearch_MouseLeftButtonUp(null, null);
        }

        public void loadDates(DateTime from , DateTime to,string client = "",string user = "")
        {
            DatesContent.Children.Clear();
            dates.Clear();
            DatesContent.Children.Add(header);
            //System.Data.DataTable dt = App.databasceConnection.query(string.Format("select tbl_dates.id,tbl_dates.is_reported,tbl_dates.creation,tbl_dates.datetime,tbl_clients.name,tbl_clients.phone from tbl_clients,tbl_dates,tbl_users where tbl_clients.id = tbl_dates.client_id and tbl_dates.user_id = tbl_users.id and tbl_dates.datetime between '{0}' and '{1}' {2} {3} {4}", from.ToString("yyyy-MM-dd 00:00:00"), to.ToString("yyyy-MM-dd 23:59:59"), client == string.Empty ? "" : " and tbl_clients.name = '" + client + "'", user == string.Empty ? "" : " and tbl_users.name = '" + user + "' ", (bool)chkAll.IsChecked ? "" : " and tbl_dates.is_reported = '" + ((bool)chkDelivered.IsChecked ? "1" : "0") + "'"));
            System.Data.DataTable dt = App.databasceConnection.query(string.Format("select tbl_dates.id as date_id, tbl_dates.is_reported as date_is_reported, tbl_dates.creation as date_creation, tbl_dates.datetime as date_datetime, tbl_dates.user_id as date_user_id, tbl_dates.client_id as date_client_id, tbl_clients.id as client_id, tbl_clients.name as client_name, tbl_clients.phone as client_phone, tbl_clients.job as client_job, tbl_clients.address as client_address, tbl_clients.birthday as client_birthday, tbl_clients.diabetesType as client_diabetesType, tbl_clients.is_active as client_is_active, tbl_clients.is_male as client_is_male, tbl_clients.creation as client_creation, tbl_clients.user_id as client_user_id, tbl_users.id as user_id, tbl_users.name as user_name, tbl_users.password as user_password, tbl_users.email as user_email, tbl_users.phone as user_phone, tbl_users.`type` as user_type from tbl_clients, tbl_dates, tbl_users where tbl_dates.client_id = tbl_clients.id and tbl_dates.user_id = tbl_users.id and tbl_dates.datetime between '{0}' and '{1}' {2} {3} {4}", from.ToString("yyyy-MM-dd 00:00:00"), to.ToString("yyyy-MM-dd 23:59:59"), client == string.Empty ? "" : " and tbl_clients.name = '" + client + "'", user == string.Empty ? "" : " and tbl_users.name = '" + user + "' ", (bool)chkAll.IsChecked ? "" : " and tbl_dates.is_reported = '" + ((bool)chkDelivered.IsChecked ? "1" : "0") + "'"));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //dates.Add(new DateItem(
                //    this,
                //    int.Parse(dt.Rows[i]["id"].ToString()),
                //    bool.Parse(dt.Rows[i]["is_reported"].ToString()),
                //    DateTime.Parse(dt.Rows[i]["creation"].ToString()),
                //    DateTime.Parse(dt.Rows[i]["datetime"].ToString()),
                //    dt.Rows[i]["name"].ToString(),
                //    dt.Rows[i]["phone"].ToString()
                //    ));
                dates.Add(new DateItem(
                    this,
                    new Date(
                        int.Parse(dt.Rows[i]["date_id"].ToString()),
                        DateTime.Parse(dt.Rows[i]["date_datetime"].ToString()),
                        dt.Rows[i]["date_is_reported"].ToString() == "True",
                        int.Parse(dt.Rows[i]["date_client_id"].ToString()),
                        int.Parse(dt.Rows[i]["date_user_id"].ToString()),
                        DateTime.Parse(dt.Rows[i]["date_creation"].ToString())
                        ),
                    new Client(
                        int.Parse(dt.Rows[i]["client_id"].ToString()),
                        dt.Rows[i]["client_name"].ToString(),
                        dt.Rows[i]["client_phone"].ToString(),
                        dt.Rows[i]["client_job"].ToString(),
                        dt.Rows[i]["client_address"].ToString(),
                        DateTime.Parse(dt.Rows[i]["client_birthday"].ToString()),
                        (Client.DiabetesTypes)int.Parse(dt.Rows[i]["client_diabetesType"].ToString()),
                        dt.Rows[i]["client_is_active"].ToString() == "True",
                        dt.Rows[i]["client_is_male"].ToString() == "True",
                        DateTime.Parse(dt.Rows[i]["client_creation"].ToString()),
                        new User()
                        ),
                    new User(
                        int.Parse(dt.Rows[i]["user_id"].ToString()),
                        dt.Rows[i]["user_name"].ToString(),
                        dt.Rows[i]["user_password"].ToString(),
                        dt.Rows[i]["user_email"].ToString(),
                        dt.Rows[i]["user_phone"].ToString(),
                        (App.Tables.Users.UserTypes)int.Parse(dt.Rows[i]["user_type"].ToString())
                        )
                    ));
                DatesContent.Children.Add(dates[i]);
            }
        }

        public void btnSearch_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (dateFrom != null && dateTo != null)
            {
                loadDates(DateTime.Parse(dateFrom.Text), DateTime.Parse(dateTo.Text));
            }
        }

        private void txtClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            //ignore non clients
            //if (selectedClient != null)
            //    return;

            //clear all clients from before query
            ClientViewerStack.Children.Clear();

            //hidden Cleints Viewer when empty txtClient
            if (txtClient.Text == string.Empty)
            {
                ClientViewerStack.Visibility = Visibility.Hidden;
            }
            else
            {
                ClientViewerStack.Visibility = Visibility.Visible;
                System.Data.DataTable dt = App.databasceConnection.query(string.Format("select * from tbl_clients where id like '%{0}%' OR name like '%{0}%' OR phone like '%{0}%'",txtClient.Text));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClientViewerStack.Children.Add(new ClientViewerItem(this, new Client(
                        int.Parse(dt.Rows[i]["id"].ToString()),
                        dt.Rows[i]["name"].ToString(),
                        dt.Rows[i]["phone"].ToString(),
                        dt.Rows[i]["job"].ToString(),
                        dt.Rows[i]["address"].ToString(),
                        DateTime.Parse(dt.Rows[i]["birthday"].ToString()),
                        (Client.DiabetesTypes)int.Parse(dt.Rows[i]["diabetesType"].ToString()),
                        dt.Rows[i]["is_active"].ToString() == "True",
                        dt.Rows[i]["is_male"].ToString() == "True",
                        DateTime.Parse(dt.Rows[i]["creation"].ToString()),
                        new User()
                        )));
                }
            }
        }

        private void txtUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserViewerStack.Children.Clear();
            if (txtUser.Text == string.Empty)
            {
                UserViewerStack.Visibility = Visibility.Hidden;
            }
            else
            {
                UserViewerStack.Visibility = Visibility.Visible;
                System.Data.DataTable dt = App.databasceConnection.query(string.Format("select * from tbl_users where id like '%{0}%' OR name like '%{0}%' OR '%{0}%';", txtUser.Text));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UserViewerStack.Children.Add(new ClientViewerItem(this, new User(
                        int.Parse(dt.Rows[0]["id"].ToString()),
                        dt.Rows[i]["name"].ToString(),
                        dt.Rows[i]["password"].ToString(),
                        dt.Rows[i]["email"].ToString(),
                        dt.Rows[i]["phone"].ToString(),
                        (App.Tables.Users.UserTypes)int.Parse(dt.Rows[0]["type"].ToString())
                        )));
                }
            }
        }

        private void btnAddDate_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (btnAddDate.Cursor == Cursors.Hand && selectedClient != null)
            {
                App.databasceConnection.query(string.Format("insert into tbl_dates (datetime,client_id,user_id) values ('{0}','{1}','{2}');", DateTime.Parse(dateAdd.Text).ToString(App.Constants.DateTimeFormatForMySQL), selectedClient.id, App.currentUser.id));
                btnSearch_MouseLeftButtonUp(null, null);
                selectedClient = null;
                selectedDate = null;
                selectedUser = null;
                btnAddDate.Cursor = Cursors.Arrow;
                btnAddDate.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2d2d2d"));
                btnAddDateName.Content = HClinic.Assets.Languages.Default.lblAddDate;
                btnAddDateIcon.Content = "\uf067";
                txtClient.Text = "";
                txtUser.Text = "";
            }
            else
            {
                new HClinic.Windows.Confirm("خطأ", "لم يتم اختيار المراجع").ShowDialog(); ;
            }
        }
    }
}
