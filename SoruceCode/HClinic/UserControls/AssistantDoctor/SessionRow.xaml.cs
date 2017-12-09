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
    /// Interaction logic for SessionRow.xaml
    /// </summary>
    public partial class SessionRow : UserControl
    {
        Main parent;
        Classes.Clients.Session session;
        public SessionRow()
        {
            InitializeComponent();
        }
        public SessionRow(Main parent):this()
        {
            this.parent = parent;
        }
        public SessionRow(Main parent,Classes.Clients.Session session):this(parent)
        {
            this.session = session;
            lblSessionBloodPressure.Content = string.Format("{0} / {1}", session.bloodPressureTop, session.bloodPressureBottom);
            lblSessionDate.Content = session.creation.ToString(App.Constants.DateFormat);
            lblSessionSugar.Content = session.sugar;
            lblSessionWeight.Content = string.Format("{0} KG", session.weight);
        }

        private void btnDocuments_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (session != null)
            {
                new Windows.Clients.Documents(session).ShowDialog();
            }
        }
    }
}
