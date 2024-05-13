using System;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Collections.Generic;
using WpfApp3.Model;
using System.Data;
using System.Windows.Documents;
using MySqlX.XDevAPI.Relational;
using System.Security.Cryptography;

namespace WpfApp3.Access
{
    public class LocalDataAccess
    {
        private LocalDataAccess() { }
        public static LocalDataAccess Instance;
        public static LocalDataAccess GetInstance()
        {
            if (Instance == null)
                Instance = new LocalDataAccess();
            return Instance;
        }
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        private void Dispose()
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }
            if (adapter != null)
            {
                adapter.Dispose();
                adapter = null;
            }
        }
        private bool DBConnection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            if (conn == null)
                conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }      
        public List<ProductionData> GetProductions()
        {
            if(DBConnection())
            {
                List<ProductionData> productions = new List<ProductionData>(); 
                try
                {
                    DataSet dataSet = new DataSet();
                    string sql = @"select * from production order by Timeat,typeis";
                    cmd = new MySqlCommand();
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataSet);
                    ProductionData data = null;
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        data = new ProductionData();
                        data.DayProcessNumber= row.Field<int>("ProcessNumber");
                        data.Time= row.Field<DateTime>("Timeat").ToString("yyyy-MM-dd");
                        data.Type= row.Field<string>("typeis");
                        data.PassRate= row.Field<double>("PassRate");
                        productions.Add(data);
                    }
                        return productions;
                }
                catch (Exception)
                {
                    throw new Exception("数据库连接失败");
                }
                finally 
                {
                    Dispose();
                }
            }
            else
                throw new Exception("数据库打开失败");
        }
        public List<ProductData> GetProducts()
        {
            if (DBConnection())
            {
                List<ProductData> productions = new List<ProductData>();
                try
                {
                    DataSet dataSet = new DataSet();
                    string sql = @"select * from product order by id";
                    cmd = new MySqlCommand();
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataSet);
                    ProductData data = null;
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        data = new ProductData();
                        data.ProductionID = row.Field<string>("ProductID");
                        data.Time = row.Field<DateTime>("CreateAt").ToString();
                        data.Type = row.Field<string>("typeis");
                        data.ZHeight = row.Field<double>("ZHeight");
                        data.ProcessTime = TimeSpan.FromSeconds(double.Parse(row.Field<string>("ProcessTime")));
                        data.Power = row.Field<double>("Power");
                        data.IsPass = row.Field<bool>("IsPass");
                        productions.Add(data);
                    }
                    return productions;
                }
                catch (Exception)
                {
                    throw new Exception("数据库连接失败");
                }
                finally
                {
                    Dispose();
                }
            }
            else
                throw new Exception("数据库打开失败");
        }
        public List<ProductData> GetProducts(MySqlParameter[] parameters)
        {
            if (DBConnection())
            {
                List<ProductData> productions = new List<ProductData>();
                try
                {
                    DataSet dataSet = new DataSet();
                    string sql = @"select * from product order by id limit @start,@count";
                    cmd = new MySqlCommand();
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    foreach (var parameter in parameters)
                    {
                        if (parameter.Value == null || parameter.Value.ToString() == "")
                        {
                            parameter.Value = DBNull.Value;
                        }
                        cmd.Parameters.Add(parameter);
                    }
                    adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataSet);
                    ProductData data = null;
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        data = new ProductData();
                        data.ProductionID = row.Field<string>("ProductID");
                        data.Time = row.Field<DateTime>("CreateAt").ToString();
                        data.Type = row.Field<string>("typeis");
                        data.ZHeight = row.Field<double>("ZHeight");
                        data.ProcessTime = TimeSpan.FromMilliseconds(double.Parse(row.Field<string>("ProcessTime")));
                        data.Power = row.Field<double>("Power");
                        data.IsPass = row.Field<bool>("IsPass");
                        productions.Add(data);
                    }
                    return productions;
                }
                catch (Exception)
                {
                    throw new Exception("数据库连接失败");
                }
                finally
                {
                    Dispose();
                }
            }
            else
                throw new Exception("数据库打开失败");
        }
        public int GetRecordCount(params object[] args)
        {
            int count = 0;
            if (DBConnection())
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    string str = "select count(1) from {0}";
                    string sql = string.Format(str, (object[])args);
                    cmd = new MySqlCommand();
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataSet);
                    count = Convert.ToInt32(dataSet.Tables[0].Rows[0].ItemArray[0]);
                    return count;
                }
                catch (Exception)
                {
                    throw new Exception("数据库连接失败");
                }
                finally
                {
                    Dispose();
                }
            }
            else
                throw new Exception("数据库打开失败");
        
        }
        public List<ProductData> SelectProduct(MySqlParameter[] parameters)
        {           
            if (DBConnection())
            {
                List<ProductData> datas= new List<ProductData>();
                try
                {
                    DataSet dataSet = new DataSet();
                    string sql = "select * from product where ProductID=@productname";
                    cmd = new MySqlCommand();
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    foreach (var parameter in parameters)
                    {
                        if (parameter.Value == null || parameter.Value.ToString() == "")
                        {
                            parameter.Value = DBNull.Value;
                        }
                        cmd.Parameters.Add(parameter);
                    }
                    adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataSet);
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        ProductData data = new ProductData();
                        data.ProductionID = row.Field<string>("ProductID");
                        data.Time = row.Field<DateTime>("CreateAt").ToString();
                        data.Type = row.Field<string>("typeis");
                        data.ZHeight = row.Field<double>("ZHeight");
                        data.ProcessTime = TimeSpan.FromMilliseconds(double.Parse(row.Field<string>("ProcessTime")));
                        data.Power = row.Field<double>("Power");
                        data.IsPass = row.Field<bool>("IsPass");
                        datas.Add(data);
                    }
                    return datas;
                }
                catch (Exception)
                {
                    throw new Exception("数据库连接失败");
                }
                finally
                {
                    Dispose();
                }
            }
            else
                throw new Exception("数据库打开失败");

        }
    }
}
