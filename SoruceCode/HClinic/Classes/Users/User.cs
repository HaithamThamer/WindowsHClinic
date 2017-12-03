using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace HClinic.Classes.Users
{
    public class User
    {
        public int id;
        public string name;
        public string password;
        public string email;
        public string phone;
        public App.Tables.Users.UserTypes type;
        public User(int id,string name,string password,string email,string phone, App.Tables.Users.UserTypes type)
        {
            this.id = id;
            this.name = name;
            this.password = password;
            this.email = email;
            this.phone = phone;
            this.type = type;
        }
        public User()
        {

        }
        public static List<User> get()
        {
            List<User> users = new List<User>();
            System.Data.DataTable dt = App.databasceConnection.query(string.Format("select * from tbl_users"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                users.Add(new User(
                        int.Parse(dt.Rows[i]["id"].ToString()),
                        dt.Rows[i]["name"].ToString(),
                        dt.Rows[i]["password"].ToString(),
                        dt.Rows[i]["email"].ToString(),
                        dt.Rows[i]["phone"].ToString(),
                        (App.Tables.Users.UserTypes)int.Parse(dt.Rows[i]["type"].ToString())
                        ));
            }
            return users;
        }
    }
}
