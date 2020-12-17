using System.Windows.Media;

namespace WPFClient
{
    public class BlueTheme : IApplicationThemeProperties
    {
        public SolidColorBrush HeadLine => new SolidColorBrush(Color.FromRgb(59, 163, 208));//#3ba3d0

        public SolidColorBrush ContentBody => new SolidColorBrush(Color.FromRgb(7, 114, 161));//#0772a1

        public SolidColorBrush MinimizeButton => new SolidColorBrush(Color.FromRgb(59, 163, 208));//#3ba3d0

        public SolidColorBrush MaximazeButton => new SolidColorBrush(Color.FromRgb(59, 163, 208));//#3ba3d0

        public SolidColorBrush CloseButton => new SolidColorBrush(Color.FromRgb(59, 163, 208));//#3ba3d0

        public SolidColorBrush MinimizeButton_Active => new SolidColorBrush(Color.FromRgb(99, 175, 208));//#63afd0

        public SolidColorBrush MaximazeButton_Active => new SolidColorBrush(Color.FromRgb(99, 175, 208));//#63afd0

        public SolidColorBrush CloseButton_Active => new SolidColorBrush(Color.FromRgb(223, 37, 57));//#e92539
    }
}
