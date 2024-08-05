using Camping.DataAccess.Functions;
using System;

namespace Camping.BLL.MakeReservationPage
{
    public class ReservationService
    {
        public int UserID { get; set; } = -1;

        public bool MakeReservation(int placeID, DateTime startDate, DateTime endDate, int numberOfPeople)
        {
            if (UserID == -1)
            {
                return false; // User not logged in
            }

            var dbFunc = new DBFunctions();

            dbFunc.InsertReservation(placeID, startDate, endDate, numberOfPeople, UserID);
            return true;
        }
    }
}
