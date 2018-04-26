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
    /// Interaction logic for SessionItem.xaml
    /// </summary>
    public partial class SessionItem : UserControl
    {
        Sessions parent;
        Session session;
        public SessionItem()
        {
            InitializeComponent();
        }
        public SessionItem(Sessions parent):this()
        {
            this.parent = parent;
        }
        public SessionItem(Sessions parent,Session session) : this(parent)
        {
            this.session = session;

            //Set variables
            lblClientName.Content = session.client.name;
            lblClientPhone.Content = session.client.phone;
            lblClientBirthday.Content = session.client.birthday.ToString("yyyy");
            lblClientGender.Content = session.client.isMale ? "Male" : "Female";

            lblSessionDate.Content = session.creation.ToString(App.Constants.DateFormat);
            lblSessionBloodPressure.Content = string.Format("{0} mmHg", session.BP);
            lblSessionNote.Content = session.note;
            lblSessionRBS.Content = string.Format("{0} mg/dl", session.RBS);
            lblSessionWeight.Content = string.Format("{0} KG", session.weight);

            lblDateDateTime.Content = session.lastDate.ToString(App.Constants.DateFormat);
            lblDateDateTime.Content = (lblDateDateTime.Content.ToString() == "01.01.2017" ? "None" : lblDateDateTime.Content);
            lblLastSession.Content = session.lastSession.ToString(App.Constants.DateFormat);

            lblSessionDiabetesType.Content = session.client.DiabetesType;
            dateNextDate.SelectedDateChanged -= dateNextDate_SelectedDateChanged;
            dateNextDate.Text = session.nextDate.ToString(App.Constants.DateTimeFormatForMySQL);
            dateNextDate.Text = (dateNextDate.Text == "2017-01-01" ? null : dateNextDate.Text);
            dateNextDate.IsEnabled = !session.isReported;
            dateNextDate.SelectedDateChanged += dateNextDate_SelectedDateChanged;

        }
        private void btnPrint_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnRemove_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (new HClinic.Windows.Confirm("","").ShowDialog() == true)
            {
                App.databasceConnection.query(string.Format("delete from tbl_sessions where tbl_sessions.id = '{0}'", session.id));
                parent.btnSearch_MouseLeftButtonUp(null, null);
            }
        }

        private void btnDocuments_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            new Windows.Clients.Documents(this.session, true).ShowDialog();
        }
        private void dateNextDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime date;
            if (dateNextDate.Text != string.Empty && DateTime.TryParse(dateNextDate.Text,out date))
            {
                App.databasceConnection.query(string.Format("delete from tbl_dates where tbl_dates.session_id = '{3}';insert into tbl_dates (datetime,client_id,user_id,session_id) values ('{1}','{0}','{2}','{3}');", session.client.id, date.ToString(App.Constants.DateTimeFormatForMySQL),App.currentUser.id,session.id));
            }
        }

        private void btnSlideAdditionalContent_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (gridContent.ColumnDefinitions[2].Width == new GridLength(0))
            {
                gridContent.ColumnDefinitions[2].Width = GridLength.Auto;
            }
            else
            {
                gridContent.ColumnDefinitions[2].Width = new GridLength(0);
            }
        }
    }
}
