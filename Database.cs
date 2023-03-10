using Csaladfa;
using Microsoft.VisualBasic;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Windows.Documents;
using System.Windows.Media;

namespace DB
{

    public class Relationship
    {
        public int id;
        public long? husband, wife;
        public long? location;
        public long? date_year, date_month, date_day;
        public bool legal;
    }


    public class Person
    {
        public int id;
        public long? parents;
        public string? surname, forename;
        public string? maiden_surname, maiden_forename;
        public char? gender;
        
        public long? birthPlace;
        public long? deathPlace;

        public long? birth_year, birth_month, birth_day;
        public long? death_year, death_month, death_day;

        public string? death_cause;
        public string? occupation;
        public string? notes;

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

        public string GenderToDisplayName()
        {
            if (gender == null)
                return "???";
            return gender switch
            {
                'M' => "Férfi",
                'F' => "Nő",
                _ => "Egyéb",
            };
        }

        public DB db = new DB();
        
        public Person[] sibblings()
        {
            Relationship parents = DB.getRelationship(this.parents);

            var reader = DB.ExecReaderCmd($"SELECT {TXT.person_cols} FROM person WHERE parentsID = {this.parents}");

            List<Person> sibblings = new List<Person>();


            while (reader.Read())
            {
                sibblings.Add(DB.getPersonFromReader(reader));
            }

            return sibblings.ToArray();
        }


    }

    public class TXT
    {
        public static string person_cols = "id, parentsID, surname, forename, maiden_surname, maiden_forename, gender, " +
            "birthPlace, deathPlace, birth_year, birth_month, birth_day, " +
            "death_year, death_month, death_day, death_cause, occupation, notes";

        public static string relationship_cols = "id, husband, wife, location, date_year, date_month, date_day, legal";
    }



    public class DB
    {
        public static SQLiteConnection _conn;

        public DB()
        {
            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
            builder.DataSource = "database.db";
            _conn = new SQLiteConnection(builder.ConnectionString);
            _conn.Open();
            Debug.WriteLine("DB connection opened");

        }

        public static SQLiteDataReader ExecReaderCmd(string command)
        {
            Debug.WriteLine($"Executing reader command: \"{command}\"");
            using (SQLiteCommand cmd = _conn.CreateCommand())
            {
                cmd.CommandText = command;
                return cmd.ExecuteReader();
            }
        }

        public static int ExecWriterCmd(string command)
        {
            Debug.WriteLine($"Executing writer command: \"{command}\"");
            using (SQLiteCommand cmd = _conn.CreateCommand())
            {
                cmd.CommandText = command;
                return cmd.ExecuteNonQuery();
            }
        }

        static private T? GetValOrNull<T>(in SQLiteDataReader reader, int col)
        {
            return reader.IsDBNull(col) ? default(T) : reader.GetFieldValue<T>(col);
        }

        static private char? StrToCharOrNull(in string? input)
        {
            if (input == null)
                return null;
            return input[0];
        }

        public static Person getPersonFromReader(SQLiteDataReader reader)
        {
            Person person = new Person();
            int i = 0;

            person.id = reader.GetInt32(i++);
            person.parents = GetValOrNull<Int64>(reader, i++);
            person.surname = GetValOrNull<string>(reader, i++);
            person.forename = GetValOrNull<string>(reader, i++);
            person.maiden_surname = GetValOrNull<string>(reader, i++);
            person.maiden_forename = GetValOrNull<string>(reader, i++);
            person.gender = StrToCharOrNull(GetValOrNull<string>(reader, i++));

            person.birthPlace = GetValOrNull<Int64>(reader, i++);
            person.deathPlace = GetValOrNull<Int64>(reader, i++);

            person.birth_year = GetValOrNull<Int64>(reader, i++);
            person.birth_month = GetValOrNull<Int64>(reader, i++);
            person.birth_day = GetValOrNull<Int64>(reader, i++);
            person.death_year = GetValOrNull<Int64>(reader, i++);
            person.death_month = GetValOrNull<Int64>(reader, i++);
            person.death_day = GetValOrNull<Int64>(reader, i++);

            person.death_cause = GetValOrNull<string>(reader, i++);
            person.occupation = GetValOrNull<string>(reader, i++);
            person.notes = GetValOrNull<string>(reader, i++);

            return person;
        }

        public static Relationship getRelationshipFromReader(SQLiteDataReader reader)
        {
            var relationship = new Relationship();
            int i = 0;

            relationship.id = reader.GetInt32(i++);
            relationship.husband = GetValOrNull<Int64>(reader, i++);
            relationship.wife = GetValOrNull<Int64>(reader, i++);
            relationship.location = GetValOrNull<Int64>(reader, i++);
            relationship.date_year = GetValOrNull<Int64>(reader, i++);
            relationship.date_month = GetValOrNull<Int64>(reader, i++);
            relationship.date_day = GetValOrNull<Int64>(reader, i++);
            relationship.legal = reader.GetBoolean(i++);

            return relationship;

    }


        public void addPerson(Person p)
        {
            ExecWriterCmd($"INSERT INTO table ({TXT.person_cols})\r\n" +
                          $"VALUES({p.id}, {p.parents}, {p.surname}, {p.forename}, {p.maiden_surname}, " +
                          $"{p.maiden_forename}, {p.gender}, {p.birthPlace}, {p.deathPlace}, " +
                          $"{p.birth_year}, {p.birth_month}, {p.birth_day}, {p.death_year}, " +
                          $"{p.death_month}, {p.death_day}, {p.death_cause}, {p.occupation}, {p.notes});");
        }


        public static Person getPerson(int id)
        {
            var reader = ExecReaderCmd($"SELECT {TXT.person_cols} FROM person WHERE id = {id}");

            reader.Read();
            return getPersonFromReader(reader);

        }

        public static Relationship getRelationship(int id)
        {
            var reader = ExecReaderCmd($"SELECT {TXT.relationship_cols} FROM relationship WHERE id = {id}");

            reader.Read();
            return getRelationshipFromReader(reader);

        }


        public static Person[] getAllPeople() {

            var reader = ExecReaderCmd($"SELECT {TXT.person_cols} FROM person");
  
            List<Person> people = new List<Person>();

            while (reader.Read())
            {
                people.Add(getPersonFromReader(reader));
            }

            return people.ToArray();

        }





        ~DB()
        {
            //_conn.Close();
            Debug.WriteLine("DB connection closed");
        }
    }
}