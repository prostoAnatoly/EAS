using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotTest;

internal class Bot
{
    private readonly ITelegramBotClient botClient = new TelegramBotClient("");

    async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            var message = update.Message;
            if (message == null || message.Text == null) { return; }

            if (message.Text.ToLower() == "/start")
            {
                await botClient.SendTextMessageAsync(message.Chat, string.Join(' ', "Добро пожаловать!", GetFullName(message.From)),
                    cancellationToken: cancellationToken);
                return;
            }

            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                // first row
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "Кнопка 1", callbackData: "post"),
                    InlineKeyboardButton.WithCallbackData(text: "Кнопка 2", callbackData: "12"),
                },
            });

            await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!", replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);
        }
    }

    Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));

        return Task.CompletedTask;
    }

    private string GetFullName(User? user)
    {
        if (user == null) { return string.Empty; }

        return string.Join(' ', user.LastName, user.FirstName);

    }
    public void Start()
    {
        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }, // receive all update types
        };
        botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );
    }

    public string FirstName => botClient.GetMeAsync().Result.FirstName;
}
