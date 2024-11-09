using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CFlow.Models
{
    
    public class Account
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email {  get; set; }

        
        public static int UserSignIn(string FirstName, string LastName, String Email, string Password) 
        {
            int result = 0;

            string ConString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "dbo.QAFlowSignIn";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@FirstName", FirstName));
            cmd.Parameters.Add(new SqlParameter("@LastName", LastName));
            cmd.Parameters.Add(new SqlParameter("@Email", Email));
            cmd.Parameters.Add(new SqlParameter("@PassWord", Password));
            cmd.CommandTimeout = 0;
            try
            {
                result = cmd.ExecuteNonQuery();
            }
            catch(Exception e) 
            { 
                string message = e.Message;
            }

            cmd.Dispose();
            sqlConnection.Close();

            return result; 
        }  
        
        public static Account LoginValidation(string Email, string Password)
        {
            Account User = null;
            string ConString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "dbo.QAFlowUsersGet";
            cmd.Parameters.Clear();
            cmd.CommandTimeout = 0;

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (((reader["Email"].ToString() == Email) || (reader["LastName"].ToString() == Email)) && (reader["Password"].ToString() == Password))
                    {
                        User = new Account
                        {
                            ID = Convert.ToInt32(reader["UserID"].ToString()),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),

                        };
                        return User;
                    }
                }
            }

            return User;
        }
    }
}