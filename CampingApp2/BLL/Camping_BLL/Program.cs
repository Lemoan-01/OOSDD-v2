using System;

namespace Camping_BLL
{
    public class BaseLogic
    {
        public NotificationService.MainWindow notifService = new NotificationService.MainWindow();
        public Camping.DataAccess.Functions.DBFunctions dbFunctions = new Camping.DataAccess.Functions.DBFunctions(); //alle dbFunctions op een plek aangemaakt

        public BaseLogic() { }
    }
}