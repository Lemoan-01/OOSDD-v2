using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camping.DataAccess.Functions;
using Interfaces;

namespace Services
{
    public class Place : IPlace
    {
        public int PlaceID { get; set; }
        public string Description { get; set; }
        public int Surface { get; set; }
        public int Price { get; set; }
        public bool Water { get; set; }
        public bool Electric { get; set; }
        public bool IsBlocked { get; set; }

        private DBFunctions _dbFunctions;
        public Place(DBFunctions dbFunctions)
        {
            _dbFunctions = dbFunctions;
        }

        public void UpdatePrice(int newPrice, int PlaceID)
        {
            _dbFunctions.UpdatePrice(newPrice, PlaceID);
        }

        public string GetDescription(int PlaceID)
        {
            return _dbFunctions.GetDescription(PlaceID);
        }

        public bool isBlockedOnID(int plekID)
        {
            return _dbFunctions.isBlockedOnID(plekID);
        }

        public byte[] GetImageFromDatabase(int placeID)
        {
            return _dbFunctions.GetImageFromDatabase(placeID);
        }

        public int GetSurface(int PlaceID)
        {
            throw new NotImplementedException();
        }

        public int GetPrice(int PlaceID)
        {
            throw new NotImplementedException();
        }

        public bool GetWater(int PlaceID)
        {
            throw new NotImplementedException();
        }

        public bool GetElectric(int PlaceID)
        {
            throw new NotImplementedException();
        }

        public bool GetBlocked(int PlaceID)
        {
            throw new NotImplementedException();
        }
    }
}
