using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfWalkThrough
{
    internal class SurnameEntry : TextBox, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            
            if ((e.Source as TextBox)?.Text?.Length > 3)
            {
                EdtErr = string.Empty;
                EdtErrVisible = Visibility.Collapsed;
                return;
            }

            EdtErr = "Příliš krátké";
            EdtErrVisible = Visibility.Visible;
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
