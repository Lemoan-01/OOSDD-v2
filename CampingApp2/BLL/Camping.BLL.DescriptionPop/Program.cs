using System;
using System.IO;
using System.Windows.Media.Imaging;
using Camping_BLL;

namespace Camping.BLL.HomePage
{
    public class SpotDescriptionLogic : BaseLogic
    {

        public SpotDescriptionLogic()
        {
        }

        public string GetSpotDescription(int placeID)
        {
            return dbFunctions.GetDescription(placeID);
        }

        public byte[] GetSpotImage(int placeID)
        {
            return dbFunctions.GetImageFromDatabase(placeID);
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
