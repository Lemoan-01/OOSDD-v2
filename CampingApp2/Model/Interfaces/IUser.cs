using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
	public interface IUser
	{
		int UserID { get; set; }
		string Email { get; set; }
		int Phonenumber { get; set; }
		string Password { get; set; }

		string GetEmail(int userID);
		int GetPhonenumber(int userID);
		string GetPassword(int userID);
	}
}
