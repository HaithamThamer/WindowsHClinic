namespace HClinic.Reports
{
    partial class SessionTicket
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator2 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
            DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator1 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.lblClientName = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSessionId = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSessionIdBarcode = new DevExpress.XtraReports.UI.XRBarCode();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.lblClinicName = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.lblClientId = new DevExpress.XtraReports.UI.XRLabel();
            this.lblClientIdBarcode = new DevExpress.XtraReports.UI.XRBarCode();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblClientIdBarcode,
            this.lblClientId,
            this.lblClientName,
            this.lblSessionId,
            this.lblSessionIdBarcode,
            this.xrLabel2});
            this.Detail.HeightF = 285.4167F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblClientName
            // 
            this.lblClientName.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.lblClientName.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClientName.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 106.5833F);
            this.lblClientName.Name = "lblClientName";
            this.lblClientName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblClientName.SizeF = new System.Drawing.SizeF(159.5833F, 33.41666F);
            this.lblClientName.StylePriority.UseBorders = false;
            this.lblClientName.StylePriority.UseFont = false;
            this.lblClientName.StylePriority.UseTextAlignment = false;
            this.lblClientName.Text = "هيثم ثامر عبدالقادر";
            this.lblClientName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblSessionId
            // 
            this.lblSessionId.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblSessionId.BorderWidth = 2F;
            this.lblSessionId.Font = new System.Drawing.Font("Calibri", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSessionId.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 0F);
            this.lblSessionId.Name = "lblSessionId";
            this.lblSessionId.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblSessionId.SizeF = new System.Drawing.SizeF(10.62498F, 63.08333F);
            this.lblSessionId.StylePriority.UseBorders = false;
            this.lblSessionId.StylePriority.UseBorderWidth = false;
            this.lblSessionId.StylePriority.UseFont = false;
            this.lblSessionId.StylePriority.UseTextAlignment = false;
            this.lblSessionId.Text = "999";
            this.lblSessionId.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblSessionId.Visible = false;
            // 
            // lblSessionIdBarcode
            // 
            this.lblSessionIdBarcode.AutoModule = true;
            this.lblSessionIdBarcode.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 63.08333F);
            this.lblSessionIdBarcode.Name = "lblSessionIdBarcode";
            this.lblSessionIdBarcode.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 100F);
            this.lblSessionIdBarcode.ShowText = false;
            this.lblSessionIdBarcode.SizeF = new System.Drawing.SizeF(10.62498F, 32.37498F);
            this.lblSessionIdBarcode.StylePriority.UseTextAlignment = false;
            this.lblSessionIdBarcode.Symbology = code128Generator2;
            this.lblSessionIdBarcode.Text = "999";
            this.lblSessionIdBarcode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblSessionIdBarcode.Visible = false;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(180F, 106.5833F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(100F, 33.41666F);
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "أسم المراجع";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine2,
            this.lblClinicName});
            this.TopMargin.HeightF = 81F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLine2
            // 
            this.xrLine2.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
            this.xrLine2.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 74.66668F);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.SizeF = new System.Drawing.SizeF(290F, 6.333319F);
            // 
            // lblClinicName
            // 
            this.lblClinicName.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblClinicName.BorderWidth = 2F;
            this.lblClinicName.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClinicName.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 10.00001F);
            this.lblClinicName.Name = "lblClinicName";
            this.lblClinicName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblClinicName.SizeF = new System.Drawing.SizeF(270F, 60.99999F);
            this.lblClinicName.StylePriority.UseBorders = false;
            this.lblClinicName.StylePriority.UseBorderWidth = false;
            this.lblClinicName.StylePriority.UseFont = false;
            this.lblClinicName.StylePriority.UseTextAlignment = false;
            this.lblClinicName.Text = "أسم الدكتور العيادة";
            this.lblClinicName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.BottomMargin.BorderWidth = 1F;
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine1,
            this.xrPageInfo1});
            this.BottomMargin.HeightF = 54F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.StylePriority.UseBorders = false;
            this.BottomMargin.StylePriority.UseBorderWidth = false;
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLine1
            // 
            this.xrLine1.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
            this.xrLine1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(290F, 6.333319F);
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
            this.xrPageInfo1.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
            this.xrPageInfo1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 6.333319F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(290F, 47.66668F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrPageInfo1.TextFormatString = "{0:yyyy-MM-dd hh:mm:ss tt}";
            // 
            // lblClientId
            // 
            this.lblClientId.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblClientId.BorderWidth = 2F;
            this.lblClientId.Font = new System.Drawing.Font("Calibri", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClientId.LocationFloat = new DevExpress.Utils.PointFloat(20.62499F, 0F);
            this.lblClientId.Name = "lblClientId";
            this.lblClientId.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblClientId.SizeF = new System.Drawing.SizeF(259.3751F, 63.08333F);
            this.lblClientId.StylePriority.UseBorders = false;
            this.lblClientId.StylePriority.UseBorderWidth = false;
            this.lblClientId.StylePriority.UseFont = false;
            this.lblClientId.StylePriority.UseTextAlignment = false;
            this.lblClientId.Text = "999";
            this.lblClientId.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblClientIdBarcode
            // 
            this.lblClientIdBarcode.AutoModule = true;
            this.lblClientIdBarcode.LocationFloat = new DevExpress.Utils.PointFloat(20.62499F, 63.08333F);
            this.lblClientIdBarcode.Name = "lblClientIdBarcode";
            this.lblClientIdBarcode.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 100F);
            this.lblClientIdBarcode.ShowText = false;
            this.lblClientIdBarcode.SizeF = new System.Drawing.SizeF(259.375F, 32.37498F);
            this.lblClientIdBarcode.StylePriority.UseTextAlignment = false;
            this.lblClientIdBarcode.Symbology = code128Generator1;
            this.lblClientIdBarcode.Text = "999";
            this.lblClientIdBarcode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // SessionTicket
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 81, 54);
            this.PageHeight = 300;
            this.PageWidth = 290;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PrinterName = "Adobe PDF";
            this.RequestParameters = false;
            this.ShowPreviewMarginLines = false;
            this.ShowPrintMarginsWarning = false;
            this.ShowPrintStatusDialog = false;
            this.Version = "17.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRLabel lblClinicName;
        private DevExpress.XtraReports.UI.XRLine xrLine2;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRLabel lblClientName;
        private DevExpress.XtraReports.UI.XRLabel lblSessionId;
        private DevExpress.XtraReports.UI.XRBarCode lblSessionIdBarcode;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRBarCode lblClientIdBarcode;
        private DevExpress.XtraReports.UI.XRLabel lblClientId;
    }
}
