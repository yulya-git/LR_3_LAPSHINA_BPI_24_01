using System.Drawing;
using System.Windows.Media;

namespace LR_2_LAPSHINA_BPI_24_01
{
    public class Formula
    {
        public Bitmap SourceLight { get; set; }
        public Bitmap SourceDark { get; set; }
        public string Name { get; set; }
        public Formula(Bitmap sourceLight, Bitmap sourceDark, string name)
        {
            SourceLight = sourceLight;
            SourceDark = sourceDark;
            Name = name;
        }

        public virtual double Calculate()
        {
            return 0.0;
        }
        public ImageSource WpfSourceLight => ImageHelper.ToBitmapImage(SourceLight);
        public ImageSource WpfSourceDark => ImageHelper.ToBitmapImage(SourceDark);
    }
}
