using Pizza.Logic.DTO;
using Pizza.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Pizza.Form.OperatorPart
{
    /// <summary>
    /// Логика взаимодействия для CreateMenu.xaml
    /// </summary>
    public partial class CreateMenu : Window
    {
        ProductSQLWork _productSQLWork; //класс работы с файлами продуктов
        CategorySQLWork _categorySQLWork; //класс работы с файлами категорий

        Product _product; //список продуктов в нынешнем заказе
        List<Product> _products; //список всех продуктов
        List<Category> _category;

        public CreateMenu()
        {
            InitializeComponent();

            _productSQLWork = new ProductSQLWork();
            _categorySQLWork = new CategorySQLWork();

            _product = new Product();
            _category = _categorySQLWork.ReadCategory();

            var category = _category.Distinct();

            for (int i = 1; i < _category.Count; i++)
            {
                if (_category[i].ParentCategory != null)
                    if (CategoryMain.Items.Count == 0)
                    {
                        CategoryMain.Items.Add(_category[i].ParentCategory.Name);
                    }
                    else if (CategoryMain.Items[CategoryMain.Items.Count - 1] != _category[i].ParentCategory.Name)
                        CategoryMain.Items.Add(_category[i].ParentCategory.Name);
            }

            MenuBoxUpdate();
        }

        void MenuBoxUpdate()
        {
            MenuBox.Items.Clear();

            _products = _productSQLWork.ReadProducts();
            _products.Sort((a, b) => a.Category.Name.CompareTo(b.Category.Name));

            for (int i = 0; i < _products.Count; i++)
            {
                MenuBox.Items.Add(_products[i]);
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _product.Name = Name.Text;
                _product.CategoryId = _category.Where(x => x.Name == Category.Text).FirstOrDefault().Id;
                _product.Price = Convert.ToDecimal(Price.Text);

                if (_product.Name == "" || _product.Category == null)
                    MessageBox.Show("Не все ячейки заполнены!", "Внимание!");
                else
                {
                    try
                    {
                        _productSQLWork.EditProduct(_product);
                    }
                    catch (Exception)
                    {
                        _productSQLWork.AddProduct(_product);
                    }

                    MenuBoxUpdate();

                    Name.Text = "";
                    Category.Text = "";
                    Price.Text = "";
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
                _productSQLWork.DeleteProduct(MenuBox.SelectedItem as Product);

                MenuBox.Items.RemoveAt(MenuBox.SelectedIndex);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CategoryMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Category.Items.Clear();

            for (int i = 0; i < _category.Count; i++)
            {
                if (_category[i].ParentCategory != null)
                    if (_category[i].ParentCategory.Name == CategoryMain.SelectedItem.ToString())
                        Category.Items.Add(_category[i].Name);
            }
        }

        private void MenuBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (MenuBox.SelectedItem != null)
            {
                Name.Text = (MenuBox.SelectedItem as Product).Name;
                CategoryMain.Text = (MenuBox.SelectedItem as Product).Category.ParentCategory.Name;
                Category.Text = (MenuBox.SelectedItem as Product).Category.Name;
                Price.Text = Convert.ToString((MenuBox.SelectedItem as Product).Price);

                _product.Name = Name.Text;
                _product.CategoryId = (MenuBox.SelectedItem as Product).CategoryId;
                _product.Price = Convert.ToDecimal(Price.Text);
            }
        }
    }
}
