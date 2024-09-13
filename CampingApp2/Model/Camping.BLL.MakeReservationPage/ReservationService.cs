using Camping_BLL;
using System;

namespace Camping.BLL.MakeReservationPage
{
    public class ReservationService : BaseLogic
    {
        public int UserID { get; set; } = -1; //y public

        public ReservationService(int placeID, DateTime startDate, DateTime endDate, int numberOfPeople)
        {
            if (UserID == -1) // User not logged in
            {
                notifService.ShowError("You must be logged in to make a reservation.");
            }

            dbFunctions.InsertReservation(placeID, startDate, endDate, numberOfPeople, UserID);
        }
    }
}
