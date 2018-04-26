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
            foreach (var DiabetesType in Enum.GetValues(typeof(Classes.Clients.Client.DiabetesTypes)))
            {
                cmbClientDiabeteType.Items.Add(DiabetesType);
            }
            cmbClientDiabeteType.Text = "";
        }

        private void btnApply_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            

            if (txtIdentify.Text != string.Empty)
            {
                System.Data.DataTable dt = App.databasceConnection.query(string.Format("select tbl_clients.id as client_id, tbl_clients.name as client_name, tbl_clients.phone as client_phone, tbl_clients.job as client_job, tbl_clients.address as client_address, tbl_clients.birthday as client_birthday, tbl_clients.diabetesType as client_diabetesType, tbl_clients.language as client_language, tbl_clients.is_active as client_is_active, tbl_clients.is_male as client_is_male, tbl_clients.creation as client_creation, tbl_clients.user_id as client_user_id, tbl_sessions.id as session_is, tbl_sessions.client_id as session_client_id, tbl_sessions.user_id as session_user_id, tbl_sessions.card_number as session_card_number,tbl_sessions.creation as session_creation, tbl_sessions.BP as session_BP, tbl_sessions.RBS as session_RBS, tbl_sessions.PR as session_PR, tbl_sessions.HbAlC as session_HbAlC,tbl_sessions.weight as session_weight, tbl_sessions.note as session_note,ifnull((select tbl_dates.datetime from tbl_dates where tbl_dates.client_id = tbl_sessions.client_id order by tbl_dates.datetime desc limit 0,1),'2017-01-01 00:00:00') as date_last_datetime,getLastSessionDatetime(tbl_sessions.client_id,tbl_sessions.id) as session_last_datetime,ifnull((select tbl_dates.datetime from tbl_dates where tbl_dates.session_id = tbl_sessions.id),'2017-01-01 00:00:00') as `date_next_datetime`,ifnull((select tbl_dates.is_reported from tbl_dates where tbl_dates.session_id = tbl_sessions.id),'0') as `date_is_reported` from tbl_clients, tbl_sessions where tbl_sessions.client_id = tbl_clients.id and " + ((bool)btnIdentifyTitle.IsChecked ? " tbl_clients.id = '{0}'" : "tbl_sessions.card_number = '{0}'") +" order by tbl_sessions.creation desc limit 0,1", txtIdentify.Text));
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
                                (Classes.Clients.Client.DiabetesTypes)int.Parse(dt.Rows[0]["client_diabetesType"].ToString()),
                                (Classes.Clients.Client.Languages)int.Parse(dt.Rows[0]["client_language"].ToString()),
                                dt.Rows[0]["client_is_active"].ToString() == "True",
                                dt.Rows[0]["client_is_male"].ToString() == "True",
                                DateTime.Parse(dt.Rows[0]["client_creation"].ToString()),
                                new Classes.Users.User()
                                ),
                            new Classes.Users.User(),
                            int.Parse(dt.Rows[0]["session_card_number"].ToString()),
                            DateTime.Parse(dt.Rows[0]["session_creation"].ToString()),
                            dt.Rows[0]["session_BP"].ToString(),
                            double.Parse(dt.Rows[0]["session_RBS"].ToString()),
                            double.Parse(dt.Rows[0]["session_PR"].ToString()),
                            double.Parse(dt.Rows[0]["session_HbAlC"].ToString()),
                            int.Parse(dt.Rows[0]["session_weight"].ToString()),
                            DateTime.Parse(dt.Rows[0]["date_last_datetime"].ToString()),
                            DateTime.Parse(dt.Rows[0]["session_last_datetime"].ToString()),
                            DateTime.Parse(dt.Rows[0]["date_next_datetime"].ToString()),
                            dt.Rows[0]["date_is_reported"].ToString() == "1",
                            dt.Rows[0]["session_note"].ToString()
                        );

                    lblClientName.Content = session.client.name;
                    lblClientPhone.Content = session.client.phone;
                    lblClientJob.Content = session.client.job;
                    lblClientGender.Content = session.client.isMale ? "Male" : "Female";
                    lblClientBirthday.Content = session.client.birthday.ToString(App.Constants.DateFormat);
                    lblClientAddress.Content = session.client.address;
                    cmbClientDiabeteType.Text = session.client.DiabetesType.ToString();
                    

                    txtBloodPressure.Text = string.Format("{0}", session.BP); 
                    txtWeight.Text = session.weight.ToString();
                    txtRBS.Text = string.Format("{0}", session.RBS);
                    txtNote.Text = session.note;
                    txtPR.Text = session.PR.ToString();
                    txtHbAlC.Text = session.HbAlC.ToString();
                    txtRBS.Text = session.RBS.ToString();

                    lastSessions.Children.Clear();
                    lastSessions.Children.Add(new SessionRow(this));
                    dt = App.databasceConnection.query(string.Format("select tbl_sessions.*,ifnull((select tbl_dates.datetime from tbl_dates where tbl_dates.client_id = tbl_sessions.client_id order by tbl_dates.datetime desc limit 0,1),'2017-01-01 00:00:00') as date_last_datetime,getLastSessionDatetime(tbl_sessions.client_id,tbl_sessions.id) as session_last_datetime,ifnull((select tbl_dates.datetime from tbl_dates where tbl_dates.session_id = tbl_sessions.id),'2017-01-01 00:00:00') as `date_next_datetime`,ifnull((select tbl_dates.is_reported from tbl_dates where tbl_dates.session_id = tbl_sessions.id),'0') as `date_is_reported` from tbl_sessions where tbl_sessions.client_id = '{0}' order by tbl_sessions.creation desc", session.client.id));
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
                                dt.Rows[i]["BP"].ToString(),
                                double.Parse(dt.Rows[i]["RBS"].ToString()),
                                double.Parse(dt.Rows[i]["PR"].ToString()),
                                double.Parse(dt.Rows[i]["HbAlC"].ToString()),
                                int.Parse(dt.Rows[i]["weight"].ToString()),
                                DateTime.Parse(dt.Rows[i]["date_last_datetime"].ToString()),
                                DateTime.Parse(dt.Rows[i]["session_last_datetime"].ToString()),
                                DateTime.Parse(dt.Rows[i]["date_next_datetime"].ToString()),
                                dt.Rows[i]["date_is_reported"].ToString() == "1",
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
                    lblClientAddress.Content = "";
                    cmbClientDiabeteType.Text = "";
                    txtIdentify.Text = "";
                    txtPR.Text = "";
                    txtBloodPressure.Text = "";
                    txtNote.Text = "";
                    txtHbAlC.Text = "";
                    txtRBS.Text = "";
                    txtWeight.Text = "";
                    session = null;
                    txtIdentify.Focus();
                }
            }
        }

        private void txtIdentify_KeyDown(object sender, KeyEventArgs e)
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
                "BP = '{1}'," +
                "PR = '{2}'," +
                "RBS = '{3}'," +
                "weight = '{4}'," +
                "note = '{5}'," +
                "HbAlC = '{6}'" +
                " where tbl_sessions.id = '{0}'",
                session.id,
                txtBloodPressure.Text,
                txtPR.Text,
                txtRBS.Text,
                txtWeight.Text,
                txtNote.Text,
                txtHbAlC.Text));
            if (session.client.DiabetesType.ToString() != cmbClientDiabeteType.Text)
            {
                Classes.Clients.Client.DiabetesTypes DiabetesType;
                Enum.TryParse<Classes.Clients.Client.DiabetesTypes>(cmbClientDiabeteType.Text, out DiabetesType);
                App.databasceConnection.query(string.Format("update tbl_clients set diabetesType = '{0}' where tbl_clients.id = '{1}'", (int)DiabetesType, session.client.id));
            }
            lastSessions.Children.Clear();
            lblClientName.Content = "";
            lblClientPhone.Content = "";
            lblClientJob.Content = "";
            lblClientGender.Content = "";
            lblClientBirthday.Content = "";
            txtIdentify.Text = "";
            txtPR.Text = "";
            txtHbAlC.Text = "";
            txtBloodPressure.Text = "";
            txtNote.Text = "";
            txtRBS.Text = "";
            txtWeight.Text = "";
            session = null;
            cmbClientDiabeteType.Text = "";
            lblClientAddress.Content = "";
            txtIdentify.Focus();
        }

        private void btnDocuments_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (session != null)
            {
                new Windows.Clients.Documents(this.session).ShowDialog();
            }
        }

        private void lblIdentifyTitle_Checked(object sender, RoutedEventArgs e)
        {
            btnIdentifyTitle.Content = HClinic.Assets.Languages.Default.btnIdentifyClientTitle;
        }

        private void lblIdentifyTitle_Unchecked(object sender, RoutedEventArgs e)
        {
            btnIdentifyTitle.Content = HClinic.Assets.Languages.Default.btnIdentifySessionCardNumberTitle;
        }
    }
}
