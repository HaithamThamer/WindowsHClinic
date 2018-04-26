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

namespace HClinic.UserControls.Settings
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        public Windows.Main parent;
        public Main(Windows.Main parent)
        {
            InitializeComponent();
            this.parent = parent;

            // Connection current settings
            txtServerIP.Text = App.RegisterValues.serverIP;
            txtServerUsername.Text = App.RegisterValues.serverUsername;
            txtServerPasseword.Password = App.RegisterValues.serverPassword;
            txtDatabaseName.Text = App.RegisterValues.databaseName;

            // Printers
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cmbHeaterPrinter.Items.Add(printer);
                cmbLaserPrinter.Items.Add(printer);
            }
            cmbHeaterPrinter.Text = App.RegisterValues.heaterPrinter;
            cmbLaserPrinter.Text = App.RegisterValues.laserPrinter;

            //info
            txtDoctorName.Text = App.tblInfo.values["doctorName"];
            txtDoctorDescription.Text = App.tblInfo.values["doctorDescription"];
        }

        private void btnServerApply_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (txtServerIP.Text != string.Empty && txtServerUsername.Text != string.Empty && txtDatabaseName.Text != string.Empty)
            {
                try
                {
                    bool isCorrect = new HDatabaseConnection.HMySQLConnection(txtServerIP.Text, txtServerUsername.Text, txtServerPasseword.Password, txtDatabaseName.Text).isCorrect;
                    App.registerConnection.setValue((int)App.RegisterNames.ServerIP, HCrypt.StringCipher.Encrypt(txtServerIP.Text));
                    App.registerConnection.setValue((int)App.RegisterNames.ServerUsername, HCrypt.StringCipher.Encrypt(txtServerUsername.Text));
                    App.registerConnection.setValue((int)App.RegisterNames.ServerPassword, HCrypt.StringCipher.Encrypt(txtServerPasseword.Password));
                    App.registerConnection.setValue((int)App.RegisterNames.DatabaseName, HCrypt.StringCipher.Encrypt(txtDatabaseName.Text));
                    new HClinic.Windows.Confirm("", "").ShowDialog();
                    Environment.Exit(0);
                }
                catch (Exception)
                {
                    // something error of data
                }
            }
            else
            {
                // values empty
            }
        }

        private void btnPrinterApply_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            App.registerConnection.setValue((int)App.RegisterNames.HeaterPrinter, HCrypt.StringCipher.Encrypt(cmbHeaterPrinter.Text));
            App.registerConnection.setValue((int)App.RegisterNames.LaserPrinter, HCrypt.StringCipher.Encrypt(cmbLaserPrinter.Text));
            App.RegisterValues.heaterPrinter = cmbHeaterPrinter.Text;
            App.RegisterValues.laserPrinter = cmbLaserPrinter.Text;

        }

        private void btnInfoApply_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            App.databasceConnection.queryNonReader(string.Format("" +
                "update tbl_info set value = '{0}' where tbl_info.name = 'doctorName';" +
                "update tbl_info set value = '{1}' where tbl_info.name = 'doctorDescription';"
                , txtDoctorName.Text, txtDoctorDescription.Text));
            App.tblInfo = new Classes.Tables.Info();
        }
    }
}
