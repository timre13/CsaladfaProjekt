﻿using Csaladfa;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Windows.Documents;

namespace DB
{

    public class Person
    {
        public int id;
        public int parents;
        public string surname, forename;
        public string maiden_surname, maiden_forename;
        public char gender;
        
        public int birthPlace;
        public int deathPlace;

        public int birth_year, birth_month, birth_day;
        public int death_year, death_month, death_day;

        public string death_cause;
        public string occupation;
        public string notes;

        public Person()
        {

        }

        /*public Person[] sibblings()
        {
            ExecWriterCmd("");
        }*/

    }


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

        private SQLiteDataReader ExecReaderCmd(string command)
        {
            Debug.WriteLine($"Executing reader command: \"{command}\"");
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

        public string columns = "id, parentsID, surname, forename, maiden_surname, maiden_forename, gender, " +
            "birthPlace, deathPlace, birth_year, birth_month, birth_day, " +
            "death_year, death_month, death_day, death_cause, occupation, note";


        public Person getPersonFromReader(SQLiteDataReader reader)
        {
            Person person = new Person();

            person.id = reader.GetInt32(0);
            person.parents = reader.GetInt32(1);
            person.surname = reader.GetString(2);
            person.forename = reader.GetString(3);
            person.maiden_surname = reader.GetString(4);
            person.maiden_forename = reader.GetString(5);
            person.gender = reader.GetChar(5);


            person.birthPlace = reader.GetInt32(6);
            person.deathPlace = reader.GetInt32(7);

            person.birth_year = reader.GetInt32(8);
            person.birth_month = reader.GetInt32(9);
            person.birth_day = reader.GetInt32(10);
            person.death_year = reader.GetInt32(11);
            person.death_month = reader.GetInt32(12);
            person.death_day = reader.GetInt32(13);

            person.death_cause = reader.GetString(14);
            person.occupation = reader.GetString(15);
            person.notes = reader.GetString(16);

            return person;
        }



        public void addPerson(Person p)
        {
            ExecWriterCmd($"INSERT INTO table ({columns})\r\n" +
                          $"VALUES({p.id}, {p.parents}, {p.surname}, {p.forename}, {p.maiden_surname}, " +
                          $"{p.maiden_forename}, {p.gender}, {p.birthPlace}, {p.deathPlace}, " +
                          $"{p.birth_year}, {p.birth_month}, {p.birth_day}, {p.death_year}, " +
                          $"{p.death_month}, {p.death_day}, {p.death_cause}, {p.occupation}, {p.notes});");
        }


        public Person getPerson(int id)
        {
            var reader = ExecReaderCmd($"SELECT {columns} FROM person WHERE id = {id}");

            reader.Read();
            return getPersonFromReader(reader);

        }

        public Person[] getAllPeople() {

            var reader = ExecReaderCmd($"SELECT {columns} FROM person");
  
            List<Person> people = new List<Person>();

            while (reader.Read())
            {
                people.Add(getPersonFromReader(reader));
            }

            return people.ToArray();

        }




        ~DB()
        {
            _conn.Close();
            Debug.WriteLine("DB connection closed");
        }
    }
}