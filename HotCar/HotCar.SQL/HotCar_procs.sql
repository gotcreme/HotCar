USE HotCar;

GO

	CREATE PROCEDURE spGetAllUsers
		AS
		BEGIN
			SELECT  u.Id, 
					u.FirstName, 
					u.SurName, 
					u.UserLogin, 
					u.UserPassword, 
					u.Mail, 
					u.PhoneNumber, 
					u.Birthday, 
					u.RoleName, 
					u.Inactive,
					--u.Photo,
					--u.PhotoFileExtension, 
					u.AboutMe					 
			FROM tblUsers u;
		END;

GO

	CREATE PROCEDURE spGetUsersByRole
			@roleName NVARCHAR(20)
		AS
		BEGIN
			SELECT  u.Id, 
					u.FirstName, 
					u.SurName, 
					u.UserLogin, 
					u.UserPassword, 
					u.Mail, 
					u.PhoneNumber, 
					u.Birthday, 
					u.RoleName, 
					u.Inactive,
					--u.Photo,
					--u.PhotoFileExtension,  
					u.AboutMe
			FROM tblUsers u
			WHERE RoleName = @roleName;
		END;

GO

	CREATE PROCEDURE spGetUserById
			@idUser INT
		AS
		BEGIN
			SELECT  u.Id, 
					u.FirstName, 
					u.SurName, 
					u.UserLogin, 
					u.UserPassword, 
					u.Mail, 
					u.PhoneNumber, 
					u.Birthday, 
					u.RoleName, 
					u.Inactive,
					--u.Photo,
					--u.PhotoFileExtension,  
					u.AboutMe
			FROM tblUsers u 
			WHERE Id = @idUser;
		END;

GO

	CREATE PROCEDURE spGetUserPhoto
			@userId INT
		AS
		BEGIN
			SELECT 	Id, 
					Photo, 
					PhotoFileExtension 
			FROM tblUsers 
			WHERE Id = @userId;
		END;

GO

	CREATE PROCEDURE spGetUserByLogin
			@login NVARCHAR(30)
		AS
		BEGIN
			SELECT  u.Id, 
					u.FirstName, 
					u.SurName, 
					u.UserLogin, 
					u.UserPassword, 
					u.Mail, 
					u.PhoneNumber, 
					u.Birthday, 
					u.RoleName, 
					u.Inactive,
					--u.Photo,
					--u.PhotoFileExtension,  
					u.AboutMe
			FROM tblUsers u WHERE UserLogin = @login;
		END;

GO

	CREATE PROCEDURE spGetAccountInfo
			@login NVARCHAR(30)
		AS
		BEGIN
			SELECT  Id, 
					UserLogin, 
					UserPassword, 
					RoleName, 
					Inactive 					 
			FROM tblUsers 
			WHERE UserLogin = @login;
		END;

GO
		CREATE PROCEDURE spGetUserRoleNameById
			@idUser INT
		AS
		BEGIN
			SELECT RoleName 
			FROM tblUsers 
			WHERE @IdUser = Id ;
		END;

GO

	CREATE PROCEDURE spGetUserRoleByLogin
			@login NVARCHAR(30)
		AS
		BEGIN
			SELECT RoleName 					
			FROM tblUsers 
			WHERE UserLogin = @login;
		END;

GO	

	CREATE PROCEDURE spGetCountUsers
		AS
		BEGIN
			SELECT COUNT(*) 
			FROM tblUsers;
		END;

GO

	CREATE PROCEDURE spGetUserPassByLogin
			@userLogin NVARCHAR(30)
		AS
		BEGIN
			SELECT UserPassword 
			FROM tblUsers 
			WHERE UserLogin = @userLogin;
		END;

GO


	CREATE PROCEDURE spSetUser
			@firstName NVARCHAR(10),
			@surName NVARCHAR(20),
			@roleName NVARCHAR(20),
			@userLogin NVARCHAR(30),
			@userPassword VARBINARY(MAX),		
			@mail NVARCHAR(35),			
			@inactive BIT			
		AS
		BEGIN
			INSERT INTO tblUsers
			(
				FirstName, 
				SurName, 
				RoleName, 
				UserLogin, 
				UserPassword, 
				Mail, 
				PhoneNumber, 
				Birthday, 
				Inactive, 
				AboutMe
			)
			VALUES
			(
				@firstName, 
				@surName, 
				@roleName, 
				@userLogin, 
				@userPassword, 
				@mail, 
				Null, 
				Null,  
				@inactive, 
				Null
			);
		END;

