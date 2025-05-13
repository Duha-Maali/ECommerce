using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Ecommerce.Data.Repositories.Interfaces;
using Ecommerce.Data.Entities;
using Ecommerce.Data.Utilities;

namespace Ecommerce.Data.Repositories.Implementations
{
    public class OrderRepository : Interfaces.IOrderRepository
    {
        private const string FileName = "orders.json";
        private readonly List<Order> _orders;

        public OrderRepository()
        {
            try
            {
                var orderList = FileHandler.ReadJson<List<Order>>(FileName);
                if (orderList == null)
                {
                    _orders = new List<Order>();
                }
                else
                {
                    _orders = orderList;
                }
            }
            catch (IOException ex)
            {
                throw new IOException($"Failed to read file {FileName}: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception($"Failed to deserialize JSON from {FileName}: {ex.Message}", ex);
            }
        }
        public void AddOrder(Order order)
        {
            if (_orders.Any(o => o.OrderID == order.OrderID))
            {
                throw new Exception($"Order with this Id {order.OrderID} already exists.");
            }
            _orders.Add(order);
            SaveChanges();
        }

        public void DeleteOrder(int id)
        {
            var order = _orders.FirstOrDefault(o => o.OrderID == id);
            if (order == null)
            {
                throw new Exception("Order not found.");
            }
            _orders.Remove(order);
            SaveChanges();
        }

        public List<Order> GetAllOrders()
        {
            return _orders;
        }

        public Order GetOrderById(int id)
        {
            var order = _orders.FirstOrDefault(o => o.OrderID == id);
            if (order == null)
            {
                throw new Exception("Order not found.");
            }
            return order;
        }

        public List<Order> GetOrdersByCustomer(int customerId)
        {
            return _orders.Where(o => o.CustomerID == customerId).ToList();
        }

        public void UpdateOrder(Order updatedOrder)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.OrderID == updatedOrder.OrderID);
            if (existingOrder == null)
            {
                throw new Exception("Order not found.");
            }
            existingOrder.CustomerID = updatedOrder.CustomerID;
            existingOrder.Cart = updatedOrder.Cart;
            existingOrder.OrderDate = updatedOrder.OrderDate;
            existingOrder.Status = updatedOrder.Status;
            existingOrder.PaymentMethod = updatedOrder.PaymentMethod;
            SaveChanges();
        }
        private void SaveChanges()
        {
            try
            {
                FileHandler.WriteJson(FileName, _orders);
            }
            catch (IOException ex)
            {
                throw new IOException($"Failed to write to file {FileName}: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception($"Failed to serialize JSON to {FileName}: {ex.Message}", ex);
            }
        }
    }
}

