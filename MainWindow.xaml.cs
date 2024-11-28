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

        private GenerateDataFragment generate = new GenerateDataFragment(); // экземпляр класса для вызова метода расчёта координат
        List<FragmentPhoto> fragments = new List<FragmentPhoto>(); // массив фрагментов
 
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
                fragments = generate.CreateRandom(widthf, heightf, Convert.ToInt32(count.Text)); // генерация координат
                tableNumber.ItemsSource = fragments;
                tableNumber.Items.Refresh();

                wpanel.Children.Clear();
                for (int i = 0; i < fragments.Count; i++)
                {
                    Grid gridRect = new Grid(); // создание контейнера для прямоугольника и текста
                    System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle { Width = widthf, Height = heightf, Fill = System.Windows.Media.Brushes.LightGray, StrokeThickness = 1 };
             
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = $"{fragments[i].CountF}, {fragments[i].WidthF}, {fragments[i].HeightF}";
                    gridRect.Children.Add(rect);
                    gridRect.Children.Add(textBlock);
                    wpanel.Children.Add(gridRect); // добавление в контейнер
                }
                
                but1.Dispatcher.InvokeAsync(DrawFragments, DispatcherPriority.SystemIdle); // метод для заполнения серых прямоугольников картинками

            }
            else
            {
                MessageBox.Show("Проверьте входные данные!");
            }
        }

        private void DrawFragments() // добавление картинок
        {

            foreach(Grid grid in wpanel.Children)
            {
                OneDrawFragments(grid);
            }
        } 
        
        private void OneDrawFragments(Grid grid) // метод отрисовки фрагмента
        {
            TextBlock textBlock = (TextBlock)grid.Children[1];
            string[] paramsf = textBlock.Text.Split(',');
 
            FragmentContext db = new FragmentContext();
            int par = Convert.ToInt32(paramsf[0]);
            List<Photos> photos = db.photos.Where(p => p.NumberF == par).ToList<Photos>();
            MemoryStream stream = new MemoryStream(photos[0].ImageData);
            var img = new Bitmap(stream);
            Bitmap region = new Bitmap(Convert.ToInt32(width.Text), Convert.ToInt32(height.Text));

            using (Graphics g = Graphics.FromImage(region)) // "вырезание" фрагмента 
            {
                g.DrawImage(img, 0, 0, new System.Drawing.Rectangle(Convert.ToInt32(paramsf[1]), Convert.ToInt32(paramsf[2]), Convert.ToInt32(width.Text), Convert.ToInt32(height.Text)), GraphicsUnit.Pixel);
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
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle { Width = Convert.ToInt32(width.Text), Height = Convert.ToInt32(height.Text), Fill = imageBrush, StrokeThickness = 1 };
                     grid.Children.RemoveRange(0, 2);
                     grid.Children.Add(rect);
                     grid.Children.Add(textBlock);

                // вызов метода для обновления компонента
                wpanel.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate ()
                {
                      wpanel.UpdateLayout();
                }));
            }
        }

    }
}
