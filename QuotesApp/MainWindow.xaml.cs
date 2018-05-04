using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace QuotesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Quote _appQuote = new Quote(true);
        public Quote AppQuote
        {
            get { return _appQuote; }
            set { _appQuote = value; OnPropertyChanged(new PropertyChangedEventArgs("AppQuote")); }
        }


        private DispatcherTimer dt = new DispatcherTimer()
        { Interval = TimeSpan.FromSeconds(10) };

        public MainWindow()
        {
            this.Loaded += AppQuote.RefreshQuotes;
            this.Loaded += AppQuote.RefreshScreen;
            InitializeComponent();
            dt.Tick += AppQuote.RefreshQuotes;
            dt.Tick += AppQuote.RefreshScreen;

            dt.Start();
            this.DataContext = this;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

    }
}
