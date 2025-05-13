using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Ecommerce.Data.Entities;
using Ecommerce.Data.Repositories.Interfaces;
using Ecommerce.Data.Utilities;

namespace Ecommerce.Data.Repositories.Implementations
{
    public class ProductRepository : Interfaces.IProductRepository
    {
        private const string FileName = "products.json";
        private readonly List<Product> _products;
        public ProductRepository()
        {
            try
            {
                var productList = FileHandler.ReadJson<List<Product>>(FileName);
                if (productList == null)
                {
                    _products = new List<Product>();
                }
                else
                {
                    _products = productList;
                }
            }
            catch (IOException)
            {
                throw new Exception($"Error reading from file {FileName}.");
            }
            catch (JsonException)
            {
                throw new Exception($"Error deserializing JSON from {FileName}. Please check the file format.");
            }
        }

        public void AddProduct(Product product)
        {
            if (_products.Any(p => p.ID == product.ID))
            {
                throw new Exception("Product with this ID already exists.");
            }
            _products.Add(product);
            SaveChanges();
        }
        public Product GetProductById(int id)
        {
            var product = _products.FirstOrDefault(p => p.ID == id);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }
            return product;
        }
        public List<Product> GetAllProducts()
        {
            return _products;
        }
        public void UpdateProduct(Product updatedProduct)
        {
            var existingProduct = _products.FirstOrDefault(p => p.ID == updatedProduct.ID);
            if (existingProduct == null)
            {
                throw new Exception("Product not found.");
            }
            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;
            SaveChanges();
        }
        public void DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.ID == id);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }
            _products.Remove(product);
            SaveChanges();
        }
        private void SaveChanges()
        {
            try
            {
                FileHandler.WriteJson(FileName, _products);
            }
            catch (IOException)
            {
                throw new Exception($"Error writing to file {FileName}.");
            }
            catch (JsonException)
            {
                throw new Exception($"Error serializing JSON to {FileName}. Please check the data format.");
            }
        }
    }
}
