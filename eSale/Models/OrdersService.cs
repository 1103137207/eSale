using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSale.Models
{
    public class OrdersService
    {
        /// <summary>
        /// 新增訂單
        /// </summary>
        /// <param name="order"></param>
        public void InsertOrders(Models.Orders order)
        {
            //todo
        }
        /// <summary>
        /// 依照Id 取得訂單資料
        /// </summary>
        /// <returns></returns>
        public List<Models.Orders> GetOrdersById(string OrderID)
        {
            DataTable dt = new DataTable();
             
            string sql = @"select a.[OrderID],a.[CustomerID],b.CompanyName ,a.[EmployeeID],c.LastName+c.FirstName as Empname,
		                    a.[OrderDate],a.[RequiredDate],a.[ShippedDate],a.[ShipperID],d.CompanyName as ShipperName,a.[Freight],
		                    a.[ShipName],a.[ShipAddress],a.[ShipCity],a.[ShipRegion],
		                    a.[ShipPostalCode],a.[ShipCountry]
                            from Sales.Orders As a
                            Inner Join Sales.Customers As b ON a.CustomerID=b.CustomerID
                            Inner Join HR.Employees As c ON a.EmployeeID=c.EmployeeID
                            Inner Join Sales.Shippers As d ON a.ShipperID=d.ShipperID
                            
                            ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.Parameters.Add(new SqlParameter("@OrderID", OrderID));

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return MapOrderDataToList(dt);
        }

        private List<Models.Orders> MapOrderDataToList(DataTable orderData)
        {
            List<Models.Orders> result = new List<Orders>();

            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Orders()
                {
                    CustomerID = (int)row["CustomerID"],
                    CompanyName = row["CompanyName"].ToString(),
                    EmployeeID = (int)row["EmployeeID"],
                    Empname = row["Empname"].ToString(),
                    Freight  = (decimal)row["Freight"],
                    OrderDate = row["OrderDate"] ==DBNull.Value?(DateTime?)null : (DateTime)row["OrderDate"],
                    OrderID = (int)row["OrderID"],
                    RequiredDate = row["RequiredDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["RequiredDate"],
                    ShipAddress = row["ShipAddress"].ToString(),
                    ShipCity = row["ShipCity"].ToString(),
                    ShipCountry = row["ShipCountry"].ToString(),
                    ShipName = row["ShipName"].ToString(),
                    ShippedDate = row["ShippedDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["ShippedDate"],
                    ShipperID = (int)row["ShipperID"],
                    ShipperName = row["ShipperName"].ToString(),
                    ShipPostalCode = row["ShipPostalCode"].ToString(),
                    ShipRegion = row["ShipRegion"].ToString(),


                });
            }
            return result;
        }

        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }


        /// <summary>
        /// 依照條件取得訂單資料
        /// </summary>
        /// <returns></returns>
        public List<Models.Orders> GetOrders()
        {
            //todo
            List<Models.Orders> result = new List<Orders>();
            result.Add(new Orders() { CustomerID = 1, CompanyName = "叡揚資訊", EmployeeID = 1, Empname = "王小明", OrderDate = DateTime.Parse("2015/11/08") });
            result.Add(new Orders() { CustomerID = 2, CompanyName = "網軟資訊", EmployeeID = 2, Empname = "李小華", OrderDate = DateTime.Parse("2015/11/01") });
            return result;
        }
        /// <summary>
        /// 刪除訂單
        /// </summary>
        public void DeleteOrdersById(string orderId)
        {
            //todo
        }
        /// <summary>
        /// 更新訂單
        /// </summary>
        public void UpdateOrders(Models.Orders order)
        {
            //todo
        }

    }
    
}
