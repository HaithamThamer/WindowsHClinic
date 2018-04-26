using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO.Ports;
using System.IO;
using System.Windows.Media.Animation;

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
        Classes.Clients.Date date;
        Classes.Clients.Client client;
        public Main()
        {
            InitializeComponent();
            //sp = new SerialPort();
            //sp.PortName = "COM9";
            //sp.BaudRate = 921600;
            //sp.ReadTimeout = 2000;
            //sp.DataReceived += Sp_DataReceived;
            //////String strE = UnicodeStr2HexStr("+9647703867142");
            //sp.Open();

            //sp.Write("AT\r");
            //Thread.Sleep(500);
            //sp.Write("AT+CMGF=1\r");
            //Thread.Sleep(500);
            //sp.Write("AT+CSCS=\"IRA\"\r");
            //Thread.Sleep(500);
            //sp.Write("AT+CMGS=\""+ "+9647703867142" + "\"\r\n");
            //Thread.Sleep(500);
            //sp.Write("New2gg2" + "\x1A");
            //Thread.Sleep(500);
            ////sp.Write("AT\r");
            ////Thread.Sleep(1500);
            //sp.Close();
            //MessageBox.Show("end");
            //string uToH = UnicodeStr2HexStr("+9647703867142").Replace("00","").ToLower();
            //string b = UnicodeStr2HexStr("Hi السلام عليكم").Replace("00", "").ToLower();
            //string d = UnicodeStr2HexStr("+9647703867142").Replace("00", "").ToLower();

        }

        //private void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    MessageBox.Show(sp.ReadExisting());
        //}

        //================> Used to encoding GSM message as UCS2
        public static String UnicodeStr2HexStr(String strMessage)
        {
            byte[] ba = Encoding.BigEndianUnicode.GetBytes(strMessage);
            String strHex = BitConverter.ToString(ba);
            strHex = strHex.Replace("-", "");
            return strHex;
        }

        public static String HexStr2UnicodeStr(String strHex)
        {
            byte[] ba = HexStr2HexBytes(strHex);
            return HexBytes2UnicodeStr(ba);
        }

        //================> Used to decoding GSM UCS2 message  
        public static String HexBytes2UnicodeStr(byte[] ba)
        {
            var strMessage = Encoding.BigEndianUnicode.GetString(ba, 0, ba.Length);
            return strMessage;
        }

        public static byte[] HexStr2HexBytes(String strHex)
        {
            strHex = strHex.Replace(" ", "");
            int nNumberChars = strHex.Length / 2;
            byte[] aBytes = new byte[nNumberChars];
            using (var sr = new StringReader(strHex))
            {
                for (int i = 0; i < nNumberChars; i++)
                    aBytes[i] = Convert.ToByte(new String(new char[2] { (char)sr.Read(), (char)sr.Read() }), 16);
            }
            return aBytes;
        }
        private void DispatcherTimerDates_Tick(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                //the message will not be send in friday.
                //the dates of saturday will send in thurssday
                System.Data.DataTable dt = App.databasceConnection.query(string.Format("SELECT tbl_dates.id as date_id,tbl_dates.datetime as date_datetime,tbl_dates.is_reported as date_is_reported,tbl_dates.client_id as date_client_id,tbl_dates.creation as date_creation,tbl_dates.user_id as date_user_id,tbl_clients.id as client_id,tbl_clients.name as client_name,tbl_clients.phone as client_phone,tbl_clients.job as client_job,tbl_clients.address as client_address,tbl_clients.birthday as client_birthday,tbl_clients.diabetesType as client_diabete_type,tbl_clients.is_active as client_is_active,tbl_clients.is_male as client_is_male,tbl_clients.creation as client_creation,tbl_clients.user_id as client_user_id,tbl_clients.`language` as client_language FROM tbl_dates,tbl_clients WHERE tbl_dates.client_id = tbl_clients.id and is_reported = 0 and datetime between '{0}' and '{1}'", DateTime.Now.AddDays(DateTime.Now.AddDays(1).DayOfWeek == DayOfWeek.Friday ? 2 : 1).ToString("yyyy-MM-dd 00:00:00"), DateTime.Now.AddDays(DateTime.Now.AddDays(1).DayOfWeek == DayOfWeek.Friday ? 2 : 1).ToString("yyyy-MM-dd 23:59:59")));
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    date = new Classes.Clients.Date(
                        int.Parse(row["date_id"].ToString()),
                        DateTime.Parse(row["date_datetime"].ToString()),
                        bool.Parse(row["date_is_reported"].ToString()),
                        int.Parse(row["date_client_id"].ToString()),
                        int.Parse(row["date_user_id"].ToString()),
                        DateTime.Parse(row["date_creation"].ToString())
                        );
                    client = new Classes.Clients.Client(
                        int.Parse(row["client_id"].ToString()),
                        row["client_name"].ToString(),
                        row["client_phone"].ToString(),
                        row["client_job"].ToString(),
                        row["client_address"].ToString(),
                        DateTime.Parse(row["client_birthday"].ToString()),
                        (Classes.Clients.Client.DiabetesTypes)int.Parse(row["client_diabete_type"].ToString()),
                        (Classes.Clients.Client.Languages)int.Parse(row["client_language"].ToString()),
                        bool.Parse(row["client_is_active"].ToString()),
                        bool.Parse(row["client_is_male"].ToString()),
                        DateTime.Parse(row["client_creation"].ToString()),
                        App.currentUser
                        );
                    this.Dispatcher.Invoke(() => {
                        lblStatus.Content = "will send message as date for [" + client.name + " (" + client.phone + ")] in " + client.language + " at " + date.datetime.ToString(App.Constants.DateFormat);
                    });

                    App.databasceConnection.queryNonReader(string.Format("update tbl_dates set is_reported = '1' where tbl_dates.id = '{0}'", date.id));
                }
                this.Dispatcher.Invoke(()=> {
                    //lblStatus.Content = string.Format(Assets.Languages.Default.lblDoneSentToAll, dt.Rows.Count);
                });
            }).Start();
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

            //set time and date every 1 sec. 
            System.Windows.Threading.DispatcherTimer dispatherTierClock = new System.Windows.Threading.DispatcherTimer();
            dispatherTierClock.Tick += DispatcherTimer_Tick;
            dispatherTierClock.Interval = new TimeSpan(0, 0, 1);
            dispatherTierClock.Start();

            //check send report of date every 1 hour and 1 sec. 
            System.Windows.Threading.DispatcherTimer dispatcherTimerDates = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimerDates.Tick += DispatcherTimerDates_Tick; ;
            dispatcherTimerDates.Interval = new TimeSpan(0, 1, 1);
            dispatcherTimerDates.Start();
            DispatcherTimerDates_Tick(null, null); // check with startup app.
        }

        private void btnHome_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            MainContent.Children.Clear();
            if (homeMain == null)
            {
                homeMain = new UserControls.Home.Main(this);
            }
            MainContent.Children.Add(homeMain);
            gridMain.ColumnDefinitions[0].Width = new GridLength(200);
            btnSlide_MouseLeftButtonUp(null, null);
        }

        private void btnClients_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainContent.Children.Clear();
            if (clientMain == null)
            {
                clientMain = new UserControls.Clients.Main(this);
            }
            MainContent.Children.Add(clientMain);
            gridMain.ColumnDefinitions[0].Width = new GridLength(200);
            btnSlide_MouseLeftButtonUp(null, null);
        }

        private void btnSettings_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainContent.Children.Clear();
            if (settingsMain == null)
            {
                settingsMain = new UserControls.Settings.Main(this);
            }
            MainContent.Children.Add(settingsMain);
            gridMain.ColumnDefinitions[0].Width = new GridLength(200);
            btnSlide_MouseLeftButtonUp(null, null);
        }

        private void btnAssistantDoctor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainContent.Children.Clear();
            if (assistantDoctorMain == null)
            {
                assistantDoctorMain = new UserControls.AssistantDoctor.Main(this);
            }
            MainContent.Children.Add(assistantDoctorMain);
            gridMain.ColumnDefinitions[0].Width = new GridLength(200);
            btnSlide_MouseLeftButtonUp(null, null);
        }

        private void btnDoctor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            gridMain.ColumnDefinitions[0].Width = new GridLength(200);
            btnSlide_MouseLeftButtonUp(null, null);
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

        private void btnSlide_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            if (gridMain.ColumnDefinitions[0].Width == new GridLength(200))
            {
                gridMain.ColumnDefinitions[0].Width = new GridLength(60);
            }
            else
            {
                gridMain.ColumnDefinitions[0].Width = new GridLength(200);
            }
        }
    }
}
