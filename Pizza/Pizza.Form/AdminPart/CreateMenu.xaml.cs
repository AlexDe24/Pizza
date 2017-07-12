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
        Product _product;
        List<Product> _products;

        FileClass _fileWork;

        List<string> _assassortment;
        public CreateMenu()
        {
            InitializeComponent();

            _fileWork = new FileClass();

            _product = new Product();
            _products = new List<Product>();

            _products = _fileWork.ReadProducts();

            _assassortment = new List<string>() { "Напитки", "Пицца", "Бургеры", "Закуски"};

            for (int i = 0; i < _assassortment.Count; i++)
            {
                Category.Items.Add(_assassortment[i]);
            }

            _products.Sort((a, b) => a.category.CompareTo(b.category));

            for (int i = 0; i < _products.Count; i++)
            {
                MenuBox.Items.Add(_products[i]);
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            _product.name = Name.Text;
            _product.category = Category.Text;
            _product.price = Convert.ToDouble(Price.Text);

            Name.Text = "";
            Category.Text = "";
            Price.Text = "";

            if (_products.Any(x => x.name == _product.name))
            {
                for (int i = 0; i < MenuBox.Items.Count; i++)
                {
                    if (_products.Any(x => x.name == _product.name))
                        _products[i] = _product;
                    if ((MenuBox.Items[i] as Product).name == _product.name)
                        MenuBox.Items[i] = _product;
                }

                _fileWork.RedactProduct(_product);
            }
            else
            {
                _products.Add(_product);
                MenuBox.Items.Add(_product);

                _fileWork.AddProduct(_product);
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
                Name.Text = (MenuBox.SelectedItem as Product).name;
                Category.Text = (MenuBox.SelectedItem as Product).category;
                Price.Text = Convert.ToString((MenuBox.SelectedItem as Product).price);

                _product.name = Name.Text;
                _product.category = Category.Text;
                _product.price = Convert.ToDouble(Price.Text);
            }
        }
    }
}
