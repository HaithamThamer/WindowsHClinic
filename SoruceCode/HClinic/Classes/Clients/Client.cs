using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HClinic.Classes.Users;
namespace HClinic.Classes.Clients
{
    public class Client
    {
        public int id;
        public string name;
        public string phone;
        public string job;
        public string address;
        public DateTime birthday;
        public bool isActive;
        public DateTime creation;
        public User user;
        public Client()
        {

        }
        public Client(int id,string name,string phone,string job,string address,DateTime birthday,bool isActive,DateTime creation,User user)
        {
            this.id = id;
            this.name = name;
            this.phone = phone;
            this.job = job;
            this.address = address;
            this.birthday = birthday;
            this.isActive = isActive;
            this.creation = creation;
            this.user = user;
        }
        public static List<Client> get()
        {
            List<Client> clients = new List<Client>();
            System.Data.DataTable dt = App.databasceConnection.query(string.Format("select 	tbl_clients.id as client_id,	tbl_clients.name as client_name,	tbl_clients.phone as client_phone,	tbl_clients.job as client_job,	tbl_clients.address as client_address,	tbl_clients.birthday as client_birthday,	tbl_clients.is_active as client_is_active,	tbl_clients.creation as client_creation,	tbl_users.id as user_id,	tbl_users.name as user_name,	tbl_users.password as user_password,	tbl_users.email as user_email,	tbl_users.phone as user_phone,	tbl_users.`type` as user_type from 	tbl_users,tbl_clients where	tbl_clients.user_id = tbl_users.id"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                clients.Add(new Client(
                    int.Parse(dt.Rows[i]["client_id"].ToString()),
                    dt.Rows[i]["client_name"].ToString(),
                    dt.Rows[i]["client_phone"].ToString(),
                    dt.Rows[i]["client_job"].ToString(),
                    dt.Rows[i]["client_address"].ToString(),
                    DateTime.Parse(dt.Rows[i]["client_birthday"].ToString()),
                    bool.Parse(dt.Rows[i]["client_is_active"].ToString()),
                    DateTime.Parse(dt.Rows[i]["client_creation"].ToString()),
                    new User(
                        int.Parse(dt.Rows[i]["user_id"].ToString()),
                        dt.Rows[i]["user_name"].ToString(),
                        dt.Rows[i]["user_password"].ToString(),
                        dt.Rows[i]["user_email"].ToString(),
                        dt.Rows[i]["user_phone"].ToString(),
                        (App.Tables.Users.UserTypes)int.Parse(dt.Rows[i]["user_type"].ToString())
                        )
                    ));
            }
            return clients;
        }
    }
}
