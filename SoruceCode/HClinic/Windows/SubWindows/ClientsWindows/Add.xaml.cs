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

namespace HClinic.Windows.SubWindows.ClientsWindows
{
    /// <summary>
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        public Add()
        {
            InitializeComponent();
        }

        private void lblName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnAdd_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (
                txtName.Text != string.Empty &&
                txtPhone.Text != string.Empty &&
                dateBirthday.Text != string.Empty
                )
            {
                App.databasceConnection.queryNonReader(string.Format("insert into tbl_clients " +
                    "(name,phone,email,address,birthday,user_id)" +
                    "values " +
                    "('{0}','{1}','{2}','{3}','{4}','{5}')" +
                    ";",
                    txtName.Text, txtPhone.Text, txtEmail.Text, txtAddress.Text, dateBirthday.Text, Properties.Settings.Default.userId));
                txtAddress.Text = txtEmail.Text = txtName.Text = txtPhone.Text = "";
                txtName.Focus();
            }
            else
            {
                new Confirm("خطأ في الأدخال", "هناك بعض الحقول الامطلوبة فارغة", "foregroundBrush", MessageBoxButton.OK).ShowDialog();
            }
        }
        private void txtPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, e.Text.Length - 1);
        }
    }
}
