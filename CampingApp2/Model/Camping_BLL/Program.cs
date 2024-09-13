using System;

namespace Camping_BLL
{
    public class BaseLogic
    {
        public NotificationService.MainWindow notifService = new NotificationService.MainWindow();
        public Camping_Data.DBFunctions dbFunctions = new Camping_Data.DBFunctions(); //alle dbFunctions op een plek aangemaakt
        public Camping_Data.DBFunctions dbFunctions


		public BaseLogic() { }
    }
}