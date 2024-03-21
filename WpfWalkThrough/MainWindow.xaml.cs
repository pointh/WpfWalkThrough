using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace WpfWalkThrough
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // **ObservableCollection** má schopnost volat aktualizaci připojeného ListView
        // při kažné změně obsahu
        public ObservableCollection<Person> Manzele { get; set; }

        public static string[] StavValues { get; set; } = EnumToStringConverter.StavStr.Values.ToArray();

        public MainWindow()
        {
            InitializeComponent();
            Manzele = new ObservableCollection<Person>();
            DataContext = this;
        }

        string on = "";
        public string On
        {
            get => on;
            set
            {
                on = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(On)));
            }
        }

        string ona = "";
        public string Ona
        {
            get => ona;
            set
            {
                ona = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ona)));
            }
        }

        Stav stav;
        public Stav Stav
        {
            get => stav;
            set
            {
                stav = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Stav)));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Person p = new Person(On, Stav, Ona);
            Debug.WriteLine(p);
            Manzele.Add(p);
        }
    }
}