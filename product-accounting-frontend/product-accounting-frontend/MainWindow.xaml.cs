using System;
using System.Collections.Generic;
using System.Windows;
using product_accounting_frontend.dao;
using System.Threading.Tasks;
using System.Windows.Controls;
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
        public List<Delivery> deliveries;
        public List<Purchase> purchases;
        public List<Transaction> transactions;
        public MainWindow()
        {            
            InitializeComponent();            
        }             

        private async void products_Click(object sender, RoutedEventArgs e)
        {
            addProductButton.Visibility = Visibility.Visible;
            addSupplierButton.Visibility = Visibility.Collapsed;
            addCouponButton.Visibility = Visibility.Collapsed;
            addDeliveryButton.Visibility = Visibility.Collapsed;
            addPurchaseButton.Visibility = Visibility.Collapsed;
            suppliersView.Visibility = Visibility.Collapsed;
            productsView.Visibility = Visibility.Visible;
            couponsView.Visibility = Visibility.Collapsed;
            purchasesView.Visibility = Visibility.Collapsed;
            deliveriesView.Visibility = Visibility.Collapsed;
            transactionsView.Visibility = Visibility.Collapsed;
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
            addProductButton.Visibility = Visibility.Collapsed;
            addSupplierButton.Visibility = Visibility.Collapsed;
            addCouponButton.Visibility = Visibility.Collapsed;
            addDeliveryButton.Visibility = Visibility.Collapsed;
            addPurchaseButton.Visibility = Visibility.Collapsed;
            suppliersView.Visibility = Visibility.Collapsed;
            productsView.Visibility = Visibility.Visible;
            couponsView.Visibility = Visibility.Collapsed;
            purchasesView.Visibility = Visibility.Collapsed;
            deliveriesView.Visibility = Visibility.Collapsed;
            transactionsView.Visibility = Visibility.Collapsed;
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
            addDeliveryButton.Visibility = Visibility.Collapsed;
            addPurchaseButton.Visibility = Visibility.Collapsed;
            suppliersView.Visibility = Visibility.Visible;
            productsView.Visibility = Visibility.Collapsed;
            couponsView.Visibility = Visibility.Collapsed;
            purchasesView.Visibility = Visibility.Collapsed;
            deliveriesView.Visibility = Visibility.Collapsed;
            transactionsView.Visibility = Visibility.Collapsed;
            SupplierDAO supplierDAO = new SupplierDAO();
            suppliers = Task.Run(supplierDAO.executeGetQuery).Result;            
            suppliersView.ItemsSource = suppliers;
        }

        private void archivedSuppliers_Click(object sender, RoutedEventArgs e)
        {
            addProductButton.Visibility = Visibility.Collapsed;
            addSupplierButton.Visibility = Visibility.Collapsed;
            addCouponButton.Visibility = Visibility.Collapsed;
            addDeliveryButton.Visibility = Visibility.Collapsed;
            addPurchaseButton.Visibility = Visibility.Collapsed;
            suppliersView.Visibility = Visibility.Visible;
            productsView.Visibility = Visibility.Collapsed;
            couponsView.Visibility = Visibility.Collapsed;
            purchasesView.Visibility = Visibility.Collapsed;
            deliveriesView.Visibility = Visibility.Collapsed;
            transactionsView.Visibility = Visibility.Collapsed;
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
            addDeliveryButton.Visibility = Visibility.Collapsed;
            addPurchaseButton.Visibility = Visibility.Collapsed;
            suppliersView.Visibility = Visibility.Collapsed;
            productsView.Visibility = Visibility.Collapsed;
            couponsView.Visibility = Visibility.Visible;
            purchasesView.Visibility = Visibility.Collapsed;
            deliveriesView.Visibility = Visibility.Collapsed;
            transactionsView.Visibility = Visibility.Collapsed;
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
            addDeliveryButton.Visibility = Visibility.Collapsed;
            addPurchaseButton.Visibility = Visibility.Collapsed;
            suppliersView.Visibility = Visibility.Collapsed;
            productsView.Visibility = Visibility.Collapsed;
            couponsView.Visibility = Visibility.Visible;
            purchasesView.Visibility = Visibility.Collapsed;
            deliveriesView.Visibility = Visibility.Collapsed;
            transactionsView.Visibility = Visibility.Collapsed;
            CouponDAO couponDAO = new CouponDAO();
            coupons = Task.Run(couponDAO.executeGetQuery).Result;
            couponsView.ItemsSource = coupons;
        }

        private async void deliveriesBtn_Click(object sender, RoutedEventArgs e)
        {
            addProductButton.Visibility = Visibility.Collapsed;
            addSupplierButton.Visibility = Visibility.Collapsed;
            addCouponButton.Visibility = Visibility.Collapsed;
            addDeliveryButton.Visibility = Visibility.Visible;
            addPurchaseButton.Visibility = Visibility.Collapsed;
            suppliersView.Visibility = Visibility.Collapsed;
            productsView.Visibility = Visibility.Collapsed;
            couponsView.Visibility = Visibility.Collapsed;
            purchasesView.Visibility = Visibility.Collapsed;
            deliveriesView.Visibility = Visibility.Visible;
            transactionsView.Visibility = Visibility.Collapsed;
            DeliveryDAO deliveryDAO = new DeliveryDAO();
            deliveries = Task.Run(deliveryDAO.executeGetQuery).Result;
            foreach(Delivery delivery in deliveries)
            {
               foreach(DeliveryProduct deliveryProduct in delivery.products)
                {
                    deliveryProduct.parentId = delivery.id;
                } 
            }
            deliveriesView.ItemsSource = deliveries;
        }

        private async void DeleteDeliveryButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            DeliveryDAO deliveryDAO = new DeliveryDAO();
            if (await deliveryDAO.executeDeleteQuery(Int32.Parse(button.Uid)))
            {
                deliveries.Remove(deliveries.Find(delivery => delivery.id.ToString() == button.Uid));
            }
            deliveriesView.Items.Refresh();
        }

        private async void EditDeliveryButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Delivery delivery = deliveries.Find(delivery => delivery.id.ToString() == button.Uid);
            delivery.isAddButtonsVisible = Visibility.Collapsed;
            delivery.isEditingButtonsVisible = Visibility.Visible;
            delivery.isViewButtonsVisible = Visibility.Collapsed;
            deliveriesView.Items.Refresh();
        }

        private async void EditDeliverySupplierButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Delivery delivery = deliveries.Find(delivery => delivery.id.ToString() == button.Uid);
            delivery.isEditConfirmationSuppliersButtonsVisible = Visibility.Visible;
            delivery.isViewSuppliersFieldsVisible = Visibility.Collapsed;
            delivery.isEditSuppliersButtonsVisible = Visibility.Collapsed;
            delivery.isAddSuppliersFieldsVisible = Visibility.Visible;
            deliveriesView.Items.Refresh();
        }

        private async void ReplaceSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            DeliveryDAO deliveryDAO = new DeliveryDAO();
            Button button = sender as Button;
            Delivery delivery = deliveries.Find(delivery => delivery.id.ToString() == button.Uid);
            await deliveryDAO.executePutQuery(delivery.id, delivery);
            deliveries.Clear();
            deliveries = Task.Run(deliveryDAO.executeGetQuery).Result;
            deliveriesView.ItemsSource = deliveries;
        }

        private async void DiscardNewSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Delivery delivery = deliveries.Find(delivery => delivery.id.ToString() == button.Uid);
            delivery.isEditConfirmationSuppliersButtonsVisible = Visibility.Collapsed;
            delivery.isViewSuppliersFieldsVisible = Visibility.Visible;
            delivery.isEditSuppliersButtonsVisible = Visibility.Visible;
            delivery.isAddSuppliersFieldsVisible = Visibility.Collapsed;
            deliveriesView.Items.Refresh();
        }


        private async void AcceptDeliveryButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Delivery delivery = deliveries.Find(delivery => delivery.id.ToString() == button.Uid);
            DeliveryDAO deliveryDAO = new DeliveryDAO();
            await deliveryDAO.executePutQuery(delivery.id, delivery);
            deliveries.Clear();
            deliveries = Task.Run(deliveryDAO.executeGetQuery).Result;
            deliveriesView.ItemsSource = deliveries;
        }

        private async void DiscardDeliveryButton_Click(object sender, RoutedEventArgs e)
        {
            DeliveryDAO deliveryDAO = new DeliveryDAO();
            deliveries.Clear();
            deliveries = Task.Run(deliveryDAO.executeGetQuery).Result;
            deliveriesView.ItemsSource = deliveries;
        }

        private async void AcceptDeliveryAddButton_Click(object sender, RoutedEventArgs e)
        {
            addDeliveryButton.Visibility = Visibility.Visible;
            Button button = sender as Button;
            int selectedDeliveryIndex = deliveries.FindIndex(delivery => delivery.id.ToString() == button.Uid);
            DeliveryDAO deliveryDAO = new DeliveryDAO();
            await deliveryDAO.executePostQuery(deliveries[selectedDeliveryIndex]);
            deliveries = Task.Run(deliveryDAO.executeGetQuery).Result;
            deliveriesView.ItemsSource = deliveries;
        }

        private async void DiscardDeliveryAddButton_Click(object sender, RoutedEventArgs e)
        {
            addDeliveryButton.Visibility = Visibility.Visible;
            deliveries.Remove(deliveries[deliveries.Count - 1]);
            deliveriesView.Items.Refresh();
        }

        private void addDeliveryButton_Click(object sender, RoutedEventArgs e)
        {
            addDeliveryButton.Visibility = Visibility.Collapsed;
            deliveries.Add(new Delivery(0, "", 0.0, new Supplier(0, "", "", false), null, 0.0, false));
            deliveries[deliveries.Count - 1].isAddSuppliersButtonsVisible = Visibility.Visible;
            deliveries[deliveries.Count - 1].isViewSuppliersFieldsVisible = Visibility.Collapsed;
            deliveries[deliveries.Count - 1].isAddSuppliersFieldsVisible = Visibility.Visible;
            deliveries[deliveries.Count - 1].isEditSuppliersButtonsVisible = Visibility.Collapsed;
            deliveries[deliveries.Count - 1].addProductButtonVisibility = Visibility.Collapsed;
            deliveriesView.Items.Refresh();
            AddAnonymousObject(deliveries[deliveries.Count - 1]);
            deliveriesView.Items.Refresh();
        }

        private void addProductToDelivery_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Delivery delivery = deliveries.Find(delivery => delivery.id.ToString() == button.Uid);
            delivery.products.Add(new DeliveryProduct(new Product(0, "", "", 0, 0, false, 0), 0 , 0));
            delivery.products[delivery.products.Count - 1].parentId = delivery.id;
            delivery.products[delivery.products.Count - 1].productInfoVisibility = Visibility.Collapsed;
            delivery.products[delivery.products.Count - 1].productAddByIdVisibility = Visibility.Visible;
            delivery.products[delivery.products.Count - 1].confirmProductVisibility = Visibility.Visible;
            delivery.products[delivery.products.Count - 1].discardProductVisibility = Visibility.Visible;
            delivery.products[delivery.products.Count - 1].removeProductVisibility = Visibility.Collapsed;
            delivery.addProductButtonVisibility = Visibility.Visible;
            
            deliveriesView.Items.Refresh();
        }

        private async void removeProductFromDeliveryBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int productId = Int32.Parse(button.Uid);
            StackPanel stackPanel = button.Parent as StackPanel;
            int deliveryId = Int32.Parse(stackPanel.Uid);
            Delivery delivery = deliveries.Find(delivery => delivery.id.ToString() == stackPanel.Uid);
            delivery.products.Remove(delivery.products.Find(product => product.product.id.ToString() == button.Uid));
            DeliveryDAO deliveryDAO = new DeliveryDAO();
            await deliveryDAO.executeDeleteProductQuery(deliveryId, productId);
            deliveries.Clear();
            deliveries = Task.Run(deliveryDAO.executeGetQuery).Result;
            foreach (Delivery deliveri in deliveries)
            {
                foreach (DeliveryProduct deliveryProduct in deliveri.products)
                {
                    deliveryProduct.parentId = deliveri.id;
                }
            }
            deliveriesView.ItemsSource = deliveries;
        }

        private async void confirmProductFromDeliveryBtn_Click(object sender, RoutedEventArgs e)
        {
            deliveriesView.Items.Refresh();
            Button button = sender as Button;
            Delivery delivery = deliveries.Find(delivery => delivery.id.ToString() == button.Uid);
            delivery.products[delivery.products.Count - 1].productInfoVisibility = Visibility.Visible;
            delivery.products[delivery.products.Count - 1].productAddByIdVisibility = Visibility.Collapsed;
            delivery.products[delivery.products.Count - 1].confirmProductVisibility = Visibility.Collapsed;
            delivery.products[delivery.products.Count - 1].discardProductVisibility = Visibility.Collapsed;
            delivery.products[delivery.products.Count - 1].removeProductVisibility = Visibility.Visible;
            delivery.addProductButtonVisibility = Visibility.Visible;
            DeliveryDAO deliveryDAO = new DeliveryDAO();
            await deliveryDAO.executePostProductQuery(delivery.id, delivery);
            deliveries = Task.Run(deliveryDAO.executeGetQuery).Result;
            deliveriesView.ItemsSource = deliveries;
        }

        private void discardProductFromDeliveryBtn_Click(object sender, RoutedEventArgs e)
        {
            deliveriesView.Items.Refresh();
            Button button = sender as Button;
            Delivery delivery = deliveries.Find(delivery => delivery.id.ToString() == button.Uid);
            delivery.products.RemoveAt(delivery.products.Count - 1);
            delivery.addProductButtonVisibility = Visibility.Visible;
            deliveriesView.Items.Refresh();
        }

        private async void commitDeliveryDelivery_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            DeliveryDAO deliveryDAO = new DeliveryDAO();
            await deliveryDAO.executePutCommit(Int32.Parse(button.Uid));
            deliveries = Task.Run(deliveryDAO.executeGetQuery).Result;
            deliveriesView.ItemsSource = deliveries;
        }

        private void committedDeliveriesBtn_Click(object sender, RoutedEventArgs e)
        {
            addProductButton.Visibility = Visibility.Collapsed;
            addSupplierButton.Visibility = Visibility.Collapsed;
            addCouponButton.Visibility = Visibility.Collapsed;
            addDeliveryButton.Visibility = Visibility.Collapsed;
            addPurchaseButton.Visibility = Visibility.Collapsed;
            suppliersView.Visibility = Visibility.Collapsed;
            productsView.Visibility = Visibility.Collapsed;
            couponsView.Visibility = Visibility.Collapsed;
            purchasesView.Visibility = Visibility.Collapsed;
            deliveriesView.Visibility = Visibility.Visible;
            transactionsView.Visibility = Visibility.Collapsed;
            DeliveryDAO deliveryDAO = new DeliveryDAO();
            deliveries = Task.Run(deliveryDAO.executeCommitedGetQuery).Result;
            foreach (Delivery delivery in deliveries)
            {
                delivery.isAddButtonsVisible = Visibility.Collapsed;
                delivery.isEditingButtonsVisible = Visibility.Collapsed;
                delivery.isViewButtonsVisible = Visibility.Collapsed;
                delivery.isAddSuppliersButtonsVisible = Visibility.Collapsed;
                delivery.addProductButtonVisibility = Visibility.Collapsed;
                delivery.isEditSuppliersButtonsVisible= Visibility.Collapsed; 
                foreach (DeliveryProduct deliveryProduct in delivery.products)
                {
                    deliveryProduct.productAddByIdVisibility = Visibility.Collapsed;
                    deliveryProduct.removeProductVisibility = Visibility.Collapsed;
                }
            }
            deliveriesView.ItemsSource = deliveries;
        }

        private async void purchaseBtn_Click(object sender, RoutedEventArgs e)
        {
            addProductButton.Visibility = Visibility.Collapsed;
            addSupplierButton.Visibility = Visibility.Collapsed;
            addCouponButton.Visibility = Visibility.Collapsed;
            addDeliveryButton.Visibility = Visibility.Collapsed;
            addPurchaseButton.Visibility = Visibility.Visible;
            suppliersView.Visibility = Visibility.Collapsed;
            productsView.Visibility = Visibility.Collapsed;
            couponsView.Visibility = Visibility.Collapsed;
            purchasesView.Visibility = Visibility.Visible;
            deliveriesView.Visibility = Visibility.Collapsed;
            transactionsView.Visibility = Visibility.Collapsed;
            PurchaseDAO purchaseDAO = new PurchaseDAO();
            purchases = await purchaseDAO.executeGetQuery();
            foreach (Purchase purchase in purchases)
            {
                foreach (DeliveryProduct deliveryProduct in purchase.products)
                {
                    deliveryProduct.parentId = purchase.id;
                }
            }
            purchasesView.ItemsSource = purchases;
        }

        private async void addPurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            addPurchaseButton.Visibility = Visibility.Collapsed;
            purchases.Add(new Purchase(0,"", null, new Coupon(0, "", 0,0,false), false));
            purchases[purchases.Count - 1].addProductButtonVisibility = Visibility.Collapsed;
            purchasesView.Items.Refresh();
            AddAnonymousObject(purchases[purchases.Count - 1]);
            purchasesView.Items.Refresh();
        }

        private async void DeletePurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            PurchaseDAO purchaseDAO = new PurchaseDAO();
            if (await purchaseDAO.executeDeleteQuery(Int32.Parse(button.Uid)))
            {
                deliveries.Remove(deliveries.Find(delivery => delivery.id.ToString() == button.Uid));
            }
            deliveriesView.Items.Refresh();
        }

        private async void EditPurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Purchase purchase = purchases.Find(purchase => purchase.id.ToString() == button.Uid);
            purchase.isEditingButtonsVisible = Visibility.Visible;
            purchase.isViewButtonsVisible = Visibility.Collapsed;
            purchase.isEditingFieldsVisible = Visibility.Visible;
            purchase.isViewFieldsVisible = Visibility.Collapsed;
            purchasesView.Items.Refresh();
        }

        private async void AcceptPurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Purchase purchase = purchases.Find(purchase => purchase.id.ToString() == button.Uid);
            
            purchasesView.Items.Refresh();
      
            PurchaseDAO purchaseDAO = new PurchaseDAO();
            await purchaseDAO.executePutQuery(purchase.id, purchase);
            purchases.Clear();
            purchases = Task.Run(purchaseDAO.executeGetQuery).Result;
            purchasesView.ItemsSource = purchases;
        }

        private async void DiscardPurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            PurchaseDAO purchaseDAO = new PurchaseDAO();
            purchases.Clear();
            purchases = Task.Run(purchaseDAO.executeGetQuery).Result;
            purchasesView.ItemsSource = purchases;
        }

        private async void AcceptAddPurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            addPurchaseButton.Visibility = Visibility.Visible;
            Button button = sender as Button;
            int selectedPurchaseIndex = purchases.FindIndex(purchase => purchase.id.ToString() == button.Uid);
            PurchaseDAO purchaseDAO = new PurchaseDAO();
            await purchaseDAO.executePostQuery(purchases[selectedPurchaseIndex]);
            purchases = Task.Run(purchaseDAO.executeGetQuery).Result;
            purchasesView.ItemsSource = purchases;
        }

        private async void DiscardAddPurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            addPurchaseButton.Visibility = Visibility.Visible;
            purchases.Remove(purchases[purchases.Count - 1]);
            purchasesView.Items.Refresh();
        }

        private async void confirmProductFromPurchaseBtn_Click(object sender, RoutedEventArgs e)
        {
            purchasesView.Items.Refresh();
            Button button = sender as Button;
            Purchase purchase = purchases.Find(purchase => purchase.id.ToString() == button.Uid);
            purchase.products[purchase.products.Count - 1].productInfoVisibility = Visibility.Visible;
            purchase.products[purchase.products.Count - 1].productAddByIdVisibility = Visibility.Collapsed;
            purchase.products[purchase.products.Count - 1].confirmProductVisibility = Visibility.Collapsed;
            purchase.products[purchase.products.Count - 1].discardProductVisibility = Visibility.Collapsed;
            purchase.products[purchase.products.Count - 1].removeProductVisibility = Visibility.Visible;
            purchase.addProductButtonVisibility = Visibility.Visible;
            PurchaseDAO purchaseDAO = new PurchaseDAO();
            await purchaseDAO.executePostProductQuery(purchase.id, purchase);
            purchases = Task.Run(purchaseDAO.executeGetQuery).Result;
            purchasesView.ItemsSource = purchases;
        }

        private async void discardProductFromPurchaseBtn_Click(object sender, RoutedEventArgs e)
        {
            deliveriesView.Items.Refresh();
            Button button = sender as Button;
            Purchase purchase = purchases.Find(purchase => purchase.id.ToString() == button.Uid);
            purchase.products.RemoveAt(purchase.products.Count - 1);
            purchase.addProductButtonVisibility = Visibility.Visible;
            purchasesView.Items.Refresh();
        }

        private async void removeProductFromPurchaseBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int productId = Int32.Parse(button.Uid);
            StackPanel stackPanel = button.Parent as StackPanel;
            int purchaseId = Int32.Parse(stackPanel.Uid);
            Purchase purchase = purchases.Find(purchase => purchase.id.ToString() == stackPanel.Uid);
            purchase.products.Remove(purchase.products.Find(product => product.product.id.ToString() == button.Uid));
            PurchaseDAO purchaseDAO = new PurchaseDAO();
            await purchaseDAO.executeDeleteProductQuery(purchaseId, productId);
            purchases.Clear();
            purchases = Task.Run(purchaseDAO.executeGetQuery).Result;
            foreach (Purchase purchas in purchases)
            {
                foreach (DeliveryProduct deliveryProduct in purchas.products)
                {
                    deliveryProduct.parentId = purchas.id;
                }
            }
            purchasesView.ItemsSource = purchases;
        }

        private async void addProductToPurchase_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Purchase purchase = purchases.Find(purchase => purchase.id.ToString() == button.Uid);
            purchase.products.Add(new DeliveryProduct(new Product(0, "", "", 0, 0, false, 0), 0, 0));
            purchase.products[purchase.products.Count - 1].parentId = purchase.id;
            purchase.products[purchase.products.Count - 1].productInfoVisibility = Visibility.Collapsed;
            purchase.products[purchase.products.Count - 1].productAddByIdVisibility = Visibility.Visible;
            purchase.products[purchase.products.Count - 1].confirmProductVisibility = Visibility.Visible;
            purchase.products[purchase.products.Count - 1].discardProductVisibility = Visibility.Visible;
            purchase.products[purchase.products.Count - 1].removeProductVisibility = Visibility.Collapsed;
            purchase.addProductButtonVisibility = Visibility.Visible;

            purchasesView.Items.Refresh();
        }

        private async void commitDeliveryPurchase_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            PurchaseDAO purchaseDAO = new PurchaseDAO();
            await purchaseDAO.executePutCommit(Int32.Parse(button.Uid));
            purchases = Task.Run(purchaseDAO.executeGetQuery).Result;
            purchasesView.ItemsSource = purchases;
        }

        private void commitedPurchaseBtn_Click(object sender, RoutedEventArgs e)
        {
            addProductButton.Visibility = Visibility.Collapsed;
            addSupplierButton.Visibility = Visibility.Collapsed;
            addCouponButton.Visibility = Visibility.Collapsed;
            addDeliveryButton.Visibility = Visibility.Collapsed;
            addPurchaseButton.Visibility = Visibility.Collapsed;
            suppliersView.Visibility = Visibility.Collapsed;
            productsView.Visibility = Visibility.Collapsed;
            couponsView.Visibility = Visibility.Collapsed;
            purchasesView.Visibility = Visibility.Visible;
            deliveriesView.Visibility = Visibility.Collapsed;
            transactionsView.Visibility = Visibility.Collapsed;
            PurchaseDAO purchaseDAO = new PurchaseDAO();
            purchases = Task.Run(purchaseDAO.executeCommitedGetQuery).Result;
            foreach (Purchase purchase in purchases)
            {
                purchase.isAddButtonsVisible = Visibility.Collapsed;
                purchase.isEditingButtonsVisible = Visibility.Collapsed;
                purchase.isViewButtonsVisible = Visibility.Collapsed;
                purchase.addProductButtonVisibility = Visibility.Collapsed;
                foreach (DeliveryProduct deliveryProduct in purchase.products)
                {
                    deliveryProduct.productAddByIdVisibility = Visibility.Collapsed;
                    deliveryProduct.removeProductVisibility = Visibility.Collapsed;
                }
            }
            purchasesView.ItemsSource = purchases;
        }

        private async void transactionsBtn_Click(object sender, RoutedEventArgs e)
        {
            addProductButton.Visibility = Visibility.Collapsed;
            addSupplierButton.Visibility = Visibility.Collapsed;
            addCouponButton.Visibility = Visibility.Collapsed;
            addDeliveryButton.Visibility = Visibility.Collapsed;
            addPurchaseButton.Visibility = Visibility.Collapsed;
            suppliersView.Visibility = Visibility.Collapsed;
            productsView.Visibility = Visibility.Collapsed;
            couponsView.Visibility = Visibility.Collapsed;
            purchasesView.Visibility = Visibility.Collapsed;
            deliveriesView.Visibility = Visibility.Collapsed;
            transactionsView.Visibility = Visibility.Visible;
            TransactionsDAO transactionDAO = new TransactionsDAO();
            transactions = await transactionDAO.executeGetTransaction();
            transactionsView.ItemsSource = transactions;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = (DatePicker)sender;
            string day;
            string month;
            if(datePicker.SelectedDate.Value.Day.ToString().Length == 1)
            {
                day = "0" + datePicker.SelectedDate.Value.Day.ToString();
            }
            else
            {
                day = datePicker.SelectedDate.Value.Day.ToString();
            }

            if (datePicker.SelectedDate.Value.Month.ToString().Length == 1)
            {
                month = "0" + datePicker.SelectedDate.Value.Month.ToString();
            }
            else
            {
                month = datePicker.SelectedDate.Value.Month.ToString();
            }

            string date = datePicker.SelectedDate.Value.Year.ToString() + "-" 
                + month + "-"
                + day;
            Delivery delivery = deliveries.Find(delivery => delivery.id.ToString() == datePicker.Uid);
            delivery.date = date;
            datePicker.Text = date;
        }

        private void DatePickerPurchase_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = (DatePicker)sender;
            string day;
            string month;
            if (datePicker.SelectedDate.Value.Day.ToString().Length == 1)
            {
                day = "0" + datePicker.SelectedDate.Value.Day.ToString();
            }
            else
            {
                day = datePicker.SelectedDate.Value.Day.ToString();
            }

            if (datePicker.SelectedDate.Value.Month.ToString().Length == 1)
            {
                month = "0" + datePicker.SelectedDate.Value.Month.ToString();
            }
            else
            {
                month = datePicker.SelectedDate.Value.Month.ToString();
            }

            string date = datePicker.SelectedDate.Value.Year.ToString() + "-"
                + month + "-"
                + day;
            Purchase purchase = purchases.Find(purchase => purchase.id.ToString() == datePicker.Uid);
            purchase.date = date;
            datePicker.Text = date;
        }
    }
}
