using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace dbProject
{
    public partial class Form3 : Form
    {
        SqlConnection memberConnection;
        SqlCommand memberCommand;
        SqlDataAdapter memberAdapter;
        DataTable memberTable;
        CurrencyManager memberManager;

        int myBookmark;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
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
            cboGender.DataBindings.Add("Text", memberTable, "MemberGender");
            txtEmail.DataBindings.Add("Text", memberTable, "MemberEmail");
            txtPhone.DataBindings.Add("Text", memberTable, "MemberPhone");
            label7.DataBindings.Add("Text", memberTable, "MemberID");
            // clear fields
            txtFirstName.Clear();
            txtLastName.Clear();
            txtAge.Clear();
            cboGender.Text = String.Empty;
            txtEmail.Clear();
            txtPhone.Clear();
            // establish curency manager
            memberManager = (CurrencyManager)
                this.BindingContext[memberTable];
            // memberManager.AddNew();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // clear fields
            txtFirstName.Clear();
            txtLastName.Clear();
            txtAge.Clear();
            cboGender.Text = String.Empty;
            txtEmail.Clear();
            txtPhone.Clear();
            memberManager.AddNew();
            //label7.Text = (int.Parse(label7.Text) + 1).ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            memberManager.EndCurrentEdit();
            myBookmark = memberManager.Position;
            MessageBox.Show(memberManager.Count+"");
            // memberTable.DefaultView.Sort = "MemberFName";
            MessageBox.Show("Record saved.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            // save updated table 
            SqlCommandBuilder memberAdapterUpdate = new SqlCommandBuilder(memberAdapter);
            memberAdapter.Update(memberTable);
            // close connection
            memberConnection.Close();
            // dispose objects
            memberConnection.Dispose();
            memberCommand.Dispose();
            memberAdapter.Dispose();
            memberTable.Dispose();
        }
    }
}
