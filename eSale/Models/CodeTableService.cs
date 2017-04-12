using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace eSale.Models
{
    public class CodeTableService
    {
        private string GetconnectionStrings()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }
        public List<CodeTable> GetTitle()
        {
            DataTable result = new DataTable();
            string sql = @"select CodeId,CodeVal from dbo.CodeTable where CodeType='Title' ";

            using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
                sqlAdapter.Fill(result);
                conn.Close();

            }
            return this.MapGetTitle(result);
        }

        private List<CodeTable> MapGetTitle(DataTable orderData)
        {
            List<CodeTable> result = new List<CodeTable>();


            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new CodeTable()
                {
                    CodeId = row["CodeId"].ToString(),
                    CodeVal = row["CodeVal"].ToString()
                });
            }
            return result;
        }
    }
}