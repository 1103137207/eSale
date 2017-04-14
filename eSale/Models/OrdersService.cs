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
        public List<Models.Orders> GetOrdersById(Orders selectitem)
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
                            where (a.OrderID LIKE @OrderID OR @OrderID='') AND (b.CompanyName LIKE '%@CompanyName%' OR @CompanyName='') AND (a.EmployeeID LIKE @EmployeeID OR @EmployeeID='')
                            AND (a.ShipperID LIKE @ShipperID OR @ShipperID='') AND (a.OrderDate = @OrderDate OR @OrderDate='') AND (a.ShippedDate = @ShippedDate OR @ShippedDate='')
                            AND (a.RequiredDate = @RequiredDate OR @RequiredDate='')
                            ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.Parameters.Add(new SqlParameter("@OrderID", selectitem.OrderID));
                cmd.Parameters.Add(new SqlParameter("@CompanyName", selectitem.CompanyName == null ? string.Empty : selectitem.CompanyName));
                cmd.Parameters.Add(new SqlParameter("@EmployeeID", selectitem.EmployeeID ));
                cmd.Parameters.Add(new SqlParameter("@ShipperID", selectitem.ShipperID));
                cmd.Parameters.Add(new SqlParameter("@OrderDate", selectitem.OrderDate.ToString() == "" ? string.Empty : string.Format("{0:yyyy-MM-dd}",selectitem.OrderDate)));                
                cmd.Parameters.Add(new SqlParameter("@RequiredDate", selectitem.RequiredDate.ToString() == "" ? string.Empty : string.Format("{0:yyyy-MM-dd}", selectitem.RequiredDate)));
                cmd.Parameters.Add(new SqlParameter("@ShippedDate", selectitem.ShippedDate.ToString() == "" ? string.Empty : string.Format("{0:yyyy-MM-dd}", selectitem.ShippedDate)));

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
                    Freight = (decimal)row["Freight"],
                    OrderDate = row["OrderDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["OrderDate"],
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
        public void DeleteOrdersById(string OrderID)
        {
            string sql = "@Delete From Sales.Orders Where OrderID=@OrderID";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderID", OrderID));

                conn.Close();
            };
        }
            
        /// <summary>
        /// 更新訂單
        /// </summary>
        public void UpdateOrders(Models.Orders order)
        {
            //todo
        }
        public List<Models.Orders> GetEmpname()
        {
            DataTable dt = new DataTable();
            string sql = @"select EmployeeID,LastName+FirstName as Empname
		                   From HR.Employees ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return MapEmpData(dt);
        }

        private List<Models.Orders> MapEmpData(DataTable empData)
        {
            List<Models.Orders> result1 = new List<Orders>();

            foreach (DataRow row in empData.Rows)
            {
                result1.Add(new Orders()
                {
                    EmployeeID = (int)row["EmployeeID"],
                    Empname = row["Empname"].ToString(),
                });
            }
            return result1;
        
    }
        public List<Models.Orders> GetShipperName()
        {
            DataTable dt = new DataTable();
            string sql = @"select ShipperID,CompanyName as ShipperName
		                   From Sales.Shippers ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return MapShipperName(dt);
        }

        private List<Models.Orders> MapShipperName(DataTable ShipData)
        {
            List<Models.Orders> result2 = new List<Orders>();

            foreach (DataRow row in ShipData.Rows)
            {
                result2.Add(new Orders()
                {
                    ShipperID = (int)row["ShipperID"],
                    ShipperName = row["ShipperName"].ToString()
                });
            }
            return result2;

        }

    }
    
}
