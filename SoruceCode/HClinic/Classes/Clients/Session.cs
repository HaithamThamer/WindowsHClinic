using HClinic.Classes.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HClinic.Classes.Clients
{
    public class Session
    {
        public int id;
        public Client client;
        public User user;
        public int cardNumber;
        public DateTime creation;
        
        //public int bloodPressureTop;
        //public int bloodPressureBottom;
        public string BP; // Top/Bottom
        public double RBS; //sugar
        public double PR; //bpm heart beat
        public double HbAlC; // percentage value %
        public int weight;
        public string note;
        public DateTime lastDate;
        public DateTime lastSession;
        public DateTime nextDate;
        public bool isReported = false;
        public Session()
        {

        }
        public Session(int id,Client client,User user,int cardNumber,DateTime creation,string BP, double RBS, double PR, double HbAlC, int weight,DateTime lastDate,DateTime lastSession, DateTime nextDate,bool isReported, string note)
        {
            this.id = id;
            this.client = client;
            this.user = user;
            this.cardNumber = cardNumber;
            this.creation = creation;
            this.BP = BP;
            this.RBS = RBS;
            this.PR = PR;
            this.HbAlC = HbAlC;
            this.weight = weight;
            this.note = note;
            this.lastDate = lastDate;
            this.lastSession = lastSession;
            this.nextDate = nextDate;
            this.isReported = isReported;
        }
        public static List<Session> get()
        {
            List<Session> sessions = new List<Session>();
            System.Data.DataTable dt = App.databasceConnection.query(string.Format(""));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //sessions.Add(new Session(
                //    int.Parse(dt.Rows[i]["id"].ToString()),
                //    new Client(),
                //    new User(),
                //    int.Parse(dt.Rows[i]["card_number"].ToString()),
                //    DateTime.Parse(dt.Rows[i]["creation"].ToString()),
                //    int.Parse(dt.Rows[i]["blood_pressure_top"].ToString()),
                //    int.Parse(dt.Rows[i]["blood_pressure_bottom"].ToString()),
                //    int.Parse(dt.Rows[i]["sugar"].ToString()),
                //    int.Parse(dt.Rows[i]["weight"].ToString()),
                //    dt.Rows[i]["note"].ToString()
                //    ));
            }
            return sessions;
        }
    }
}
