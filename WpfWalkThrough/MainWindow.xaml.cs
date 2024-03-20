using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WpfWalkThrough
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
            // Tady jenom měníme hodnotu vlastnosti,
            // takže se zavolá její setter
            // a ze setteru se zavolá PropertyChanged.Invoke(...)
            LastClick = e.Source.ToString().ToUpper();
            // V tomto případě se tedy vyhneme přímému volání
            // jména prvku UI, který se má změnit.
            // To nám umožňuje zpracovávat a testova logiku aplikace bez ohledu 
            // na její prezentaci v GUI, kterou můžeme realizovat bindingem později.

            // Tady přímo měníme obsah prvku, který voláme přímo jménem (label1)
            label1.Content += "\n" + e.Source.ToString();
        }

        private void ButtonImage_MouseEnter(object sender, MouseEventArgs e)
        {
            label1.Content += "\n" + "Myš přišla";
        }

        string edtText;
        public string EdtText
        {
            get => edtText;
            set
            {
                edtText = value;
                if (value.Length > 3)
                {
                    EdtErr = string.Empty;
                    EdtErrVisible = Visibility.Collapsed;
                }
                else
                {
                    EdtErr = "Příliš krátké";
                    EdtErrVisible = Visibility.Visible;
                }
            }
        }

        Visibility edtErrVisible;
        public Visibility EdtErrVisible
        {
            get => edtErrVisible;
            set
            {
                edtErrVisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EdtErrVisible)));
            }
        }


        string edtErr;
        public string EdtErr
        {
            get => edtErr;
            set
            {
                edtErr = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EdtErr)));
            }
        }

    }
}