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

namespace HClinic.Windows.UserControls.ClientsControls
{
    /// <summary>
    /// Interaction logic for ClientBox.xaml
    /// </summary>
    public partial class ClientBox : UserControl
    {
        public ClientBox(string name,int id,DateTime birthday,DateTime last)
        {
            InitializeComponent();
            Name.Content = name;
            Id.Content = id;
            Birthday.Content = birthday.ToString("yyyy.MM.dd");
            Last.Content = last.ToString("yyyy.MM.dd");
        }

        private void btnRemove_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (new HClinic.Windows.SubWindows.Confirm("سيتم مسح المراجع","هل انت موافق على مسح المراجع " + Name.Content).ShowDialog() == true)
            {
                
            }
        }

        private void btnUpdate_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
