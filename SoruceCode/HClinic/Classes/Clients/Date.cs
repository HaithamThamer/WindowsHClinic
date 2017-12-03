using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HClinic.Classes.Users;
namespace HClinic.Classes.Clients
{
    public class Date
    {
        public int id;
        public DateTime datetime;
        public bool isReported;
        public int clientId = 0;
        public int userId = 0;
        public DateTime creation;
        public Date()
        {

        }
        public Date(int id,DateTime datetime,bool isReported,int clientId,int userId,DateTime creation)
        {
            this.id = id;
            this.datetime = datetime;
            this.isReported = isReported;
            this.clientId = clientId;
            this.userId = userId;
            this.creation = creation;
        }
    }
}
