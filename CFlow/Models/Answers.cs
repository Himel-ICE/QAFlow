using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CFlow.Models
{
    public class Answers
    {
        public int AID { get; set; }
        public int QID { get; set; }
        public int UID { get; set; }
        public string Answer {  get; set; }
        public string DateTime {  get; set; }
        public int totalAnswer { get; set; }
        public string UserName {  get; set; }

        public static void InsertAnswer(string Answer, int QID, int UID)
        {
            string ConString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "dbo.QAFlowInsertAnswer";
            cmd.Parameters.Clear();
            cmd.CommandTimeout = 0;
            cmd.Parameters.Add(new SqlParameter("@Answer", Answer));
            cmd.Parameters.Add(new SqlParameter("@QID", @QID));
            cmd.Parameters.Add(new SqlParameter("@UserID", UID));
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }
            cmd.Dispose();
            sqlConnection.Close();
        }

        public static List<Answers> AnswerList(int QID)
        {
            List<Answers> list = new List<Answers>();
            string ConString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType =System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "dbo.QAFlowSelectAnswer";
            cmd.Parameters.Clear();
            cmd.CommandTimeout = 0;
            cmd.Parameters.Add(new SqlParameter("@QID", QID));
            
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Answers answer = new Answers();
                    answer.UserName = reader["FirstName"].ToString();
                    answer.DateTime = reader["DateTime"].ToString();
                    answer.Answer = reader["Answer"].ToString();
                    list.Add(answer);
                }
            }
            cmd.Dispose();
            sqlConnection.Close();

            return list;
        }
    }

    
}