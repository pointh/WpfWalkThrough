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

namespace WpfWalkThrough
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        string lastClick;
        public string LastClick 
        { 
            get => lastClick; 
            set 
            { 
                lastClick = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LastClick")); 
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            LastClick = "Zatím nic";
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Window_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            LastClick = e.Source.ToString().ToUpper();
            label.Content += "\n" +e.Source.ToString();
        }
    }
}