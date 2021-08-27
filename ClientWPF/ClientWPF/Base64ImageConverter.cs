using System;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ClientWPF
{
    public class Base64ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string s = value as string;
            BitmapImage bitmap;
            if (s == null)
                return null;
            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = new MemoryStream(System.Convert.FromBase64String(s));
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
            bitmap.CacheOption = BitmapCacheOption.Default;
            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
