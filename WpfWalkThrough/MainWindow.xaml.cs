using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfWalkThrough
{
    public partial class MainWindow : Window, INotifyPropertyChanged, INotifyDataErrorInfo
    {
        // Událost při změně kteréhokoliv elementu, reprezentovaného zde vlastností.
        public event PropertyChangedEventHandler? PropertyChanged;

        // Událost při chybě validace formuláře (změnil se seznam chyb)
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        // Chyby budeme ukládat do dictionary, kde klíčem bude název vlastnosti a hodnottou seznam chyb,
        // které patří k této vlastnosti.
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> Errors
        {
            get { return _errors; }
            set { _errors = value; }
        }

        // **ObservableCollection** má schopnost volat aktualizaci připojeného ListView
        // při kažné změně obsahu
        public ObservableCollection<Person> Manzele { get; set; }

        public static string[] StavValues { get; set; } = EnumToStringConverter.StavStr.Values.ToArray();

        public MainWindow()
        {
            InitializeComponent();
            Manzele = new ObservableCollection<Person>();
            DataContext = this;
            ErrorsChanged += CollectErrors;
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

        string errorText = "";
        public string ErrorText
        {
            get => errorText;
            set
            {
                errorText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorText)));
            }
        }


        bool Single()
        {
            return Stav == Stav.Rozvedeny || Stav == Stav.Svobodny;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var tb1Binding = tb1.GetBindingExpression(TextBox.TextProperty);
            var tb2Binding = tb2.GetBindingExpression(TextBox.TextProperty);

            if (tb1Binding?.ValidationErrors?.Count > 0)
            {
                AddError(On, "Není možné uložit záznam, má-li pole Manžel chybu");
                return;
            }

            if (tb2Binding?.ValidationErrors?.Count > 0 && Single() == false)
            {
                AddError(On, "Není možné uložit záznam, má-li pole Manželka chybu");
                return;
            }

            ClearErrors();

            if (Single())
            {
                if (string.IsNullOrEmpty(On))
                {
                    return;
                }
                if (!string.IsNullOrEmpty(Ona))
                {
                    AddError(On, "Svobodný nebo rozvedený nemůže mít manželku.");
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(Ona));
                    return;
                }
            }

            if (!Single())
            {
                if (string.IsNullOrEmpty(On))
                {
                    return;
                }
                if (string.IsNullOrEmpty(Ona))
                {
                    AddError(On, "Ženatý musí mít manželku.");
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(On));
                    return;
                }
            } 
            
            // Všechno je v pořádku
            Person p = new Person(On, Stav, Ona);
            Manzele.Add(p);
        }

        public bool HasErrors => _errors.Count == 0;

        public IEnumerable GetErrors(string? propertyName)
        {
            return Errors[propertyName];
        }

        public void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors[propertyName] = new List<string>();

            if (!_errors[propertyName].Contains(error))
            {
                _errors[propertyName].Add(error);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        public void ClearErrors()
        {
            Errors.Clear();
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(""));
        }

        private void CollectErrors(object? sender, DataErrorsChangedEventArgs e)
        {
            StringBuilder sb = new();
            foreach(var error in Errors)
            {
                if (error.Value != null)
                {
                    sb.AppendLine(error.Key + "\n");
                    sb.Append(string.Join("\n", error.Value));
                }
            }
            ErrorText = sb.ToString();
        }
    }
}