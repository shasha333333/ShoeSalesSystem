using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ShoeSalesSystem.Entities;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace ShoeSalesSystem.Helpers
{
    internal class SqlHelper
    {

        public static string server = "127.0.0.1";
        public static string database = "shoesalessystem";
        public static string uid = "root";
        public static string pwd = "admin";
        public static string conStr = String.Format("server={0};database={1};uid={2};pwd={3};charset=utf8", server, database, uid, pwd);


        /// <summary>
        /// 测试连接
        /// </summary>

        public static void Test()
        {
            using (MySqlConnection connect = new MySqlConnection(conStr))
            {
                string sql = "select * from customerorders;";
                MySqlCommand cmd = new MySqlCommand(sql, connect);
                connect.Open();
                MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    Console.WriteLine(dr[0].ToString() + " "
                                    + dr[1].ToString() + " "
                                    + dr[2].ToString());
                }
            }
            Console.WriteLine("111");

        }

        public static DataTable Query(string sql)
        {
            using (var con = new MySqlConnection(conStr))
            {
                using (var cmd = new MySqlCommand(sql, con))
                {
                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        try
                        {
                            var dt = new DataTable();
                            da.Fill(dt);
                            return dt;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 增删改函数,传入sql语句,返回变更的数据库条数
        /// </summary>
        /// <exception cref="Exception">更改出错就会抛出该异常</exception>
        public static bool Update(string sql)
        {
            using (var con = new MySqlConnection(conStr))
            {
                using (var cmd = new MySqlCommand(sql, con))
                {
                    try
                    {
                        con.Open();
                        cmd.CommandType = CommandType.Text;
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public static DataTable GetAllOrders(int shoeID)
        {
            string sql = $"SELECT * FROM customerorders WHERE ShoeID = {shoeID};";
            return Query(sql);
        }

        public static int GetWomenShoesNum()
        {
            string sql = "SELECT COUNT(*) AS TotalCount FROM WomenShoes;";
            DataTable dataTable = Query(sql);
            int totalCount = Convert.ToInt32(dataTable.Rows[0][0]);
            return totalCount;
        }

        public static bool SubStock(int shoeID,int subNum)
        {
            string sql = $"UPDATE WomenShoes SET StockQuantity = StockQuantity - {subNum} WHERE ShoeID = {shoeID};";
            return Update(sql);
        }

        public static bool DeleteShoe(int shoeID)
        {
            string sql = $"DELETE FROM WomenShoes WHERE ShoeID = {shoeID};";
            return Update(sql);
        }


        public static Shoe GetShoe(int shoeID)
        {
            string sql = $"SELECT * FROM WomenShoes WHERE ShoeID = {shoeID};";
            DataTable dataTable = Query(sql);
      
            Shoe shoe = new Shoe
            {
                ShoeId = Convert.ToInt32(dataTable.Rows[0]["ShoeID"]),
                ShoeModel = dataTable.Rows[0]["Model"].ToString(),
                Origin = dataTable.Rows[0]["Origin"].ToString(),
                Price = Convert.ToDecimal(dataTable.Rows[0]["Price"]),
                StockQuantity = Convert.ToInt32(dataTable.Rows[0]["StockQuantity"])

                // 可能还有其他属性，根据实际情况添加
            };

            return shoe;
        }

        public static List<Shoe> GetWomenShoesPaged(int pageIndex,int pageSize)
        {
            string sql = $"SELECT * FROM WomenShoes ORDER BY ShoeID LIMIT {pageIndex * pageSize}, {pageSize}";
            DataTable dataTable = Query(sql);
            List<Shoe> shoes = new List<Shoe>();

            foreach (DataRow row in dataTable.Rows)
            {
                Shoe shoe = new Shoe
                {
                    ShoeId = Convert.ToInt32(row["ShoeID"]),
                    ShoeModel = row["Model"].ToString(),
                    Origin = row["Origin"].ToString(),
                    Price = Convert.ToDecimal(row["Price"]),
                    StockQuantity = Convert.ToInt32(row["StockQuantity"]),
                };

                shoes.Add(shoe);
            }

            return shoes;
        }


        public static List<Shoe> GetAllWomenShoes()
        {
            string sql = "SELECT * FROM WomenShoes ORDER BY ShoeID LIMIT;";
            DataTable dataTable = Query(sql);

            List<Shoe> shoes = new List<Shoe>();

            foreach (DataRow row in dataTable.Rows)
            {
                Shoe shoe = new Shoe
                {
                    ShoeId = Convert.ToInt32(row["ShoeID"]),
                    ShoeModel = row["Model"].ToString(),
                    Origin = row["Origin"].ToString(),
                    Price = Convert.ToDecimal(row["Price"]),
                    StockQuantity = Convert.ToInt32(row["StockQuantity"])
                };

                shoes.Add(shoe);
            }

            return shoes;
        }

        public static bool AddCustomerOrders(Order order)
        {
            string sql = String.Format("INSERT INTO customerorders (ShoeID, CustomerName, CustomerPhone, OrderQuantity) VALUES ('{0}','{1}','{2}','{3}')",
                order.ShoeId, order.CustomerName, order.ContactNumber, order.QuantityOrdered);
            //MessageBox.Show(sql);
            return Update(sql);
        }
    }
}
