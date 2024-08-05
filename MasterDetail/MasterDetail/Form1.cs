using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterDetail
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void salesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.salesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.erpAssistDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'erpAssistDataSet.SalesDtl' 資料表。您可以視需要進行移動或移除。
            this.salesDtlTableAdapter.Fill(this.erpAssistDataSet.SalesDtl);
            // TODO: 這行程式碼會將資料載入 'erpAssistDataSet.Sales' 資料表。您可以視需要進行移動或移除。
            this.salesTableAdapter.Fill(this.erpAssistDataSet.Sales);

        }

        private void testToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.salesTableAdapter.Test(this.erpAssistDataSet.Sales);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
