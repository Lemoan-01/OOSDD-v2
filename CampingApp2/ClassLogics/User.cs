using System.Xml.Schema;
using Camping.DataAccess.Functions;
using Interfaces;

namespace Services
{
    public class User : IUser
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public int Phonenumber { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Password { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private DBFunctions _dbFunctions;

        public User(DBFunctions dbFunctions) 
        {
            _dbFunctions = dbFunctions;
        }

        public string GetEmail(int UserID)
        {
           return _dbFunctions.GetEmail(UserID);
        }

        public int GetPhonenumber(int userID)
        {
            throw new NotImplementedException();
        }

        public string GetPassword(int userID)
        {
            throw new NotImplementedException();
        }
    }
}
