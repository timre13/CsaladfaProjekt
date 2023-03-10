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
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (2,NULL,'De??k','G??bor',NULL,NULL,'M','',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (3,1,'De??k','Istv??n','',NULL,'M',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (4,1,'De??k','P??ter',NULL,NULL,'M',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (5,NULL,'N??meth','??gota',NULL,NULL,'F',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (6,NULL,'Szab??','Margit',NULL,NULL,'F',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (7,2,'De??k','T??mea',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (8,2,'De??k','L??szl??',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (9,2,'De??k','Fl??ra',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (10,3,'De??k','Mikl??s',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (11,3,'De??k','Gertr??d',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (12,NULL,'Miksz??th','Viktor',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (13,NULL,'Koncz','Veronika',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (14,NULL,'Hermann','Csaba',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (15,NULL,'Heged??s','Berta',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (16,NULL,'T??rteli','Alex',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (17,4,'Miksz??th','K??lm??n',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (18,4,'Miksz??th','Alexandra',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (19,5,'De??k','Aur??lia',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (20,6,'Hermann','P??l',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (21,6,'Hermann','Annam??ria',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (22,6,'Hermann','Benedek',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (23,7,'De??k','Margit',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (24,7,'De??k','Marcell',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (25,8,'T??rteli','Tam??s',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (26,8,'T??rteli','Ign??c',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (27,8,'T??rteli','M??ria',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (28,NULL,'Fejes','Anik??',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (29,NULL,'V??rady','Krist??f',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (30,NULL,'Sz??l??ssy','Noel',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (31,NULL,'Csord??s','Andrea',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (32,NULL,'Kond??s','Mil??n',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (33,NULL,'P??sztor','Fanni',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (34,NULL,'Fodr??sz','Imre',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (35,NULL,'V??g??','Laura',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (36,NULL,'M??ller','??va',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "person" ("id","parentsID","surname","forename","maiden_surname","maiden_forename","gender","birthPlace","deathPlace","birth_year","birth_month","birth_day","death_year","death_month","death_day","death_cause","occupation","notes") VALUES (38,NULL,'Bog??romi','Benj??min',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (1,'Asz??d',1);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (2,'Budakal??sz',1);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (3,'Cegl??dbercel',1);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (4,'D??nszentmikl??s',1);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (5,'Dunavars??ny',1);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (6,'Nagytarcsa',1);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (7,'T??alm??s',1);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (8,'??csa',1);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (9,'Kiskunf??legyh??za',2);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (10,'Tiszak??cske',2);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (11,'Kunszentmikl??s',2);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (12,'Kiskunhalas',2);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (13,'Kerekegyh??za',2);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (14,'Soltszentimre',2);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (15,'Tomajmonostora',3);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (16,'??rm??nyes',3);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (17,'??cs??d',3);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (18,'J??szfels??szentgy??rgy',3);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (19,'J??szals??szentgy??rgy',3);
INSERT INTO "settlement" ("id","settlement","provinceID") VALUES (20,'J??szj??k??halma',3);
INSERT INTO "province" ("id","province","countryID","divorce_year","divorce_month","divorce_day") VALUES (1,'Pest v??rmegye','1',NULL,NULL,NULL);
INSERT INTO "province" ("id","province","countryID","divorce_year","divorce_month","divorce_day") VALUES (2,'B??cs-Kiskun v??rmegye','1',NULL,NULL,NULL);
INSERT INTO "province" ("id","province","countryID","divorce_year","divorce_month","divorce_day") VALUES (3,'J??sz-Nagykun-Szolnok v??rmegye','1',NULL,NULL,NULL);
INSERT INTO "province" ("id","province","countryID","divorce_year","divorce_month","divorce_day") VALUES (4,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "province" ("id","province","countryID","divorce_year","divorce_month","divorce_day") VALUES (5,NULL,NULL,NULL,NULL,NULL);
INSERT INTO "country" ("id","country") VALUES (1,'Magyarorsz??g');
COMMIT;
