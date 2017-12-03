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
            lblClientBirthday.Content = session.client.birthday.ToString(App.Constants.DateFormat);

            lblSessionDate.Content = session.creation.ToString(App.Constants.DateFormat);
            lblSessionBloodPressure.Content = string.Format("{0} Top , {1} Bottom", session.bloodPressureTop, session.bloodPressureBottom);
            lblSessionNote.Content = session.note;
            lblSessionSugar.Content = string.Format("{0}", session.sugar);
            lblSessionWeight.Content = string.Format("{0} KG", session.weight);
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
    }
}
