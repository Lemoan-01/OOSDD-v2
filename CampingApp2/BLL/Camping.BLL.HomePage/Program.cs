// BusinessLogicLayer.cs
using Camping.DataAccess.Functions;
using System;

namespace Camping_BLL_HomePage
{
    public class BLLHomePage
    {
        private DBFunctions dbFunc;

        public BLLHomePage()
        {
            dbFunc = new DBFunctions();
        }

        public string GetUserEmail(int userID)
        {
            return dbFunc.GetEmail(userID);
        }
    }
}
