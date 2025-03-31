using HTTPClassLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMSConsoleApp
{
    public class OrderHandler
    {
        public static List<OrderItem> ParseOrder(string input, List<Dish> menu)
        {
            var orderItems = new List<OrderItem>();

            try
            {
                var pairs = input.Split(';');
                foreach (var pair in pairs)
                {
                    var parts = pair.Split(':');
                    if (parts.Length != 2 || !double.TryParse(parts[1], out double quantity) || quantity <= 0)
                    {
                        throw new Exception("Ошибка формата ввода.");
                    }

                    var dish = menu.FirstOrDefault(d => d.Article == parts[0]);
                    if (dish == null)
                    {
                        throw new Exception($"Блюдо с кодом {parts[0]} не найдено.");
                    }

                    orderItems.Add(new OrderItem { Id = dish.Article, Quantity = quantity });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}. Попробуйте снова.");
                return null;
            }

            return orderItems;
        }
    }

}
