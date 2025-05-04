using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Ecommerce.Data.Entities;
using Ecommerce.Data.Repositories.Interfaces;
using Ecommerce.Data.Utilities;

namespace Ecommerce.Data.Repositories.Implementations
{
    public class ShoppingCartRepository : Interfaces.IShoppingCartRepository
    {
        private const string FileName = "shoppingcarts.json";
        private readonly List<ShoppingCart> _carts;
        public ShoppingCartRepository()
        {
            try
            {
                var cartList = FileHandler.ReadJson<List<ShoppingCart>>(FileName);
                if (cartList == null)
                {
                    _carts = new List<ShoppingCart>();
                }
                else
                {
                    _carts = cartList;
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

        public void AddShoppingCart(ShoppingCart cart)
        {
            if (_carts.Any(c => c.CartID == cart.CartID))
            {
                throw new Exception($"Shopping Cart with this ID {cart.CartID} already exists.");
            }
            _carts.Add(cart);
            SaveChanges();
        }

        public void DeleteShoppingCart(int id)
        {
            var cart = _carts.FirstOrDefault(c => c.CartID == id);
            if (cart == null)
            {
                throw new Exception("Shopping Cart not found.");
            }
            _carts.Remove(cart);
            SaveChanges();
        }

        public List<ShoppingCart> GetAllShoppingCarts()
        {
            return _carts;
        }

        public ShoppingCart GetShoppingCartByCustomer(int customerId)
        {
            var cart = _carts.FirstOrDefault(c => c.CustomerID == customerId);
            if (cart == null)
            {
                throw new Exception("Shopping Cart not found.");
            }
            return cart;
        }

        public ShoppingCart GetShoppingCartById(int id)
        {
            var cart = _carts.FirstOrDefault(c => c.CartID == id);
            if (cart == null)
            {
                throw new Exception("Shopping Cart for this customer not found.");
            }
            return cart;
        }

        public void UpdateShoppingCart(ShoppingCart updatedCart)
        {
            var existingCart = _carts.FirstOrDefault(c => c.CartID == updatedCart.CartID);
            if (existingCart == null)
            {
                throw new Exception("Shopping Cart not found.");
            }
            existingCart.CustomerID = updatedCart.CustomerID;
            existingCart.Products = updatedCart.Products;
            SaveChanges();
        }
        private void SaveChanges()
        {
            try
            {
                FileHandler.WriteJson(FileName, _carts);
            }
            catch (IOException)
            {
                throw new Exception($"Error writing to file {FileName}.");
            }
            catch (JsonException)
            {
                throw new Exception($"Error serializing JSON to {FileName}. Please check the file format.");
            }
        }
    }
}
