using System;

namespace HotCar.Repositories.Sql
{
    public static class StoredProcs
    {
        public const string GET_ALL_USER = "spGetAllUsers";                             

        public const string GET_USERS_BY_ROLE = "spGetUsersByRole";                     

        public const string GET_USER_PASS_BY_LOGIN = "spGetUserPassByLogin";            
        
        public const string GET_USER_BY_ID = "spGetUserById";                           

        public const string GET_USER_BY_LOGIN = "spGetUserByLogin";
       
        public const string GET_ACCOUNT = "spGetAccountInfo";         

        public const string GET_USER_ROLE_BY_LOGIN = "spGetUserRoleByLogin";            

        public const string GET_USERS_BY_FIRST_NAME = "spGetUserByFirstName";

        public const string GET_USERS_BY_SUR_NAME = "spGetUserBySurName";

        public const string GET_USERS_BY_SUR_FIRST_NAME = "spGetUserBySurFirstName";

        public const string GET_USER_PHOTO = "spGetUserPhoto";

        public const string DELETE_USER_BY_ID = "spDeleteUserById";

        public const string GET_DRIVER_BY_TRIP_ID = "spGetDriverByTripId";

        public const string GET_ALL_PASSENGERS_BY_TRIP_ID = "spGetAllPassengersByTripId";

        public const string DELETE_USER_BY_LOGIN = "spDeleteUserByLogin";               

        public const string LOCK_USER_BY_ID = "spLockUserById";

        public const string UNLOCK_USER_BY_ID = "spUnlockUserById";

        public const string CREATE_NEW_USER = "spSetUser";                              

        public const string UPDATE_USER_BY_ID = "spUpdateUserById";                     

        public const string UPDATE_USER_BY_LOGIN = "spUpdateUserByLogin";               

        public const string GET_USER_ROLE_NAME_BY_ID = "spGetUserRoleNameById";         

        public const string GET_NUMBER_OF_USERS = "spGetCountUsers";                    

        public const string IS_MAIL_VALID = "spIsMailValid";

        public const string IS_LOGIN_VALID = "spIsLoginValid";          

        public const string IS_PHONE_VALID = "spIsPhoneValid";

        public const string GET_USER_LOGIN_BY_ID = "spGetUserLoginById";

        public const string GET_COMMENTS_DRIVERS_ABOUT_PASSENGER_BY_PASSENGER_ID =
            "spGetCommentsDriversAboutPassengerByPassengerId";

        public const string GET_COMMENTS_PASENGERS_ABOUT_DRIVER_BY_DRIVER_ID =
            "spGetCommentsPassengersAboutDriversByDriverId";

        public const string GET_ALL_AVAILABLE_TRIPS = "spGetAllAvailableTrips";

        public const string GET_LOCATIONS = "spGetLocations";

        public const string GET_ALL_TRIPS_BY_DRIVER_ID = "spGetAllTripsByDriverId";

        public const string GET_COUNT_CONDUCTED_TRIPS_BY_DRIVER_ID = "spGetCountConductedTripsByDriverId";

        public const string GET_ALL_TRIPS_BY_PASSENGER_ID = "spGetAllTripsByPassengerId";

        public const string GET_COUNT_CONDUCTED_TRIPS_BY_PASSENGER_ID = "spGetCountConductedTripsByPassengerId";

        public const string GET_CAR_BY_TRIP_ID = "spGetCarByTripId";

        public const string GET_ALL_CARS_BY_USER_ID = "spGetAllCarsUserByUserId";

        public const string DELETE_CAR_BY_ID = "spDeleteCarById"; 

        public const string UPDATE_CAR_BY_ID = "spUpdateCarById";

        public const string REGISTER_NEW_CAR = "spRegisterNewCar";

        public const string DELETE_TRIP_BY_ID = "spDeleteTripById"; 

        public const string UPDATE_TRIP_BY_ID = "spUpdateTripById";

        public const string UNSIGN_PASSENGER_FROM_TRIP = "spUnsignPassengerFromTrip";

        public const string SIGN_PASSENGER_TO_TRIP = "spSignPassengerToTrip";

        public const string UPDATE_PASSENGER_FROM_TRIP = "spUpdatePassengerFromTrip";

        public const string ADD_ROUTE = "spInsertRoute";

        public const string INSERT_TRIP = "spInsertTrip";

        public const String GET_ALL_USER_TRIPS = "spGetAllUserTrips";

        public const String GET_TRIP_BY_ID = "spGetTripById";               // added
    }
}
