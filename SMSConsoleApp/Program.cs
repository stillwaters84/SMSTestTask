using HTTPClassLib.Models;
using SMSConsoleApp;

Logger.Initialize();
Logger.Info("Приложение запущено.");

using var db = new AppDbContext();
await db.Database.EnsureCreatedAsync();

var client = new HttpClientWrapper();

Console.WriteLine("Получение меню...");
var menu = await client.GetMenuAsync();

if (menu == null || menu.Count == 0)
{
    Logger.Error("Ошибка получения меню.");
    Console.WriteLine("Ошибка получения меню.");
    return;
}

db.Dishes.AddRange(menu);
await db.SaveChangesAsync();

Console.WriteLine("\nДоступные блюда:");
foreach (var dish in menu)
{
    Console.WriteLine($"{dish.Name} - {dish.Article} - {dish.Price} руб.");
}

List<OrderItem> orderItems = null;
do
{
    Console.Write("\nВведите заказ (код:количество;код:количество;...): ");
    string input = Console.ReadLine();
    orderItems = OrderHandler.ParseOrder(input, menu);
}
while (orderItems == null);

bool success = await client.SendOrderAsync(orderItems);

if (success)
{
    Logger.Info("Заказ успешно отправлен.");
    Console.WriteLine("УСПЕХ");
}
else
{
    Logger.Error("Ошибка при отправке заказа.");
    Console.WriteLine("Ошибка при отправке заказа.");
}

Logger.Info("Программа завершена.");
