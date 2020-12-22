using System.Windows.Media;

namespace WPFClient
{
    public class GreenTheme : IApplicationThemeProperties
    {
        public Color HeadLine => Color.FromRgb(0, 113, 97);//#007161

        public Color ContentBody => Color.FromRgb(0, 71, 61);//#00473d

        public Color MinimizeButton => Color.FromRgb(0, 113, 97);//#007161

        public Color MaximazeButton => Color.FromRgb(0, 113, 97);//#007161

        public Color CloseButton => Color.FromRgb(0, 113, 97);//#007161

        public Color MinimizeButton_Active => Color.FromRgb(50, 153, 138);//#32998a

        public Color MaximazeButton_Active => Color.FromRgb(50, 153, 138);//#32998a

        public Color CloseButton_Active => Color.FromRgb(223, 37, 57);//#e92539
    }
}
