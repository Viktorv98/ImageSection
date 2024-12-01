using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ImageSection
{
    public class FragmentPhoto // класс для хранения информации фрагментов
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

    public class PictureFragment
    {
        public string PictureNumber { get; set; }
        public bool IsImageLoaded { get; set; }

        public int width { get; set; }
        public int height { get; set; }
        public ImageBrush ImagePath { get; set; }

        public ObservableCollection<PictureFragment> LoadImage(List<FragmentPhoto> fragmentPhoto, int width, int height)
        {
            this.width = width;
            this.height = height;
            ObservableCollection<PictureFragment> pictures = new ObservableCollection<PictureFragment>();
            // Загрузка изображений
            foreach (var fragment in fragmentPhoto)
            {
                pictures.Add(LoadOneImage(fragment));
            }
            return pictures;
        }
     
        public PictureFragment LoadOneImage(FragmentPhoto fragmentPhoto)
        {
            string pictureNumber = $"{fragmentPhoto.CountF}, {fragmentPhoto.WidthF}, {fragmentPhoto.HeightF}";
            MemoryCache Cache = new MemoryCache(new MemoryCacheOptions());
            ImageBrush imageBrush = new ImageBrush();
            if (!Cache.TryGetValue(pictureNumber, out imageBrush))
            {
                FragmentContext db = new FragmentContext();
                List<Photos> photos = db.photos.Where(p => p.NumberF == fragmentPhoto.CountF).ToList<Photos>();
                MemoryStream stream = new MemoryStream(photos[0].ImageData);
                var img = new Bitmap(stream);
                Bitmap region = new Bitmap(width, height);

                using (Graphics g = Graphics.FromImage(region)) // "вырезание" фрагмента 
                {
                    g.DrawImage(img, 0, 0, new System.Drawing.Rectangle(fragmentPhoto.WidthF, fragmentPhoto.HeightF, width, height), GraphicsUnit.Pixel);
                }
                using (MemoryStream memory = new MemoryStream()) // создание ImageBrush для заливки фона прямоугольника
                {
                    region.Save(memory, ImageFormat.Png);
                    memory.Position = 0;
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = memory;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                    ImageBrush imageBrush1 = new ImageBrush();
                    imageBrush1.ImageSource = bitmapImage;
                    imageBrush = imageBrush1;
                }
                Cache.Set(pictureNumber, imageBrush);
            }
            PictureFragment picture = new PictureFragment();
            picture.PictureNumber = pictureNumber;
            picture.IsImageLoaded = true;
            picture.width = width;
            picture.height = height;
            picture.ImagePath = imageBrush;

            return picture;
        }
    }

}
