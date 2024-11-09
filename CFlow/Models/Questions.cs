using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CFlow.Models
{ 
    public class Questions
    {
        public int QID { get; set; }
        public string QTitle {  get; set; }
        public string QDescription {  get; set; }
        public string UserName { get; set; }
        public int UserID { get; set; }
        public string Tags {  get; set; }
        public string tags { get; set; }
        public string DateTime {  get; set; }
        public int TotalVote {  get; set; }
        public int TotalViews { get; set; }
        public int TotalAnswer { get; set; }
        public int UpVote { get; set; }
        public int DownVote { get; set; }

        public static Questions QuestionView(int QID)
        {
            Questions question = new Questions();

            string ConString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "dbo.QAFlowSelectSingleQuestion";
            cmd.Parameters.Clear();
            cmd.CommandTimeout = 0;
            cmd.Parameters.Add(new SqlParameter("@QID", QID));
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                question.DateTime = reader["DateTime"].ToString();
                question.QTitle = reader["Title"].ToString();
                question.TotalViews = Convert.ToInt32(reader["views"].ToString());
                question.QDescription = reader["QDescription"].ToString();
                question.UserName = reader["UserName"].ToString();
                question.TotalAnswer = Convert.ToInt32(reader["AnswerCount"].ToString());
                question.UpVote = Convert.ToInt32(reader["UpVote"].ToString());
                question.DownVote = Convert.ToInt32(reader["DownVote"].ToString());
                question.TotalVote = question.UpVote - question.DownVote;
            }


            return question;
        }

        public static int UpdateViewConut(int QID)
        {
            int result = 0;
            string ConString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "dbo.QAFlowUpdateViews";
            cmd.Parameters.Clear();
            cmd.CommandTimeout = 0;
            cmd.Parameters.Add(new SqlParameter("@QID", QID));
            try 
            {
                result = cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                string msg = e.Message;
            }
            cmd.Dispose();
            sqlConnection.Close();

            return result;
        }

        public static List<Questions> QuestionLists()
        {
            List<Questions> list = new List<Questions>();

            string ConString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "dbo.QAFlowSelectQuestion";
            cmd.Parameters.Clear();
            cmd.CommandTimeout = 0;
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Questions obj = new Questions();
                    obj.QID = Convert.ToInt32(reader["QID"].ToString());
                    obj.QTitle = reader["Title"].ToString();
                    obj.QDescription = reader["QDescription"].ToString();
                    obj.UserID = Convert.ToInt32(reader["UserID"].ToString());
                    obj.UserName = reader["UserName"].ToString();
                    obj.DateTime = reader["DateTime"].ToString();
                    obj.Tags = reader["Tags"].ToString();
                    obj.TotalViews = Convert.ToInt32(reader["views"].ToString());
                    obj.TotalAnswer = Convert.ToInt32(reader["AnswerCount"].ToString());

                    InsertTags(obj.QID, obj.Tags);

                    list.Add(obj);
                }
            }
            cmd.Dispose();
            sqlConnection.Close();



            return list;
        }

        public static void InsertTags(int QID, string Tags)
        {
            int result = 0;
            string[] tags = Tags.Split(' ');

            string ConString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();

            for (int i = 0; i < tags.Length; i++)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "dbo.QTagsInsertTag";
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter("@QID", QID));
                cmd.Parameters.Add(new SqlParameter("@Tags", tags[i]));
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch(Exception e) 
                { 
                    string message = e.Message;
                }
                cmd.Dispose();
            }
            sqlConnection.Close();

        }

        public static int InsertQuestion(string QTitle, string QDescription, int UserID, string Tags)
        {
            int result = 0;


            string ConString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection= sqlConnection;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "dbo.QAFlowInsertQuestion";
            cmd.Parameters.Clear();
            cmd.CommandTimeout = 0;
            cmd.Parameters.Add(new SqlParameter("@Title", QTitle));
            cmd.Parameters.Add(new SqlParameter("@QDescription", QDescription));
            cmd.Parameters.Add(new SqlParameter("@UserID", UserID));
            cmd.Parameters.Add(new SqlParameter("@Tags", Tags));
            try
            {
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;    
            }
            cmd.Dispose();
            sqlConnection.Close();

            return result;
        }
        public static int InsertVote(int QID, int UserID, int Vote)
        {
            int result = 0;
            string ConString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "dbo.QAFlowInsertVote";
            cmd.Parameters.Clear();
            cmd.CommandTimeout = 0;
            cmd.Parameters.Add(new SqlParameter("@QID", QID));
            cmd.Parameters.Add(new SqlParameter("@UserID", UserID));
            cmd.Parameters.Add(new SqlParameter("@Vote", Vote));
            try
            {
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            cmd.Dispose();
            sqlConnection.Close();

            return result;
        }
    }
}