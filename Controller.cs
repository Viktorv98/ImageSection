using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSection
{
    public class MyController
    {
        private GenerateDataFragment generate = new GenerateDataFragment();
        private PictureFragment picture = new PictureFragment();

        public List<FragmentPhoto> CreateRandom(int WF, int HF, int CF)
        {
            return generate.CreateRandom(WF, HF, CF);
        }

        public ObservableCollection<PictureFragment> LoadImage(List<FragmentPhoto> fragmentPhoto, int width, int height)
        {
            return picture.LoadImage(fragmentPhoto, width, height);
        }
    }
}
