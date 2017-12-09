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
        public byte[] document;
        private Documents()
        {
            InitializeComponent();
        }
        public Documents( Classes.Clients.Session session):this()
        {
            this.session = session;
            loadImages();
        }
        public void loadImages()
        {
            images.Children.Clear();
            System.Data.DataTable dt = App.databasceConnection.query(string.Format("select id,description,document from tbl_sessions_documents where session_id = '{0}' order by id desc;", session.id));
            BitmapImage bi;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = new MemoryStream((byte[])dt.Rows[i]["document"]);
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();
                bi.Freeze();
                Image img = new Image()
                {
                    Source = bi,
                    Margin = new Thickness(5),
                    Width = 100,
                    Height = 100,
                    ToolTip = dt.Rows[i]["description"].ToString()
                };
                img.MouseLeftButtonUp += delegate (object o, MouseButtonEventArgs e) {
                    string tempName = System.IO.Path.GetTempFileName() + "_hclinic.jpg"; ;
                    if (File.Exists(tempName))
                    {
                        File.Delete(tempName);
                    }
                    BitmapEncoder be = new JpegBitmapEncoder();
                    be.Frames.Add(BitmapFrame.Create(img.Source as BitmapSource));
                    be.Save(new FileStream(tempName, FileMode.Create));
                    System.Diagnostics.Process.Start(tempName);
                };
                images.Children.Add(img);
            }
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
            if (document != null)
            {
                App.databasceConnection.HClinicSessionDocumentsAdd(session.id, txtDescription.Text, document);
                btnAddIcon.Content = "\uf07b";
                btnAddName.Content = "أختيار الملف";
                txtDescription.Text = "";
                document = null;
                loadImages();
            }
            else
            {
                OpenFileDialog ofd = new OpenFileDialog();
                FileStream fs;
                BinaryReader br;
                ofd.Filter = "Image files | *.jpg";
                ofd.Multiselect = false;
                if (ofd.ShowDialog() == true)
                {
                    btnAddIcon.Content = "\uf067";
                    btnAddName.Content = "أضافة";
                    fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
                    br = new BinaryReader(fs);
                    document = br.ReadBytes((int)fs.Length);
                    br.Close();
                    fs.Close();
                }
            }
            

        }
    }
}