GO

	CREATE PROCEDURE spUpdateUserById
			@idUser INT,
			@firstName NVARCHAR(10),
			@surName NVARCHAR(20),
			@roleName NVARCHAR(20),								
			@mail NVARCHAR(35),
			@phoneNumber NVARCHAR(13),
			@birthday DATE,
			@inactive BIT,
			@aboutMe NVARCHAR(MAX) 
		AS
		BEGIN
			UPDATE tblUsers 
			SET 
				FirstName = @firstName, 
				SurName = @surName,
				RoleName=@roleName, 			 			
				Mail = @mail,
				PhoneNumber = @phoneNumber, 
				Birthday = @birthday, 
				Inactive = @inactive,
				AboutMe = @aboutMe 
			WHERE Id = @idUser;
		END;

GO

	CREATE PROCEDURE spUpdateUserByLogin
			@idUser INT,
			@firstName NVARCHAR(10),
			@surName NVARCHAR(20),
			@roleName NVARCHAR(20),
			@userLogin NVARCHAR(30),				
			@mail NVARCHAR(35),
			@phoneNumber NVARCHAR(13),
			@birthday DATE,
			@inactive BIT,
			@aboutMe NVARCHAR(MAX) 
		AS
		BEGIN
			UPDATE tblUsers 
			SET 
				FirstName = @firstName, 
				SurName = @surName,
				RoleName=@roleName, 			
				Mail = @mail,
				PhoneNumber = @phoneNumber, 
				Birthday = @birthday, 
				Inactive = @inactive,
				AboutMe = @aboutMe 
			WHERE UserLogin = @userLogin;
		END;

GO

	CREATE PROCEDURE spDeleteUserById
			@idUser INT
		AS
		BEGIN
			DELETE 
			FROM tblUsers 
			WHERE Id = @idUser;
		END;

GO

	CREATE PROCEDURE spDeleteUserByLogin
			@login NVARCHAR(20)
		AS
		BEGIN
			DELETE 
			FROM tblUsers 
			WHERE UserLogin = @login;
		END;

GO

	CREATE PROCEDURE spIsMailValid
			@mail NVARCHAR(35)
		AS
		BEGIN
			SELECT COUNT(*) 
			FROM tblUsers 
			WHERE Mail = @mail;
		END;

GO

	CREATE PROCEDURE spIsLoginValid
			@login NVARCHAR(35)
		AS
		BEGIN
			SELECT COUNT(*) 
			FROM tblUsers 
			WHERE UserLogin = @login;
		END;

GO

	CREATE PROCEDURE spIsPhoneValid
			@phone NVARCHAR(13)
		AS
		BEGIN
			SELECT COUNT(*) 
			FROM tblUsers 
			WHERE PhoneNumber = @phone;
		END;

GO

	CREATE PROCEDURE spGetUserBySurName
			@surName NVARCHAR(20)
		AS
		BEGIN
			SELECT  u.Id, 
					u.FirstName, 
					u.SurName, 
					u.UserLogin, 
					u.UserPassword, 
					u.Mail, 
					u.PhoneNumber, 
					u.Birthday, 
					u.RoleName, 
					u.Inactive,
					--u.Photo,
					--u.PhotoFileExtension,  
					u.AboutMe	
			FROM tblUsers u 
			WHERE SurName = @surName;
		END;

GO

	CREATE PROCEDURE spGetUserLoginById
			@id INT
		AS
		BEGIN
			SELECT UserLogin 
			FROM tblUsers 
			WHERE Id = @id;
		END;

GO

	CREATE PROCEDURE spGetUserByFirstName
			@firstName NVARCHAR(10)
		AS
		BEGIN
			SELECT  u.Id, 
					u.FirstName, 
					u.SurName, 
					u.UserLogin, 
					u.UserPassword, 
					u.Mail, 
					u.PhoneNumber, 
					u.Birthday, 
					u.RoleName, 
					u.Inactive,
					--u.Photo,
					--u.PhotoFileExtension,  
					u.AboutMe	
			FROM tblUsers u
			WHERE FirstName = @firstName;
		END;

