using DevExpress.XtraReports.UI;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HClinic.Windows.Clients
{
    /// <summary>
    /// Interaction logic for Documents.xaml
    /// </summary>
    public partial class Documents : Window
    {
        public Classes.Clients.Session session;
        public List<byte[]> documents;
        bool isFullScreen = false;
        int[] imagesId;
        int imageCurrentId = 0;
        private Documents(bool isFullScreen = false)
        {
            InitializeComponent();
            this.isFullScreen = isFullScreen;
            this.WindowState = isFullScreen ? WindowState.Maximized : WindowState.Normal;
            documents = new List<byte[]>();
        }
        public Documents( Classes.Clients.Session session,bool isFullScreen = false) :this(isFullScreen)
        {
            this.session = session;

            System.Data.DataTable dt = App.databasceConnection.query(string.Format("select id from tbl_sessions_documents where session_id = '{0}' order by id desc;", session.id));
            imagesId = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                imagesId[i] = int.Parse(dt.Rows[i][0].ToString());
            }
            lblName.Content = string.Format("{0} Document of {1} Documents", imageCurrentId + (imagesId.Length > 0 ? 1 : 0), imagesId.Length);
            if (imagesId.Length > 0)
            {            
                setImage(imagesId[0]);
            }
        }
        public void setImage(int documentId)
        {
            imgCurrent.Children.Clear();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream((byte[])App.databasceConnection.query(string.Format("select document from tbl_sessions_documents where id = '{0}'", documentId )).Rows[0][0]);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            bi.Freeze();
            Image img = new Image() {
                Source = bi,
                Margin = new Thickness(5),
            };
            img.MouseLeftButtonUp += (object sender, MouseButtonEventArgs e)=>{
                string tempName = System.IO.Path.GetTempFileName() + "_hclinic.jpg";
                if (File.Exists(tempName))
                {
                    File.Delete(tempName);
                }
                BitmapEncoder be = new JpegBitmapEncoder();
                be.Frames.Add(BitmapFrame.Create(img.Source as BitmapSource));
                FileStream fs = new FileStream(tempName, FileMode.Create);
                be.Save(fs);
                fs.Close();
                System.Diagnostics.Process.Start(tempName);
            };
            imgCurrent.Children.Add(img);
        }


        private void btnClose_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void lblName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void btnAdd_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (documents.Count != 0)
            {
                for (int i = 0; i < documents.Count; i++)
                {
                    App.databasceConnection.HClinicSessionDocumentsAdd(session.id, txtDescription.Text, documents[i]);
                }
                
                btnAddIcon.Content = "\uf07b";
                btnAddName.Content = "أختيار الملف";
                txtDescription.Text = "";
                documents.Clear();
            }
            else
            {
                OpenFileDialog ofd = new OpenFileDialog();
                FileStream fs;
                BinaryReader br;
                ofd.Filter = "Image files | *.jpg";
                ofd.Multiselect = true;
                documents.Clear();
                if (ofd.ShowDialog() == true)
                {
                    btnAddIcon.Content = "\uf067";
                    btnAddName.Content = "أضافة";
                    for (int i = 0; i < ofd.FileNames.Length; i++)
                    {
                        fs = new FileStream(ofd.FileNames[i], FileMode.Open, FileAccess.Read);
                        br = new BinaryReader(fs);
                        documents.Add(br.ReadBytes((int)fs.Length));
                        br.Close();
                        fs.Close();
                    }
                }
            }

            System.Data.DataTable dt = App.databasceConnection.query(string.Format("select id from tbl_sessions_documents where session_id = '{0}' order by id desc;", session.id));
            imagesId = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                imagesId[i] = int.Parse(dt.Rows[i][0].ToString());
            }
            lblName.Content = string.Format("{0} Document of {1} Documents", 0, imagesId.Length);
            if (imagesId.Length > 0)
            {
                setImage(imagesId[imageCurrentId]);
            }

        }

        private void btnPrint_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (_images.Count > 0)
            //{

            //    for (int i = 0; i < _images.Count; i++)
            //    {
            //        new ReportPrintTool(new Reports.Document(_images[i]) { PrinterName = App.RegisterValues.laserPrinter }).Print();
            //    }
            //}
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right && imageCurrentId + 1 < imagesId.Length)
            {
                setImage(imagesId[++imageCurrentId]);
            }
            else if (e.Key == Key.Left && imageCurrentId - 1 >= 0)
            {
                setImage(imagesId[--imageCurrentId]);
            }
            lblName.Content = string.Format("{0} Document of {1} Documents", imageCurrentId, imagesId.Length);
        }
    }
}
