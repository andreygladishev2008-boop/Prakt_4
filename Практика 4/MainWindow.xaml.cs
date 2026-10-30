using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Практика_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Worker CurrentWorker = new Worker();

        public ObservableCollection<Worker> Workers { get; set; } = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = CurrentWorker;
            ListView_Workers.ItemsSource = Workers;
            CurrentWorker.Id = GenerateNewId();
            LoadAllWorkers();
        }

        private int GenerateNewId()
        {
            int id = 1;
            while (File.Exists($"{id}.json"))
            {
                id++;
            }
            return id;
        }

        private void LoadAllWorkers()
        {
            Workers.Clear();
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.json");
            foreach (var file in files)
            {
                try
                {
                    string json = File.ReadAllText(file);
                    var w = JsonSerializer.Deserialize<Worker>(json);
                    if (w != null && w.Id > 0) Workers.Add(w);
                }
                catch
                {}
            }
        }

        private void ListViewWorkers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListView_Workers.SelectedItem is Worker w)
            {
                CurrentWorker.Id = w.Id;
                CurrentWorker.LName = w.LName;
                CurrentWorker.Name = w.Name;
                CurrentWorker.Otch = w.Otch;
                CurrentWorker.Duty = w.Duty;
            }
        }

        private void Save_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string fileName = $"{CurrentWorker.Id}.json";
                string json = JsonSerializer.Serialize(CurrentWorker, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(fileName, json);
                LoadAllWorkers();
                MessageBox.Show($"Сохранено в {fileName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            CurrentWorker.Id = GenerateNewId();
            CurrentWorker.LName = "";
            CurrentWorker.Name = "";
            CurrentWorker.Otch = "";
            CurrentWorker.Duty = "";
            ListView_Workers.SelectedItem = null;
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(Find_ID.Text, out int searchId))
            {
                MessageBox.Show("Введите числовой ID.");
                return;
            }

            string fileName = $"{searchId}.json";
            if (!File.Exists(fileName))
            {
                MessageBox.Show($"Запись с ID = {searchId} не найдена.");
                return;
            }

            string json = File.ReadAllText(fileName);
            var loaded = JsonSerializer.Deserialize<Worker>(json);
            if (loaded != null)
            {
                CurrentWorker.Id = loaded.Id;
                CurrentWorker.LName = loaded.LName;
                CurrentWorker.Name = loaded.Name;
                CurrentWorker.Otch = loaded.Otch;
                CurrentWorker.Duty = loaded.Duty;
            }
        }
    }
}