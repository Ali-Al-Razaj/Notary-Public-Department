CREATE DATABASE NotaryPublicDepartment;

use NotaryPublicDepartment;

-------------------------------------------------------------------------------------------

-- Governorates table
CREATE TABLE Governorates (
    GovernorateID INT IDENTITY(1,1) PRIMARY KEY,
    GovernorateName NVARCHAR(100) NOT NULL
);

-- RegistrationAuthorities table
CREATE TABLE RegistrationAuthorities (
    RegistrationAuthorityID INT IDENTITY(1,1) PRIMARY KEY,
    RegistrationAuthorityName NVARCHAR(100) NOT NULL
);

-- RegistrationRecords table
CREATE TABLE RegistrationRecords (
    RegistrationRecordID INT IDENTITY(1,1) PRIMARY KEY,
    RegistrationRecordName NVARCHAR(100) NOT NULL
);

-- People table
CREATE TABLE People (
    PersonID INT IDENTITY(1,1) PRIMARY KEY,
    NationalNumber NVARCHAR(50) UNIQUE NOT NULL,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    FatherName NVARCHAR(100),
    MotherName NVARCHAR(100),
    BirthPlaceID INT,
    DateOfBirth DATE,
    RegistrationAuthorityID INT,
    RegistrationRecordID INT,
    Gender BIT NOT NULL,  -- 0 = Male, 1 = Female
    Adress NVARCHAR(255),
    Phone NVARCHAR(20),
    GrantHistory DATE,
    CardNumber NVARCHAR(50),
	CONSTRAINT FK_People_BirthPlace FOREIGN KEY (BirthPlaceID)
        REFERENCES Governorates(GovernorateID),
    CONSTRAINT FK_People_RegistrationAuthority FOREIGN KEY (RegistrationAuthorityID)
        REFERENCES RegistrationAuthorities(RegistrationAuthorityID),
    CONSTRAINT FK_People_RegistrationRecord FOREIGN KEY (RegistrationRecordID)
        REFERENCES RegistrationRecords(RegistrationRecordID)
);

-- Users table
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    PersonID INT NOT NULL,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_Users_Person FOREIGN KEY (PersonID)
        REFERENCES People(PersonID)
);

-- DocumentsTypes table
CREATE TABLE DocumentsTypes (
    DocumentTypeID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Body NVARCHAR(MAX) NOT NULL
);

-- Notaries table
CREATE TABLE Notaries (
    NotaryPublicID INT IDENTITY(1,1) PRIMARY KEY,
    Number INT NOT NULL,
    GovernorateID INT NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    CONSTRAINT FK_Notaries_Governorate FOREIGN KEY (GovernorateID)
        REFERENCES Governorates(GovernorateID)
);

-- Documents table
CREATE TABLE Documents (
    DocumentID INT IDENTITY(1,1) PRIMARY KEY,
    DocumentTypeID INT NOT NULL,
    Date DATE NOT NULL,
    NotaryPublicID INT NOT NULL,
    CONSTRAINT FK_Documents_DocumentType FOREIGN KEY (DocumentTypeID)
        REFERENCES DocumentsTypes(DocumentTypeID),
    CONSTRAINT FK_Documents_Notary FOREIGN KEY (NotaryPublicID)
        REFERENCES Notaries(NotaryPublicID)
);

-- DocumentPeopleRecords table
CREATE TABLE DocumentPeopleRecords (
    RecordID INT IDENTITY(1,1) PRIMARY KEY,
    DocumentID INT NOT NULL,
    PersonID INT NOT NULL,
    PersonRole BIT NOT NULL, -- 0 = First Party, 1 = Second Party
    CONSTRAINT FK_DocumentPeopleRecords_Document FOREIGN KEY (DocumentID)
        REFERENCES Documents(DocumentID),
    CONSTRAINT FK_DocumentPeopleRecords_Person FOREIGN KEY (PersonID)
        REFERENCES People(PersonID)
);
