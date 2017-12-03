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
using System.Windows.Shapes;

namespace HClinic.Windows.Clients
{
    /// <summary>
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        UserControls.Clients.ManagerItem parent;
        int id = -1;
        public Add(UserControls.Clients.ManagerItem parent,int id = -1)
        {
            InitializeComponent();
            this.parent = parent;
            this.id = id;
            if (id != -1)
            {
                System.Data.DataTable dt = App.databasceConnection.query(string.Format("select * from tbl_clients where id = '{0}';", id));
                lblTitle.Content = "تعديل المراجع - " + dt.Rows[0]["name"];
                lblAdd.Content = "تعديل";
                txtAddress.Text = dt.Rows[0]["address"].ToString();
                txtBirthday.Text = dt.Rows[0]["birthday"].ToString();
                txtJob.Text = dt.Rows[0]["job"].ToString();
                txtName.Text = dt.Rows[0]["name"].ToString();
                txtPhone.Text = dt.Rows[0]["phone"].ToString();
            }
        }
        private Add()
        {

        }
        private void btnAdd_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (txtName.Text != string.Empty && txtPhone.Text != string.Empty)
            {
                if (this.id == -1)
                {
                    App.databasceConnection.query(string.Format("insert into tbl_clients " +
                                        "(name,phone,job,address,birthday,is_active,user_id) " +
                                        "values " +
                                        "('{0}','{1}','{2}','{3}','{4}','{5}','{6}');",
                                        txtName.Text,
                                        txtPhone.Text,
                                        txtJob.Text,
                                        txtAddress.Text,
                                        txtBirthday.Text,
                                        (bool)isActive.IsChecked ? "1" : "0",
                                        App.currentUser.id));
                    
                    
                }
                else
                {
                    App.databasceConnection.query(string.Format("update tbl_clients set name = '{1}',phone = '{2}',job = '{3}',address = '{4}',birthday = '{5}',is_active = '{6}' where id = '{0}';",
                        id,
                        txtName.Text,
                        txtPhone.Text,
                        txtJob.Text,
                        txtAddress.Text,
                        txtBirthday.Text,
                        (bool)isActive.IsChecked ? "1" : "0"));
                }
                parent.parent.loadClients();
                this.Close();
            }
            else
            {
                new Confirm("خطأ", "أملاء جميع المتطلبات", MessageBoxButton.OK).ShowDialog();
            }
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void lblTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void changeChecked_Checked(object sender, RoutedEventArgs e)
        {
            isActive.Content = "فعال";
        }

        private void changeChecked_Unchecked(object sender, RoutedEventArgs e)
        {
            isActive.Content = "غير فعال";
        }
    }
}
