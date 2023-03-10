BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "relationship" (
	"id"	INTEGER NOT NULL,
	"husband"	INTEGER,
	"wife"	INTEGER,
	"location"	INTEGER,
	"date_year"	INTEGER,
	"date_month"	INTEGER,
	"date_day"	INTEGER,
	"legal"	INTEGER NOT NULL,
	PRIMARY KEY("id")
);
CREATE TABLE IF NOT EXISTS "person" (
	"id"	INTEGER NOT NULL,
	"parentsID"	INTEGER,
	"surname"	TEXT,
	"forename"	TEXT,
	"maiden_surname"	TEXT,
	"maiden_forename"	TEXT,
	"gender"	TEXT,
	"birthPlace"	INTEGER,
	"deathPlace"	INTEGER,
	"birth_year"	INTEGER,
	"birth_month"	INTEGER,
	"birth_day"	INTEGER,
	"death_year"	INTEGER,
	"death_month"	INTEGER,
	"death_day"	INTEGER,
	"death_cause"	TEXT,
	"occupation"	TEXT,
	"notes"	TEXT,
	PRIMARY KEY("id")
);
CREATE TABLE IF NOT EXISTS "settlement" (
	"id"	INTEGER NOT NULL,
	"settlement"	TEXT(10),
	"provinceID"	INTEGER,
	PRIMARY KEY("id")
);
CREATE TABLE IF NOT EXISTS "province" (
	"id"	INTEGER NOT NULL,
	"province"	TEXT,
	"countryID"	text,
	"divorce_year"	integer,
	"divorce_month"	integer,
	"divorce_day"	integer,
	PRIMARY KEY("id")
);
CREATE TABLE IF NOT EXISTS "country" (
	"id"	INTEGER NOT NULL,
	"country"	TEXT(12),
	PRIMARY KEY("id")
);
INSERT INTO "relationship" ("id","husband","wife","location","date_year","date_month","date_day","legal") VALUES (1,2,1,4,1950,12,24,1);
INSERT INTO "relationship" ("id","husband","wife","location","date_year","date_month","date_day","legal") VALUES (2,3,5,14,1980,4,1,1);
INSERT INTO "relationship" ("id","husband","wife","location","date_year","date_month","date_day","legal") VALUES (3,4,6,'','','',NULL,0);
INSERT INTO "relationship" ("id","husband","wife","location","date_year","date_month","date_day","legal") VALUES (4,7,12,1,2001,8,15,1);
INSERT INTO "relationship" ("id","husband","wife","location","date_year","date_month","date_day","legal") VALUES (5,8,13,3,2001,11,28,1);
INSERT INTO "relationship" ("id","husband","wife","location","date_year","date_month","date_day","legal") VALUES (6,9,14,9,2002,6,27,1);
INSERT INTO "relationship" ("id","husband","wife","location","date_year","date_month","date_day","legal") VALUES (7,10,15,11,2003,7,25,1);
INSERT INTO "relationship" ("id","husband","wife","location","date_year","date_month","date_day","legal") VALUES (8,11,16,12,2000,9,22,1);
INSERT INTO "relationship" ("id","husband","wife","location","date_year","date_month","date_day","legal") VALUES (9,17,28,11,2026,6,1,1);
INSERT INTO "relationship" ("id","husband","wife","location","date_year","date_month","date_day","legal") VALUES (10,18,29,19,2025,5,18,1);
INSERT INTO "relationship" ("id","husband","wife","location","date_year","date_month","date_day","legal") VALUES (11,19,30,20,2022,4,22,1);
INSERT INTO "relationship" ("id","husband","wife","location","date_year","date_month","date_day","legal") VALUES (12,20,31,1,2019,2,27,1);
INSERT INTO "relationship" ("id","husband","wife","location","date_year","date_month","date_day","legal") VALUES (13,21,32,3,2030,3,23,1);
INSERT INTO "relationship" ("id","husband","wife","location","date_year","date_month","date_day","legal") VALUES (14,22,33,2,2029,1,20,1);
INSERT INTO "relationship" ("id","husband","wife","location","date_year","date_month","date_day","legal") VALUES (15,23,34,8,2016,5,7,1);
INSERT INTO "relationship" ("id","husband","wife","location","date_year","date_month","date_day","legal") VALUES (16,24,35,16,2026,9,8,1);
INSERT INTO "relationship" ("id","husband","wife","location","date_year","date_month","date_day","legal") VALUES (17,25,36,17,2028,10,9,1);
INSERT INTO "relationship" ("id","husband","wife","location","date_year","date_month","date_day","legal") VALUES (18,26,37,19,NULL,'',NULL,0);
INSERT INTO "relationship" ("id","husband","wife","location","date_year","date_month","date_day","legal") VALUES (19,27,37,1,12,12,12,1);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (1,'','Budai','Tamara','','','F','Budapest',NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (2,NULL,'Deák','Gábor',NULL,NULL,'M','',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (3,1,'Deák','István','',NULL,'M',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (4,1,'Deák','Péter',NULL,NULL,'M',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (5,NULL,'Németh','Ágota',NULL,NULL,'F',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (6,NULL,'Szabó','Margit',NULL,NULL,'F',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (7,2,'Deák','Tímea',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (8,2,'Deák','László',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (9,2,'Deák','Flóra',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (10,3,'Deák','Miklós',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (11,3,'Deák','Gertrúd',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (12,NULL,'Mikszáth','Viktor',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (13,NULL,'Koncz','Veronika',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (14,NULL,'Hermann','Csaba',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (15,NULL,'Hegedűs','Berta',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (16,NULL,'Törteli','Alex',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (17,4,'Mikszáth','Kálmán',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (18,4,'Mikszáth','Alexandra',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (19,5,'Deák','Aurélia',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (20,6,'Hermann','Pál',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (21,6,'Hermann','Annamária',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (22,6,'Hermann','Benedek',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (23,7,'Deák','Margit',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (24,7,'Deák','Marcell',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (25,8,'Törteli','Tamás',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (26,8,'Törteli','Ignác',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (27,8,'Törteli','Mária',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (28,NULL,'Fejes','Anikó',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (29,NULL,'Várady','Kristóf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (30,NULL,'Szőlőssy','Noel',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (31,NULL,'Csordás','Andrea',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (32,NULL,'Kondás','Milán',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (33,NULL,'Pásztor','Fanni',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (34,NULL,'Fodrász','Imre',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (35,NULL,'Vágó','Laura',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (36,NULL,'Müller','Éva',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (38,NULL,'Bogáromi','Benjámin',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (1,'Aszód',1);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (2,'Budakalász',1);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (3,'Ceglédbercel',1);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (4,'Dánszentmiklós',1);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (5,'Dunavarsány',1);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (6,'Nagytarcsa',1);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (7,'Tóalmás',1);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (8,'Ócsa',1);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (9,'Kiskunfélegyháza',2);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (10,'Tiszakécske',2);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (11,'Kunszentmiklós',2);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (12,'Kiskunhalas',2);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (13,'Kerekegyháza',2);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (14,'Soltszentimre',2);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (15,'Tomajmonostora',3);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (16,'Örményes',3);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (17,'Öcsöd',3);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (18,'Jászfelsőszentgyörgy',3);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (19,'Jászalsószentgyörgy',3);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (20,'Jászjákóhalma',3);
INSERT INTO "province" ("id","province","countryID","divorce_year","divorce_month","divorce_day") VALUES (1,'Pest vármegye','1',NULL,NULL,NULL);
INSERT INTO "province" ("id","province","countryID","divorce_year","divorce_month","divorce_day") VALUES (2,'Bács-Kiskun vármegye','1',NULL,NULL,NULL);
INSERT INTO "province" ("id","province","countryID","divorce_year","divorce_month","divorce_day") VALUES (3,'Jász-Nagykun-Szolnok vármegye','1',NULL,NULL,NULL);
INSERT INTO "province" ("id","province","countryID","divorce_year","divorce_month","divorce_day") VALUES (4,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "province" ("id","province","countryID","divorce_year","divorce_month","divorce_day") VALUES (5,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "country" ("id","country") VALUES (1,'Magyarország');
COMMIT;
