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
    /// Interaction logic for DateItem.xaml
    /// </summary>
    public partial class DateItem : UserControl
    {
        public Dates parent;
        public Date date;
        public Client client;
        public User user;
        int dateId = 0;

        private DateItem()
        {
            InitializeComponent();
        }
        public DateItem(Dates parent):this()
        {
            this.parent = parent;
            
        }
        public DateItem(Dates parent,Date date,Client client,User user):this(parent)
        {
            this.date = date;
            this.client = client;
            this.user = user;
            lblClientName.Content = client.name;
            lblClientPhone.Content = client.phone;
            lblDateCreation.Content = date.creation.ToString(App.Constants.DateFormat);
            lblDatetime.Content = date.datetime.ToString(App.Constants.DateFormat);
            if (date.isReported)
            {
                lblClientName.Foreground = lblClientPhone.Foreground = lblDateCreation.Foreground = lblDatetime.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#ccc");
            }
            btnRemove.Cursor = btnUpdate.Cursor = Cursors.Hand;
            lblClientName.FontWeight = lblClientPhone.FontWeight = lblDateCreation.FontWeight = lblDatetime.FontWeight = FontWeights.Regular;
            lblClientName.HorizontalContentAlignment =  lblClientPhone.HorizontalAlignment = lblDateCreation.HorizontalContentAlignment = lblDatetime.HorizontalContentAlignment = HorizontalAlignment.Left;
            BorderMain.BorderThickness = new Thickness(0, 0, 0, 2);
            BorderMain.Margin = new Thickness(5, 0, 5, 0);
            BorderMain.Padding = new Thickness(2);
        }
        private void btnRemove_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (date == null)
                return;
            if ((bool)new Windows.Confirm("","").ShowDialog())
            {
                App.databasceConnection.query(string.Format("delete from tbl_dates where id = '{0}';", date.id));
                this.parent.btnAddDateName.Content = "Add";
                this.parent.btnAddDateIcon.Content = "\uf067";
                this.parent.btnAddDate.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2d2d2d"));
                this.parent.btnSearch_MouseLeftButtonUp(null, null);
            }
        }

        private void btnUpdate_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (date == null)
                return;
            this.parent.selectedClient = client;
            this.parent.txtClient.Text = client.name;
            this.parent.selectedDate = date;
            this.parent.btnAddDateName.Content = Assets.Languages.Default.btnUpdateName;
            this.parent.btnAddDateIcon.Content = "\uf021";
            this.parent.btnAddDate.Cursor = Cursors.Hand;
            this.parent.isAdd = false;
            this.parent.btnAddDate.Background = (Brush)new BrushConverter().ConvertFromString(FindResource("foregroundColor").ToString());
            this.parent.ClientViewerStack.Children.Clear();
        }
    }
}
