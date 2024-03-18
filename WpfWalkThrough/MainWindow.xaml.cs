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

        public List<string> History {  get; set; }

        // Tohle je "plain vanilla" řešení pro binding
        // privátní pole třídy typu string ...
        string? lastClick;

        // ... je odkazované z vlastnosti podobného jména - Konvence: vlastnosti jsou s velkým počátečním písmenem
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

                    // Tohle je hack, který aktualizuje zobrazení ListView souběžně s History
                    // Lepší je použít typ ObservableCollection<string> ...
                    LVHistory.ItemsSource = null;
                    LVHistory.ItemsSource = History;
                    // Ani následující nepomůže:
                    // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(History)));
                    //
                    // Je to chyba ListView nebo vlastnost, která nám umožňuje rozhodovat o tom, kdy dojde 
                    // k překreslení? 
                    // Může se to hodit, když např. načítáme History ze souboru a přidáváme řádek po řádku.
                    // Kdyby se ListView překreslovalo při každém přidání řádku, program by to hodně zpomalilo.
                }
                
                // ... oznam WPF UI, že se musí překreslit část uživatelského interface,
                // která má binding na LastClick.
                // Operátor ?. zajistí, že se Invoke zavolá jenom když PropertyChanged != null
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LastClick)));
                // To je stejné jako PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LastClick")); 
                // nameof(LastClick) nám kompilátor vyhodí jako chybu, když změníme název vlastnosti LastClick,
                // což u stringu "LastClick" udělat nemůže - string jako string
                // To může vést k nepříjemným chybám při REFAKTORINGU
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            // všechny valastnosti, které mají binding z příslušného XAML souboru (MainWindow.xaml)
            // se budou hledat v této třídě.
            History = new List<string>();
            DataContext = this;
        }

        private void Window_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            // Tady jenom měníme hodnotu vlastnosti,
            // takže se zavolá její setter
            // a ze setteru se zavolá PropertyChanged.Invoke(...)
            LastClick = e?.Source?.ToString()?.ToUpper();
            // V tomto případě se tedy vyhneme přímému volání
            // jména prvku UI, který se má změnit.
            // To nám umožňuje zpracovávat a testova logiku aplikace bez ohledu 
            // na její prezentaci v GUI, kterou můžeme realizovat bindingem později.
        }
    }
}