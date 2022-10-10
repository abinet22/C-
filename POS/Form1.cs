using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class Form1 : Form
    {
        Form2 form2;
        Form1 form1;
        // for PrintDialog, PrintPreviewDialog and PrintDocument:
        private System.Windows.Forms.PrintDialog prnDialog;
        private System.Windows.Forms.PrintPreviewDialog prnPreview;
        private System.Drawing.Printing.PrintDocument prnDocument;
        private System.Drawing.Printing.PrintDocument prnDocumentatach;
        // private System.ComponentModel.Container components = null;

        // for Invoice Head:
        private string InvTitle, InvTitlef, footerimg;
        private string InvSubTitle1, InvSubTitle1f;
        private string InvSubTitle2, InvSubTitle2f;
        private string InvSubTitle3, InvSubTitle4, InvSubTitle5, InvSubTitle6, InvSubTitle3f, InvSubTitle4f, InvSubTitle5f;
        private string InvImage;

        // for Database:
        private OleDbConnection cnn;
        private OleDbCommand cmd;
        private string rdrInvoice;
        private string strCon;
        private string InvSql;

        // for Report:
        private int CurrentY;
        private int CurrentX;
        private int leftMargin;
        private int rightMargin;
        private int topMargin;
        private int bottomMargin;
        private int InvoiceWidth;
        private int InvoiceHeight;
        private string CustomerName;
        private string CustomerCity, refno, preby,to,buyertin,rowdata,fsno, saledt, saletime;
        private string itemid, description;
        private decimal Quan, UnitPr, LTotal;
        private string SellerName;
        private string SaleID;
        private string SaleDate;
        private decimal SaleFreight;
        private decimal SubTotal;
        private decimal InvoiceTotal;
        private bool ReadInvoice;
        private int AmountPosition;

        // Font and Color:------------------
        // Title Font
        public Font InvTitleFont = new Font("Sans I Am", 10, FontStyle.Regular);
        public Font InvTitleFontf = new Font("TIFAX", 10, FontStyle.Bold);
        public Font InvTitleFontFWO = new Font("TIFAX", 7, FontStyle.Bold);
        // Title Font height
        public Font InvTitleFontfTOT = new Font("TIFAX", 12, FontStyle.Bold);
        private int InvTitleHeight, InvTitleHeightn, InvoiceFontHeighttot;
        // SubTitle Font
        public Font InvFontNumTOP = new Font("FacsimileLL", 9, FontStyle.Regular);
        public Font InvFontNum = new Font("FacsimileLL", 7, FontStyle.Regular);
        public Font InvFontNumBold = new Font("FacsimileLL", 10, FontStyle.Bold);
        public Font InvSubTitleFontf = new Font("Sans I Am", 10, FontStyle.Regular);
        public Font InvSubTitleFontIMGE = new Font("Sans I Am", 10, FontStyle.Italic);
        // SubTitle Font height
        private int InvSubTitleHeight, InvSubTitleHeightn, InvoiceFontHeightnum, InvSubTitleHeightF;
        // Invoice Font
        public Font InvSubTitleFont = new Font("Sans I Am", 10, FontStyle.Regular);
        public Font InvoiceFont = new Font("Sans I Am", 10, FontStyle.Regular);
        public Font InvoiceFont2 = new Font("Sans I Am", 9, FontStyle.Regular);
        public Font InvoiceInfo = new Font("Sans I Am", 10, FontStyle.Regular);
        public Font InvoiceFontf = new Font("Sans I Am", 10, FontStyle.Regular);
        public Font InvoiceFonttotal = new Font("Sans I Am", 10, FontStyle.Regular);
        public Font InvoiceFontET = new Font("Consolas", 10, FontStyle.Regular);
        public System.Drawing.Font InvFontNumS = new System.Drawing.Font("Sans I Am", 9, FontStyle.Regular);

        // Invoice Font height
        private int InvoiceFontHeight, InvoiceFontHeightDES, InvoiceFontHeightf;
        // Blue Color
        private SolidBrush BlueBrush = new SolidBrush(Color.Blue);
        // Red Color
        private SolidBrush RedBrush = new SolidBrush(Color.Red);
        // Black Color
        private SolidBrush BlackBrush = new SolidBrush(Color.Black);
        public Form1()
        {
            InitializeComponent();
            this.prnDialog = new System.Windows.Forms.PrintDialog();
            this.prnPreview = new System.Windows.Forms.PrintPreviewDialog();
            this.prnDocument = new System.Drawing.Printing.PrintDocument();
            this.prnDocumentatach = new System.Drawing.Printing.PrintDocument();
            // prnDocument->PrinterSettings->PaperSizes = 
            // The Event of 'PrintPage'
            prnDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(prnDocument_PrintPage);
            Margins margins = new Margins(1, 1, 1, 1);
            //PaperSize paperSize = new PaperSize("pos80", 4, 5);
            //prnDocument.DefaultPageSettings.PaperSize = paperSize;
            prnDocument.DefaultPageSettings.Margins = margins;


            prnDocumentatach.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(prnDocumentatach_PrintPage);
            Margins marginss = new Margins(100, 100, 100, 100);
            //PaperSize paperSize = new PaperSize("pos80", 4, 5);
            //prnDocument.DefaultPageSettings.PaperSize = paperSize;
            prnDocumentatach.DefaultPageSettings.Margins = marginss;
            // prnDocument.DefaultPageSettings.PaperSize.Width=80;
        }

      
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
              
        }
      
       // Form1 form1;
        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            //Form2 log = new Form2(this);
            //log.ShowDialog();
            //if (form2 == null)
            //{
            //    form2 = new Form2();
            //    Form1 form1 = this;
            //    form2.MdiParent = form1;
            //    splitContainer1.Panel2.Controls.Add(form2);

            //    form2.Show();
            //}
            //else
            //{
            //    form2.Activate();
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Random rand = new System.Random((int)System.DateTime.Now.Ticks);
            int random = rand.Next(1, 10000);
            textBoxsaleref.Text = "0000" + Convert.ToString(random);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //dataGridView1.Rows[0].Cells[0].Value = textBox1.Text;
            //dataGridView1.Rows[0].Cells[1].Value = textBox2.Text;
            //dataGridView1.Rows[0].Cells[2].Value = textBox3.Text;
            //dataGridView1.Rows[0].Cells[3].Value = textBox4.Text;
            //dataGridView1.Rows[0].Cells[4].Value = textBox5.Text;
            decimal linetotal = Convert.ToDecimal(textBox4.Text) * Convert.ToDecimal(textBox3.Text);
            //Math.Round(linetotal);
            string linetotals = Convert.ToString(linetotal);
            dataGridView1.Rows.Add(textBox1.Text, textBox2.Text, textBox4.Text, textBox3.Text, linetotals);

            Clearall();

        }

        private void Clearall()
        {
             textBox1.Text="";
             textBox2.Text = "";
           textBox3.Text = "";
            textBox4.Text = "";
            textBoxsubtot.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            decimal sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                sum += Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value);
            }
            textBoxsubtot.Text = sum.ToString();
            textBox7.Text = Convert.ToString(15) +""+ "%";
            decimal vat= sum * 15 /100;
            textBoxvat.Text = vat.ToString();

            decimal grandtot = sum + vat;
            textBoxgrandtot.Text = Convert.ToString(grandtot);
            int itemno = dataGridView1.Rows.Count;
            int itemcount = itemno - 1;
            textBoxnoitem.Text = Convert.ToString(itemno);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ReadInvoice = false;
            DisplayInvoice(); // Print Preview
        }

        private void textBoxcomname_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonPRNAACH_Click(object sender, EventArgs e)
        {
            ReadInvoice = false;
            PrintReportatach(); // Print Invoice
        }

        private void PrintReportatach()
        {
            try
            {
                // prnDocument.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1170); // all sizes are converted from mm to inches & then multiplied by 100.


                prnDocumentatach.Print();
                // prnDocument->DefaultPageSettings->PaperSize = prnDocument->PrinterSettings->PaperSizes[70];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void DisplayInvoice()
        {
            prnPreview.Document = this.prnDocument;

            try
            {
                prnPreview.ShowDialog();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void btnPreview_Click(object sender, EventArgs e)
        {
            ReadInvoice = false;
            DisplayDialog(); // Print Dialog
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        //bool forcepgsize(System.Drawing.Printing.PrintDocument prnDocument,System.Drawing.Printing.PaperKind paperKind)
        //{
        //    for(int i=0; i<prnDocument.PrinterSettings.PaperSizes.Count;++i)
        //    {
        //        if(prnDocument.PrinterSettings.PaperSizes[i].Kind==paperKind)
        //        {
        //            prnDocument.DefaultPageSettings.PaperSize = prnDocument.PrinterSettings.PaperSizes[i];
        //            return true;
        //        }

        //    }
        //    return false;
        //}
        private void DisplayDialog()
        {
            try
            {
                prnDialog.Document = this.prnDocument;
                DialogResult ButtonPressed = prnDialog.ShowDialog();
                // If user Click 'OK', Print Invoice
                if (ButtonPressed == DialogResult.OK)

                   // forcepgsize(this.prnDocument, System.Drawing.Printing.PaperKind.Prc32KBigRotated);
                    prnDocument.Print();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReadInvoice = false;
            PrintReport(); // Print Invoice
        }
        private void PrintReport()
        {
            try
            {
               // prnDocument.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1170); // all sizes are converted from mm to inches & then multiplied by 100.

               
                prnDocument.Print();
               // prnDocument->DefaultPageSettings->PaperSize = prnDocument->PrinterSettings->PaperSizes[70];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void prnDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            leftMargin = (int)e.MarginBounds.Left;
            rightMargin = (int)e.MarginBounds.Right;
            topMargin = (int)e.MarginBounds.Top;
            bottomMargin = (int)e.MarginBounds.Bottom;
            InvoiceWidth = (int)e.MarginBounds.Width;
            InvoiceHeight = (int)e.MarginBounds.Height;

            if (!ReadInvoice)
              //  ReadInvoiceData();

            SetInvoiceHead(e.Graphics); // Draw Invoice Head
          SetOrderData(e.Graphics); // Draw Order Data
            SetInvoiceData(e.Graphics, e); // Draw Invoice Data
                                           // SetInvoiceTotal(e.Graphics,e);
            //SetInvoiceFooter(e.Graphics);
            ReadInvoice = true;
        }
        private void ReadInvoiceHead()
        {
            //Titles and Image of invoice:
           // InvTitle = textBoxcomname.Text;
            InvSubTitle1 = textBoxONE.Text;
            InvSubTitle2 = textBoxcomname.Text;
            InvSubTitle4 = textBoxcityctry.Text;
            InvSubTitle3 = textBoxstrretadd.Text;
            InvSubTitle5 = textBoxtele.Text;
            InvSubTitle6 = textBoXSIX.Text;
            //  InvImage = Application.StartupPath + @"\Images\" + "InvPic.jpg";
        }
        private void SetInvoiceHead(Graphics g)
        {
            ReadInvoiceHead();

            CurrentY = topMargin;
            CurrentX = leftMargin;
            int ImageHeight = 0;

            // Draw Invoice image:
            if (System.IO.File.Exists(InvImage))
            {
                Bitmap oInvImage = new Bitmap(InvImage);
                // Set Image Left to center Image:
                int xImage = CurrentX + (InvoiceWidth - oInvImage.Width) / 2;
                ImageHeight = oInvImage.Height; // Get Image Height
                g.DrawImage(oInvImage, xImage, CurrentY);
            }

            InvTitleHeight = (int)(InvTitleFont.GetHeight(g));
            InvSubTitleHeight = (int)(InvSubTitleFont.GetHeight(g)) ;
            InvSubTitleHeightF = (int)(InvTitleFontf.GetHeight(g)) ;

            // Get Titles Length:
            int lenInvTitle = (int)g.MeasureString(InvTitle, InvTitleFont).Width;
            int lenInvSubTitle1 = (int)g.MeasureString(InvSubTitle1, InvFontNum).Width;
            int lenInvSubTitle2 = (int)g.MeasureString(InvSubTitle2, InvTitleFontf).Width;
            int lenInvSubTitle3 = (int)g.MeasureString(InvSubTitle3, InvTitleFontFWO).Width;
            int lenInvSubTitle4 = (int)g.MeasureString(InvSubTitle4, InvTitleFontFWO).Width;
            int lenInvSubTitle5 = (int)g.MeasureString(InvSubTitle5, InvTitleFont).Width;
            int lenInvSubTitle6 = (int)g.MeasureString(InvSubTitle6, InvFontNum).Width;
            // Set Titles Left:
            int xInvTitle = CurrentX + (InvoiceWidth - lenInvTitle) / 2;
            int xInvSubTitle1 = CurrentX -15+ (InvoiceWidth - lenInvSubTitle1) / 2;
            int xInvSubTitlenew = CurrentX +13+ (InvoiceWidth - lenInvSubTitle1 ) / 2;
            int xInvSubTitle2 = CurrentX + (InvoiceWidth - lenInvSubTitle2) / 2;
            int xInvSubTitle3 = CurrentX + (InvoiceWidth - lenInvSubTitle3) / 2;
            int xInvSubTitle4 = CurrentX + (InvoiceWidth - lenInvSubTitle4) / 2;
            int xInvSubTitle5 = CurrentX + (InvoiceWidth - lenInvSubTitle5) / 2;
            int xInvSubTitle6 = CurrentX + (InvoiceWidth - lenInvSubTitle6) / 2;

            // Draw Invoice Head:
            //if (InvTitle != "")
            //{
            //    CurrentY = CurrentY + ImageHeight;
            //    g.DrawString(InvTitle, InvTitleFont, BlueBrush, xInvTitle, CurrentY);
            //}
            if (InvSubTitle1 != "")
            {
               // CurrentY = CurrentY + InvTitleHeight;
                g.DrawString("TIN:", InvoiceFont, BlueBrush, xInvSubTitle1, CurrentY);
                CurrentY = CurrentY -1  ;
                
                g.DrawString(InvSubTitle1, InvFontNumTOP, BlueBrush, xInvSubTitlenew, CurrentY);
            }
            if (InvSubTitle2 != "")
            {
                CurrentY = CurrentY + InvSubTitleHeight +1;
                g.DrawString(InvSubTitle2, InvTitleFontf , BlueBrush, xInvSubTitle2, CurrentY);
            }
            if (InvSubTitle3 != "")
            {
                CurrentY = CurrentY + InvSubTitleHeight;
                g.DrawString(InvSubTitle3, InvTitleFontFWO, BlueBrush, xInvSubTitle3, CurrentY);
            }
            if (InvSubTitle4 != "")
            {
                CurrentY = CurrentY + InvSubTitleHeightF;
                g.DrawString(InvSubTitle4, InvTitleFontFWO, BlueBrush, xInvSubTitle4, CurrentY);
            }
            if (InvSubTitle5 != "")
            {
                CurrentY = CurrentY + InvSubTitleHeight;
                g.DrawString(InvSubTitle5, InvTitleFont, BlueBrush, xInvSubTitle5, CurrentY);
            }
            if (InvSubTitle6 != "")
            {
                CurrentY = CurrentY + InvSubTitleHeightF;
                g.DrawString(InvSubTitle6, InvFontNum, BlueBrush, xInvSubTitle6, CurrentY);
            }

            // Draw line:
            CurrentY = CurrentY + InvSubTitleHeight + 8;
            //float[] dashline = { 7, 5, 7, 5 };
            //Pen blackpen = new Pen(Color.Black,1);
            //blackpen.DashPattern = dashline;
            
            //g.DrawLine(blackpen, new Point(CurrentX, CurrentY), new Point(rightMargin, CurrentY));
            //g.DrawLine(new Pen(Brushes.Black, 2), CurrentX, CurrentY, rightMargin, CurrentY);
        }
        private void ReadOrderData()
        {
            refno = textBoxrefno.Text;
          preby = textBoxpreby.Text ;
            to = textBoxinvto.Text ;
            buyertin= textBoxbuytin.Text;
            fsno = textBoxsaleref.Text;
            saledt = dtpdbirth.Text;

            saletime = textBoxtime.Text;
            //  InvImage = Application.StartupPath + @"\Images\" + "InvPic.jpg";
        }
        private void SetOrderData(Graphics g)
        {// Set Company Name, City, Salesperson, Order ID and Order 
            ReadOrderData();

            string FieldValue = "";
            InvoiceFontHeight = (int)(InvoiceFont.GetHeight(g));
            InvoiceFontHeightDES = (int)(InvoiceFont2.GetHeight(g));
            InvoiceFontHeightnum = (int)(InvFontNum.GetHeight(g));
            // Set Company Name:
            CurrentX = leftMargin;
            CurrentY = CurrentY + 6;
            FieldValue = "FS No: ";
            g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY);
            CurrentX = leftMargin + (int)g.MeasureString(FieldValue, InvoiceFont).Width - 4;
            CurrentY = CurrentY ;
            FieldValue = textBoxsaleref.Text;

            g.DrawString(FieldValue, InvFontNumTOP, BlackBrush, CurrentX, CurrentY);

            CurrentX = leftMargin;
            CurrentY = CurrentY + InvoiceFontHeightnum;
            FieldValue = saledt;
            g.DrawString(FieldValue, InvFontNumTOP, BlackBrush, CurrentX, CurrentY);
            CurrentX = rightMargin;
          
            FieldValue = saletime;
            int xaddtime = rightMargin - (int)g.MeasureString(FieldValue, InvFontNumTOP).Width;
            g.DrawString(FieldValue, InvFontNumTOP, BlackBrush, xaddtime, CurrentY);

            CurrentX = leftMargin;
            FieldValue = "# Cash Sales Invoice";
            CurrentY = CurrentY + InvoiceFontHeight + 8;
            
            g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY);
            // Set City
            CurrentX = rightMargin;
            int newy = rightMargin - (int)g.MeasureString("#", InvoiceFont).Width; 
            //// newlly added
           
            FieldValue = "#";
            g.DrawString(FieldValue, InvoiceFont, BlackBrush, newy, CurrentY);

            CurrentX = leftMargin;
            CurrentY = CurrentY + InvoiceFontHeight;
            // CurrentX = CurrentX + (int)g.MeasureString(FieldValue, InvoiceFont).Width + 16;
            FieldValue = "# Reference:";
            g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY);
           // CurrentX = leftMargin + (int)g.MeasureString(FieldValue, InvoiceFont).Width -3;
         
            CurrentX = CurrentX + (int)g.MeasureString(FieldValue, InvoiceFont).Width ;
            FieldValue = "CSI-STI-";
            g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY);
            CurrentX = CurrentX + (int)g.MeasureString(FieldValue, InvoiceFont).Width - 2;

            FieldValue = textBoxrefno.Text;
            CurrentY = CurrentY -1;
            g.DrawString(FieldValue, InvFontNumTOP, BlackBrush, CurrentX, CurrentY);
            CurrentX = rightMargin;
            int newy2 = rightMargin - (int)g.MeasureString("#", InvoiceFont).Width;
            //CurrentY = CurrentY + 4;
            FieldValue = "#";
            g.DrawString(FieldValue, InvoiceFont, BlackBrush, newy2, CurrentY);

            // Set Salesperson:
            CurrentX = leftMargin;
            CurrentY = CurrentY + InvoiceFontHeight -1;
            FieldValue = "# Prepared by:" ;

            g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY);
            CurrentX = leftMargin + (int)g.MeasureString(FieldValue, InvoiceFont).Width - 4;
          //  CurrentY = CurrentY ;
            FieldValue = textBoxpreby.Text;

            g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY);
            CurrentX = rightMargin;
            int newy3 = rightMargin - (int)g.MeasureString("#", InvoiceFont).Width;
            //CurrentY = CurrentY +4;
            FieldValue = "#";
            g.DrawString(FieldValue, InvoiceFont, BlackBrush, newy3, CurrentY);
            // Set Order ID:
            CurrentX = leftMargin;
            CurrentY = CurrentY + InvoiceFontHeight;
            FieldValue = "# To:";
            g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY);
            CurrentX = leftMargin + (int)g.MeasureString(FieldValue, InvoiceFont).Width - 4;
            //CurrentY = CurrentY ;
            FieldValue = textBoxinvto.Text;

            g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY);
             CurrentX = rightMargin;
            int newy4 = rightMargin - (int)g.MeasureString("#", InvoiceFont).Width;
            //CurrentY = CurrentY +4;
            FieldValue = "#";
            g.DrawString(FieldValue, InvoiceFont, BlackBrush, newy4, CurrentY);
            // Set Order Date:
            //CurrentX = leftMargin;
            //CurrentY = CurrentY + InvoiceFontHeight;
            //FieldValue = "Cashier :" + preby;
            //g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY);
            CurrentX = leftMargin;
            CurrentY = CurrentY + InvoiceFontHeight;
            // CurrentX = CurrentX + (int)g.MeasureString(FieldValue, InvoiceFont).Width + 16;
            FieldValue = "Buyer's TIN:";
            g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY);
            CurrentX = leftMargin + (int)g.MeasureString(FieldValue, InvoiceFont).Width - 4;
           // CurrentY = CurrentY+1;
            FieldValue = textBoxbuytin.Text;

            g.DrawString(FieldValue, InvFontNumTOP, BlackBrush, CurrentX, CurrentY);
            // Draw line:
         //   CurrentY = CurrentY + InvoiceFontHeight + 8;
            // g.DrawString("#", InvoiceFont, BlackBrush, leftMargin, CurrentY);
            // g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY, rightMargin, CurrentY);
            // CurrentY = CurrentY + InvSubTitleHeight + 8;
            //float[] dashline = { 7, 5, 7, 5 };
            //Pen blackpen = new Pen(Color.Black, 1);
            //blackpen.DashPattern = dashline;

            //g.DrawLine(blackpen, new Point(leftMargin, CurrentY), new Point(rightMargin, CurrentY));
        }

        private void SetInvoiceData(Graphics g, System.Drawing.Printing.PrintPageEventArgs e)
        {// Set Invoice Table:
            //ReadInvoiceData();
            string FieldValue = "";
            int CurrentRecord = 0;
            int RecordsPerPage = 20; // twenty items in a page
            decimal Amount = 0;
            bool StopReading = false;
            
            // Set Table Head:
            int xpq = leftMargin + (int)g.MeasureString("3", InvFontNum).Width;
            int xdes = leftMargin;
            int xProductName = leftMargin + (int)g.MeasureString("ITI", InvoiceFont).Width ;
          //  CurrentY = CurrentY + InvoiceFontHeight  ;
            //  g.DrawString("ITEM ID", InvoiceFont, BlueBrush, xProductID, CurrentY);

           // int xProductName = xProductID + (int)g.MeasureString("ITEM ID", InvoiceFont).Width + 2;
           // g.DrawString("DESCRIPTION", InvoiceFont, BlueBrush, xProductName, CurrentY);

            int xUnitPrice = xProductName + (int)g.MeasureString("DESCRIPTION", InvoiceFont).Width + 12;
           // g.DrawString("QTY", InvoiceFont, BlueBrush, xUnitPrice, CurrentY);

            int xQuantity = xUnitPrice + (int)g.MeasureString("QTYI", InvoiceFont).Width ;
           // g.DrawString("PRICE", InvoiceFont, BlueBrush, xQuantity, CurrentY);

            AmountPosition = rightMargin - (int)g.MeasureString("123456789", InvFontNum).Width;
            int xAmounts = rightMargin - (int)g.MeasureString("AMOUNT", InvoiceFont).Width;
            //g.DrawString("AMOUNT", InvoiceFont, BlueBrush, AmountPosition, CurrentY);
            //AmountPosition = xDiscount + (int)g.MeasureString("Discount", InvoiceFont).Width + 2;
           // g.DrawString("AMOUNT", InvoiceFont, BlueBrush, xAmounts, CurrentY);
            CurrentY = CurrentY + InvoiceFontHeight + 4;
          
            //        xAmount = xAmount - (int)g.MeasureString(FieldValue, InvoiceFont).Width;
            // g.DrawString("#", InvoiceFont, BlackBrush, leftMargin, CurrentY);
            // g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY, rightMargin, CurrentY);
            // CurrentY = CurrentY + InvSubTitleHeight + 8;
            //float[] dashline = { 7, 5, 7, 5 };
            //Pen blackpen = new Pen(Color.Black, 1);
            //blackpen.DashPattern = dashline;

            //g.DrawLine(blackpen, new Point(leftMargin, CurrentY), new Point(rightMargin, CurrentY));

          

            // Set Invoice Table:
           // CurrentY = CurrentY + 4;
            int rowcount = dataGridView1.Rows.Count;
            //Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            //dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
            //e.Graphics.DrawImage(bm, 0, 0);
            //for (int i=0; i<rowcount-1;i++)
            //{
            //    for(int j = 0; i < dataGridView1.Columns.Count; j++)
            //    {
            //        itemid = Convert.ToString(dataGridView1.Rows[i].Cells["ItemID"].Value);
            //        description = Convert.ToString(dataGridView1.Rows[i].Cells["Description"].Value);
            //        Quan = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Quantity"].Value);
            //        UnitPr = Convert.ToDecimal(dataGridView1.Rows[i].Cells["UnitPrice"].Value);
            //        LTotal = Convert.ToDecimal(dataGridView1.Rows[i].Cells["LineTotal"].Value);

            //        FieldValue = description;
            //        // if Length of (Product Name) > 20, Draw 20 character only
            //        if (FieldValue.Length > 10)
            //            FieldValue = FieldValue.Remove(10, FieldValue.Length - 10);
            //        g.DrawString(FieldValue, InvoiceFont, BlackBrush, xProductName, CurrentY);
            //        FieldValue = String.Format("{0:0.00}", Quan);
            //        g.DrawString(FieldValue, InvoiceFont, BlackBrush, xUnitPrice, CurrentY);
            //        FieldValue = String.Format("{0:0.00}", UnitPr);
            //        g.DrawString(FieldValue, InvoiceFont, BlackBrush, xQuantity, CurrentY);
            //        //FieldValue = String.Format("{0:0.00%}", LTotal);
            //        //g.DrawString(FieldValue, InvoiceFont, BlackBrush, xDiscount, CurrentY);

            //        //Amount = Convert.ToDecimal(LTotal);
            //        // Format Extended Price and Align to Right:
            //        FieldValue = String.Format("{0:0.00}", LTotal);
            //        int xAmount = AmountPosition + (int)g.MeasureString("123456789", InvoiceFont).Width;
            //        xAmount = xAmount - (int)g.MeasureString(FieldValue, InvoiceFont).Width;
            //        g.DrawString(FieldValue, InvoiceFont, BlackBrush, xAmount, CurrentY);
            //        CurrentY = CurrentY + InvoiceFontHeight;
            //    }


            //}

            foreach (DataGridViewRow row in dataGridView1.Rows)
            // for (int row = 0; row< dataGridView1.Rows.Count; row++)
            {

                // int j = dataGridView1.Columns.Count;
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {

                    //rowdata = row.Cells[col].Value.ToString();
                    itemid = Convert.ToString(row.Cells["ItemID"].Value);
                    description = Convert.ToString(row.Cells["Description"].Value);
                    Quan = Convert.ToDecimal(row.Cells["Quantity"].Value);
                    UnitPr = Convert.ToDecimal(row.Cells["UnitPrice"].Value);
                    LTotal = Convert.ToDecimal(row.Cells["LineTotal"].Value);
                }


                //FieldValue = itemid;
                //g.DrawString(FieldValue, InvoiceFont, BlackBrush, xProductID, CurrentY);
                
              
                CurrentY = CurrentY + 4;
                //FieldValue = String.Format("{0:0.00%}", LTotal);
                //g.DrawString(FieldValue, InvoiceFont, BlackBrush, xDiscount, CurrentY);

                string FieldValue1 = String.Format("{0:0}", Quan);
                //g.DrawString(FieldValue, InvFontNum, BlackBrush, xUnitPrice, CurrentY);



                string FieldValue2 = String.Format("{0:0.00}", UnitPr);
                FieldValue = FieldValue1 + "*" + FieldValue2 +"=";
                g.DrawString(FieldValue, InvFontNum, BlackBrush, xProductName, CurrentY);
                //Amount = Convert.ToDecimal(LTotal);
                // Format Extended Price and Align to Right:
                CurrentX = leftMargin;
                CurrentY = CurrentY + InvoiceFontHeightDES;
                FieldValue = description;
                // if Length of (Product Name) > 20, Draw 20 character only
                if (FieldValue.Length > 17)
                    FieldValue = FieldValue.Remove(17, FieldValue.Length - 17);
                g.DrawString(FieldValue, InvoiceFont2, BlackBrush, CurrentX, CurrentY);

               // CurrentY = CurrentY + InvoiceFontHeight + 3;
                string FieldValue6 = String.Format("{0:N2}", LTotal);
                // CurrentX = rightMargin;
                FieldValue = "*" + FieldValue6;
                int xAmount = AmountPosition + (int)g.MeasureString("123456789", InvFontNum).Width;
                //CurrentY = CurrentY -3;
                xAmount = xAmount - (int)g.MeasureString(FieldValue, InvFontNum).Width;
                g.DrawString(FieldValue, InvFontNum, BlackBrush, xAmount, CurrentY);

                CurrentY = CurrentY + InvoiceFontHeightDES;
            }

            SetInvoiceTotal(g);
            SetInvoiceFooter(g);
            //while (CurrentRecord < RecordsPerPage)
            //{
            //FieldValue = itemid;
            //g.DrawString(FieldValue, InvoiceFont, BlackBrush, xProductID, CurrentY);
            //FieldValue = description;
            //// if Length of (Product Name) > 20, Draw 20 character only
            //if (FieldValue.Length > 20)
            //    FieldValue = FieldValue.Remove(20, FieldValue.Length - 20);
            //g.DrawString(FieldValue, InvoiceFont, BlackBrush, xProductName, CurrentY);
            //FieldValue = String.Format("{0:0.00}", Quan);
            //g.DrawString(FieldValue, InvoiceFont, BlackBrush, xUnitPrice, CurrentY);
            //FieldValue = UnitPr;
            //g.DrawString(FieldValue, InvoiceFont, BlackBrush, xQuantity, CurrentY);
            //FieldValue = String.Format("{0:0.00%}", LTotal);
            //g.DrawString(FieldValue, InvoiceFont, BlackBrush, xDiscount, CurrentY);

            //Amount = Convert.ToDecimal(rowdata);
            //// Format Extended Price and Align to Right:
            //FieldValue = String.Format("{0:0.00}", Amount);
            //int xAmount = AmountPosition + (int)g.MeasureString("Extended Price", InvoiceFont).Width;
            //xAmount = xAmount - (int)g.MeasureString(FieldValue, InvoiceFont).Width;
            //g.DrawString(FieldValue, InvoiceFont, BlackBrush, xAmount, CurrentY);
            //CurrentY = CurrentY + InvoiceFontHeight;

            //FieldValue = rdrInvoice["ITEM ID"].ToString();
            //g.DrawString(FieldValue, InvoiceFont, BlackBrush, xProductID, CurrentY);
            //FieldValue = rdrInvoice["DESCRIPTION"].ToString();
            //// if Length of (Product Name) > 20, Draw 20 character only
            //if (FieldValue.Length > 20)
            //    FieldValue = FieldValue.Remove(20, FieldValue.Length - 20);
            //g.DrawString(FieldValue, InvoiceFont, BlackBrush, xProductName, CurrentY);
            //FieldValue = String.Format("{0:0.00}", rdrInvoice["UnitPrice"]);
            //g.DrawString(FieldValue, InvoiceFont, BlackBrush, xUnitPrice, CurrentY);
            //FieldValue = rdrInvoice["Quantity"].ToString();
            //g.DrawString(FieldValue, InvoiceFont, BlackBrush, xQuantity, CurrentY);
            //FieldValue = String.Format("{0:0.00%}", rdrInvoice["Discount"]);
            //g.DrawString(FieldValue, InvoiceFont, BlackBrush, xDiscount, CurrentY);

            //Amount = Convert.ToDecimal(rdrInvoice["ExtendedPrice"]);
            //// Format Extended Price and Align to Right:
            //FieldValue = String.Format("{0:0.00}", Amount);
            //int xAmount = AmountPosition + (int)g.MeasureString("Extended Price", InvoiceFont).Width;
            //xAmount = xAmount - (int)g.MeasureString(FieldValue, InvoiceFont).Width;
            //g.DrawString(FieldValue, InvoiceFont, BlackBrush, xAmount, CurrentY);
            //CurrentY = CurrentY + InvoiceFontHeight;

            ////if (!rdrInvoice.Read())
            ////{
            ////    StopReading = true;
            ////    break;
            ////}

            //    CurrentRecord++;
            //}

            if (CurrentRecord < RecordsPerPage)
                e.HasMorePages = false;
            else
                e.HasMorePages = true;

            if (StopReading)
            {
              //  rdrInvoice.Close();
             //   cnn.Close();
              SetInvoiceTotal(g);
            }

            g.Dispose();
        }

        private void ReadInvoiceData()
        {
            //for (int roows = 0; roows < dataGridView1.Rows.Count; roows++)
            //{
            //    for (int col = 0; col < dataGridView1.Rows[roows].Cells.Count; col++)
            //    {
            //        rowdata = dataGridView1.Rows[roows].Cells[col].Value.ToString();
            //    }
            //}
           // int col = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                   
                    //rowdata = row.Cells[col].Value.ToString();
                    itemid = Convert.ToString(row.Cells["ItemID"].Value);
                    description = Convert.ToString(row.Cells["Description"].Value);
                    Quan = Convert.ToDecimal(row.Cells["Quantity"].Value);
                    UnitPr = Convert.ToDecimal(row.Cells["UnitPrice"].Value);
                    LTotal = Convert.ToDecimal(row.Cells["LineTotal"].Value);
                }
            }
        }

        private void SetInvoiceTotal(Graphics g)
        {// Set Invoice Total:
         // Draw line:
            SubTotal = Convert.ToDecimal(textBoxsubtot.Text);
            SaleFreight = Convert.ToDecimal(textBoxvat.Text);
            InvoiceTotal = Convert.ToDecimal(textBoxgrandtot.Text);
            string NoitemCount = textBoxnoitem.Text;
            CurrentX = leftMargin;
            int CurrentX1 = leftMargin + (int)g.MeasureString("091234567809123456", InvoiceFont).Width;
            CurrentX = rightMargin;
            int CurrentX2 = rightMargin - (int)g.MeasureString("091234567809123456", InvoiceFont).Width;
            CurrentY = CurrentY + 12;
            InvoiceFontHeighttot = (int)(InvoiceFonttotal.GetHeight(g));
            InvoiceFontHeightnum  = (int)(InvFontNum.GetHeight(g));
            // CurrentY = CurrentY + InvoiceFontHeight + 8;
            // g.DrawString("#", InvoiceFont, BlackBrush, leftMargin, CurrentY);
            // g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY, rightMargin, CurrentY);
            // CurrentY = CurrentY + InvSubTitleHeight + 8;
            float[] dashline = { 7, 5, 7, 5 };
            Pen blackpen = new Pen(Color.Black, 1);
            blackpen.DashPattern = dashline;

            g.DrawLine(blackpen, new Point(CurrentX1, CurrentY), new Point(CurrentX2, CurrentY));
            //  g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY, rightMargin, CurrentY);
            // Get Right Edge of Invoice:
            //int AmountPosition = xQuantity + (int)g.MeasureString("PRICE", InvoiceFont).Width + 2;
            //g.DrawString("AMOUNT", InvoiceFont, BlueBrush, AmountPosition, CurrentY);
            int xRightEdg = AmountPosition + (int)g.MeasureString("123456789", InvFontNum).Width;

            // Write Sub Total:
            CurrentX = leftMargin;
           // int xSubTotal = AmountPosition - (int)g.MeasureString("Sub Totalfggfg", InvoiceFont).Width;
            CurrentY = CurrentY + 4;
            g.DrawString("TXBL 1", InvoiceFont2, RedBrush, CurrentX, CurrentY);
            string TotalValues = String.Format("{0:N2}", SubTotal);
            string TotalValue = "*" + TotalValues;
            CurrentY = CurrentY - 3;
            int xTotalValue = xRightEdg - (int)g.MeasureString(TotalValue, InvFontNum).Width;
            g.DrawString(TotalValue, InvFontNum, BlackBrush, xTotalValue, CurrentY);

            // Write VAT:
           // int xOrderFreight = AmountPosition - (int)g.MeasureString("VAT(15%)fgfgf", InvoiceFont).Width;
            CurrentY = CurrentY + InvoiceFontHeightnum;
            g.DrawString("TAX 15%", InvoiceFont2, RedBrush, CurrentX, CurrentY);
            string FreightValues = String.Format("{0:N2}", SaleFreight);
           // string result = FreightValues.ToString("C").Replace("$","");
            string FreightValue = "*" + FreightValues;
            CurrentY = CurrentY - 3;
            int xFreight = xRightEdg - (int)g.MeasureString(FreightValue, InvFontNum).Width;
            g.DrawString(FreightValue, InvFontNum, BlackBrush, xFreight, CurrentY);

            CurrentY = CurrentY + InvoiceFontHeight +8;
            CurrentX = leftMargin;
            int CurrentX3 = leftMargin + (int)g.MeasureString("091234567809123456", InvoiceFont).Width;
            CurrentX = rightMargin;
            int CurrentX4 = rightMargin - (int)g.MeasureString("091234567809123456", InvoiceFont).Width;
            float[] dashlines = { 7, 5, 7, 5 };
            Pen blackpens = new Pen(Color.Black, 1);
            blackpens.DashPattern = dashline;

            g.DrawLine(blackpens, new Point(CurrentX3, CurrentY), new Point(CurrentX4, CurrentY));
            // Write Invoice Total:
            // int xInvoiceTotal = AmountPosition - (int)g.MeasureString("Grand Totalhfhhh", InvoiceFont).Width;
            CurrentX = leftMargin;
            CurrentY = CurrentY + 8;
            g.DrawString("TOTAL", InvTitleFontfTOT, BlackBrush, CurrentX, CurrentY);
            //string InvoiceValue = String.Format("{0:0.00}", InvoiceTotal);
            string InvoiceValues = String.Format("{0:N2}", InvoiceTotal);
            // string result = FreightValues.ToString("C").Replace("$","");
            string InvoiceValue = "*" + InvoiceValues;
            //CurrentY = CurrentY ;
            int xInvoiceValue = xRightEdg - (int)g.MeasureString(InvoiceValue, InvFontNumBold).Width;
            g.DrawString(InvoiceValue, InvFontNumBold, BlackBrush, xInvoiceValue, CurrentY);

            CurrentY = CurrentY + InvoiceFontHeightnum +4;
            g.DrawString("CASH", InvoiceFont2, BlackBrush, CurrentX, CurrentY);

            string InvoiceValuecs = String.Format("{0:N2}", InvoiceTotal);
            string InvoiceValuec = "*" + InvoiceValues;
            int xInvoiceValuec = xRightEdg - (int)g.MeasureString(InvoiceValuec, InvFontNum).Width;
            //CurrentY = CurrentY ;
            g.DrawString(InvoiceValuec, InvFontNum, BlackBrush, xInvoiceValuec, CurrentY);
            //int xNoitem = AmountPosition - (int)g.MeasureString("Grand Totalhfhhh", InvoiceFont).Width;
            CurrentY = CurrentY + InvoiceFontHeightnum;
            g.DrawString("ITEM#", InvoiceFont2, BlackBrush, CurrentX, CurrentY);
            string Noitem = String.Format("{0:0.00}", NoitemCount);
            CurrentY = CurrentY - 3;
            int xNoitemvalue = xRightEdg - (int)g.MeasureString(Noitem, InvFontNum).Width;
            g.DrawString(Noitem, InvFontNum, BlackBrush, xNoitemvalue, CurrentY);
            //bool StopReading = false;
            //if (StopReading)
            //{
            //    //  rdrInvoice.Close();
            //    //   cnn.Close();
            //    SetInvoiceFooter(h);
            //}

            //g.Dispose();

        }

        private void SetInvoiceFooter(Graphics h)
        {
            ReadInvoiceFooter();
            //InvTitlef = "Harambe Technologies";
            //InvSubTitle1f = "22 Gollagul street";
            //InvSubTitle2f = "Addis Ababa, Ethiopia";
            //InvSubTitle3f = "TEL.0913863171";
            //ReadInvoiceFooter();
            //string FieldValue = "";
            //InvoiceFontHeightf = 10;
            // Set Company Name:
            CurrentX = leftMargin;
            // CurrentY = CurrentY + 8;
            //CurrentY = topMargin;
            CurrentY = CurrentY + 8 + InvoiceFontHeight;
            //CurrentX = leftMargin;
            int ImageHeight = 0;

            // Draw Invoice image:
            if (System.IO.File.Exists(InvImage))
            {
                Bitmap oInvImage = new Bitmap(InvImage);
                // Set Image Left to center Image:
                int xImage = CurrentX + (InvoiceWidth - oInvImage.Width) / 2;
                ImageHeight = oInvImage.Height; // Get Image Height
                h.DrawImage(oInvImage, xImage, CurrentY);
            }
            InvTitleHeightn = (int)(InvTitleFontf.GetHeight(h));
            InvSubTitleHeightn = (int)(InvTitleFont.GetHeight(h));

            int Invoi = (int)(InvoiceFontf.GetHeight(h)) + 4;
            //InvTitleHeightn = CurrentY + InvoiceFontHeightf;
            //InvSubTitleHeightn = CurrentY + InvoiceFontHeightf;
            ////InvTitleHeightn = 8;
            //InvSubTitleHeightn = 8;

            // Get Titles Length:
            int lenInvTitlef = (int)h.MeasureString(InvTitlef, InvTitleFontf).Width;
            int lenInvSubTitle1f = (int)h.MeasureString(InvSubTitle1f, InvoiceFontET).Width;
            int lenInvSubTitle2f = (int)h.MeasureString(InvSubTitle2f, InvTitleFontf).Width;
            int lenInvSubTitle3f = (int)h.MeasureString(InvSubTitle3f, InvTitleFontf).Width;
            int lenInvSubTitle4f = (int)h.MeasureString(InvSubTitle4f, InvSubTitleFontf).Width;
            int leninvfooimg = (int)h.MeasureString(footerimg, InvoiceFontET).Width;
            int leninvfooimgt = (int)h.MeasureString("ET", InvSubTitleFontIMGE).Width;
            // Set Titles Left:
            int xInvTitlef = CurrentX + (InvoiceWidth - lenInvTitlef) / 2;
            int xInvSubTitle1f = CurrentX + (InvoiceWidth - lenInvSubTitle1f) / 2;
            int xInvSubTitle1ff = CurrentX + 10 + (InvoiceWidth - lenInvSubTitle1f) / 2;
            int xInvSubTitle2f = CurrentX + (InvoiceWidth - lenInvSubTitle2f) / 2;
            int xInvSubTitle3f = CurrentX + (InvoiceWidth - lenInvSubTitle3f) / 2;
            int xInvSubTitle4f = CurrentX + (InvoiceWidth - lenInvSubTitle4f) / 2;
            int xfooterimg = CurrentX - 14 + (InvoiceWidth - lenInvSubTitle1f) / 2;
            int xfooterimgt = xfooterimg + leninvfooimg;
            int xfooterimgS = xInvSubTitle1ff - leninvfooimg;
            // Draw Invoice Head:
            if (InvTitlef != "")
            {
                CurrentY = CurrentY + 4;
                h.DrawString(InvTitlef, InvTitleFontf, BlueBrush, xInvTitlef, CurrentY);
            }
            if (InvSubTitle1f != "")
            {
                //CurrentY = CurrentY + InvSubTitleHeightn -2;
                //h.DrawString(footerimg, InvSubTitleFontIMGE, BlueBrush, xfooterimg, CurrentY);
                //CurrentY = CurrentY +2;
                CurrentY = CurrentY + InvTitleHeightn +2;
                int CurrentY2 = CurrentY + 1;
                Rectangle rect = new Rectangle(xfooterimgS, CurrentY, xfooterimgt, CurrentY2);
                //int x = xfooterimgt - xfooterimg;
                // int y = CurrentY2 - CurrentY2;
                // Image image = Image.FromFile("E:\\BackUps\\Game1380\\POS\\et.png");
                using (Image src = Image.FromFile("C:\\Users\\acer\\Desktop\\Game1380\\POS\\ok.jpg"))
                {
                    using (Bitmap dst = new Bitmap(leninvfooimgt, Invoi))
                    {
                        //  CurrentX = leftMargin;
                        h.SmoothingMode = SmoothingMode.AntiAlias;
                        h.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        h.DrawImage(src, xfooterimgS, CurrentY, dst.Width, dst.Height);


                        //dst.Save("scale.png", ImageFormat.Png);
                    }
                }
                h.DrawString(InvSubTitle1f, InvoiceFontET, BlueBrush, xInvSubTitle1ff, CurrentY2);

            }
            if (InvSubTitle2f != "")
            {
                CurrentY = CurrentY + InvSubTitleHeightn +3 ;
                h.DrawString(InvSubTitle2f, InvTitleFontf, BlueBrush, xInvSubTitle2f, CurrentY);
            }
            if (InvSubTitle3f != "")
            {
                CurrentY = CurrentY + InvSubTitleHeightn;
                h.DrawString(InvSubTitle3f, InvFontNum, BlueBrush, xInvSubTitle3f, CurrentY);
            }
            if (InvSubTitle4f != "")
            {
                CurrentY = CurrentY + InvSubTitleHeightn + 2;
                h.DrawString(InvSubTitle4f, InvSubTitleFontf, BlueBrush, xInvSubTitle4f, CurrentY);
            }
            if (InvSubTitle5f != "")
            {
                CurrentY = CurrentY + InvSubTitleHeightn;
                h.DrawString(InvSubTitle5f, InvSubTitleFontf, BlueBrush, xInvSubTitle4f, CurrentY);
            }
            //// Draw line:
            //CurrentY = CurrentY + InvSubTitleHeightn + 8;
            //float[] dashline = { 2, 2, 2, 2 };
            //Pen blackpen = new Pen(Color.Black, 2);
            //blackpen.DashPattern = dashline;

            //h.DrawLine(blackpen, new Point(CurrentX, CurrentY), new Point(rightMargin, CurrentY));
            ////g.DrawLine(new Pen(Brushes.Black, 2), CurrentX, CurrentY, rightMargin, CurrentY);
        }

        private void ReadInvoiceFooter()
        {
            //Footer and Image of footer:
            InvTitlef = "E R C A";
            footerimg = "ET";
            InvSubTitle1f = textBox5.Text;
            InvSubTitle2f = "Powered by MarakiPOS 4.0";
           // InvSubTitle3f = "0924 37 21 49W011 515 40 00";
            //string no = Convert.ToString("/011 515 40 00");

            //InvSubTitle3f = "0924 37 21 49" + "" + no;
            //InvSubTitle4f = "Prepared by MarakiPos";
            //InvSubTitle5f = "1637C85D985D038CE31638234E57"; 
            //  InvImage = Application.StartupPath + @"\Images\" + "InvPic.jpg";31
        }

        private void textBoxsaleref_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Random rand = new System.Random((int)System.DateTime.Now.Ticks);
            int random = rand.Next(1, 10000);
            textBoxrefno.Text = "0000"+ Convert.ToString(random);
        }

        private void prnDocumentatach_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            leftMargin = (int)e.MarginBounds.Left;
            rightMargin = (int)e.MarginBounds.Right;
            topMargin = (int)e.MarginBounds.Top;
            bottomMargin = (int)e.MarginBounds.Bottom;
            InvoiceWidth = (int)e.MarginBounds.Width;
            InvoiceHeight = (int)e.MarginBounds.Height;

            if (!ReadInvoice)
                //  ReadInvoiceData();

                //SetInvoiceHeadatach(e.Graphics); // Draw Invoice Head
            SetOrderDataatach(e.Graphics); // Draw Order Data
           // SetInvoiceDataatach(e.Graphics, e); // Draw Invoice Data
                                           // SetInvoiceTotal(e.Graphics,e);
                                           //SetInvoiceFooter(e.Graphics);
            ReadInvoice = true;

        }

        private void SetInvoiceDataatach(Graphics graphics, PrintPageEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SetOrderDataatach(Graphics g)
        {
            ReadInvoiceHead();
            string FieldValue = "";

            InvTitle = textBoxcomname.Text;
            InvSubTitle1 = textBoxstrretadd.Text;
            InvSubTitle2 = textBoxcityctry.Text;
            InvSubTitle3 = "TEL:" + " " + textBoxtele.Text;



            InvoiceFontHeight = (int)(InvoiceFont.GetHeight(g));
            CurrentX = leftMargin;
            CurrentY = CurrentY + 8;
           

            CurrentY = topMargin;
            int curty = CurrentY +2 ;
           

            FieldValue = InvTitle;
            g.DrawString(FieldValue, InvTitleFont, BlackBrush, CurrentX, CurrentY);
            CurrentX = rightMargin;
          
            //g.DrawLine(new Pen(Brushes.Black), xaddinfol, curty, rightMargin, curty);
          
            //FieldValue = "#A";
            //g.DrawString(FieldValue, InvoiceInfo, BlackBrush, xaddinfo, CurrentY);
            //int curtyl = CurrentY + InvoiceFontHeight + 4;
            //g.DrawLine(new Pen(Brushes.Black), xaddinfol, curtyl, rightMargin, curtyl);
            //g.DrawLine(new Pen(Brushes.Black), xaddinfol, curty, xaddinfol, rightMargin);
           // g.DrawLine(new Pen(Brushes.Black), rightMargin, curty, rightMargin, curtyl);
          // g.DrawRectangles(new Pen(Brushes.Black), RectangleF[xaddinfol, xaddinfol, rightMargin, rightMargin] );
            CurrentX = leftMargin;
            CurrentY = CurrentY + InvoiceFontHeight;
            FieldValue = InvSubTitle1;
            g.DrawString(FieldValue, InvSubTitleFont, BlackBrush, CurrentX, CurrentY);
           // CurrentX = rightMargin;
            CurrentY = CurrentY + InvoiceFontHeight;
            //  int xaddtime = rightMargin - (int)g.MeasureString("Infos", InvoiceFont).Width;   
            FieldValue = InvSubTitle2;
            g.DrawString(FieldValue, InvTitleFont, BlackBrush, CurrentX, CurrentY);
            CurrentX = leftMargin;
            CurrentY = CurrentY + InvoiceFontHeight;
            //  int xaddtime = rightMargin - (int)g.MeasureString("Infos", InvoiceFont).Width;   
            FieldValue = "TIN:-";
            g.DrawString(FieldValue, InvTitleFont, BlackBrush, CurrentX, CurrentY);
           //CurrentX = rightMargin;
            CurrentY = CurrentY + InvoiceFontHeight;
            //  int xaddtime = rightMargin - (int)g.MeasureString("Infos", InvoiceFont).Width;   
            FieldValue = InvSubTitle3;
            g.DrawString(FieldValue, InvTitleFont, BlackBrush, CurrentX, CurrentY);
            CurrentX = leftMargin;
            //CurrentY = CurrentY + InvoiceFontHeight + 4;
            //FieldValue = "Cash Sales Invoice ";
            //g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY);
            // Set City:

            //CurrentX = leftMargin;
            //CurrentY = CurrentY + InvoiceFontHeight;
            //// CurrentX = CurrentX + (int)g.MeasureString(FieldValue, InvoiceFont).Width + 16;
            //FieldValue = "Reference: " + refno;
            //g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY);
            //// Set Salesperson:
            //CurrentX = leftMargin;
            //CurrentY = CurrentY + InvoiceFontHeight;
            //FieldValue = "PrePared by: " + preby;
            //g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY);
            //// Set Order ID:
            //CurrentX = leftMargin;
            //CurrentY = CurrentY + InvoiceFontHeight;
            //FieldValue = "To: " + to;
            //g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY);
            //// Set Order Date:
            //CurrentX = leftMargin;
            //CurrentY = CurrentY + InvoiceFontHeight;
            //// CurrentX = CurrentX + (int)g.MeasureString(FieldValue, InvoiceFont).Width + 16;
            //FieldValue = "Buyer's TIN: " + buyertin;
            //g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY);

            // Draw line:
            CurrentY = CurrentY + InvoiceFontHeight;
            string InvTitleatach = "Cash Sales Atachment";
            int lenInvTitlef = (int)g.MeasureString(InvTitleatach, InvTitleFontf).Width;

            // Set Titles Left:
            int xInvTitlef = CurrentX + (InvoiceWidth - lenInvTitlef) / 2;
            FieldValue = InvTitleatach;
            g.DrawString(FieldValue, InvTitleFont, BlueBrush, xInvTitlef, CurrentY);
            // Set Company Name:
            // g.DrawString("#", InvoiceFont, BlackBrush, leftMargin, CurrentY);
            //  g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY, rightMargin, CurrentY);
            // CurrentY = CurrentY + InvSubTitleHeight + 8;
            //    float[] dashline = { 7, 5, 7, 5 };
            //    Pen blackpen = new Pen(Color.Black, 1);
            //    blackpen.DashPattern = dashline;

            //    g.DrawLine(blackpen, new Point(leftMargin, CurrentY), new Point(rightMargin, CurrentY));

        }
    }
}
