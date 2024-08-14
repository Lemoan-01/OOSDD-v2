using System;
using System.IO;
using System.Windows.Media.Imaging;
using Camping.DataAccess.Functions;

namespace Camping.BLL.HomePage
{
    public class SpotDescriptionLogic
    {
        private DBFunctions dbFunc;

        public SpotDescriptionLogic()
        {
            dbFunc = new DBFunctions();
        }

        public string GetSpotDescription(int placeID)
        {
            return dbFunc.GetDescription(placeID);
        }

        public byte[] GetSpotImage(int placeID)
        {
            return dbFunc.GetImageFromDatabase(placeID);
        }

        public BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = ms;
                bitmap.EndInit();
                return bitmap;
            }
        }
    }
}