GO

	CREATE PROCEDURE spGetUserBySurFirstName
			@surName NVARCHAR(20),
			@firstName NVARCHAR(10)
		AS
		BEGIN
			SELECT  u.Id, 
					u.FirstName, 
					u.SurName, 
					u.UserLogin, 
					u.UserPassword, 
					u.Mail, 
					u.PhoneNumber, 
					u.Birthday, 
					u.RoleName, 
					u.Inactive,
					--u.Photo,
					--u.PhotoFileExtension,  
					u.AboutMe	
			FROM tblUsers u
			WHERE SurName = @surName AND FirstName = @firstName;
		END;

GO

	CREATE PROCEDURE spLockUserById
			@idUser INT
		AS
		BEGIN
			UPDATE tblUsers 
			SET Inactive = 1 
			WHERE Id = @idUser;
		END;

GO

	CREATE PROCEDURE spUnlockUserById
			@idUser INT
		AS
		BEGIN
			UPDATE tblUsers 
			SET	Inactive = 0 
			WHERE Id = @idUser 
		END;

GO
	CREATE PROCEDURE spGetAllAvailableTrips
			@Time DATETIME
		AS
		BEGIN
			SELECT  u.FirstName, 
					u.SurName, 
					u.Birthday,
					u.PhoneNumber,
					u.Mail,
					u.Photo,
					u.PhotoFileExtension,
					t.AvailablePlacesCount, 
					t.AdditionalInfo, 
					t.CostOneSeat, 
					t.TripTime, 
					t.Id 
			FROM tblTrips t 
			LEFT JOIN tblUsers u 
			ON t.OwnerId = u.Id 
			WHERE t.TripTime > @Time;
		END;
GO
-- дати всі поїздки, які здійснив водій
	CREATE PROCEDURE spGetAllTripsByDriverId
			@driverId INT
		AS
		BEGIN
			SELECT  t.Id, 
					t.AvailablePlacesCount, 
					(SELECT UserLogin 
					 FROM tblUsers 
					 WHERE t.OwnerId = Id) 
					 AS DriverLogin,
					t.TripTime, 
					t.CostOneSeat, 
					t.CarId, 
					t.AdditionalInfo
			FROM tblTrips t 
			WHERE t.OwnerId = @driverId;
		END;

GO
	-- дати кількість поїздок водія
	CREATE PROCEDURE spGetCountConductedTripsByDriverId
			@driverId INT
		AS
		BEGIN
			SELECT COUNT(*) 
			FROM tblTrips t 
			WHERE t.OwnerId = @driverId;
		END;

GO
	-- дати всі поїздки, в яких приймав участь пасажир
	CREATE PROCEDURE spGetAllTripsByPassengerId
			@passengerId INT
		AS
		BEGIN
			SELECT  t.Id, 
					t.AvailablePlacesCount, 
					(SELECT UserLogin 
					 FROM tblUsers 
					 WHERE t.OwnerId = Id) 
					 AS DriverLogin,
					t.TripTime,
					t.CostOneSeat, 
					t.CarId, 
					t.AdditionalInfo

			FROM	tblTrips t, 
					tblPassengers p 

			WHERE	p.TripId = t.Id 
				AND p.PassengerId = @passengerId;
		END;

GO
		-- дати кількість поїздок в яких приймав участь пасажир
	CREATE PROCEDURE spGetCountConductedTripsByPassengerId
			@passengerId INT
		AS
		BEGIN
			SELECT COUNT(*) 

			FROM	tblTrips t,	
					tblPassengers p 

			WHERE	p.TripId = t.Id 
				AND p.PassengerId = @passengerId;
		END;

GO	
	-- дати всіх користувачів за Id поїздки
	CREATE PROCEDURE spGetAllPassengersByTripId
			@tripId INT
		AS
		BEGIN
			SELECT  Id, 
					CountReservedSeats, 
					TripId, 
					Cost, 
					PassengerRoute, 
					CommentId,
					(SELECT UserLogin 
					 FROM tblUsers 
					 WHERE PassengerId = Id) 
					 AS PassengerLogin

			FROM tblPassengers 

			WHERE TripId = @tripId;
		END;

