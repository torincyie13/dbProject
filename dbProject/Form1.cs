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
    }
}
