using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using MSSQL_Framework;

namespace Excel_Load
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //發票主檔的讀取
        private void ExcelButton_Click(object sender, EventArgs e)
        {
            // 使用檔案對話框選擇檔案
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                // 建立Excel應用程式物件
                Excel.Application excelApp = new Excel.Application();

                // 開啟Excel檔案
                Excel.Workbook workbook = excelApp.Workbooks.Open(filePath);

                // 選取第一個工作表
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                // 取得使用的範圍
                Excel.Range range = worksheet.UsedRange;

                // 取得行數與列數
                int rowCount = range.Rows.Count;
                int colCount = range.Columns.Count;

                // 清除原有的資料
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                // 加入列標題
                for (int i = 1; i <= colCount; i++)
                {
                    string columnName = Convert.ToString((range.Cells[1, i] as Excel.Range).Value2);
                    dataGridView1.Columns.Add("Column" + i, columnName);
                }

                // 加入資料
                for (int row = 2; row <= rowCount; row++)
                {
                    List<string> rowData = new List<string>();
                    for (int col = 1; col <= colCount; col++)
                    {
                        string cellValue = Convert.ToString((range.Cells[row, col] as Excel.Range).Value2);
                        rowData.Add(cellValue);
                    }
                    dataGridView1.Rows.Add(rowData.ToArray());
                }

                // 關閉Excel檔案
                workbook.Close();
                excelApp.Quit();

                MessageBox.Show("讀取完畢");
            }
        }


        //發票主檔的匯入
        private void MSSQLbutton_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            // 添加欄到DataTable，欄位標題使用DataGridView的欄位標題
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                dt.Columns.Add(column.HeaderText);
            }

            // 添加列到DataTable，列數據使用DataGridView的值
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // 檢查是否為空白行
                if (!IsRowEmpty(row))
                {
                    DataRow dataRow = dt.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dataRow[cell.ColumnIndex] = cell.Value;
                    }
                    dt.Rows.Add(dataRow);
                }
            }

            MessageBox.Show("讀取完畢");


            //=========================================SQL Server連線===============================
            SqlDBConnection sdc = new SqlDBConnection();
            //sdc.connString為dll檔中的連線字串用法
            using (SqlConnection connMS = new SqlConnection(sdc.connString))
            {
                // 開啟連接
                connMS.Open();

                string insertQuery = "INSERT INTO dbo.EInvoiceCheck (InvoiceNo, Format, Status, InvoiceDate, BuyCompanyId, Buyer, SellCompanyId, Seller, SendDate, SalesAmount, ZeroTaxAmount, TaxFreeAmount, Tax, Total, TaxType, Remark, Sender) " +
                                     "VALUES (@InvoiceNo, @Format, @Status, @InvoiceDate, @BuyCompanyId, @Buyer, @SellCompanyId, @Seller, @SendDate, @SalesAmount, @ZeroTaxAmount, @TaxFreeAmount, @Tax, @Total, @TaxType, @Remark, @Sender)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, connMS))
                {
                    cmd.Parameters.Add("@InvoiceNo", SqlDbType.NVarChar, 20);
                    cmd.Parameters.Add("@Format", SqlDbType.NVarChar, 5);
                    cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 10);
                    cmd.Parameters.Add("@InvoiceDate", SqlDbType.Date);
                    cmd.Parameters.Add("@BuyCompanyId", SqlDbType.NVarChar, 10);
                    cmd.Parameters.Add("@Buyer", SqlDbType.NVarChar, 50);
                    cmd.Parameters.Add("@SellCompanyId", SqlDbType.NVarChar, 10);
                    cmd.Parameters.Add("@Seller", SqlDbType.NVarChar, 50);
                    cmd.Parameters.Add("@SendDate", SqlDbType.Date);
                    cmd.Parameters.Add("@SalesAmount", SqlDbType.Float);
                    cmd.Parameters.Add("@ZeroTaxAmount", SqlDbType.Float);
                    cmd.Parameters.Add("@TaxFreeAmount", SqlDbType.Float);
                    cmd.Parameters.Add("@Tax", SqlDbType.Float);
                    cmd.Parameters.Add("@Total", SqlDbType.Float);
                    cmd.Parameters.Add("@TaxType", SqlDbType.NVarChar, 10);
                    cmd.Parameters.Add("@Remark", SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@Sender", SqlDbType.NVarChar, 50);

                    foreach (DataRow row in dt.Rows)
                    {
                        cmd.Parameters["@InvoiceNo"].Value = row["發票號碼"];
                        cmd.Parameters["@Format"].Value = row["格式代號"];
                        cmd.Parameters["@Status"].Value = row["發票狀態"];
                        cmd.Parameters["@InvoiceDate"].Value = row["發票日期"];
                        cmd.Parameters["@BuyCompanyId"].Value = row["買方統一編號"];
                        cmd.Parameters["@Buyer"].Value = row["買方名稱"];
                        cmd.Parameters["@SellCompanyId"].Value = row["賣方統一編號"];
                        cmd.Parameters["@Seller"].Value = row["賣方名稱"];
                        cmd.Parameters["@SendDate"].Value = row["寄送日期"];
                        cmd.Parameters["@SalesAmount"].Value = row["銷售額合計"];
                        cmd.Parameters["@ZeroTaxAmount"].Value = row["零稅銷售額"];
                        cmd.Parameters["@TaxFreeAmount"].Value = row["免稅銷售額"];
                        cmd.Parameters["@Tax"].Value = row["營業稅"];
                        cmd.Parameters["@Total"].Value = row["總計"];
                        cmd.Parameters["@TaxType"].Value = row["課稅別"];
                        cmd.Parameters["@Remark"].Value = row["總備註"];
                        cmd.Parameters["@Sender"].Value = row["傳送方名稱"];

                        cmd.ExecuteNonQuery();
                    }
                }
                connMS.Close();
            }
        }


        //發票明細的讀取
        private void ExcelButton2_Click(object sender, EventArgs e)
        {
            // 使用檔案對話框選擇檔案
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                // 建立Excel應用程式物件
                Excel.Application excelApp = new Excel.Application();

                // 開啟Excel檔案
                Excel.Workbook workbook = excelApp.Workbooks.Open(filePath);

                // 選取第二個工作表
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[2];

                // 取得使用的範圍
                Excel.Range range = worksheet.UsedRange;

                // 取得行數與列數
                int rowCount = range.Rows.Count;
                int colCount = range.Columns.Count;

                // 清除原有的資料
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                // 加入列標題
                for (int i = 1; i <= colCount; i++)
                {
                    string columnName = Convert.ToString((range.Cells[1, i] as Excel.Range).Value2);
                    dataGridView1.Columns.Add("Column" + i, columnName);
                }

                // 加入資料
                for (int row = 2; row <= rowCount; row++)
                {
                    List<string> rowData = new List<string>();
                    for (int col = 1; col <= colCount; col++)
                    {
                        string cellValue = Convert.ToString((range.Cells[row, col] as Excel.Range).Value2);
                        rowData.Add(cellValue);
                    }
                    dataGridView1.Rows.Add(rowData.ToArray());
                }

                // 關閉Excel檔案
                workbook.Close();
                excelApp.Quit();

                MessageBox.Show("讀取完畢");
            }
        }


        //發票明細的匯入
        private void MSSQLbutton2_Click(object sender, EventArgs e)
        {
            DataTable dt2 = new DataTable();

            // 添加欄到DataTable，欄位標題使用DataGridView的欄位標題
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                dt2.Columns.Add(column.HeaderText);
            }

            // 添加列到DataTable，列數據使用DataGridView的值
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // 檢查是否為空白行
                if (!IsRowEmpty(row))
                {
                    DataRow dataRow = dt2.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dataRow[cell.ColumnIndex] = cell.Value;
                    }
                    dt2.Rows.Add(dataRow);
                }
            }

            MessageBox.Show("讀取完畢");


            //=========================================SQL Server連線===============================
            SqlDBConnection sdc = new SqlDBConnection();
            //sdc.connString為dll檔中的連線字串用法
            using (SqlConnection connMS = new SqlConnection(sdc.connString))
            {
                // 開啟連接
                connMS.Open();

                string insertQuery = "INSERT INTO dbo.EInvoiceCheckDetail (InvoiceNo, InvoiceDate, SNo, Items, Quantity, Unit, SubTotal) " +
                         "VALUES (@InvoiceNo, @InvoiceDate, @SNo, @Items, @Quantity, @Unit, @SubTotal)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, connMS))
                {
                    cmd.Parameters.Add("@InvoiceNo", SqlDbType.NVarChar, 20);
                    cmd.Parameters.Add("@InvoiceDate", SqlDbType.Date);
                    cmd.Parameters.Add("@SNo", SqlDbType.Int);
                    cmd.Parameters.Add("@Items", SqlDbType.NVarChar, 300);
                    cmd.Parameters.Add("@Quantity", SqlDbType.Float);
                    cmd.Parameters.Add("@Unit", SqlDbType.NVarChar, 10);
                    cmd.Parameters.Add("@SubTotal", SqlDbType.Float);

                    foreach (DataRow row in dt2.Rows)
                    {
                        cmd.Parameters["@InvoiceNo"].Value = row["發票號碼"];
                        cmd.Parameters["@InvoiceDate"].Value = row["發票日期"];
                        cmd.Parameters["@SNo"].Value = row["序號"];
                        cmd.Parameters["@Items"].Value = row["發票品名"];
                        cmd.Parameters["@Quantity"].Value = row["數量"];
                        cmd.Parameters["@Unit"].Value = row["單位"];
                        cmd.Parameters["@SubTotal"].Value = row["小計"];

                        cmd.ExecuteNonQuery();
                    }
                }
                connMS.Close();
            }
        }


        // 檢查DataGridView的行是否為空白行
        private bool IsRowEmpty(DataGridViewRow row)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.Value != null && !string.IsNullOrEmpty(cell.Value.ToString()))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
