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
namespace HClinic.Windows.UserControls
{
    /// <summary>
    /// Interaction logic for Clients.xaml
    /// </summary>
    public partial class Clients : UserControl
    {
        UserControls.ClientsControls.Manager manager;
        public Clients()
        {
            InitializeComponent();
        }

        private void btnClientsManager_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ClientsContent.Children.Clear();
            if (manager == null)
            {
                manager = new ClientsControls.Manager();
                manager.Height = Double.NaN;
                manager.Width = Double.NaN;
            }
            ClientsContent.Children.Add(manager);
        }
    }
}
