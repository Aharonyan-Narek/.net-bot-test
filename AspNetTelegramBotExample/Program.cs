using AspNetTelegramBotExample;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

Environment.SetEnvironmentVariable("CURRENT_PATH", Directory.GetCurrentDirectory() + "../../../../");

Dictionary<string, string>? config;
using (var r = new StreamReader(Environment.GetEnvironmentVariable("CURRENT_PATH") + "config.json"))
{
   config =
        new JsonSerializer().Deserialize<Dictionary<string, string>>(new JsonTextReader(r));
}

var bot = new TelegramBotClient(config["BOT_TOKEN"]);

User me = await bot.GetMeAsync();
Console.Title = me.Username ?? "My awesome Bot";

using var cts = new CancellationTokenSource();

bot.StartReceiving(Handlers.HandleUpdateAsync,
    Handlers.HandleErrorAsync,
    new ReceiverOptions(),
    cts.Token);

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

cts.Cancel();
