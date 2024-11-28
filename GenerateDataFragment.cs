using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.Http;
using System.Data.Entity;
using System.Net;

namespace ImageSection
{
    class GenerateDataFragment // класс для генерации номеров картинок и координат
    {
        public List<FragmentPhoto> fragments = new List<FragmentPhoto>(); // массив с номерами картинов и координатами
        public List<string> listURL = new List<string>(); // массив URL
        public GenerateDataFragment()
        {
            //listURL.Add("https://upload.wikimedia.org/wikipedia/commons/9/9d/Stones-in-water.jpg");
            listURL.Add("http://www.kryzuy.com/wp-content/uploads/2015/12/KryzUy.Com-Sumilon-Island-Blue-Water-Resort-2.jpg");
            listURL.Add("https://img.freepik.com/free-photo/reflection-of-rocky-mountains-and-sky-on-beautiful-lake_23-2148153610.jpg?w=2000&t=st=1707286026~exp=1707286626~hmac=33cfd04e4e898e7a58210bba5129a7e07a5c4bde22abf12467472f3073d6dd0e");
            listURL.Add("https://img.freepik.com/free-photo/vestrahorn-mountains-in-stokksnes-iceland_335224-667.jpg?w=2000&t=st=1707286136~exp=1707286736~hmac=e87482823b94ab19abc794f7eafcb0356b97e0bbf563aec15dd4596f026aff72");
            listURL.Add("https://img.freepik.com/free-photo/pier-at-a-lake-in-hallstatt-austria_181624-44201.jpg?w=2000&t=st=1707286209~exp=1707286809~hmac=9e29fcb6cf717450de3ebe4b34222b356166d91e6ad651fedbfa63a2822b4f77");

        }

        // метод генерации номеров картин и координат фрагментов 
        public List<FragmentPhoto> CreateRandom(int WF, int HF, int CF) 
        {
            fragments.Clear();

            List<DatePhoto> datePhotos = new List<DatePhoto>();
            List<int> numberphoto = new List<int>();
            Random random = new Random();
            for (int i = 0; i < CF; i++)
            {
                numberphoto.Add(random.Next(listURL.Count));
            }
            int[] uniqueNumbers = numberphoto.Distinct().ToArray();
            Array.Sort(uniqueNumbers);
            FragmentContext db = new FragmentContext();
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE photos");
            db.SaveChanges();
            foreach (int i in uniqueNumbers)
            {
                int wf, hf;
                byte[] imageData = DownloadImage(listURL[i]);          
                db.photos.Add(new Photos(i, i, imageData));
                db.SaveChanges();
                MemoryStream stream = new MemoryStream(imageData);
                var img = new Bitmap(stream);
                hf = img.Height - HF;
                wf = img.Width - WF;
                stream.Close();
                datePhotos.Add(new DatePhoto(i, wf, hf));
            }

            for (int i = 0; i < numberphoto.Count; i++)
            {
                int k1 = 0;
                int k = numberphoto[i];
                for( int j = 0; j < datePhotos.Count; j++)
                {
                    if (datePhotos[j].NumberP == k)
                    {
                        k1 = j;
                        break;
                    }
                }
                fragments.Add(new FragmentPhoto(k, random.Next(datePhotos[k1].WidthP), random.Next(datePhotos[k1].HeightP)));
            }
            return fragments;
        }

        // метод загрузки картинок по ссылке
        // не работает для первой картинки
        // попытки применить другие методы также успеха не принесли, сайт постоянно "не отвечает" на запрос
        private static byte[] DownloadImage(string url) 
        {
            var httpClient = new WebClient();
            var response = httpClient.DownloadData(url);

            return response;
        }
    }

    struct DatePhoto // структура для удобства
    {
        public int NumberP;
        public int WidthP;
        public int HeightP;

        public DatePhoto(int i, int wf, int hf)
        {
            NumberP = i;
            WidthP = wf;
            HeightP = hf;
        }
    }
}
