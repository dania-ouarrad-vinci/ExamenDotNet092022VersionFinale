using ExamenSeptembre202309.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenSeptembre202309.ViewModels
{
    public class ProductModel
    {
        private readonly Product _monProduct;

        public Product MonProduct
        {
            get { return _monProduct; }
        }

  

        public ProductModel(Product current)
        {
            this._monProduct = current;
        }

        public int ProductId
        {
            get
            {
                    return _monProduct.ProductId;
            }
        }


        public String ProductName
        {
            get { return _monProduct.ProductName; }
            set
            {
                _monProduct.ProductName = value;
            }
        }

        public String QuantityPerUnit
        {
            get { return _monProduct.QuantityPerUnit; }
            set
            {
                _monProduct.QuantityPerUnit = value;
            }
        }

        public String ContactName
        {
            get
            {
                return _monProduct.Supplier?.ContactName;
            }
        }
    }
}
