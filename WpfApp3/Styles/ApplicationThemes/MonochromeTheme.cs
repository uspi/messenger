using System.Windows.Media;

namespace WPFClient
{
    class MonochromeTheme : IApplicationThemeProperties
    {
        public Color HeadLine => Color.FromRgb(228, 228, 228);//#e4e4e4

        public Color ContentBody => Color.FromRgb(162, 162, 162);//#a2a2a2

        public Color MinimizeButton => Color.FromRgb(228, 228, 228);//#e4e4e4

        public Color MaximazeButton => Color.FromRgb(228, 228, 228);//#e4e4e4

        public Color CloseButton => Color.FromRgb(228, 228, 228);//#e4e4e4

        public Color MinimizeButton_Active => Color.FromRgb(134, 134, 134);//#868686

        public Color MaximazeButton_Active => Color.FromRgb(134, 134, 134);//#868686

        public Color CloseButton_Active => Color.FromRgb(23, 23, 23);//#171717
    }
}
