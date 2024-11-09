using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CFlow.Models
{
    public class Tag
    {
        public string tag {  get; set; }

        public static List<Tag> TagsList(int QID)
        {
            List<Tag> list = new List<Tag>();
            string ConString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "dbo.QAFlowSelecetTags";
            cmd.Parameters.Clear();
            cmd.CommandTimeout = 0;
            cmd.Parameters.Add(new SqlParameter("@QID", QID));
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Tag obj = new Tag();

                    obj.tag = reader["Name"].ToString();

                    list.Add(obj);
                }
            }
            cmd.Dispose();
            sqlConnection.Close();

            return list;
        }
    }
}