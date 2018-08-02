using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace AdditionalRepositoryMethods
{
    public class Repository : IDisposable
    {
        const string conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Учёба\warehouse.mdf;Integrated Security=True;Connect Timeout=30";

        SqlConnection _con;

        public Repository()
        {
            _con = new SqlConnection(conStr);
        }

        public void Dispose()
        {
            _con.Dispose();
        }

        public List<Record> GetRecords()
        {
            string commandString = "SELECT Id, Text, Author, RecordDate FROM Records";

            List<Record> result = new List<Record>();

            using (SqlCommand command = new SqlCommand(commandString, _con))
            {
                if (_con.State != ConnectionState.Open)
                    _con.Open();

                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        result.Add(new Record
                        {
                            Id = (int)reader["Id"],
                            Text = (string)reader["Text"],
                            Author = (string)reader["Author"],
                            RecordDate = (DateTime)reader["RecordDate"],
                        });
                    }
                }
            }
            _con.Close();
            return result;
        }

        public void CreateRecord(Record record)
        {
            string sql = @"INSERT INTO Records (Text, Author, RecordDate)
                        VALUES (@Text, @Author, @RecordDate) ";

            using (SqlCommand command = new SqlCommand(sql, _con))
            {
                command.Parameters.AddWithValue("@Text", record.Text);
                command.Parameters.AddWithValue("@Author", record.Author);
                command.Parameters.AddWithValue("@RecordDate", record.RecordDate);
                _con.Open();
                command.ExecuteNonQuery();
            }
            _con.Close();
        }
        public void UpdateRecord(int id, string newText, string newAuthor, string newRecordDate)
        {
            string sql = $@"UPDATE Records SET [Text] = @Text, [Author] = @Author, [RecordDate] = @RecordDate WHERE [Id] = @id;";
            using (SqlCommand command = new SqlCommand(sql, _con))
            {
                command.Parameters.AddWithValue("@Text", newText);
                command.Parameters.AddWithValue("@Author", newAuthor);
                command.Parameters.AddWithValue("@RecordDate", newRecordDate);
                command.Parameters.AddWithValue("@id", id);
                _con.Open();
                command.ExecuteNonQuery();
            }
            _con.Close();
        }

        public void DeleteRecord(int id)
        {
            string sql = $@"DELETE FROM Records WHERE [Id] = @id;";
            using (SqlCommand command = new SqlCommand(sql, _con))
            {
                command.Parameters.AddWithValue("@id", id);
                _con.Open();
                command.ExecuteNonQuery();
            }
            _con.Close();
        }
    }
}
