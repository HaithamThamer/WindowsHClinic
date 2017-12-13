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
                lblClientAddTitle.Content = "تعديل المراجع - " + dt.Rows[0]["name"];
                btnAddName.Content = "تعديل";
                txtAddress.Text = dt.Rows[0]["address"].ToString();
                txtBirthday.Text = dt.Rows[0]["birthday"].ToString();
                txtJob.Text = dt.Rows[0]["job"].ToString();
                txtName.Text = dt.Rows[0]["name"].ToString();
                txtPhone.Text = dt.Rows[0]["phone"].ToString();
                txtId.Text = dt.Rows[0]["id"].ToString();

                isMale.IsChecked = dt.Rows[0]["is_male"].ToString() == "True";
                isActive.IsChecked = dt.Rows[0]["is_active"].ToString() == "True";
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
                                        "("+(txtId.Text.Length > 0 ? "id," : "")+ "name,phone,job,address,birthday,is_active,is_male,user_id) " +
                                        "values " +
                                        "(" + (txtId.Text.Length > 0 ? "'{0}'," : "") + "'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}');",
                                        txtId.Text,
                                        txtName.Text,
                                        txtPhone.Text,
                                        txtJob.Text,
                                        txtAddress.Text,
                                        txtBirthday.Text,
                                        (bool)isActive.IsChecked ? "1" : "0",
                                        (bool)isMale.IsChecked ? "1" : "0",
                                        App.currentUser.id));
                    
                    
                }
                else
                {
                    App.databasceConnection.query(string.Format("update tbl_clients set id = '{1}', name = '{2}',phone = '{3}',job = '{4}',address = '{5}',birthday = '{6}',is_active = '{7}',is_male = '{8}' where id = '{0}';",
                        id,
                        (txtId.Text.Length > 0 ? txtId.Text : id.ToString()),
                        txtName.Text,
                        txtPhone.Text,
                        txtJob.Text,
                        txtAddress.Text,
                        txtBirthday.Text,
                        (bool)isActive.IsChecked ? "1" : "0",
                        (bool)isMale.IsChecked ? "1" : "0"));
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
            isActive.Content = "Active";
        }

        private void changeChecked_Unchecked(object sender, RoutedEventArgs e)
        {
            isActive.Content = "Non-Active";
        }

        private void isMale_Checked(object sender, RoutedEventArgs e)
        {
            isMale.Content = "Male";
        }

        private void isMale_Unchecked(object sender, RoutedEventArgs e)
        {
            isMale.Content = "Female";
        }
    }
}
