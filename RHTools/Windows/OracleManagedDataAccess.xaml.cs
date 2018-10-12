using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RHTools
{
    public partial class OracleManagedDataAccess : Window
    {
        ConnForOracle conn;

        public OracleManagedDataAccess()
        {
            InitializeComponent();
        }

        private void Button_Click_Connect(object sender, RoutedEventArgs e)
        {
            string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + TextBoxHost.Text.Trim()
                + ")(PORT=1521))(CONNECT_DATA=(SID=" + TextBoxDb.Text.Trim()
                + ")));User Id=" + TextBoxAccount.Text.Trim()
                + ";Password=" + TextBoxPassword.Text.Trim();
            conn = new ConnForOracle(connString);
            if (conn.OpenConn())
            {
                ButtonExecute.IsEnabled = true;
                MessageBox.Show("Connected!");
            }
            else
            {
                ButtonExecute.IsEnabled = false;
                MessageBox.Show("Failed!");
            }
        }

        private void Button_Click_Execute(object sender, RoutedEventArgs e)
        {
            DataSet ds = conn.ReturnDataSet(TextBoxSQL.Text.Trim(), "ds1");
            DataGridResult.ItemsSource = ds.Tables[0].DefaultView;
        }
    }

    class ConnForOracle
    {
        protected OracleConnection Connection;
        public string connectionString;
        public ConnForOracle()
        {
        }

        #region 带参数的构造函数
        /// <summary>  
        /// 带参数的构造函数  
        /// </summary>  
        /// <param name="ConnString">数据库联接字符串</param>  
        public ConnForOracle(string ConnString)
        {
            Connection = new OracleConnection(ConnString);
        }
        #endregion

        #region 打开数据库
        /// <summary>  
        /// 打开数据库  
        /// </summary>  
        public bool OpenConn()
        {
            try
            {
                if (this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region 关闭数据库联接
        /// <summary>  
        /// 关闭数据库联接  
        /// </summary>  
        public void CloseConn()
        {
            if (Connection.State == ConnectionState.Open)
                Connection.Close();
        }
        #endregion

        #region 执行SQL语句，返回数据到DataSet中
        /// <summary>  
        /// 执行SQL语句，返回数据到DataSet中  
        /// </summary>  
        /// <param name="sql">sql语句</param>  
        /// <param name="DataSetName">自定义返回的DataSet表名</param>  
        /// <returns>返回DataSet</returns>  
        public DataSet ReturnDataSet(string sql, string DataSetName)
        {
            DataSet dataSet = new DataSet();
            //OpenConn();
            OracleDataAdapter OraDA = new OracleDataAdapter(sql, Connection);
            OraDA.Fill(dataSet, DataSetName);
            //    CloseConn();  
            return dataSet;
        }
        #endregion

        /// <summary>
        /// 从数据库中查询数据
        /// </summary>
        /// <param name="sqlSearch">查询用SQL语句</param>
        /// <returns></returns>
        public DataTable GetDataTableFromSQL(string sqlSearch)
        {
            //返回的DataTable
            DataTable dt = new System.Data.DataTable();

            //读取数据
            {
                try
                {
                    //打开连接
                    if (this.Connection.State != ConnectionState.Open)
                        this.Connection.Open();
                    OracleCommand cmd = this.Connection.CreateCommand();
                    cmd.CommandText = sqlSearch;
                    OracleDataReader odr = cmd.ExecuteReader();

                    //读取列信息
                    for (int i = 0; i < odr.FieldCount; i++)
                    {
                        dt.Columns.Add(odr.GetName(i));
                    }

                    //读取各行存入到DataTable中
                    while (odr.Read())
                    {
                        string[] s = new string[odr.FieldCount];
                        for (int i = 0; i < odr.FieldCount; i++)
                        {
                            s[i] = odr.GetValue(i).ToString();
                        }
                        dt.Rows.Add(s);
                    }

                }
                catch (Exception ex)
                {
                    //输出异常后抛出
                    Console.WriteLine("Function SearchCommand: " + ex.Message);
                    //throw new Exception(ex.Message);
                    MessageBox.Show("异常SQL->\r" + sqlSearch);
                }
                finally
                {
                    //关闭连接
                    //oc.Close();
                }
            }

            return dt;
        }
    }
}
