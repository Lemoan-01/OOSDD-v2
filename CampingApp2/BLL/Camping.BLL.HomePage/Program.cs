// BusinessLogicLayer.cs
using Camping_BLL;
using System;

namespace Camping_BLL_HomePage
{
    public class BLLHomePage : BaseLogic
    {

        public BLLHomePage()
        {
        }

        public string GetUserEmail(int userID)
        {
            return dbFunctions.GetEmail(userID);
        }
    }
}
