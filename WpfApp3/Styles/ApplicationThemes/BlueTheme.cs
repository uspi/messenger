using System.Windows.Media;

namespace WPFClient
{
    public class BlueTheme : IApplicationThemeProperties
    {
        public Color HeadLine => Color.FromRgb(59, 163, 208);//#3ba3d0

        public Color ContentBody => Color.FromRgb(7, 114, 161);//#0772a1

        public Color MinimizeButton => Color.FromRgb(59, 163, 208);//#3ba3d0

        public Color MaximazeButton => Color.FromRgb(59, 163, 208);//#3ba3d0

        public Color CloseButton => Color.FromRgb(59, 163, 208);//#3ba3d0

        public Color MinimizeButton_Active => Color.FromRgb(99, 175, 208);//#63afd0

        public Color MaximazeButton_Active => Color.FromRgb(99, 175, 208);//#63afd0

        public Color CloseButton_Active => Color.FromRgb(223, 37, 57);//#e92539
    }
}
