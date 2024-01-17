using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace samochody
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Samochod> list = new ObservableCollection<Samochod>();
        public MainWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            list.Add(new Samochod(_NrRejestracyjny.Text, _Marka.Text, int.Parse(_RokProdukcji.Text), _Kolor.Text, int.Parse(_IloscPasazerow.Text)));
            dataGrid.ItemsSource = list;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            list.Clear();
            dataGrid.ItemsSource = list;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "JSON file(*.json)|*.json";
            if (dlg.ShowDialog() == true)
            {
                File.WriteAllText(dlg.FileName, JsonSerializer.Serialize(list));
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JSON file(*.json)|*.json";
            if (dlg.ShowDialog() == true)
            {
                string json = File.ReadAllText(dlg.FileName);
                list.Clear();
                list = JsonSerializer.Deserialize<ObservableCollection<Samochod>>(json);
                dataGrid.ItemsSource = list;
            }
        }
    }
}
