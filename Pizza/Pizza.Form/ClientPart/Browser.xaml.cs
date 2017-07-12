﻿using System;
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
    /// Логика взаимодействия для Browser.xaml
    /// </summary>
    public partial class Browser : Window
    {
        public Browser(string site)
        {
            InitializeComponent();

            BrowserWindow.Source = new Uri(site);
        }
    }
}