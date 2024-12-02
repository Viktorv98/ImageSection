using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageSection
{
    public class MyController // для отделения логики от интерфейса
    {
        private GenerateDataFragment generate = new GenerateDataFragment();
        private PictureFragment picture = new PictureFragment();

        public List<FragmentPhoto> CreateRandom(int WF, int HF, int CF)
        {
            return generate.CreateRandom(WF, HF, CF);
        }

        public ObservableCollection<PictureFragment> LoadGreyRect(List<FragmentPhoto> fragmentPhoto, int width, int height)
        {
            return picture.LoadGreyRect(fragmentPhoto, width, height);
        }

        public BitmapImage LoadImageAsync(PictureFragment fragment)
        {
            return picture.LoadFillRect(fragment);
        }
    }
}
