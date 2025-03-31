using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Sms.Test;
using Grpc.Net.Client;
using HTTPClassLib.Models;
using Google.Protobuf.WellKnownTypes;

namespace gRPCClassLibrary.Services
{
    public class RestaurantService
    {
        private readonly SmsTestService.SmsTestServiceClient _client;

        public RestaurantService(string serverUrl)
        {
            var channel = GrpcChannel.ForAddress(serverUrl);
            _client = new SmsTestService.SmsTestServiceClient(channel);
        }

        public async Task<List<MenuItem>> GetMenuAsync()
        {
            var response = await _client.GetMenuAsync(new BoolValue { Value = true});
            if (!response.Success)
            {
                throw new Exception(response.ErrorMessage);
            }
            return response.MenuItems.ToList();
        }

        public async Task<bool> SendOrderAsync(string orderId, List<HTTPClassLib.Models.OrderItem> items)
        {
            var request = new Sms.Test.Order { Id = orderId };
            request.OrderItems.AddRange(items.Select(item => new Sms.Test.OrderItem
            {
                Id = item.Id,
                Quantity = item.Quantity
            }));

            var response = await _client.SendOrderAsync(request);

            if (!response.Success)
            {
                throw new Exception(response.ErrorMessage);
            }

            return response.Success;
        }
    }
}
