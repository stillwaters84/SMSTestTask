using HTTPClassLib;
using HTTPClassLib.Models;
using HTTPClassLib.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMSConsoleApp
{
    public class HttpClientWrapper
    {
        private readonly RestaurantService _httpClient; // Экземпляр HTTP-библиотеки
        private string _baseUrl;

        public HttpClientWrapper()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string url = config["HttpSettings:Url"];
            string login = config["HttpSettings:Login"];
            string password = config["HttpSettings:Password"];

            _httpClient = new RestaurantService(url, login, password);
        }

        public async Task<List<Dish>> GetMenuAsync()
        {
            var menuItems = await _httpClient.GetMenuItemsAsync();
            return menuItems.ConvertAll(m => new Dish
            {
                Id = m.Id,
                Name = m.Name,
                Article = m.Article,
                Price = m.Price,
                IsWeighted = m.IsWeighted,
                FullPath = m.FullPath,
                Barcodes = m.Barcodes
            });
        }

        public async Task<bool> SendOrderAsync(List<OrderItem> items)
        {
            Order newOrder = new Order { OrderId = new Guid(), Items = items };
            return await _httpClient.PostOrderAsync(newOrder);
        }
    }

}
