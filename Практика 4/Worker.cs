using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Практика_4
{
    public class Worker : INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get => _id;
            set { if (_id != value) { _id = value; OnPropertyChanged(); } }
        }

        private string _lname = "";
        public string LName
        {
            get => _lname;
            set { if (_lname != value) { _lname = value; OnPropertyChanged(); OnPropertyChanged(nameof(FullName)); } }
        }

        private string _name = "";
        public string Name
        {
            get => _name;
            set { if (_name != value) { _name = value; OnPropertyChanged(); OnPropertyChanged(nameof(FullName)); } }
        }

        private string _otch = "";
        public string Otch
        {
            get => _otch;
            set { if (_otch != value) { _otch = value; OnPropertyChanged(); OnPropertyChanged(nameof(FullName)); } }
        }

        private string _duty = "";
        public string Duty
        {
            get => _duty;
            set { if (_duty != value) { _duty = value; OnPropertyChanged(); } }
        }

        public string FullName => $"{LName} {Name} {Otch}".Trim();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
