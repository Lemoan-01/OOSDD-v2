using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
	public interface IPlace
	{
		int PlaceID { get; set; }
		string Description { get; set; }
		int Surface { get; set; }
		int Price { get; set; }
		bool Water { get; set; }
		bool Electric { get; set; }
		bool IsBlocked { get; set; }

		int GetSurface(int PlaceID);
		string GetDescription(int PlaceID);
		int GetPrice(int PlaceID);
		bool GetWater(int PlaceID);
		bool GetElectric(int PlaceID);
		bool GetBlocked(int PlaceID);


	}
}
