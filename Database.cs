using Csaladfa;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Windows.Documents;
using System.Windows.Media;

namespace DB
{

    public class Relationship
    {
        public int id;
        public int husband, wife;
        public int location;
        public int date_year, date_month, date_day;
        public bool legal;
    }


    public class Person
    {
        public int id;
        public int parents;
        public string surname, forename;
        public string maiden_surname, maiden_forename;
        public char? gender;
        
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

        public Brush GenderToBrush()
        {
            if (gender == null)
                return Brushes.Gray;
            switch (gender)
            {
            case 'M':
                return Brushes.LightBlue;
            case 'F':
                return Brushes.LightPink;
            case 'X':
            default:
                return Brushes.LightYellow;
            }
        }

        public DB db = new DB();
        
        public Person[] sibblings()
        {
            db.ExecWriterCmd("SELECT");


            return new Person[0];
        }


    }

    public class TXT
    {
        public string person_cols = "id, parentsID, surname, forename, maiden_surname, maiden_forename, gender, " +
            "birthPlace, deathPlace, birth_year, birth_month, birth_day, " +
            "death_year, death_month, death_day, death_cause, occupation, notes";

        public string relationship_cols = "id, husband, wife, location, date_year, date_month, date_day, legal";
    }



    public class DB
    {
        public SQLiteConnection _conn;

        public DB()
        {
            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
            builder.DataSource = "database.db";
            _conn = new SQLiteConnection(builder.ConnectionString);
            _conn.Open();
            Debug.WriteLine("DB connection opened");

        }

        public SQLiteDataReader ExecReaderCmd(string command)
        {
            Debug.WriteLine($"Executing reader command: \"{command}\"");
            using (SQLiteCommand cmd = _conn.CreateCommand())
            {
                cmd.CommandText = command;
                return cmd.ExecuteReader();
            }
        }

        public int ExecWriterCmd(string command)
        {
            Debug.WriteLine($"Executing writer command: \"{command}\"");
            using (SQLiteCommand cmd = _conn.CreateCommand())
            {
                cmd.CommandText = command;
                return cmd.ExecuteNonQuery();
            }
        }

        

        private Person getPersonFromReader(SQLiteDataReader reader)
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

        private Relationship getRelationshipFromReader(SQLiteDataReader reader)
        {
            var relationship = new Relationship();

            relationship.id = reader.GetInt32(0);
            relationship.husband = reader.GetInt32(1);
            relationship.wife = reader.GetInt32(2);
            relationship.location = reader.GetInt32(3);
            relationship.date_year = reader.GetInt32(4);
            relationship.date_month = reader.GetInt32(5);
            relationship.date_day = reader.GetInt32(6);
            relationship.legal = reader.GetBoolean(7);

            return relationship;

    }

        private TXT txt = new TXT();

        public void addPerson(Person p)
        {
            ExecWriterCmd($"INSERT INTO table ({txt.person_cols})\r\n" +
                          $"VALUES({p.id}, {p.parents}, {p.surname}, {p.forename}, {p.maiden_surname}, " +
                          $"{p.maiden_forename}, {p.gender}, {p.birthPlace}, {p.deathPlace}, " +
                          $"{p.birth_year}, {p.birth_month}, {p.birth_day}, {p.death_year}, " +
                          $"{p.death_month}, {p.death_day}, {p.death_cause}, {p.occupation}, {p.notes});");
        }


        public Person getPerson(int id)
        {
            var reader = ExecReaderCmd($"SELECT {txt.person_cols} FROM person WHERE id = {id}");

            reader.Read();
            return getPersonFromReader(reader);

        }

        public Relationship getRelationship(int id)
        {
            var reader = ExecReaderCmd($"SELECT {txt.relationship_cols} FROM relationship WHERE id = {id}");

            reader.Read();
            return getRelationshipFromReader(reader);

        }


        public Person[] getAllPeople() {

            var reader = ExecReaderCmd($"SELECT {txt.person_cols} FROM person");
  
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