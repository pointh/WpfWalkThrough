using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WpfWalkThrough
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // **ObservableCollection** má schopnost volat aktualizaci připojeného ListView
        // při kažné změně obsahu
        public ObservableCollection<string> History {  get; set; }

        string? lastClick;

        public string? LastClick 
        { 
            get => lastClick; 
            set 
            { 
                // Když se změní hodnota vlastnosti ...
                lastClick = value;
                
                if (value != null) 
                {
                    History.Add(value);
                }
                
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LastClick)));
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            History = new ObservableCollection<string>();
            DataContext = this;
        }

        private void Window_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            LastClick = e?.Source?.ToString()?.ToUpper();
        }
    }
}