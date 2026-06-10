using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Zadanie_3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // 1. Список для хранения самих объектов компьютеров

        private List<Shop> _archiveList = new List<Shop>();

        // 2. Список для хранения текстовых строк, которые видит пользователь

        private List<string> _uiStrings = new List<string>();

        public MainWindow()
        {
            InitializeComponent();

            // Связываем наш список строк с ListBox на форме

            ComputersListBox.ItemsSource = _uiStrings;

            TypeComboBox.SelectionChanged += TypeComboBox_SelectionChanged;

            // Добавляем стартовые демо-данные

            AddShop(new Shop("Шпатен", 15000, 800000, 5000));
            AddShop(new ShopD("Хаммам", 40, 200000, 200, 15));

            UpdateStatus();
        }

        // Вспомогательный метод: заставляет ListBox на экране перерисоваться [3, 4]

        private void RefreshUI()
        {
            ComputersListBox.ItemsSource = null;
            ComputersListBox.ItemsSource = _uiStrings;
        }

        // Перегрузка №1: Принимает готовый объект

        public void AddShop(Shop sp)
        {
            _archiveList.Add(sp);
            _uiStrings.Add(sp.GetInfo());
            RefreshUI(); // Вручную обновляем экран
        }

        // Перегрузка №2: Принимает параметры из TextBox-ов

        public void AddShop(string shop, int sales, double money, int customers, string type, int discount = 0)
        {
            Shop newSp;

            if (type == "Есть скидки")
            {
                newSp = new ShopD(shop, sales, money, customers, discount);
            }
            else
            {
                newSp = new Shop(shop, sales, money, customers);
            }

            _archiveList.Add(newSp);
            _uiStrings.Add(newSp.GetInfo());
            RefreshUI(); // Вручную обновляем экран
        }

        // Перегрузка №1: Удаление по индексу (номеру строки)

        public void RemoveShop(int index)
        {
            if (index >= 0 && index < _archiveList.Count)
            {
                _archiveList.RemoveAt(index);
                _uiStrings.RemoveAt(index);
                RefreshUI(); // Вручную обновляем экран
            }
        }

        // Перегрузка №2: Удаление самого последнего элемента

        public void RemoveShop()
        {
            if (_archiveList.Count > 0)
            {
                int lastIndex = _archiveList.Count - 1;
                _archiveList.RemoveAt(lastIndex);
                _uiStrings.RemoveAt(lastIndex);
                RefreshUI();
            }
        }

        //Кнопка добавления магазина

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string shop = ShopName.Text;
                int sales = int.Parse(SalesBox.Text);
                double money = double.Parse(MoneyBox.Text);
                int customers = int.Parse(CustomersBox.Text);
                string selectedType = (TypeComboBox.SelectedItem as ComboBoxItem).Content.ToString();

                if (selectedType == "Есть скидки")
                {
                    int discount = int.Parse(DiscBox.Text);
                    AddShop(shop, sales, money, customers, selectedType, discount);
                }
                else AddShop(shop, sales, money, customers, selectedType);

                // Очищаем поля

                ShopName.Clear(); SalesBox.Clear(); CustomersBox.Clear(); MoneyBox.Clear();

                UpdateStatus();
            }
            catch
            {
                MessageBox.Show("Пожалуйста, корректно заполните все поля!", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = ComputersListBox.SelectedIndex;
            if (selectedIndex == -1)
            {
                MessageBox.Show("Выберите компьютер из списка для удаления!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Вызываем перегрузку удаления №1

            RemoveShop(selectedIndex);
            UpdateStatus();
        }

        private void DeleteLastButton_Click(object sender, RoutedEventArgs e)
        {
            // Вызываем перегрузку удаления №2

            RemoveShop();
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            if (!_archiveList.Any())
            {
                StatusTextBlock.Text = "База пуста. Добавьте первый компьютер.";
                return;
            }

            int totalCount = _archiveList.Count();
            double averageMoney = _archiveList.Average(sp => sp.Money);
            var bestSp = _archiveList.OrderByDescending(sp => sp.GetQ()).First();

            StatusTextBlock.Text = $"Всего Магазинов: {totalCount} | Средняя выручка: {averageMoney:F1}руб | Лучший магазин по среднему чеку: {bestSp.ShopName} (Q = {bestSp.GetQ():F2})";
        }

        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (discountPanel == null) return;

            var selectedItem = TypeComboBox.SelectedItem as ComboBoxItem;
            if (selectedItem != null && selectedItem.Content.ToString() == "Есть скидки")
            {
                discountPanel.Visibility = Visibility.Visible;
            }
            else
            {
                discountPanel.Visibility = Visibility.Collapsed;
            }
        }
    }
}