GO
	-- дати водія за Id поїздки
	CREATE PROCEDURE spGetDriverByTripId
			@TripId INT
		AS
		BEGIN
			SELECT  u.Id, 
					u.FirstName, 
					u.SurName, 
					u.UserLogin, 
					u.UserPassword, 
					u.Mail, 
					u.PhoneNumber, 
					u.Birthday, 
					u.RoleName, 
					u.Inactive,
					--u.Photo,
					--u.PhotoFileExtension,  
					u.AboutMe		
			FROM tblUsers u
			WHERE Id = (SELECT OwnerId 
						FROM tblTrips 
						WHERE Id = @TripId);
		END;

GO

	-- всі коментарі про UserId як водія
	CREATE PROCEDURE spGetCommentsPassengersAboutDriverByDriverId
			@UserId INT
		AS
		BEGIN
			SELECT	c.Id, 
					c.CommentTime, 
					c.CommentText, 
					p.TripId,
					(SELECT UserLogin 
					 FROM tblUsers 
					 WHERE p.PassengerId = Id) 
					AS SenderLogin
			FROM	tblComments c, 
					tblTrips t, 
					tblPassengers p
			WHERE	t.OwnerId = @UserId 
				AND p.TripId = t.Id 
				AND p.CommentId = c.Id 
				AND c.IsPassenger = 1
		END;

GO

	-- всі коментарі про UserId як пасажира
	CREATE PROCEDURE spGetCommentsDriversAboutPassengerByPassengerId
			@UserId INT
		AS
		BEGIN
			SELECT  c.Id, 
					c.CommentTime, 
					c.CommentText, 
					p.TripId,
					(SELECT UserLogin 
					 FROM tblUsers 
					 WHERE t.OwnerId = Id) 
					AS SenderLogin
			FROM	tblComments c, 
					tblTrips t, 
					tblPassengers p
			WHERE	t.OwnerId = @UserId 
				AND p.TripId = t.Id 
				AND p.CommentId = c.Id 
				AND c.IsPassenger = 0
		END;
GO
	-- авто поїздки
	CREATE PROCEDURE spGetCarByTripId
			@tripId INT
		AS
		BEGIN
			SELECT  c.Id, 
					c.OwnerId, 
					c.MakeCar, 
					c.Photo, 
					t.Id, 
					t.AdditionalInfo, 
					t.AvailablePlacesCount, 
					t.CarId, 
					t.CostOneSeat, 
					t.OwnerId, 
					t.TripTime 
			FROM tblCars c, tblTrips t
			WHERE t.Id = @tripId 
				AND c.Id = t.CarId; 
		END;

GO

	-- автомобілі юзера
	CREATE PROCEDURE spGetAllCarsUserByUserId
			@userId INT
		AS
		BEGIN
			SELECT  c.Id, 
					c.OwnerId, 
					c.MakeCar, 
					c.Photo
			FROM tblCars c
			WHERE c.OwnerId = @userId;
		END;

GO

	CREATE PROCEDURE spUpdateCarById
			@id INT, 
			@photo BINARY,
			@makeCar NVARCHAR(MAX),
			@ownerId INT
		AS
		BEGIN
			UPDATE tblCars 
			SET 
				Photo = @photo,
				MakeCar = @makeCar, 			
				OwnerId = @ownerId
			WHERE Id = @id;
		END;

GO

	CREATE PROCEDURE spDeleteCarById
			@carId INT
		AS
		BEGIN
			DELETE 
			FROM tblCars 
			WHERE Id = @carId;
		END;

GO

	CREATE PROCEDURE spRegisterNewCar
			@photo BINARY,
			@makeCar NVARCHAR(MAX),
			@ownerId INT
		AS
		BEGIN
			INSERT INTO tblCars
			(
				Photo, 
				MakeCar, 
				OwnerId
			) 
			VALUES
			(
				@photo, 
				@makeCar, 
				@ownerId
			);
		END;

GO

	CREATE PROCEDURE spDeleteTripById
			@tripId INT
		AS
		BEGIN
			DELETE 
			FROM tblTrips 
			WHERE Id = @tripId;
		END;

