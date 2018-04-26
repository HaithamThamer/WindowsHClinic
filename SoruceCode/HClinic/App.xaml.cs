using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.Core;
using System.Threading;
using System.IO.Ports;

namespace HClinic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static HDatabaseConnection.HMySQLConnection databasceConnection;
        public static HRegsiter.Product registerConnection;
        public static Classes.GSM gsm;
        public static Windows.Main windowMain;
        public Windows.Login windowLogin;
        public static Classes.Users.User currentUser;
        public static Classes.Tables.Info tblInfo;
        //public static SerialPort GSM;
        //public Windows.SubWindows.Splash windowSplash;
        public enum RegisterNames
        {
            ServerIP,
            ServerUsername,
            ServerPassword,
            DatabaseName,
            HeaterPrinter,
            LanguageDefault,
            GSMPortName,
            GSMBaudRate,
            GSMReadTimeout,
            GSMCharaterSet,
            LaserPrinter
        }
        public class Tables
        {
            public class Users
            {
                public enum UserTypes
                {
                    Guest,
                    Admin,
                    Assistant,
                    Doctor,
                    Reception
                }
            }
        }
        public static class Constants
        {
            public static string DateFormat { get { return "dd.MM.yyyy"; } }
            public static string TimeFormat { get { return "hh:mm:ss"; } }
            public static string DateTimeFormat { get { return "dd.MM.yyyy hh:mm:ss"; } }
            public static string DateTimeFormatForMySQL { get { return "yyyy-MM-dd hh:mm:ss"; } }

        }
        public static class RegisterValues
        {
            public static string 
                serverIP = "",
                serverUsername = "",
                serverPassword = "",
                databaseName = "",
                heaterPrinter = "",
                languageDefault = "",
                GSMPortName = "",
                GSMBaudRate = "",
                GSMReadTimeout = "",
                GSMCharaterSet = "",
                laserPrinter = ""
                ;
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //windowSplash = new HClinic.Windows.SubWindows.Splash();
            //Thread threadSplash = new Thread(() => {
            //    windowSplash.Show();
            //});
            //threadSplash.SetApartmentState(ApartmentState.STA);
            //threadSplash.Start();

            registerConnection = new HRegsiter.Product(System.Reflection.Assembly.GetExecutingAssembly().Location);

            //tempraory : set values of register (then must be installed from setup
            //registerConnection.setValue((int)RegisterNames.ServerIP, HCrypt.StringCipher.Encrypt("localhost"));
            //registerConnection.setValue((int)RegisterNames.ServerUsername, HCrypt.StringCipher.Encrypt("root"));
            //registerConnection.setValue((int)RegisterNames.ServerPassword, HCrypt.StringCipher.Encrypt("123123"));
            //registerConnection.setValue((int)RegisterNames.DatabaseName, HCrypt.StringCipher.Encrypt("db_h_clinic"));
            //registerConnection.setValue((int)RegisterNames.HeaterPrinter, HCrypt.StringCipher.Encrypt("Adobe PDF"));
            //registerConnection.setValue((int)RegisterNames.LanguageDefault, HCrypt.StringCipher.Encrypt("ar-IQ"));
            //registerConnection.setValue((int)RegisterNames.GSMPortName, HCrypt.StringCipher.Encrypt("COM4"));
            //registerConnection.setValue((int)RegisterNames.GSMBaudRate, HCrypt.StringCipher.Encrypt("115200"));
            //registerConnection.setValue((int)RegisterNames.GSMReadTimeout, HCrypt.StringCipher.Encrypt("2000"));
            //registerConnection.setValue((int)RegisterNames.GSMCharaterSet, HCrypt.StringCipher.Encrypt("HEX"));
            //registerConnection.setValue((int)RegisterNames.LaserPrinter, HCrypt.StringCipher.Encrypt("Adobe PDF"));


            //Get register values
            RegisterValues.serverIP = HCrypt.StringCipher.Decrypt(registerConnection.getValue((int)RegisterNames.ServerIP));
            RegisterValues.serverUsername = HCrypt.StringCipher.Decrypt(registerConnection.getValue((int)RegisterNames.ServerUsername));
            RegisterValues.serverPassword = HCrypt.StringCipher.Decrypt(registerConnection.getValue((int)RegisterNames.ServerPassword));
            RegisterValues.databaseName = HCrypt.StringCipher.Decrypt(registerConnection.getValue((int)RegisterNames.DatabaseName));
            RegisterValues.heaterPrinter = HCrypt.StringCipher.Decrypt(registerConnection.getValue((int)RegisterNames.HeaterPrinter));
            RegisterValues.languageDefault = HCrypt.StringCipher.Decrypt(registerConnection.getValue((int)RegisterNames.LanguageDefault));
            RegisterValues.GSMPortName = HCrypt.StringCipher.Decrypt(registerConnection.getValue((int)RegisterNames.GSMPortName));
            RegisterValues.GSMBaudRate = HCrypt.StringCipher.Decrypt(registerConnection.getValue((int)RegisterNames.GSMBaudRate));
            RegisterValues.GSMCharaterSet = HCrypt.StringCipher.Decrypt(registerConnection.getValue((int)RegisterNames.GSMCharaterSet));
            RegisterValues.GSMReadTimeout = HCrypt.StringCipher.Decrypt(registerConnection.getValue((int)RegisterNames.GSMReadTimeout));
            RegisterValues.laserPrinter = HCrypt.StringCipher.Decrypt(registerConnection.getValue((int)RegisterNames.LaserPrinter));

            //Create database connection
            try
            {
                databasceConnection = new HDatabaseConnection.HMySQLConnection(
                RegisterValues.serverIP,
                RegisterValues.serverUsername,
                RegisterValues.serverPassword,
                RegisterValues.databaseName
                );
            }
            catch (Exception)
            {

                throw;
            }
            tblInfo = new Classes.Tables.Info();

            //set default language
            //HClinic.Assets.Languages.Default.Culture = new System.Globalization.CultureInfo("it-IT");
            windowLogin = (e.Args.Length > 0 ? new HClinic.Windows.Login(e.Args[0],e.Args[1]) : new HClinic.Windows.Login());
            windowMain = new Windows.Main();
            bool s = (bool)windowLogin.ShowDialog();
            if (s == true)
            {
                //Create connection with GSM Device
                try
                {
                    gsm = new Classes.GSM(
                        RegisterValues.GSMPortName,
                        int.Parse(RegisterValues.GSMBaudRate),
                        int.Parse(RegisterValues.GSMReadTimeout),
                        RegisterValues.GSMCharaterSet
                        );
                }
                catch (Exception ex)
                {
                    new Windows.Confirm(Assets.Languages.Default.errorGsm, ex.Message.ToString(), MessageBoxButton.OK).ShowDialog();
                    windowMain.lblGSMStatus.Content = Assets.Languages.Default.error + ": " + ex.Message.ToString();
                }
                windowMain.ShowDialog();
            }
            Application.Current.Shutdown();
            //DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName();
        }

        private void GSM_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //MessageBox.Show(GSM.ReadExisting());
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            //GSM.Close();
            databasceConnection.closeConnection();
            //DevExpress.Xpf.Core.ApplicationThemeHelper.SaveApplicationThemeName();
        }
    }
}
