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

namespace HClinic.Windows.SubWindows
{
    /// <summary>
    /// Interaction logic for Confirm.xaml
    /// </summary>
    public partial class Confirm : Window
    {
        public Confirm(string title,string description,  string Brush = "foregroundBrush", MessageBoxButton buttons = MessageBoxButton.OKCancel)
        {
            InitializeComponent();
            stackPanelTitleBar.Background = (Brush)FindResource(Brush);
            lblTitle.Content = title;
            lblDescription.Content = description;
            lblIcon.Foreground = (Brush)FindResource(Brush);
            if (buttons == MessageBoxButton.OK)
            {
                btnCancel.Visibility = Visibility.Hidden;
            }
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnCancel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            btnClose_MouseLeftButtonUp(null, null);
        }

        private void btnOkay_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void stackPanelTitleBar_MouseDown(object sender, MouseButtonEventArgs e)
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
