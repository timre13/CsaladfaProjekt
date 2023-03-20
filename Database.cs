using Csaladfa;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Windows;
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

            sibblings.Remove(this);


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
                Debug.WriteLine("sikeres beolvasás");
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

        /*
        public static void addPerson(Person p)
        {
            ExecWriterCmd($"INSERT INTO table ({TXT.person_cols})\r\n" +
                          $"VALUES({p.id}, {p.parents}, {p.surname}, {p.forename}, {p.maiden_surname}, " +
                          $"{p.maiden_forename}, {p.gender}, {p.birthPlace}, {p.deathPlace}, " +
                          $"{p.birth_year}, {p.birth_month}, {p.birth_day}, {p.death_year}, " +
                          $"{p.death_month}, {p.death_day}, {p.death_cause}, {p.occupation}, {p.notes});");
        }
        */

        public static string StringToSql(in string? input)
        {
            return input == null ? "NULL" : "'" + input + "'";
        }

        public static string CharToSql(in char? input)
        {
            return input == null ? "NULL" : "'" + input + "'";
        }

        public static string LongToSql(in long? input)
        {
            return input == null ? "NULL" : input!.ToString();
        }

        public static void UpdatePerson(Person person)
        {
            ExecWriterCmd($"UPDATE person SET parentsID = {LongToSql(person.parents)}," +
                $"surname = {StringToSql(person.surname)}, forename = {StringToSql(person.forename)}, " +
                $"maiden_surname = {StringToSql(person.maiden_surname)}, maiden_forename = {StringToSql(person.maiden_forename)}, gender = {CharToSql(person.gender)}, " +
                $"birthPlace = {LongToSql(person.birthPlace)}, deathPlace = {LongToSql(person.deathPlace)}, birth_year = {LongToSql(person.birth_year)}, " +
                $"birth_month = {LongToSql(person.birth_month)}, birth_day = {LongToSql(person.birth_day)}, death_year = {LongToSql(person.death_year)}, " +
                $"death_month = {LongToSql(person.death_month)}, death_day = {LongToSql(person.death_day)}, death_cause = {StringToSql(person.death_cause)}, " +
                $"occupation = {StringToSql(person.occupation)}, notes = {StringToSql(person.notes)} WHERE id = {person.id}");
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
                long? countryId = GetValOrNull<long>(provReader, 1);
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

        public class Province
        {
            public int id = -1;
            public string name = "";
            public string countryName = "";

            string DisplayName { get => $"{name}, {countryName}"; }

            public Province(int id, string name, string countryName)
            {
                this.id = id;
                this.name = name;
                this.countryName = countryName;
            }
        }

        public static Province[] GetAllProvinces()
        {
            var reader = ExecReaderCmd(
                $"SELECT province.id, province.province, country.country " +
                $"FROM province LEFT JOIN country ON province.countryID = country.id;");

            var output = new List<Province>();
            while (reader.Read())
            {
                var prov = new Province(reader.GetInt32(0), GetValOrNull<string>(reader, 1) ?? "???", GetValOrNull<string>(reader, 2) ?? "???");
                output.Add(prov);
            }
            return output.ToArray();
        }

        public static void AddSettlement(in string name, int provinceName)
        {
            ExecWriterCmd($"INSERT INTO settlement (settlement, provinceID) VALUES ('{name}', {provinceName})");
        }

        public static int AddPerson()
        {
            ExecWriterCmd("INSERT INTO person (forename) VALUES (NULL)");
            var reader = ExecReaderCmd("SELECT MAX(id) from person;");
            reader.Read();
            return reader.GetInt32(0);
        }

        public static void Close()
        {

            Debug.WriteLine(getPerson(3).sibblings());



            _conn.Close();
            Debug.WriteLine("DB connection closed");
        }
    }
}