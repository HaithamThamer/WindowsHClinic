using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Windows.Media.Imaging;
using System.IO;

namespace HClinic.Reports
{
    public partial class Document : DevExpress.XtraReports.UI.XtraReport
    {
        public Document(BitmapImage img)
        {
            InitializeComponent();
            using (MemoryStream ms = new MemoryStream())
            {
                BitmapEncoder bmp = new BmpBitmapEncoder();
                bmp.Frames.Add(BitmapFrame.Create(img));
                bmp.Save(ms);
                picture.Image = new Bitmap(new Bitmap(ms));
            }
        }
    }
}
