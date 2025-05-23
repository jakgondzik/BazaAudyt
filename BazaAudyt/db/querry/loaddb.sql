CREATE TABLE CzlonkowieZespolu (
    Id INT PRIMARY KEY,
    Imie VARCHAR(50),
    Nazwisko VARCHAR(50),
    Inicjaly VARCHAR(10),
    Telefon VARCHAR(20),
    CzyAdmin BIT,
    Warstwa VARCHAR(50),
    CzyAudytor BIT
);
CREATE TABLE StanowiskaPracy (
    Id INT PRIMARY KEY,
    Wydzial VARCHAR(100),
    Proces VARCHAR(100),
    Gniazdo VARCHAR(100),
    NrGniazda INT,
    RodzajStanowiska VARCHAR(100),
    IdLidera INT,
    Typ VARCHAR(100),
    ObszarLPA VARCHAR(100)
);
CREATE TABLE LPA_PlanAudytow (
    Id INT PRIMARY KEY,
    Audytor INT,
    Towarzyszacy VARCHAR(100),
    Data DATETIME,
    Stanowisko INT,
    DataPlanowana DATETIME,
    ObszarAudytu VARCHAR(100),
    DataWykonania DATETIME,
    DataZamkniecia DATETIME,
    Pozycja VARCHAR(100),
    KierownikObszaru VARCHAR(100),
    Wydzial VARCHAR(100),
    Brygada INT,
    Audytowany VARCHAR(100),
    Komentarz TEXT,
    FOREIGN KEY (Audytor) REFERENCES CzlonkowieZespolu(Id),
    FOREIGN KEY (Stanowisko) REFERENCES StanowiskaPracy(Id)
);
CREATE TABLE LPA_Pytania (
    Id INT PRIMARY KEY,
    Pytanie VARCHAR(255),
    Obszar VARCHAR(100),
    Nr INT,
    Aktywne BIT,
    Norma VARCHAR(100),
    Waga INT
);

CREATE TABLE LPA_Wyniki (
    Id INT PRIMARY KEY,
    Pytanie INT,
    Wynik VARCHAR(100),
    IdAudytu INT,
    Komentarz TEXT,
    Wartosc INT,
    Uwagi TEXT,
    FOREIGN KEY (Pytanie) REFERENCES LPA_Pytania(Id),
    FOREIGN KEY (IdAudytu) REFERENCES LPA_PlanAudytow(Id)
);





CREATE TABLE LPA_PodsumowanieWynikow (
    Id INT PRIMARY KEY,
    IdCzesci INT,
    DataWykonania DATETIME,
    IdAudytowanego INT,
    IdAudytu INT,
    Komentarz TEXT,
    MistrzZmianowy VARCHAR(100),
    Audytowany VARCHAR(100),
    Rozpoczety BIT,
    FOREIGN KEY (IdAudytu) REFERENCES LPA_PlanAudytow(Id)
);
