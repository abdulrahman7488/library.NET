using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookshop1.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public void AddItem(CartItem item)
        {
            var existingItem = Items.FirstOrDefault(i => i.BookID == item.BookID);
            if (existingItem == null)
            {
                Items.Add(item);
            }
            else
            {
                existingItem.Quantity += item.Quantity;
            }
        }

        public void RemoveItem(int bookId)
        {
            var itemToRemove = Items.FirstOrDefault(i => i.BookID == bookId);
            if (itemToRemove != null)
            {
                Items.Remove(itemToRemove);
            }
        }

        public decimal GetTotal()
        {
            return (decimal)Items.Sum(i => i.Price * i.Quantity);
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}