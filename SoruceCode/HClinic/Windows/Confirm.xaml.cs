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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HClinic.Windows
{
    /// <summary>
    /// Interaction logic for Confirm.xaml
    /// </summary>
    public partial class Confirm : Window
    {
        public Confirm(string title,string description,MessageBoxButton messageBoxButton = MessageBoxButton.OKCancel)
        {
            InitializeComponent();
            lblTitle.Content = title;
            txtDescription.Text = description;
            if (messageBoxButton == MessageBoxButton.OK)
            {
                Buttons.Children.Remove(btnCloseLarge);
                TitleBar.Children.Remove(btnCloseSmall);
            }
        }

        private void btnDone_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnCloseLarge_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
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
