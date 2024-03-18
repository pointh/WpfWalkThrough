using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WpfWalkThrough
{
    public class Body
    {
        public double Weight { get; set; }
        public DateTime HitTime { get; set; }

        public Body(double _weight)
        {
            Weight = _weight;
            HitTime = DateTime.Now;
        }
    }
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // **ObservableCollection** má schopnost volat aktualizaci připojeného ListView
        // při kažné změně obsahu
        public ObservableCollection<Body> Bodies { get; set; }

        string? lastClick;

        public string? LastClick 
        { 
            get => lastClick; 
            set 
            { 
                // Když se změní hodnota vlastnosti ...
                lastClick = value;
                
                // Kliknutí vždy zároveň vytvoří novou instanci Body
                if (value != null) 
                {
                    Bodies.Add(new Body(20));
                }
                
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LastClick)));
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            Bodies = new ObservableCollection<Body>();
            DataContext = this;
        }

        private void Window_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            LastClick = e?.Source?.ToString()?.ToUpper();
        }
    }
}