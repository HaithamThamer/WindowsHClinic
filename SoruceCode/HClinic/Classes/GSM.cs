using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HClinic.Classes
{
    public class GSM
    {
        SerialPort gsm;
        public string Manufacturer = "";
        public string SMSCenter = "";
        public GSM(string portName = "COM1", int baudRate = 9600, int readTimeout = 2000, string characterSet = "GSM")
        {
            gsm = new SerialPort();
            gsm.PortName = portName;
            gsm.BaudRate = baudRate;
            gsm.ReadTimeout = readTimeout;
            gsm.DataReceived += Gsm_DataReceived;
            gsm.Open();
            gsm.Write("AT\r");
            long beginTime = DateTime.Now.Ticks;
            string data;
            while ((data = gsm.ReadExisting()) == string.Empty)
            {

            }
            if (data == "")
            {
                throw new Exception("error: Failed to open connection with Serial Port of GSM (" + gsm.PortName + ")");
            }
            gsm.Write("AT+CGMI\r");
            while ((data = gsm.ReadExisting()) == string.Empty)
            {

            }
            if (data == "")
            {
                throw new Exception("error: Failed to open connection with Serial Port of GSM (" + gsm.PortName + ")");
            }
            data = data.Replace("\r", string.Empty).Replace("\n", string.Empty).ToLower();
            Manufacturer = data;
            gsm.Write("AT+CSCA?\r");
            while ((data = gsm.ReadExisting()) == string.Empty)
            {

            }
            if (data == "")
            {
                throw new Exception("error: Failed to open connection with Serial Port of GSM (" + gsm.PortName + ")");
            }
            data = data.Replace("\r", string.Empty).Replace("\n", string.Empty).ToLower();
            SMSCenter = data;
            SMSCenter = SMSCenter.Substring(SMSCenter.IndexOf("\"") + 1);
            SMSCenter = SMSCenter.Substring(0, SMSCenter.IndexOf("\""));
            string d = stringToHex("+964750001140");
            //sendMessage("009647703867142", "hi");
            call("0096477038767142");
        }
        ~GSM()
        {
            gsm.Close();
        }
        public string stringToHex(string str)
        {
            return BitConverter.ToString(Encoding.Default.GetBytes(str)).Replace("-", "");
        }
        public string hexToString(string hex)
        {
            string result = "";
            
            return result;
        }
        private void Gsm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //MessageBox.Show(gsm.ReadExisting());
        }
        public bool sendMessage(string number,string body)
        {
            gsm.Write("\rAT\r");
            string data;
            //while ((data = gsm.ReadLine()) == string.Empty){}
            gsm.Write("AT+CSCS = \"HEX\"");
            Thread.Sleep(1000);
            //while ((data = gsm.ReadLine()) == string.Empty) { }
            gsm.Write("AT+CMGF = 0");
            Thread.Sleep(1000);
            //while ((data = gsm.ReadLine()) == string.Empty) { }
            gsm.Write(string.Format("AT+CMGS = \"{0}\"\r\n{1}\x1A", UnicodeStr2HexStr(number), UnicodeStr2HexStr(body)));
            Thread.Sleep(1000);
            data = gsm.ReadExisting();
            //while ((data = gsm.ReadLine()) == string.Empty) { }
            return false;
        }
        public static String UnicodeStr2HexStr(String strMessage)
        {
            byte[] ba = Encoding.BigEndianUnicode.GetBytes(strMessage);
            String strHex = BitConverter.ToString(ba);
            strHex = strHex.Replace("-", "");
            return strHex.Replace("00", "").ToLower();
        }
        public bool call(string number)
        {
            gsm.Write(string.Format("ATD{0};", number));
            return false;
        }
        public string sendUSSD(string code)
        {

            return "";
        }
        public string getOwnPhoneNumber()
        {
            gsm.Write("AT+CNUM\r");
            long beginTime = DateTime.Now.Ticks;
            string data;
            while ((data = gsm.ReadExisting()) == string.Empty)
            {
                
            }
            data = data.Replace("\r", string.Empty).Replace("\n", string.Empty).ToLower();
            if (data == "")
            {
                throw new Exception("error with connect to gsm");
                
            }
            data = data.Split(',')[1].Replace("\"", string.Empty);
            return ("00" + data);
        }

    }
}
