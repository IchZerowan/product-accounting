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
using product_accounting_frontend.models;

namespace product_accounting_frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Product> products;
        public List<Supplier> suppliers;
        public List<Coupon> coupons;
        public MainWindow()
        {
            InitializeComponent();            
        }             

        private async void products_Click(object sender, RoutedEventArgs e)
        {
            suppliersView.Visibility = Visibility.Collapsed;
            productsView.Visibility = Visibility.Visible;
            addProductButton.Visibility = Visibility.Visible;
            addSupplierButton.Visibility = Visibility.Collapsed;
            addCouponButton.Visibility = Visibility.Collapsed; 
            ProductDAO productDAO = new ProductDAO();
            products = Task.Run(productDAO.executeGetQuery).Result;
            productsView.ItemsSource = products;
        }

        private void AddAnonymousObject(EntityBase entity)
        {
            entity.isViewButtonsVisible = Visibility.Collapsed;
            entity.isEditingButtonsVisible = Visibility.Collapsed;
            entity.isViewFieldsVisible = Visibility.Collapsed;
            entity.isEditingFieldsVisible = Visibility.Collapsed;
            entity.isAddFieldsVisible = Visibility.Visible;
            entity.isAddButtonsVisible = Visibility.Visible;
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
            int selectedProductIndex = products.FindIndex(product => product.id.ToString() == button.Uid);
            products[selectedProductIndex].isViewButtonsVisible = Visibility.Collapsed;
            products[selectedProductIndex].isEditingButtonsVisible = Visibility.Visible;
            products[selectedProductIndex].isViewFieldsVisible = Visibility.Collapsed;
            products[selectedProductIndex].isEditingFieldsVisible = Visibility.Visible;
            productsView.Items.Refresh();
        }

        private async void DiscardButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int selectedProductIndex = products.FindIndex(product => product.id.ToString() == button.Uid);
            products[selectedProductIndex].isViewButtonsVisible = Visibility.Visible;
            products[selectedProductIndex].isEditingButtonsVisible = Visibility.Collapsed;
            products[selectedProductIndex].isViewFieldsVisible = Visibility.Visible;
            products[selectedProductIndex].isEditingFieldsVisible = Visibility.Collapsed;
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
            products[selectedProductIndex].isViewButtonsVisible = Visibility.Visible;
            products[selectedProductIndex].isEditingButtonsVisible = Visibility.Collapsed;
            products[selectedProductIndex].isViewFieldsVisible = Visibility.Visible;
            products[selectedProductIndex].isEditingFieldsVisible = Visibility.Collapsed;
            productsView.Items.Refresh();
        }

        private async void AddProductButtonClicked(Object sender, RoutedEventArgs e)
        {
            addProductButton.Visibility = Visibility.Collapsed;
            products.Add(new Product(0, "", "", 0,0, false, 0));
            productsView.Items.Refresh();
            AddAnonymousObject(products[products.Count - 1]);
            productsView.Items.Refresh();
        }
        

        private async void AcceptAddButton_Click(Object sender, RoutedEventArgs e)
        {
            addProductButton.Visibility = Visibility.Visible;
            Button button = sender as Button;
            int selectedProductIndex = products.FindIndex(product => product.id.ToString() == button.Uid);           
            ProductDAO productDAO = new ProductDAO();
            await productDAO.executePostQuery(products[selectedProductIndex]);
            products = Task.Run(productDAO.executeGetQuery).Result;            
            productsView.ItemsSource = products;
            productsView.Items.Refresh();
        }

        private async void DiscardAddButton_Click(Object sender, RoutedEventArgs e)
        {
            addProductButton.Visibility = Visibility.Visible;
            products.Remove(products[products.Count - 1]);
            productsView.Items.Refresh();
        }

        private void archivedProducts_Click(object sender, RoutedEventArgs e)
        {
            suppliersView.Visibility = Visibility.Collapsed;
            productsView.Visibility = Visibility.Visible;
            addProductButton.Visibility = Visibility.Collapsed;
            addSupplierButton.Visibility = Visibility.Collapsed;
            addCouponButton.Visibility = Visibility.Collapsed;
            ProductDAO productDAO = new ProductDAO();
            products = Task.Run(productDAO.executeArchivedGetQuery).Result;
            foreach(Product product in products)
            {
                product.isViewButtonsVisible = Visibility.Collapsed;
            }
            productsView.ItemsSource = products;
        }

        private async void suppliers_Click(object sender, RoutedEventArgs e)
        {
            addProductButton.Visibility = Visibility.Collapsed;
            addSupplierButton.Visibility = Visibility.Visible;
            addCouponButton.Visibility = Visibility.Collapsed;
            suppliersView.Visibility = Visibility.Visible;
            productsView.Visibility = Visibility.Collapsed;
            SupplierDAO supplierDAO = new SupplierDAO();
            suppliers = Task.Run(supplierDAO.executeGetQuery).Result;            
            suppliersView.ItemsSource = suppliers;
        }

        private void archivedSuppliers_Click(object sender, RoutedEventArgs e)
        {
            addProductButton.Visibility = Visibility.Collapsed;
            addSupplierButton.Visibility = Visibility.Collapsed;
            addCouponButton.Visibility = Visibility.Collapsed;
            suppliersView.Visibility = Visibility.Visible;
            productsView.Visibility = Visibility.Collapsed;
            SupplierDAO supplierDAO = new SupplierDAO();
            suppliers = Task.Run(supplierDAO.executeArchivedGetQuery).Result;
            foreach (Supplier supplier in suppliers)
            {
                supplier.isViewButtonsVisible = Visibility.Collapsed;
            }
            suppliersView.ItemsSource = suppliers;
        }

        private async void DeleteSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;            
            SupplierDAO supplierDAO = new SupplierDAO();
            if (await supplierDAO.executeDeleteQuery(Int32.Parse(button.Uid)))
            {
                suppliers.Remove(suppliers.Find(supplier=> supplier.id.ToString() == button.Uid));
            }                
            suppliersView.Items.Refresh();
        }

        private async void EditSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int selectedSupplierIndex = suppliers.FindIndex(supplier => supplier.id.ToString() == button.Uid);
            suppliers[selectedSupplierIndex].isViewButtonsVisible = Visibility.Collapsed;
            suppliers[selectedSupplierIndex].isEditingButtonsVisible = Visibility.Visible;
            suppliers[selectedSupplierIndex].isViewFieldsVisible = Visibility.Collapsed;
            suppliers[selectedSupplierIndex].isEditingFieldsVisible = Visibility.Visible;
            suppliersView.Items.Refresh();
        }

        private async void AcceptSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            SupplierDAO supplierDAO = new SupplierDAO();
            if (await supplierDAO.executePutQuery(Int32.Parse(((Button)sender).Uid), suppliers[suppliers
                .FindIndex(supplier =>
                    supplier.id.ToString() == ((Button)sender).Uid
                )]))
            {
            }
            suppliers.Clear();
            suppliers = Task.Run(supplierDAO.executeGetQuery).Result;
            suppliersView.ItemsSource = suppliers;
            Button button = sender as Button;
            int selectedSupplierIndex = suppliers.FindIndex(supplier => supplier.id.ToString() == button.Uid);
            suppliers[selectedSupplierIndex].isViewButtonsVisible = Visibility.Visible;
            suppliers[selectedSupplierIndex].isEditingButtonsVisible = Visibility.Collapsed;
            suppliers[selectedSupplierIndex].isViewFieldsVisible = Visibility.Visible;
            suppliers[selectedSupplierIndex].isEditingFieldsVisible = Visibility.Collapsed;
            suppliersView.Items.Refresh();
        }

        private async void DiscardSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int selectedSupplierIndex = suppliers.FindIndex(product => product.id.ToString() == button.Uid);
            suppliers[selectedSupplierIndex].isViewButtonsVisible = Visibility.Visible;
            suppliers[selectedSupplierIndex].isEditingButtonsVisible = Visibility.Collapsed;
            suppliers[selectedSupplierIndex].isViewFieldsVisible = Visibility.Visible;
            suppliers[selectedSupplierIndex].isEditingFieldsVisible = Visibility.Collapsed;
            suppliersView.Items.Refresh();
            suppliers.Clear();
            SupplierDAO supplierDAO = new SupplierDAO();
            suppliers = Task.Run(supplierDAO.executeGetQuery).Result;
            suppliersView.ItemsSource = suppliers;
        }

        private async void AcceptSupplierAddButton_Click(object sender, RoutedEventArgs e)
        {
            addSupplierButton.Visibility = Visibility.Visible;
            Button button = sender as Button;
            int selectedSupplierIndex = suppliers.FindIndex(supplier => supplier.id.ToString() == button.Uid);
            SupplierDAO supplierDAO = new SupplierDAO();
            await supplierDAO.executePostQuery(suppliers[selectedSupplierIndex]);
            suppliers = Task.Run(supplierDAO.executeGetQuery).Result;
            suppliersView.ItemsSource = suppliers;           
        }

        private async void DiscardSupplierAddButton_Click(object sender, RoutedEventArgs e)
        {
            addSupplierButton.Visibility = Visibility.Visible;
            suppliers.Remove(suppliers[suppliers.Count - 1]);
            suppliersView.Items.Refresh();
        }

        private async void AddSupplierButtonClicked(object sender, RoutedEventArgs e)
        {
            addSupplierButton.Visibility = Visibility.Collapsed;
            suppliers.Add(new Supplier(0, "", "", false));
            suppliersView.Items.Refresh();
            AddAnonymousObject(suppliers[suppliers.Count-1]);            
            suppliersView.Items.Refresh();
        }

        private void addCouponButton_Click(object sender, RoutedEventArgs e)
        {
            addCouponButton.Visibility = Visibility.Collapsed;
            coupons.Add(new Coupon(0, "", 0.0, 0, false));
            couponsView.Items.Refresh();
            AddAnonymousObject(coupons[coupons.Count - 1]);
            couponsView.Items.Refresh();
        }

        private async void DiscardCouponAddButton_Click(object sender, RoutedEventArgs e)
        {
            addCouponButton.Visibility = Visibility.Visible;
            coupons.Remove(coupons[coupons.Count - 1]);
            couponsView.Items.Refresh();
        }

        private async void AcceptCouponAddButton_Click(object sender, RoutedEventArgs e)
        {           
            addCouponButton.Visibility = Visibility.Visible;
            Button button = sender as Button;
            int selectedCouponIndex = coupons.FindIndex(coupon => coupon.id.ToString() == button.Uid);
            CouponDAO couponDAO = new CouponDAO();
            await couponDAO.executePostQuery(coupons[selectedCouponIndex]);
            coupons = Task.Run(couponDAO.executeGetQuery).Result;
            couponsView.ItemsSource = coupons;
        }

        private async void DiscardCouponButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int selectedCouponIndex = coupons.FindIndex(coupon => coupon.id.ToString() == button.Uid);
            coupons[selectedCouponIndex].isViewButtonsVisible = Visibility.Visible;
            coupons[selectedCouponIndex].isEditingButtonsVisible = Visibility.Collapsed;
            coupons[selectedCouponIndex].isViewFieldsVisible = Visibility.Visible;
            coupons[selectedCouponIndex].isEditingFieldsVisible = Visibility.Collapsed;
            couponsView.Items.Refresh();
            coupons.Clear();
            CouponDAO couponDAO = new CouponDAO();
            coupons = Task.Run(couponDAO.executeGetQuery).Result;
            couponsView.ItemsSource = coupons;
        }

        private async void AcceptCouponButton_Click(object sender, RoutedEventArgs e)
        {
            CouponDAO couponDAO = new CouponDAO();
            if (await couponDAO.executePutQuery(Int32.Parse(((Button)sender).Uid), coupons[coupons
                .FindIndex(coupon =>
                    coupon.id.ToString() == ((Button)sender).Uid
                )]))
            {
            }
            coupons.Clear();
            coupons = Task.Run(couponDAO.executeGetQuery).Result;
            couponsView.ItemsSource = coupons;
            Button button = sender as Button;
            int selectedCouponIndex = coupons.FindIndex(coupon => coupon.id.ToString() == button.Uid);
            coupons[selectedCouponIndex].isViewButtonsVisible = Visibility.Visible;
            coupons[selectedCouponIndex].isEditingButtonsVisible = Visibility.Collapsed;
            coupons[selectedCouponIndex].isViewFieldsVisible = Visibility.Visible;
            coupons[selectedCouponIndex].isEditingFieldsVisible = Visibility.Collapsed;
            couponsView.Items.Refresh();
        }

        private async void DeleteCouponButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            CouponDAO couponDAO = new CouponDAO();
            if (await couponDAO.executeDeleteQuery(Int32.Parse(button.Uid)))
            {
                coupons.Remove(coupons.Find(coupon => coupon.id.ToString() == button.Uid));
            }
            couponsView.Items.Refresh();
        }

        private async void EditCouponButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int selectedCouponIndex = coupons.FindIndex(coupon => coupon.id.ToString() == button.Uid);
            coupons[selectedCouponIndex].isViewButtonsVisible = Visibility.Collapsed;
            coupons[selectedCouponIndex].isEditingButtonsVisible = Visibility.Visible;
            coupons[selectedCouponIndex].isViewFieldsVisible = Visibility.Collapsed;
            coupons[selectedCouponIndex].isEditingFieldsVisible = Visibility.Visible;
            couponsView.Items.Refresh();
        }

        private void archivedCouponsBtn_Click(object sender, RoutedEventArgs e)
        {
            addProductButton.Visibility = Visibility.Collapsed;
            addSupplierButton.Visibility = Visibility.Collapsed;
            addCouponButton.Visibility = Visibility.Collapsed;
            suppliersView.Visibility = Visibility.Collapsed;
            productsView.Visibility = Visibility.Collapsed;
            couponsView.Visibility = Visibility.Visible;
            CouponDAO couponDAO = new CouponDAO();
            coupons = Task.Run(couponDAO.executeArchivedGetQuery).Result;
            foreach (Coupon coupon in coupons)
            {
                coupon.isViewButtonsVisible = Visibility.Collapsed;
            }
            couponsView.ItemsSource = coupons;
        }

        private void couponsBtn_Click(object sender, RoutedEventArgs e)
        {
            addProductButton.Visibility = Visibility.Collapsed;
            addSupplierButton.Visibility = Visibility.Collapsed;
            addCouponButton.Visibility = Visibility.Visible;
            suppliersView.Visibility = Visibility.Collapsed;
            productsView.Visibility = Visibility.Collapsed;
            couponsView.Visibility = Visibility.Visible;
            CouponDAO couponDAO = new CouponDAO();
            coupons = Task.Run(couponDAO.executeGetQuery).Result;
            couponsView.ItemsSource = coupons;
        }
    }
}
