using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Windows.Documents;

namespace DB
{
    public class DB
    {
        private SQLiteConnection _conn;

        public DB()
        {
            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
            builder.DataSource = "database.db";
            _conn = new SQLiteConnection(builder.ConnectionString);
            _conn.Open();
            Debug.WriteLine("DB connection opened");
        }

        /*
        public string GetPersonName(int id)
        {
            var reader = ExecReaderCmd($"SELECT name, age FROM people WHERE id = {id}");
            var list = new List<string>();
            while (reader.Read())
            {
                list.Add(reader.GetString(0));
            }
        }
        */

        /*
        public void AddPerson(string name)
        {
            ExecWriterCmd($"INSERT INTO people VALUES (name={name}");
        }
        */

        private SQLiteDataReader ExecReaderCmd(string command)
        {
            Debug.WriteLine($"Executing reader command: \"{ command }\"");
            using (SQLiteCommand cmd = _conn.CreateCommand())
            {
                cmd.CommandText = command;
                return cmd.ExecuteReader();
            }
        }

        private int ExecWriterCmd(string command)
        {
            Debug.WriteLine($"Executing writer command: \"{command}\"");
            using (SQLiteCommand cmd = _conn.CreateCommand())
            {
                cmd.CommandText = command;
                return cmd.ExecuteNonQuery();
            }
        }

        ~DB()
        {
            _conn.Close();
            Debug.WriteLine("DB connection closed");
        }
    }
}