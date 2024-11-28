using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace ImageSection
{
    class FragmentContext: DbContext // класс для работы с Entity Freamwork
    {
        public FragmentContext() : base("DefaultConnection")
        {

        }
        public DbSet<Photos> photos { get; set; } = null;
    }

    class Photos
    {
        [Key]
        public int Number { get; set; }

        public int NumberF { get; set; }

        public byte[] ImageData { get; set; }

        public Photos(int numberid, int number, byte[] imageData)
        {
            NumberF = numberid;
            Number = number;
            ImageData = imageData;
        }

        public Photos()
        {

        }
    }
}
