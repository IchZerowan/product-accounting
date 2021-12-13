using System;
using System.Collections.Generic;
using System.Windows;
using System.Net;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using product_accounting_frontend.dao;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace product_accounting_frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Product> products;
        public MainWindow()
        {
            InitializeComponent();
        }        

        private async void products_Click(object sender, RoutedEventArgs e)
        {
            addProductButton.Visibility = Visibility.Visible;
            ProductDAO productDAO = new ProductDAO();
            products = Task.Run(productDAO.executeGetQuery).Result;
            productsView.ItemsSource = products;
        }

        

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;            
            ProductDAO productDAO = new ProductDAO();
            if (await productDAO.executeDeleteQuery(Int32.Parse(button.Uid)))
            {
                products.Remove(products.Find(product=>product.id.ToString() == button.Uid));
            }                
            productsView.Items.Refresh();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int selectedProductInted = products.FindIndex(product => product.id.ToString() == button.Uid);
            products[selectedProductInted].isViewVisible = Visibility.Collapsed;
            products[selectedProductInted].isEditingVisible = Visibility.Visible;
            productsView.Items.Refresh();
        }

        private async void DiscardButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int selectedProductIndex = products.FindIndex(product => product.id.ToString() == button.Uid);
            products[selectedProductIndex].isViewVisible = Visibility.Visible;
            products[selectedProductIndex].isEditingVisible = Visibility.Collapsed;
            productsView.Items.Refresh();
            products.Clear();
            ProductDAO productDAO = new ProductDAO();
            products = Task.Run(productDAO.executeGetQuery).Result;
            productsView.ItemsSource = products;
        }

        private async void AcceptButton_Click(Object sender, RoutedEventArgs e)
        {
            ProductDAO productDAO = new ProductDAO();
            if(await productDAO.executePutQuery(Int32.Parse(((Button)sender).Uid), products[products
                .FindIndex(product =>
                    product.id.ToString() == ((Button)sender).Uid
                )])){
            }
            products.Clear();            
            products = Task.Run(productDAO.executeGetQuery).Result;
            productsView.ItemsSource = products;
            Button button = sender as Button;
            int selectedProductIndex = products.FindIndex(product => product.id.ToString() == button.Uid);
            products[selectedProductIndex].isViewVisible = Visibility.Visible;
            products[selectedProductIndex].isEditingVisible = Visibility.Collapsed;
            productsView.Items.Refresh();
        }

        private async void AddProductButtonClicked(Object sender, RoutedEventArgs e)
        {
            products.Add(new Product(0, "", "", 0,0, false, 0));
            productsView.Items.Refresh();
            int lastProductIndex = products.Count - 1;
            products[lastProductIndex].isViewVisible = Visibility.Collapsed;
            products[lastProductIndex].isEditingVisible = Visibility.Collapsed;
            products[lastProductIndex].isAddVisible = Visibility.Visible;
            productsView.Items.Refresh();
        }
        

        private async void AcceptAddButton_Click(Object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int selectedProductIndex = products.FindIndex(product => product.id.ToString() == button.Uid);
            
            ProductDAO productDAO = new ProductDAO();
            productDAO.executePostQuery(products[selectedProductIndex]);
            products.Clear();
            products = Task.Run(productDAO.executeGetQuery).Result;
            productsView.ItemsSource = products;
        }

        private async void DiscardAddButton_Click(Object sender, RoutedEventArgs e)
        {
            products.Remove(products[products.Count - 1]);
            productsView.Items.Refresh();
        }

        private void archivedProducts_Click(object sender, RoutedEventArgs e)
        {
        }

        private void suppliers_Click(object sender, RoutedEventArgs e)
        {
        }

        private void archivedSuppliers_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
