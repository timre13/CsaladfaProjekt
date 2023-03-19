﻿using Csaladfa;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
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

        public int GenderToIndex()
        {
            if (gender == null)
                return 0;
            return gender switch
            {
                'M' => 1,
                'F' => 2,
                _ => 3,
            };
        }
        
        public Person[] sibblings()
        {
            if (this.parents == null)
                return new Person[] { };

            Relationship parents = DB.getRelationship(this.parents.Value);

            var reader = DB.ExecReaderCmd($"SELECT {TXT.person_cols} FROM person WHERE parentsID = {this.parents}");

            List<Person> sibblings = new List<Person>();


            while (reader.Read())
            {
                sibblings.Add(DB.getPersonFromReader(reader));
            }

            return sibblings.ToArray();
        }


    }

    public class Settlement
    {
        public int id = -1;
        public string? name = "";
        public string? provinceName = "";
        public string? countryName = "";

        public string DisplayName { get
            {
                if (id == -1)
                    return "(Ismeretlen)";
                return $"{countryName ?? "-"}, {provinceName ?? "-"}, {name ?? "-"}";
            } }
    }

    public class TXT
    {
        public static string person_cols = "id, parentsID, surname, forename, maiden_surname, maiden_forename, gender, " +
            "birthPlace, deathPlace, birth_year, birth_month, birth_day, " +
            "death_year, death_month, death_day, death_cause, occupation, notes";

        public static string relationship_cols = "id, husband, wife, location, date_year, date_month, date_day, legal";
    }



    public static class DB
    {
        public static SQLiteConnection _conn;

        static DB()
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
                int count = cmd.ExecuteNonQuery();
                Debug.WriteLine($"Modified {count} rows");
                return count;
            }
        }

        private static T? GetValOrNull<T>(in SQLiteDataReader reader, int col)
        {
            return reader.IsDBNull(col) ? default(T) : reader.GetFieldValue<T>(col);
        }

        private static char? StrToCharOrNull(in string? input)
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


        public static void addPerson(Person p)
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

        public static Relationship getRelationship(long id)
        {
            var reader = ExecReaderCmd($"SELECT {TXT.relationship_cols} FROM relationship WHERE id = {id}");

            reader.Read();
            return getRelationshipFromReader(reader);

        }


        public static Person[] getAllPeople()
        {

            var reader = ExecReaderCmd($"SELECT {TXT.person_cols} FROM person");

            List<Person> people = new List<Person>();

            while (reader.Read())
            {
                people.Add(getPersonFromReader(reader));
            }

            return people.ToArray();

        }

        private static long? ToLongOrNull(in string? value)
        {
            if (value == null)
                return null;
            return long.Parse(value);
        }

        public static Settlement GetSettlement(int id)
        {
            Settlement settl = new Settlement();
            settl.id = id;
            var settlementReader = ExecReaderCmd($"SELECT settlement, provinceID FROM settlement WHERE id = { id }");
            settlementReader.Read();
            settl.name = GetValOrNull<string>(settlementReader, 0);
            long? provId = GetValOrNull<long>(settlementReader, 1);
            if (provId != null)
            {
                var provReader = ExecReaderCmd($"SELECT province, countryID FROM province WHERE id = { provId }");
                provReader.Read();
                settl.provinceName = GetValOrNull<string>(provReader, 0);
                long? countryId = ToLongOrNull(GetValOrNull<string>(provReader, 1)); // FIXME: Ez (countryID) valamiért TEXT az adatbázisban
                if (countryId != null)
                {
                    var countryReader = ExecReaderCmd($"SELECT country FROM country WHERE id = { countryId }");
                    countryReader.Read();
                    settl.countryName = GetValOrNull<string>(countryReader, 0);
                }
            }
            return settl;
        }

        public static Settlement[] GetAllSettlements()
        {
            List<int> ids = new List<int>();
            var reader = ExecReaderCmd("SELECT id FROM settlement");
            while (reader.Read())
            {
                ids.Add(reader.GetInt32(0));
            }
            return ids.Select(x => GetSettlement(x)).ToArray();
        }

        public static void AddCountry(in string name)
        {
            ExecWriterCmd($"INSERT INTO country (country) VALUES ('{name}')");
        }

        public class Country
        {
            public int id = -1;
            public string name = "";

            public Country(int id, in string name)
            {
                this.id = id;
                this.name = name;
            }
        }

        public static Country[] GetAllCountries()
        {
            List<Country> outputs = new List<Country>();
            var reader = ExecReaderCmd("SELECT id, country FROM country");
            while (reader.Read())
            {
                outputs.Add(new Country(reader.GetInt32(0), GetValOrNull<string>(reader, 1) ?? "???"));
            }
            return outputs.ToArray();
        }

        public static void AddProvince(in string name, int countryId)
        {
            ExecWriterCmd($"INSERT INTO province (province, countryID) VALUES ('{name}', {countryId})");
        }

        public static void Close()
        {
            _conn.Close();
            Debug.WriteLine("DB connection closed");
        }
    }
}