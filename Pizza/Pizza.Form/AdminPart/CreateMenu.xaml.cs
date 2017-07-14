using Pizza.Logic;
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
using System.Windows.Shapes;

namespace Pizza.Form
{
    /// <summary>
    /// Логика взаимодействия для CreateMenu.xaml
    /// </summary>
    public partial class CreateMenu : Window
    {
        FileClass _fileWork; //класс работы с файлами

        Product _product; //список продуктов в нынешнем заказе
        List<Product> _products; //список всех продуктов

        public CreateMenu()
        {
            InitializeComponent();

            _fileWork = new FileClass();
            _product = new Product();

            string[] category = Properties.Resources.Сategory.Split(','); //Загрузка списка категорий из ресурсов
            
            for (int i = 0; i < category.Length; i++)
            {
                Category.Items.Add(category[i]);
            }

            MenuBoxUpdate();
        }

        void MenuBoxUpdate()
        {
            MenuBox.Items.Clear();

            _products = _fileWork.ReadProducts();

            _products.Sort((a, b) => a.category.CompareTo(b.category));

            for (int i = 0; i < _products.Count; i++)
            {
                MenuBox.Items.Add(_products[i]);
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _product.name = Name.Text;
                _product.category = Category.Text;
                _product.price = Convert.ToDouble(Price.Text);

                Name.Text = "";
                Category.Text = "";
                Price.Text = "";

                try
                {
                    _fileWork.RedactProduct(_product);
                }
                catch (Exception)
                {
                    _fileWork.AddProduct(_product);
                }

                MenuBoxUpdate();
            }
            catch (Exception)
            {
                MessageBox.Show("Не все ячейки заполнены!", "Внимание!");
            } 
        }

        private void DelProduct_Click(object sender, RoutedEventArgs e)
        {
            if (MenuBox.SelectedIndex != -1)
            {
                _fileWork.DelProduct(_products[MenuBox.SelectedIndex]);

                MenuBox.Items.RemoveAt(MenuBox.SelectedIndex);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MenuBox.SelectedIndex != -1)
            {
                Name.Text = (MenuBox.Items[MenuBox.SelectedIndex] as Product).name;
                Category.Text = (MenuBox.Items[MenuBox.SelectedIndex] as Product).category;
                Price.Text = Convert.ToString((MenuBox.Items[MenuBox.SelectedIndex] as Product).price);

                _product.name = Name.Text;
                _product.category = Category.Text;
                _product.price = Convert.ToDouble(Price.Text);
            }
        }

    }
}
