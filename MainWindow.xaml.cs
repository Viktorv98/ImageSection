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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ImageSection
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<FragmentPhoto> fragments = new List<FragmentPhoto>(); // массив фрагментов
        public ObservableCollection<PictureFragment> PictureFragments = new ObservableCollection<PictureFragment>();
        public MyController controller = new MyController();

        public MainWindow()
        {
            InitializeComponent();           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {          
            if ((width.Text != "") && (height.Text != "") && (count.Text != "")) // проверка на заполнение полей
            {
                int heightf = Convert.ToInt32(height.Text);
                int widthf = Convert.ToInt32(width.Text);
                fragments.Clear(); // очистка массива при повторном нажатии кнопки

                fragments = controller.CreateRandom(widthf, heightf, Convert.ToInt32(count.Text)); // генерация координат
                tableNumber.ItemsSource = fragments;
                tableNumber.Items.Refresh();

                PictureFragments = controller.LoadImage(fragments, widthf, heightf);              
                icon.ItemsSource = PictureFragments;
            }
            else
            {
                MessageBox.Show("Проверьте входные данные!");
            }
        }

    }
}
