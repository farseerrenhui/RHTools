using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace RHTools
{
    public partial class ExcelMicrosoftOleDb : Window
    {
        public ExcelMicrosoftOleDb()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Excel数据导入到Datatable
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private DataTable getExcelDatatable(String fileUrl, String table)
        {
            const string cmdText = "Provider=Microsoft.Ace.OleDb.12.0;Data Source={0};Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
            DataTable dt = null;
            //建立连接
            OleDbConnection conn = new OleDbConnection(string.Format(cmdText, fileUrl));
            try
            {
                //打开连接
                if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                //获取Excel的第一个Sheet名称
                string sheetName = schemaTable.Rows[0]["TABLE_NAME"].ToString().Trim();
                //查询sheet中的数据
                string strSql = "select * from [" + sheetName + "]";
                OleDbDataAdapter da = new OleDbDataAdapter(strSql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, table);
                dt = ds.Tables[0];
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                MessageBox.Show("Excel文件内容读取有误");
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        private void Button_Click_LoadExcelFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataTable dt = getExcelDatatable(file.FileName, "mytable");
                if (dt != null)
                {
                    DataGridExcel.ItemsSource = dt.DefaultView;
                }
            }
        }
    }
}
