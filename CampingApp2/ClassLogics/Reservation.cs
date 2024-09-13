using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camping.DataAccess.Functions;
using Interfaces;

namespace Services
{
    public class Reservation : IReservation
    {
        public int ReservationID { get; set; }
        public int PlaceID { get; set; }
        public int UserID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PeopleAmount { get; set; }

        private DBFunctions _dbFunctions;

        public Reservation(DBFunctions dbFunctions) 
        {
            _dbFunctions = dbFunctions;
        }

        public List<int> GetReservationsByUserID(int userID)
        {
            return _dbFunctions.GetReservationsByUserID(userID);
        }

        public List<int> GetPlaceIDsByUserID(int userID)
        {
            return _dbFunctions.GetPlaceIDByUserID(userID);
        }

        public int GetPlaceIDByReservationID(int reservationID)
        {
            return _dbFunctions.GetPlaceIDByReservationID(reservationID);
        }

        public void InsertReservation(int placeID, DateTime startDate, DateTime endDate, int peopleAmount, int userID, bool isBlocked)
        {
            _dbFunctions.InsertReservation(placeID, startDate, endDate, peopleAmount, userID, isBlocked); 
        }

        public bool IsAvailable(int placeID, DateTime startDate, DateTime endDate)
        {
            return _dbFunctions.IsAvailable(placeID, startDate, endDate);
        }

        public string GetReservationDescription(int reservationID)
        {
            return _dbFunctions.GetReservationDescription(reservationID);
        }

        public void DeleteReservation(int userID, int placeID, DateTime startDate, DateTime endDate)
        {
            _dbFunctions.DeleteReservation(userID, placeID, startDate, endDate);
        }

        public void DeleteReservation(int reservationID)
        {
            _dbFunctions.DeleteReservation(reservationID);
        }
    }
}
