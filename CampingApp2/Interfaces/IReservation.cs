using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IReservation
    {
        int ReservationID { get; set; }
        int PlaceID { get; set; }
        int UserID { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        int PeopleAmount { get; set; }

        bool IsAvailable(int placeID, DateTime startDate, DateTime endDate);
        void InsertReservation(int placeID, DateTime startDate, DateTime endDate, int peopleAmount, int userID, bool isBlocked);
        void DeleteReservation(int reservationID);
        string GetReservationDescription(int reservationID);
    }
}
