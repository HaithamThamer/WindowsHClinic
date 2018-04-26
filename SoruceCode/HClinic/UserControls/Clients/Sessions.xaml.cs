using DevExpress.Xpf.Printing;
using DevExpress.XtraReports.UI;
using HClinic.Classes.Clients;
using HClinic.Classes.Users;
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
    /// Interaction logic for Sessions.xaml
    /// </summary>
    public partial class Sessions : UserControl
    {
        Main parent;
        public Client selectedClient = null;
        public User selectedUser = null;
        public Date selectedDate = null;
        public List<SessionItem> sessions;
        private Sessions()
        {
            InitializeComponent();
            sessions = new List<SessionItem>();
            dateSessionFrom.Text = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            dateSessionTo.Text = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
        }
        public Sessions(Main parent):this()
        {
            this.parent = parent;
            SessionsContent.Children.Add(new SessionItem(this));
            btnSearch_MouseLeftButtonUp(null, null);
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
                System.Data.DataTable dt = App.databasceConnection.query(string.Format("select * from tbl_clients where id like '%{0}%' OR name like '%{0}%' OR phone like '%{0}%'", txtClient.Text));
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
                        (Client.Languages)int.Parse(dt.Rows[i]["language"].ToString()),
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
                        dt.Rows[0]["name"].ToString(),
                        dt.Rows[0]["password"].ToString(),
                        dt.Rows[0]["email"].ToString(),
                        dt.Rows[0]["phone"].ToString(),
                        (App.Tables.Users.UserTypes)int.Parse(dt.Rows[0]["type"].ToString())
                        )));
                }
            }
        }
        public void loadSessions(DateTime sessionFrom,DateTime sessionTo,string client = "",string user = "")
        {
            SessionsContent.Children.Clear();
            sessions.Clear();
            System.Data.DataTable dt = App.databasceConnection.query(string.Format("select tbl_sessions.id as session_id, tbl_sessions.client_id as session_client_id,  tbl_sessions.user_id as session_user_id,  tbl_sessions.card_number as session_card_number, tbl_sessions.HbAlC as session_HbAlC, tbl_sessions.RBS as session_RBS, tbl_sessions.PR as session_PR, tbl_sessions.BP as session_BP,tbl_sessions.weight as session_weight,tbl_sessions.note as session_note,tbl_sessions.creation as session_creation,tbl_clients.id as client_id,tbl_clients.name as client_name,tbl_clients.phone as client_phone,tbl_clients.job as client_job,tbl_clients.address as client_address,tbl_clients.birthday as client_birthday,tbl_clients.diabetesType as client_diabetes_types,tbl_clients.diabetesType as client_language,tbl_clients.is_active as client_is_active,tbl_clients.is_male as client_is_male,tbl_clients.creation as client_creation,tbl_clients.user_id as client_user_id,tbl_users.id as user_id,tbl_users.name as user_name,tbl_users.password as user_password, tbl_users.email as user_email, tbl_users.phone as user_phone, tbl_users.`type` as user_type, ifnull((select tbl_dates.datetime from tbl_dates where tbl_dates.client_id = tbl_sessions.client_id order by tbl_dates.datetime desc limit 0,1),'2017-01-01 00:00:00') as date_last_datetime,getLastSessionDatetime(tbl_sessions.client_id,tbl_sessions.id) as session_last_datetime,ifnull((select tbl_dates.datetime from tbl_dates where tbl_dates.session_id = tbl_sessions.id),'2017-01-01 00:00:00') as `date_next_datetime`,ifnull((select tbl_dates.is_reported from tbl_dates where tbl_dates.session_id = tbl_sessions.id),'0') as `date_is_reported` from tbl_sessions,tbl_clients,tbl_users where tbl_sessions.client_id = tbl_clients.id and tbl_sessions.user_id = tbl_users.id and tbl_sessions.creation between '{0}' and '{1}' {2} {3} order by tbl_sessions.id desc", sessionFrom.ToString("yyyy-MM-dd 00:00:00"), sessionTo.ToString("yyyy-MM-dd 23:59:59"), client == "" ? "" : " and tbl_clients.name = '" + client + "' ", user == "" ? "" : " and tbl_users.name = '" + user + "' "));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sessions.Add(new SessionItem(
                    this,
                    new Session(
                        int.Parse(dt.Rows[i]["session_id"].ToString()),
                        new Client(
                            int.Parse(dt.Rows[i]["client_id"].ToString()),
                            dt.Rows[i]["client_name"].ToString(),
                            dt.Rows[i]["client_phone"].ToString(),
                            dt.Rows[i]["client_job"].ToString(),
                            dt.Rows[i]["client_address"].ToString(),
                            DateTime.Parse(dt.Rows[i]["client_birthday"].ToString()),
                            (Client.DiabetesTypes)int.Parse(dt.Rows[i]["client_diabetes_types"].ToString()),
                            (Client.Languages)int.Parse(dt.Rows[i]["client_language"].ToString()),
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
                            ),
                        int.Parse(dt.Rows[i]["session_card_number"].ToString()),
                        DateTime.Parse(dt.Rows[i]["session_creation"].ToString()),
                        dt.Rows[i]["session_BP"].ToString(),
                        double.Parse(dt.Rows[i]["session_RBS"].ToString()),
                        double.Parse(dt.Rows[i]["session_PR"].ToString()),
                        double.Parse(dt.Rows[i]["session_HbAlC"].ToString()),
                        int.Parse(dt.Rows[i]["session_weight"].ToString()),
                        DateTime.Parse(dt.Rows[i]["date_last_datetime"].ToString()),
                        DateTime.Parse(dt.Rows[i]["session_last_datetime"].ToString()),
                        DateTime.Parse(dt.Rows[i]["date_next_datetime"].ToString()),
                        dt.Rows[i]["date_is_reported"].ToString() == "1",
                        dt.Rows[i]["session_note"].ToString()
                        )
                    ));
                SessionsContent.Children.Add(sessions[i]);
            }
        }
        public void btnSearch_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            loadSessions(DateTime.Parse(dateSessionFrom.Text), DateTime.Parse(dateSessionTo.Text), txtClient.Text, txtUser.Text);
        }

        private void btnSessionNew_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (selectedClient == null)
            {
                new HClinic.Windows.Confirm("", "").ShowDialog();
            }
            else
            {
                int lastCardNumber;
                System.Data.DataTable dt = App.databasceConnection.query(string.Format("select card_number,creation from tbl_sessions order by creation desc limit 0,1"));
                if (dt.Rows.Count == 0)
                {
                    lastCardNumber = 1;
                }
                else
                {
                    if (DateTime.Now.ToString("yyyy-MM-dd 00:00:00") == DateTime.Parse(dt.Rows[0]["creation"].ToString()).ToString("yyyy-MM-dd 00:00:00"))
                    {
                        lastCardNumber = int.Parse(dt.Rows[0]["card_number"].ToString()) + 1;
                    }
                    else
                    {
                        lastCardNumber = 1;
                    }
                }
                App.databasceConnection.queryNonReader(string.Format("insert into tbl_sessions (client_id,user_id,card_number) values ('{0}','{1}','{2}');", selectedClient.id, App.currentUser.id, lastCardNumber));
                new ReportPrintTool(new Reports.SessionTicket(lastCardNumber,selectedClient)).Print();
                btnSessionNew.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2d2d2d"));
                btnSessionNew.Cursor = Cursors.Arrow;
                selectedClient = null;
                txtClient.Text = "";
                dateSessionFrom.Text = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
                dateSessionTo.Text = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
                btnSearch_MouseLeftButtonUp(null, null);
            }
        }
    }
}
