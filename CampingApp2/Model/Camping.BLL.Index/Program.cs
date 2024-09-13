// BusinessLogicLayer.cs
using Camping_BLL;
using System;

namespace Camping_BLL_Index
{
    public class BLLIndex : BaseLogic
    {

        public BLLIndex()
        {
        }

        public string GetUserEmail(int userID)
        {
            return dbFunctions.GetEmail(userID);
        }
    }
}
