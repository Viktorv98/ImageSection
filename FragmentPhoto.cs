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

        public ObservableCollection<PictureFragment> LoadGreyRect(List<FragmentPhoto> fragmentPhoto, int width, int height) // метод создания массива фрагментов
        {
            this.width = width;
            this.height = height;
            ObservableCollection<PictureFragment> pictures = new ObservableCollection<PictureFragment>();
            // Загрузка изображений
            foreach (var fragment in fragmentPhoto)
            {
                pictures.Add(LoadOneGreyRect(fragment));
            }
            return pictures;
        }

        public PictureFragment LoadOneGreyRect(FragmentPhoto fragmentPhoto) // метод создания одного фрагмента в виде серого прямоугольника
        {
            PictureFragment picture = new PictureFragment();
            string pictureNumber = $"{fragmentPhoto.CountF}, {fragmentPhoto.WidthF}, {fragmentPhoto.HeightF}";
            picture.PictureNumber = pictureNumber;
            picture.IsImageLoaded = true;
            picture.width = width;
            picture.height = height;

            return picture;
        }

        public BitmapImage LoadFillRect(PictureFragment fragmentPhoto) // метод для подгрузки одного фрагмента
        {
            string[] paramsf = fragmentPhoto.PictureNumber.Split(',');
            int number = Convert.ToInt32(paramsf[0]);
            FragmentContext db = new FragmentContext();
            List<Photos> photos = db.photos.Where(p => p.NumberF == number).ToList<Photos>();
            MemoryStream stream = new MemoryStream(photos[0].ImageData);
            var img = new Bitmap(stream);
            Bitmap region = new Bitmap(width, height);
            BitmapImage bitmapImage = new BitmapImage();
            using (Graphics g = Graphics.FromImage(region)) // "вырезание" фрагмента 
            {
                g.DrawImage(img, 0, 0, new System.Drawing.Rectangle(Convert.ToInt32(paramsf[1]), Convert.ToInt32(paramsf[2]), width, height), GraphicsUnit.Pixel);
            }
            using (MemoryStream memory = new MemoryStream()) // создание BitmapImage для заливки фона прямоугольника
            {
                region.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            return bitmapImage;
        }
    }
}

