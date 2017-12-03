using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HClinic.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login(string username = "",string password = "")
        {
            InitializeComponent();
            txtUsername.Text = username;
            txtPassword.Password = password;
            this.Loaded += Login_Loaded;
        }

        private void Login_Loaded(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text.Length > 0)
            {
                btnLogin_MouseLeftButtonUp(null, null);
            }
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (new Confirm("الخروج من النظام","هل تود بالفعل الخروج من النظام؟").ShowDialog() == true)
            {
                this.Close();
            }
        }
        private void btnLogin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (txtUsername.Text == string.Empty && txtPassword.Password == string.Empty)
            {
                lblNote.Content = "أسم المستخدم او كلمة المرور فارغة";
                return;
            }
            bool isLogin = false;
            HDatabaseConnection.HMySQLConnection db = App.databasceConnection;
            DataTable dt = App.databasceConnection.query(string.Format("select * from tbl_users where tbl_users.name = '{0}' and tbl_users.password = md5('{1}')", txtUsername.Text, txtPassword.Password));
            isLogin = dt.Rows.Count > 0;

            if (isLogin)
            {
                App.currentUser = new Classes.Users.User(
                    int.Parse(dt.Rows[0]["id"].ToString()),
                    dt.Rows[0]["name"].ToString(),
                    dt.Rows[0]["password"].ToString(),
                    dt.Rows[0]["email"].ToString(),
                    dt.Rows[0]["phone"].ToString(),
                    (App.Tables.Users.UserTypes)int.Parse(dt.Rows[0]["type"].ToString())
                    );
                this.DialogResult = true;
                this.Close();
            }
            lblNote.Content = "أسم المستخدم او كلمة المرور خاطئة";
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_MouseLeftButtonUp(null, null);
            }
        }

        private void stackPanelTop_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void lblTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
