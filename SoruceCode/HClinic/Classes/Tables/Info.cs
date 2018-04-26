using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HClinic.Classes.Tables
{
    public class Info : ITable
    {
        public Dictionary<string, string> values;
        public Info()
        {
            this.values = initializeValues();
        }
        public Dictionary<string, string> initializeValues()
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            System.Data.DataTable dt = App.databasceConnection.query(string.Format("select * from tbl_info"));
            foreach (System.Data.DataRow row in dt.Rows)
            {
                values[row["name"].ToString()] = row["value"].ToString();
            }
            return values;
        }
    }
}
