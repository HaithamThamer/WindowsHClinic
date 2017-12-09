using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.Core;
using System.Threading;

namespace HClinic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static HDatabaseConnection.HMySQLConnection databasceConnection;
        public static HRegsiter.Product registerConnection;
        public static Windows.Main windowMain;
        public Windows.Login windowLogin;
        public static Classes.Users.User currentUser;
        //public Windows.SubWindows.Splash windowSplash;
        public enum RegisterNames
        {
            ServerIP,
            ServerUsername,
            ServerPassword,
            DatabaseName,
            PrinterDefaultOne,
            LanguageDefault
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
                printerDefaultOne = "",
                languageDefault = "";
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
            registerConnection.setValue((int)RegisterNames.ServerIP, HCrypt.StringCipher.Encrypt("127.0.0.1"));
            registerConnection.setValue((int)RegisterNames.ServerUsername, HCrypt.StringCipher.Encrypt("root"));
            registerConnection.setValue((int)RegisterNames.ServerPassword, HCrypt.StringCipher.Encrypt("123123"));
            registerConnection.setValue((int)RegisterNames.DatabaseName, HCrypt.StringCipher.Encrypt("db_h_clinic"));
            registerConnection.setValue((int)RegisterNames.PrinterDefaultOne, HCrypt.StringCipher.Encrypt("Adobe PDF"));
            registerConnection.setValue((int)RegisterNames.LanguageDefault, HCrypt.StringCipher.Encrypt("ar-IQ"));

            //Get register values
            RegisterValues.serverIP = HCrypt.StringCipher.Decrypt(registerConnection.getValue((int)RegisterNames.ServerIP));
            RegisterValues.serverUsername = HCrypt.StringCipher.Decrypt(registerConnection.getValue((int)RegisterNames.ServerUsername));
            RegisterValues.serverPassword = HCrypt.StringCipher.Decrypt(registerConnection.getValue((int)RegisterNames.ServerPassword));
            RegisterValues.databaseName = HCrypt.StringCipher.Decrypt(registerConnection.getValue((int)RegisterNames.DatabaseName));
            RegisterValues.printerDefaultOne = HCrypt.StringCipher.Decrypt(registerConnection.getValue((int)RegisterNames.PrinterDefaultOne));
            RegisterValues.languageDefault = HCrypt.StringCipher.Decrypt(registerConnection.getValue((int)RegisterNames.LanguageDefault));

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

            //set default language
            HClinic.Assets.Languages.Default.Culture = new System.Globalization.CultureInfo("it-IT");

            windowLogin = (e.Args.Length > 0 ? new HClinic.Windows.Login(e.Args[0],e.Args[1]) : new HClinic.Windows.Login());
            windowMain = new Windows.Main();

            if (windowLogin.ShowDialog() == true)
            {
                windowMain.ShowDialog();
            }
            Application.Current.Shutdown();
            //DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            //DevExpress.Xpf.Core.ApplicationThemeHelper.SaveApplicationThemeName();
        }
    }
}
