using ExamenSeptembre202309.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApplication1.ViewModels;

namespace ExamenSeptembre202309.ViewModels
{
    public class ProductVM
    {
        private NorthwindContext dc = new NorthwindContext();


        private ProductModel _selectedProduct;
        private ObservableCollection<ProductModel> _ProductsList;
        private ObservableCollection<string> _salesList;

        private DelegateCommand _updateCommand;

        public ObservableCollection<ProductModel> ProductsList
        {
            get
            {
                if (_ProductsList == null)
                {
                    _ProductsList = loadProducts();
                }

                return _ProductsList;

            }

        }

        public ObservableCollection<string> SalesList
        {
            get
            {
                if (_salesList == null)
                {
                    _salesList = loadTotalSalesPerProduct();
                }
                return _salesList;
            }
        }

        public ObservableCollection<string> loadTotalSalesPerProduct()
        {
            var totalSales = dc.Products
                .GroupJoin(
                    dc.OrderDetails,
                    product => product.ProductId,
                    orderDetail => orderDetail.ProductId,
                    (product, orderDetails) => new
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        TotalSales = orderDetails.Sum(od => od.Quantity * od.UnitPrice)
                    })
                .ToList();

            ObservableCollection<string> salesList = new ObservableCollection<string>();

            foreach (var item in totalSales)
            {
                salesList.Add($"{item.ProductId}: {item.TotalSales}");
            }

            return salesList;
        }

        private ObservableCollection<ProductModel> loadProducts()
        {
            ObservableCollection<ProductModel> localCollection = new ObservableCollection<ProductModel>();
            foreach (var item in dc.Products)
            {
                localCollection.Add(new ProductModel(item));

            }

            return localCollection;

        }


        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value;  }

        }


        public DelegateCommand UpdateCommand
        {
            get { return _updateCommand = _updateCommand ?? new DelegateCommand(UpdateProduct); }
        }

 
        private void UpdateProduct()
        {
            Product verif = dc.Products.Where(e => e.ProductId == SelectedProduct.MonProduct.ProductId).SingleOrDefault();
            if (verif != null)
            {
                dc.SaveChanges();
                MessageBox.Show("Enregistrement en base de données fait");
            }
        }
    }
}