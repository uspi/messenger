using System.Windows.Media;

namespace WPFClient
{
    public class GreenTheme : IApplicationThemeProperties
    {
        public SolidColorBrush HeadLine => new SolidColorBrush(Color.FromRgb(0, 113, 97));//#007161

        public SolidColorBrush ContentBody => new SolidColorBrush(Color.FromRgb(0, 71, 61));//#00473d

        public SolidColorBrush MinimizeButton => new SolidColorBrush(Color.FromRgb(0, 113, 97));//#007161

        public SolidColorBrush MaximazeButton => new SolidColorBrush(Color.FromRgb(0, 113, 97));//#007161

        public SolidColorBrush CloseButton => new SolidColorBrush(Color.FromRgb(0, 113, 97));//#007161

        public SolidColorBrush MinimizeButton_Active => new SolidColorBrush(Color.FromRgb(50, 153, 138));//#32998a

        public SolidColorBrush MaximazeButton_Active => new SolidColorBrush(Color.FromRgb(50, 153, 138));//#32998a

        public SolidColorBrush CloseButton_Active => new SolidColorBrush(Color.FromRgb(223, 37, 57));//#e92539
    }
}
