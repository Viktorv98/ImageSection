using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ImageSection
{
    class FragmentPhoto // класс для хранения информации фрагментов
    {
        public int CountF { get; set; }
        public int WidthF { get; set; }
        public int HeightF { get; set; }


        public FragmentPhoto(int CF, int WF, int HF)
        {
            WidthF = WF;
            HeightF = HF;
            CountF = CF;
        }
    }
}
