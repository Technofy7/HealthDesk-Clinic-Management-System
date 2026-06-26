create database healthcaredb;

use healthcaredb;

create table users (
	id int identity(1,1) primary key,
	name nvarchar(100) not null,
	email nvarchar(100) not null,
	passwordHash nvarchar(500) not null,
	roles nvarchar(20) not null
)

CREATE TABLE Doctors (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    Specialization NVARCHAR(100) NOT NULL,
    Bio NVARCHAR(MAX),

    CONSTRAINT FK_Doctors_Users
        FOREIGN KEY (UserId)
        REFERENCES Users(Id)
        ON DELETE CASCADE
);

CREATE TABLE Patients (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    DOB DATE,
    BloodGroup NVARCHAR(10),

    CONSTRAINT FK_Patients_Users
        FOREIGN KEY (UserId)
        REFERENCES Users(Id)
        ON DELETE CASCADE
);

CREATE TABLE TimeSlots (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DoctorId INT NOT NULL,
    DayOfWeek TINYINT NOT NULL
    CHECK (DayOfWeek IN (1,2,3,4,5,6,7)),   --- since there will be only 7 days in week we are using tinyint and added checker
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,

    CONSTRAINT FK_TimeSlots_Doctors
        FOREIGN KEY (DoctorId)
        REFERENCES Doctors(Id)
        ON DELETE CASCADE
);

-- Appointments Table
CREATE TABLE Appointments (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT NOT NULL,
    DoctorId INT NOT NULL,
    SlotId INT NOT NULL,
    [Date] DATE NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Pending',
    Notes NVARCHAR(MAX),

    CONSTRAINT FK_Appointments_Patients
        FOREIGN KEY (PatientId)
        REFERENCES Patients(Id),

    CONSTRAINT FK_Appointments_Doctors
        FOREIGN KEY (DoctorId)
        REFERENCES Doctors(Id),

    CONSTRAINT FK_Appointments_TimeSlots
        FOREIGN KEY (SlotId)
        REFERENCES TimeSlots(Id)
);

-- MedicalRecords Table
CREATE TABLE MedicalRecords (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AppointmentId INT NOT NULL,
    Diagnosis NVARCHAR(MAX),
    Prescription NVARCHAR(MAX),

    CONSTRAINT FK_MedicalRecords_Appointments
        FOREIGN KEY (AppointmentId)
        REFERENCES Appointments(Id)
        ON DELETE CASCADE
);


delete MedicalRecords;
delete doctors;
delete Patients;
delete users;
delete timeslots;

--DECLARE @sql NVARCHAR(MAX) = '';

--SELECT @sql += 'ALTER TABLE ' + QUOTENAME(s.name) + '.' + QUOTENAME(t.name)
--    + ' DROP CONSTRAINT ' + QUOTENAME(fk.name) + ';' + CHAR(13)
--FROM sys.foreign_keys fk
--JOIN sys.tables t ON fk.parent_object_id = t.object_id
--JOIN sys.schemas s ON t.schema_id = s.schema_id;

--EXEC sp_executesql @sql;

---- Now drop tables in any order
--DROP TABLE IF EXISTS MedicalRecords, Appointments, TimeSlots, Doctors, Patients, Users;

select * from users;    