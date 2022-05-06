using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Printing;

namespace dbProject
{
    public partial class Form1 : Form
    {

        SqlConnection memberConnection;
        SqlCommand memberCommand;
        SqlDataAdapter memberAdapter;
        DataTable memberTable;
        CurrencyManager memberManager;
        public Form1()
        {
            InitializeComponent();
        }

        private void membersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.membersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.clutchDBDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = Path.GetFullPath("ClutchDB.mdf");
            // connect to Phone database
            memberConnection = new
        SqlConnection("Data Source=.\\SQLEXPRESS; AttachDBFilename=" + path + "; Integrated Security=True; Connect Timeout=30;" +
        "User Instance=True");
            memberConnection.Open();
            // establish command object
            memberCommand = new SqlCommand("SELECT * FROM Members ORDER BY MemberFName", memberConnection);
            // establish data adapter / data table
            memberAdapter = new SqlDataAdapter();
            memberAdapter.SelectCommand = memberCommand;
            memberTable = new DataTable();
            memberAdapter.Fill(memberTable);

            // bind controls to data table
            txtFirstName.DataBindings.Add("Text", memberTable, "MemberFName");
            txtLastName.DataBindings.Add("Text", memberTable, "MemberLName");
            txtAge.DataBindings.Add("Text", memberTable, "MemberAge");
            txtGender.DataBindings.Add("Text", memberTable, "MemberGender");
            txtEmail.DataBindings.Add("Text", memberTable, "MemberEmail");
            txtPhone.DataBindings.Add("Text", memberTable, "MemberPhone");
            // establish curency manager
            memberManager = (CurrencyManager)
                this.BindingContext[memberTable];
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // close the connection
            memberConnection.Close();
            // dispose of the objects
            memberConnection.Dispose();
            memberCommand.Dispose();
            memberAdapter.Dispose();
            memberTable.Dispose();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            memberManager.Position = 0;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            memberManager.Position--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            memberManager.Position++;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            memberManager.Position = memberManager.Count - 1;
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            // instantiates form 2
            Form2 f2 = new Form2(ref memberTable);
            // Shows Form2
            f2.Show(); 
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // Declaring document
            PrintDocument recordDocument;
            // Create the document and name it
            recordDocument = new PrintDocument();
            recordDocument.DocumentName = "Member Info";
            // Add code handler
            recordDocument.PrintPage += new PrintPageEventHandler(this.PrintRecord);
            dlgPreview.Document = recordDocument;
            dlgPreview.ShowDialog();
            // Print document
            recordDocument.Print();
            // dispose of document
            recordDocument.Dispose();
        }

        private void PrintRecord (object sender, PrintPageEventArgs e)
        {
            // print graphic and heading ( 1 inch in height)
            Pen myPen = new Pen(Color.Black, 3);
            e.Graphics.DrawRectangle(myPen, e.MarginBounds.Left, e.MarginBounds.Top, e.MarginBounds.Width, 100);
            // print heading
            string s = "Members Info";
            Font myFont = new Font("Rockwell", 24, FontStyle.Bold);
            SizeF sSize = e.Graphics.MeasureString(s, myFont);
            e.Graphics.DrawString(s, myFont, Brushes.Black, e.MarginBounds.Left + 100 +
                Convert.ToInt32(0.5 * (e.MarginBounds.Width - 100 - sSize.Width)),
                e.MarginBounds.Top + Convert.ToInt32(0.5 * (100 - sSize.Height)));
            myFont = new Font("Rockwell", 12, FontStyle.Regular);
            int y = 300;
            int dy = Convert.ToInt32(e.Graphics.MeasureString("S",
                myFont).Height);
            // print first name
            e.Graphics.DrawString("First Name: " + txtFirstName.Text, myFont,
                Brushes.Black, e.MarginBounds.Left, y);
            y += 2 * dy;
            // print last name
            e.Graphics.DrawString("Last Name: " + txtLastName.Text, myFont,
                Brushes.Black, e.MarginBounds.Left, y);
            y += 2 * dy;
            // print age
            e.Graphics.DrawString("Age: " + txtAge.Text, myFont,
                Brushes.Black, e.MarginBounds.Left, y);
            y += 2 * dy;
            // print gender
            e.Graphics.DrawString("Gender: " + txtGender.Text, myFont,
                Brushes.Black, e.MarginBounds.Left, y);
            y += 2 * dy;
            // print email
            e.Graphics.DrawString("Email: " + txtEmail.Text, myFont,
                Brushes.Black, e.MarginBounds.Left, y);
            y += 2 * dy;
            // print phone
            e.Graphics.DrawString("Phone #: " + txtPhone.Text, myFont,
                Brushes.Black, e.MarginBounds.Left, y);
            y += 2 * dy;
            e.HasMorePages = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // instantiates form 3
            Form3 f3 = new Form3();
            // Shows Form2
            f3.Show();
        }
    }
}
