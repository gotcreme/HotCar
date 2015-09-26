CREATE DATABASE HotCar;

GO

USE HotCar

CREATE TABLE tblUsers
(
	Id INT IDENTITY(1, 1) NOT NULL ,
	FirstName NVARCHAR(10) NOT NULL ,
	SurName NVARCHAR(20) NOT NULL ,
	RoleName NVARCHAR(20) NOT NULL,
	UserLogin NVARCHAR(30) NOT NULL ,
	UserPassword VARBINARY(MAX) NOT NULL ,	
	Mail NVARCHAR(35) NOT NULL ,
	PhoneNumber NVARCHAR(13) NULL,
	Birthday DATE NULL,
	Inactive BIT NOT NULL,
	AboutMe NVARCHAR(MAX) NULL,
	Photo VARBINARY(MAX) NULL, 
	PhotoFileExtension NVARCHAR(10) NULL,
	CONSTRAINT pk_UsersId PRIMARY KEY (Id), 
	CONSTRAINT uñ_UsersUserLogin UNIQUE (UserLogin),
	CONSTRAINT uc_UsersMail UNIQUE (Mail), 
	CONSTRAINT ck_UsersMail CHECK (Mail LIKE '%@%.%'),	
	CONSTRAINT ck_UsersPhoneNumber CHECK (PhoneNumber LIKE '+[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
);

CREATE TABLE tblCars
(
	Id INT IDENTITY(1, 1) NOT NULL,
	Photo BINARY NULL,
	MakeCar NVARCHAR(MAX) NOT NULL,
	OwnerId INT NOT NULL,
	CONSTRAINT pk_CarsId PRIMARY KEY (Id),
	CONSTRAINT fk_CarsOwnerId FOREIGN KEY (OwnerId) REFERENCES	tblUsers (Id)
);

CREATE TABLE tblTrips 
(
  Id INT IDENTITY (1, 1) NOT NULL ,
  OwnerId INT NOT NULL ,
  AvailablePlacesCount INT NOT NULL ,
  TripTime SMALLDATETIME NOT NULL , -- 
  CostOneSeat MONEY NOT NULL, -- Cost of trip for one person, 
  CarId INT NOT NULL,
  AdditionalInfo NVARCHAR(MAX),
  CONSTRAINT pk_TripsId PRIMARY KEY (Id),
  CONSTRAINT fk_TripsIdOwner FOREIGN KEY (OwnerId) REFERENCES tblUsers (Id),
  CONSTRAINT fk_TripsCarId FOREIGN KEY (CarId) REFERENCES tblCars (Id)
 );

  CREATE TABLE tblRoutes
(
	Id INT IDENTITY(1, 1) NOT NULL,
	TripId INT NOT NULL ,
	Longitude FLOAT NOT NULL,
	Latitude  FLOAT NOT NULL,	
	CONSTRAINT pk_RoutesId PRIMARY KEY (Id),
	CONSTRAINT fk_RoutesIdTrip FOREIGN KEY (TripId) REFERENCES tblTrips(Id),
);

 CREATE TABLE tblComments
(
	Id INT IDENTITY(1, 1) NOT NULL,
	CommentTime DATE NOT NULL,
	CommentText NVARCHAR(MAX) NOT NULL,
	IsPassenger BIT NOT NULL,
	CONSTRAINT pk_CommentsId PRIMARY KEY (Id)
);

CREATE TABLE tblPassengers
(
	Id INT IDENTITY(1, 1) NOT NULL ,
	TripId INT NOT NULL ,
	CountReservedSeats tinyint NOT NULL , -- Count reserved seats for this passanger
	PassengerId INT NOT NULL , 
	Cost INT NOT NULL, -- Summary cost for this passenger
	PassengerRoute NVARCHAR(MAX) NOT NULL,
	CommentId INT NULL,
	UNIQUE (TripId, PassengerId), -- One passenger can't reservation single trip 2 time
	CONSTRAINT pk_PassengersId PRIMARY KEY (Id),
	CONSTRAINT fk_PassengersIdTrip FOREIGN KEY (TripId) REFERENCES tblTrips(Id),
	CONSTRAINT fk_PassengersPassengerId FOREIGN KEY (PassengerId) REFERENCES tblUsers(Id),
	CONSTRAINT fk_PassengersCommentId FOREIGN KEY (CommentId) REFERENCES tblComments(Id),
);

GO
