// See https://aka.ms/new-console-template for more information


using TelegramBotTest;

var bot = new Bot();

Console.WriteLine("Запускаем бота " + bot.FirstName);

bot.Start();

Console.WriteLine("Запущен бот " + bot.FirstName);

Console.ReadLine();