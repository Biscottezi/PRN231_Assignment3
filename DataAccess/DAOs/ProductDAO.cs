using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class ProductDAO
    {
        private static ProductDAO _instance = null;
        private static readonly object InstanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock(InstanceLock)
                {
                    if(_instance == null)
                    {
                        _instance = new ProductDAO();
                    }
                    return _instance;
                }
            }
        }

        public IEnumerable<Product> GetProductList()
        {
            List<Product> products = null;
            try
            {
                var db = new ProductStoreDbContext();
                products = db.Products
                    .Include(p => p.Category)
                    .ToList();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error - GetProductList - ProductDAO -> " + e.Message);
            }
            return products;
        }

        public Product GetById(int id)
        {
            Product product = null;
            try
            {
                var db = new ProductStoreDbContext();
                product = db.Products
                    .Include(p => p.Category)
                    .FirstOrDefault(p => p.ProductId == id);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error - GetProductById - ProductDAO -> " + e.Message);
            }
            return product;
        }

        public void Create(Product product)
        {
            try
            {
                var db = new ProductStoreDbContext();
                db.Products.Add(product);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error - CreateProduct - ProductDAO -> " + e.Message);
            }
        }

        public void Update(Product product)
        {
            try
            {
                var p = GetById(product.ProductId);
                if(p == null)
                {
                    throw new Exception("Product not found");
                }
                else
                {
                    var db = new ProductStoreDbContext();
                    db.Entry<Product>(product).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error - UpdateProduct - ProductDAO -> " + e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var p = GetById(id);
                if (p == null)
                {
                    throw new Exception("Product not found");
                }
                else
                {
                    var db = new ProductStoreDbContext();
                    db.Remove(p);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error - DeleteProduct - ProductDAO -> " + e.Message);
            }
        }
    }
}
