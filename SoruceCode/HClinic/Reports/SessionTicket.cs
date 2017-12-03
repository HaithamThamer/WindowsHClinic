using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HClinic.Classes.Clients;
namespace HClinic.Reports
{
    public partial class SessionTicket : DevExpress.XtraReports.UI.XtraReport
    {
        Client client;
        public SessionTicket(int cardNumber, Client client)
        {
            InitializeComponent();
            this.client = client;
            lblSessionId.Text = cardNumber.ToString();
            lblSessionIdBarcode.Text = lblSessionId.Text;
            lblClientName.Text = client.name;
        }

    }
}
