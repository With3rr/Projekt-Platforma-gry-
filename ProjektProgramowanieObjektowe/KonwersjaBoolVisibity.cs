using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ProjektProgramowanieObjektowe
{
    /// <summary>
    /// Klasa odpowiedzialna za konwersję z wartości typu bool na Visibility.
    /// </summary>
    class KonwersjaBoolVisibity : IValueConverter
    {
        /// <summary>
        /// Konwercja z Bool na visibility.
        /// </summary>
        /// <param name="value">Objekt sprawdzany pod kątem wartości</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Zwraca wartość typu Visibility</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if((bool)value==true )
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
            
        }

        /// <summary>
        /// Konwersja w drugą stronę,nie wokożystywana.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
