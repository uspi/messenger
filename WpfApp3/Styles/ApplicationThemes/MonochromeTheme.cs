using System.Windows.Media;

namespace WPFClient
{
    class MonochromeTheme : IApplicationThemeProperties
    {
        public SolidColorBrush HeadLine => new SolidColorBrush(Color.FromRgb(228, 228, 228));//#e4e4e4

        public SolidColorBrush ContentBody => new SolidColorBrush(Color.FromRgb(162, 162, 162));//#a2a2a2

        public SolidColorBrush MinimizeButton => new SolidColorBrush(Color.FromRgb(228, 228, 228));//#e4e4e4

        public SolidColorBrush MaximazeButton => new SolidColorBrush(Color.FromRgb(228, 228, 228));//#e4e4e4

        public SolidColorBrush CloseButton => new SolidColorBrush(Color.FromRgb(228, 228, 228));//#e4e4e4

        public SolidColorBrush MinimizeButton_Active => new SolidColorBrush(Color.FromRgb(134, 134, 134));//#868686

        public SolidColorBrush MaximazeButton_Active => new SolidColorBrush(Color.FromRgb(134, 134, 134));//#868686

        public SolidColorBrush CloseButton_Active => new SolidColorBrush(Color.FromRgb(23, 23, 23));//#171717
    }
}