GO

	CREATE PROCEDURE spUpdateTripById
			@id INT, 
			@driverLogin NVARCHAR(30), 			
			@availablePlacesCount TINYINT,
			@tripTime SMALLDATETIME,
			@costOneSeat INT,
			@carId INT
		AS
		BEGIN
			UPDATE tblTrips 
			SET 
				OwnerId = ( SELECT Id 
							FROM tblUsers 
							WHERE UserLogin = @driverLogin),				 
				AvailablePlacesCount = @availablePlacesCount,
				TripTime = @tripTime,
				CostOneSeat = @costOneSeat,
				CarId = @carId
			WHERE Id = @id;
		END;

GO

		CREATE PROCEDURE spUnsignPassengerFromTrip
			@id INT, 
			@tripId INT
		AS
		BEGIN
			DELETE 
			FROM tblPassengers 
			WHERE Id = @id 
				AND TripId = @tripId;
		END;

--GO

--		CREATE PROCEDURE spUpdatePassengerFromTrip
--			@id INT, 
--			@passengerLogin NVARCHAR(30),
--			@countReservedSeats TINYINT,
--			@tripId INT,
--			@cost INT,
--			@passengerRoute NVARCHAR(MAX)
--		AS
--		BEGIN
--			UPDATE tblPassenger SET 
--				PassengerId = (SELECT Id FROM tblUsers WHERE UserLogin = @passengerLogin),
--				PassengerRoute = @passengerRoute, 
--				CountReservedSeats = @countReservedSeats,
--				TripId = @tripId,
--				Cost = @cost
--			WHERE Id = @id;
--		END;
		
GO

		CREATE PROCEDURE spSignPassengerToTrip
			@passengerLogin NVARCHAR(30),
			@countReservedSeats TINYINT,
			@tripId INT,
			@cost INT,
			@passengerRoute NVARCHAR(MAX)
		AS
		BEGIN
			DECLARE @passengerId INT;
			SELECT @passengerId = Id 
			FROM tblUsers 
			WHERE UserLogin = @passengerLogin;

			INSERT INTO tblPassengers
			(
				PassengerId, 
				CountReservedSeats, 
				TripId, 
				Cost, 
				PassengerRoute
			) 
			VALUES
			(
				@passengerId, 
				@countReservedSeats, 
				@tripId, 
				@cost, 
				@passengerRoute
			);
		END;
		
GO

	CREATE PROCEDURE spInsertRoute
			@tripId INT,
			@longitude NVARCHAR(20),
			@latitude NVARCHAR(20)					
		AS
		BEGIN
			INSERT INTO tblRoutes
			(
				TripId, 
				Longitude, 
				Latitude
			)
			VALUES
			(
				@tripId, 
				@longitude, 
				@latitude
			);
		END;

GO

	CREATE PROCEDURE spInsertTrip
			@ownerLogin NVARCHAR(30),
			@placeCount int,
			@tripTime smalldatetime,
			@costOneSeat money,
			@carId int,
			@additionalInfo	NVARCHAR(MAX)			
		AS
		BEGIN
			DECLARE @driverId INT;

			SELECT @driverId = Id 
			FROM tblUsers 
			WHERE UserLogin = @ownerLogin;

			INSERT INTO tblTrips
			(
				OwnerId, 
				AvailablePlacesCount, 
				TripTime, 
				CostOneSeat, 
				CarId, 
				AdditionalInfo
			)
			VALUES
			(
				@driverId, 
				@placeCount, 
				@tripTime, 
				@costOneSeat, 
				@carId, 
				@additionalInfo 
			);
		END;
		SELECT @@IDENTITY AS [@@IDENTITY];

GO

	CREATE PROCEDURE spGetLocations
			@id INT
		AS
		BEGIN
			SELECT  r.Id, 
					r.TripId, 
					r.Latitude, 
					r.Longitude
			FROM tblRoutes r 
			WHERE TripId=@id;
		END;
GO

	CREATE PROCEDURE spGetTripById
			@id INT
		AS
		BEGIN
			SELECT  t.Id,
					t.AdditionalInfo,
					t.AvailablePlacesCount,
					t.CarId,
					t.CostOneSeat,
					t.OwnerId,
					t.TripTime,
					u.UserLogin,
					u.AboutMe,					
					u.FirstName, 
					u.SurName, 
					u.Birthday,
					u.PhoneNumber,
					u.Mail,
					u.Photo,
					u.PhotoFileExtension
			FROM tblTrips t
			LEFT JOIN tblUsers u 
			ON t.OwnerId = u.Id			
			WHERE t.Id=@id;
		END;

GO