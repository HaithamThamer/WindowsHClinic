using System.Collections.Generic;

namespace HClinic.Classes.Tables
{
    interface ITable
    {
        Dictionary<string, string> initializeValues();
    }
}
