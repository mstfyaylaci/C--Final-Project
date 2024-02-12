using DataAccess.Abstact;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            _products=new List<Product>()
            {
                new Product{ProductId=1,ProductName="Bardak",CategoryId=2,UnitPrice=20,UnitsInStock=3},
                new Product{ProductId=2,ProductName="Çatal",CategoryId=1,UnitPrice=30,UnitsInStock=30}
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            //Product productToDelete=null;

            //foreach (var p in _products)
            //{
            //    if (product.ProductId==p.ProductId)
            //    {
            //        productToDelete = p;
            //    }

            //}
            //_products.Remove(product);// sadece bu satır olsa çalışmaz çünkü referans tip olduğu için 

            Product productToDelete = productToDelete =_products.SingleOrDefault(p => p.ProductId==product.ProductId);
            
            _products.Remove(productToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
           return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategoryId(int categoryId)
        {
           return _products.Where(p=>p.CategoryId==categoryId).ToList();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            Product updateProduct=_products.SingleOrDefault(product=>product.ProductId==product.ProductId);
            updateProduct.ProductName=product.ProductName;
            updateProduct.CategoryId = product.CategoryId;
            updateProduct.UnitPrice=product.UnitPrice;
            updateProduct.UnitsInStock=product.UnitsInStock; 

        }
    }
}
