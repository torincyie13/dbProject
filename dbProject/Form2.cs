using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace dbProject
{
    public partial class Form2 : Form
    {
        public Form2(ref DataTable dt)
        {
            InitializeComponent();
            membersDataGridView.DataSource = dt;
        }

        private void membersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.membersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.clutchDBDataSet);

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'clutchDBDataSet.Members' table.
            // You can move, or remove it, as needed.
            // this.membersTableAdapter.Fill(this.clutchDBDataSet.Members);

        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
