using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfWalkThrough.Properties;

namespace WpfWalkThrough
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // Tohle je "plain vanilla" řešení pro binding
        // privátní pole třídy typu string ...
        string lastClick;

        // ... je odkazované z vlastnosti podobného jména - Konvence: vlastnosti jsou s velkým počátečním písmenem
        public string LastClick 
        { 
            get => lastClick; 
            set 
            { 
                // Když se změní hodnota vlastnosti ...
                lastClick = value; 

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
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

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
            label1.Content += "\n" +e.Source.ToString();
        }

        private void ButtonImage_MouseEnter(object sender, MouseEventArgs e)
        {
            label1.Content += "\n" + "Myš přišla";
        }
    }
}