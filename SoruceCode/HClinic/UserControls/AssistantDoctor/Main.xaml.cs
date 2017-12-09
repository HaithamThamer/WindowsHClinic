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

namespace HClinic.UserControls.AssistantDoctor
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        public Windows.Main parent;
        public HClinic.Classes.Clients.Session session;
        public Main(Windows.Main parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void btnApply_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (txtCardNumber.Text != string.Empty)
            {
                System.Data.DataTable dt = App.databasceConnection.query(string.Format("select tbl_clients.id as client_id, tbl_clients.name as client_name, tbl_clients.phone as client_phone, tbl_clients.job as client_job, tbl_clients.address as client_address, tbl_clients.birthday as client_birthday, tbl_clients.is_active as client_is_active, tbl_clients.is_male as client_is_male, tbl_clients.creation as client_creation, tbl_clients.user_id as client_user_id, tbl_sessions.id as session_is, tbl_sessions.client_id as session_client_id, tbl_sessions.user_id as session_user_id, tbl_sessions.card_number as session_card_number, tbl_sessions.creation as session_creation, tbl_sessions.blood_pressure_top as session_blood_pressure_top, tbl_sessions.blood_pressure_bottom as session_blood_pressure_bottom, tbl_sessions.sugar as session_sugar, tbl_sessions.weight as session_weight, tbl_sessions.note as session_note,ifnull((select tbl_dates.datetime from tbl_dates where tbl_dates.client_id = tbl_sessions.client_id order by tbl_dates.datetime desc limit 0,1),'2017-01-01 00:00:00') as date_last_datetime,getLastSessionDatetime(tbl_sessions.client_id,tbl_sessions.id) as session_last_datetime from tbl_clients, tbl_sessions where tbl_sessions.client_id = tbl_clients.id and tbl_sessions.card_number = '{0}' order by tbl_sessions.creation desc limit 0,1", txtCardNumber.Text));
                if (dt.Rows.Count > 0)
                {
                    session = new Classes.Clients.Session(
                        int.Parse(dt.Rows[0]["session_is"].ToString()),
                            new Classes.Clients.Client(
                                int.Parse(dt.Rows[0]["client_id"].ToString()),
                                dt.Rows[0]["client_name"].ToString(),
                                dt.Rows[0]["client_phone"].ToString(),
                                dt.Rows[0]["client_job"].ToString(),
                                dt.Rows[0]["client_address"].ToString(),
                                DateTime.Parse(dt.Rows[0]["client_birthday"].ToString()),
                                dt.Rows[0]["client_is_active"].ToString() == "True",
                                dt.Rows[0]["client_is_male"].ToString() == "True",
                                DateTime.Parse(dt.Rows[0]["client_creation"].ToString()),
                                new Classes.Users.User()
                                ),
                            new Classes.Users.User(),
                            int.Parse(dt.Rows[0]["session_card_number"].ToString()),
                            DateTime.Parse(dt.Rows[0]["session_creation"].ToString()),
                            int.Parse(dt.Rows[0]["session_blood_pressure_top"].ToString()),
                            int.Parse(dt.Rows[0]["session_blood_pressure_bottom"].ToString()),
                            int.Parse(dt.Rows[0]["session_sugar"].ToString()),
                            int.Parse(dt.Rows[0]["session_weight"].ToString()),
                            DateTime.Parse(dt.Rows[0]["date_last_datetime"].ToString()),
                            DateTime.Parse(dt.Rows[0]["session_last_datetime"].ToString()),
                            dt.Rows[0]["session_note"].ToString()
                        );

                    lblClientName.Content = session.client.name;
                    lblClientPhone.Content = session.client.phone;
                    lblClientJob.Content = session.client.job;
                    lblClientGender.Content = session.client.isMale ? "ذكر" : "أنثى";
                    lblClientBirthday.Content = session.client.birthday.ToString(App.Constants.DateFormat);

                    txtBloodPressureTop.Text = session.bloodPressureTop.ToString();
                    txtBloodPressureBottom.Text = session.bloodPressureBottom.ToString();
                    txtWeight.Text = session.weight.ToString();
                    txtSugar.Text = session.sugar.ToString();
                    txtNote.Text = session.note;

                    lastSessions.Children.Add(new SessionRow(this));
                    dt = App.databasceConnection.query(string.Format("select tbl_sessions.*,ifnull((select tbl_dates.datetime from tbl_dates where tbl_dates.client_id = tbl_sessions.client_id order by tbl_dates.datetime desc limit 0,1),'2017-01-01 00:00:00') as date_last_datetime,getLastSessionDatetime(tbl_sessions.client_id,tbl_sessions.id) as session_last_datetime from tbl_sessions where tbl_sessions.client_id = '{0}' order by tbl_sessions.creation desc", session.client.id));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lastSessions.Children.Add(new SessionRow(
                            this,
                            new Classes.Clients.Session(
                                int.Parse(dt.Rows[i]["id"].ToString()),
                                session.client,
                                new Classes.Users.User(),
                                int.Parse(dt.Rows[i]["card_number"].ToString()),
                                DateTime.Parse(dt.Rows[i]["creation"].ToString()),
                                int.Parse(dt.Rows[i]["blood_pressure_top"].ToString()),
                                int.Parse(dt.Rows[i]["blood_pressure_bottom"].ToString()),
                                int.Parse(dt.Rows[i]["sugar"].ToString()),
                                int.Parse(dt.Rows[i]["weight"].ToString()),
                                DateTime.Parse(dt.Rows[i]["date_last_datetime"].ToString()),
                                DateTime.Parse(dt.Rows[i]["session_last_datetime"].ToString()),
                                dt.Rows[i]["note"].ToString()
                                )
                            ));
                    }
                }
                else
                {
                    lastSessions.Children.Clear();
                    lblClientName.Content = "";
                    lblClientPhone.Content = "";
                    lblClientJob.Content = "";
                    lblClientGender.Content = "";
                    lblClientBirthday.Content = "";
                    txtCardNumber.Text = "";
                    txtBloodPressureBottom.Text = "";
                    txtBloodPressureTop.Text = "";
                    txtNote.Text = "";
                    txtSugar.Text = "";
                    txtWeight.Text = "";
                    session = null;
                    txtCardNumber.Focus();
                }
            }
        }

        private void txtCardNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnApply_MouseLeftButtonUp(null, null);
            }
        }

        private void btnUpdate_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            App.databasceConnection.query(string.Format(" " +
                "update tbl_sessions " +
                "set " +
                "blood_pressure_top = '{1}'," +
                "blood_pressure_bottom = '{2}'," +
                "sugar = '{3}'," +
                "weight = '{4}'," +
                "note = '{5}'" +
                " where tbl_sessions.id = '{0}'",
                session.id,
                txtBloodPressureTop.Text,
                txtBloodPressureBottom.Text,
                txtSugar.Text,
                txtWeight.Text,
                txtNote.Text));

            lastSessions.Children.Clear();
            lblClientName.Content = "";
            lblClientPhone.Content = "";
            lblClientJob.Content = "";
            lblClientGender.Content = "";
            lblClientBirthday.Content = "";
            txtCardNumber.Text = "";
            txtBloodPressureBottom.Text = "";
            txtBloodPressureTop.Text = "";
            txtNote.Text = "";
            txtSugar.Text = "";
            txtWeight.Text = "";
            session = null;
            txtCardNumber.Focus();
        }

        private void btnDocuments_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (session != null)
            {
                new Windows.Clients.Documents(this.session).ShowDialog();
            }
        }
    }
}
