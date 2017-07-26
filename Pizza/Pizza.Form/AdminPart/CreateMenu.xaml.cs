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
        List<Category> _category;

        public CreateMenu()
        {
            InitializeComponent();

            _fileWork = new FileClass();
            _product = new Product();
            _category = _fileWork.ReadCategory();

            var category = _category.Distinct();

            for (int i = 1; i < _category.Count; i++)
            {
                if (_category[i].parentCategory != null)
                    if (CategoryMain.Items.Count == 0)
                    {
                        CategoryMain.Items.Add(_category[i].parentCategory.Name);
                    }
                    else if (CategoryMain.Items[CategoryMain.Items.Count - 1] != _category[i].parentCategory.Name)
                        CategoryMain.Items.Add(_category[i].parentCategory.Name);
            }

            MenuBoxUpdate();
        }

        void MenuBoxUpdate()
        {
            MenuBox.Items.Clear();

            _products = _fileWork.ReadProducts();
            _products.Sort((a, b) => a.category.Name.CompareTo(b.category.Name));

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
                _product.category = _category.Where(x => x.Name == Category.Text).FirstOrDefault();
                _product.price = Convert.ToDecimal(Price.Text);

                if (_product.name == "" || _product.category == null)
                    MessageBox.Show("Не все ячейки заполнены!", "Внимание!");
                else
                {
                    Name.Text = "";
                    //CategoryMain.Text = "";
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
            }
            catch (Exception)
            {
                MessageBox.Show("Неверный формат строки!", "Внимание!");
            }
        }

        private void DelProduct_Click(object sender, RoutedEventArgs e)
        {
            if (MenuBox.SelectedIndex != -1)
            {
                _fileWork.DelProduct(MenuBox.SelectedItem as Product);

                MenuBox.Items.RemoveAt(MenuBox.SelectedIndex);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Name.Text = (MenuBox.SelectedItem as Product).name;
            CategoryMain.Text = (MenuBox.SelectedItem as Product).category.parentCategory.Name;
            Category.Text = (MenuBox.SelectedItem as Product).category.Name;
            Price.Text = Convert.ToString((MenuBox.SelectedItem as Product).price);

            _product.name = Name.Text;
            _product.category = (MenuBox.SelectedItem as Product).category;
            _product.price = Convert.ToDecimal(Price.Text);
        }

        private void CategoryMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Category.Items.Clear();

            for (int i = 0; i < _category.Count; i++)
            {
                if (_category[i].parentCategory != null)
                    if (_category[i].parentCategory.Name == CategoryMain.SelectedItem.ToString())
                        Category.Items.Add(_category[i].Name);
            }
        }
    }
}
