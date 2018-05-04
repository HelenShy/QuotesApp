using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Windows.Media;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Net.Http;
using System.Windows.Media.Imaging;
using System.Threading;
using System.Runtime.CompilerServices;

namespace QuotesApp
{
    public class Quote : INotifyPropertyChanged
    {
        private static string _currentQuote;
        private static Brush _quoteColor;
        private static BitmapImage _myImg;
        static object block = new object();
        bool change { get; set; } = true;

        public Quote(bool c)
        {
            change = c;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private static List<string> AllQuotes { get; set; }
        public  string CurrentQuote
        {
            get { return _currentQuote; }
            set
            {
                _currentQuote = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentQuote"));
            }
        }

        public Brush QuoteColor
        {
            get { return _quoteColor; }
            set
            {
                _quoteColor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("QuoteColor"));
            }
        }

        public BitmapImage MyImg
        {
            get { return _myImg; }
            set
            {
                _myImg = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MyImg"));
            }
        }

        public BitmapImage newImg
        {
            get;
            set;
        }

        private static string QuotesPath { get { return Environment.CurrentDirectory + "/../../Assets/Quotes.txt"; } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }


        public async void RefreshQuotes(object sender, object e)
        {
            string Text = String.Empty;
            string line;
            using (FileStream stream =  File.OpenRead(QuotesPath))
            {
                StreamReader reader = new StreamReader(stream, Encoding.Unicode);
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    Text += line;
                }
                reader.Dispose();
            }

            Regex rg = new Regex(@"\d+\.");
            Text = rg.Replace(Text, "@");
            AllQuotes = Text.Split('@').ToList();


            Random rnd = new Random();
            CurrentQuote = AllQuotes[rnd.Next(AllQuotes.Count)];

            Type brushesType = typeof(Brushes);
            PropertyInfo[] properties = brushesType.GetProperties();

            QuoteColor = (Brush)properties[rnd.Next(properties.Length)].GetValue(null, null); 

        }

        public void RefreshScreen(object sender, object e)
            {
            //var client = new HttpClient();`
            change = !change;
            if (change)
            {
                MyImg = new BitmapImage(new Uri(@"https://source.unsplash.com/collection/1017"));

            }
            else
            {
                MyImg = new BitmapImage(new Uri(@"https://source.unsplash.com/collection/152630"));//152630
            }

           // MyImg.DownloadCompleted += objImage_DownloadCompleted;
            
        }

        private void objImage_DownloadCompleted(object sender, EventArgs e)
        {
            Monitor.Enter(block);
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create((BitmapImage)sender));

            using (var filestream = new FileStream(@"OutputImage.jpg", FileMode.Create))
                encoder.Save(filestream);
            Monitor.Exit(block);
           // SetWallpaper(Environment.CurrentDirectory + @"\OutputImage.jpg", 2, 0);
        }

        private void SetWallpaper(string WallpaperLocation, int WallpaperStyle, int TileWallpaper)
        {
            bool success = false;
            while (!success)
            {
                try
                {
                    Monitor.Enter(block);
                    // Sets the actual wallpaper
                    SystemParametersInfo(20, 0, WallpaperLocation, 0x01 | 0x02);
                    // Set the wallpaper style to streched (can be changed to tile, center, maintain aspect ratio, etc.
                    RegistryKey rkWallPaper = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", true);
                    // Sets the wallpaper style
                    rkWallPaper.SetValue("WallpaperStyle", WallpaperStyle);
                    // Whether or not this wallpaper will be displayed as a tile
                    rkWallPaper.SetValue("TileWallpaper", TileWallpaper);
                    rkWallPaper.Close();
                    Monitor.Exit(block);
                    success = true;
                }
                catch (Exception)
                {
                }
                
            }
            //// Sets the actual wallpaper
            //SystemParametersInfo(20, 0, WallpaperLocation, 0x01 | 0x02);
            //// Set the wallpaper style to streched (can be changed to tile, center, maintain aspect ratio, etc.
            //RegistryKey rkWallPaper = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", true);
            //// Sets the wallpaper style
            //rkWallPaper.SetValue("WallpaperStyle", WallpaperStyle);
            //// Whether or not this wallpaper will be displayed as a tile
            //rkWallPaper.SetValue("TileWallpaper", TileWallpaper);
            //rkWallPaper.Close();
        }
    }
}
