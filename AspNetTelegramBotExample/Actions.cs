using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace AspNetTelegramBotExample;

public static class Actions
{
    public static async Task<Message> SendInlineKeyboard(ITelegramBotClient botClient, Message message)
    {
        await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
        InlineKeyboardMarkup inlineKeyboard = new(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("11", "11"),
                    InlineKeyboardButton.WithCallbackData("12", "12"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("21", "21"),
                    InlineKeyboardButton.WithCallbackData("22", "22"),
                },
            });
        return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
            text: "Choose",
            replyMarkup: inlineKeyboard);
    }

    public static async Task<Message> SendReplyKeyboard(ITelegramBotClient botClient, Message message)
    {
        ReplyKeyboardMarkup replyKeyboardMarkup = new(
            new[]
            {
                new KeyboardButton[] {"11", "12"},
                new KeyboardButton[] {"21", "22"},
            })
        {
            ResizeKeyboard = true
        };
        return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
            text: "Choose",
            replyMarkup: replyKeyboardMarkup);
    }

    public static async Task<Message> RemoveKeyboard(ITelegramBotClient botClient, Message message)
    {
        return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
            text: "Removing keyboard",
            replyMarkup: new ReplyKeyboardRemove());
    }

    public static async Task<Message> SendFile(ITelegramBotClient botClient, Message message)
    {
        await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.UploadPhoto);
        var filePath = Environment.GetEnvironmentVariable("CURRENT_PATH") + "Files/flowers.png";
        await using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        var fileName = filePath.Split(Path.DirectorySeparatorChar).Last();
        return await botClient.SendPhotoAsync(chatId: message.Chat.Id,
            photo: new InputOnlineFile(fileStream, fileName),
            caption: "Nice Picture");
    }

    public static async Task<Message> RequestContactAndLocation(ITelegramBotClient botClient, Message message)
    {
        ReplyKeyboardMarkup requestReplyKeyboard = new(
            new[]
            {
                KeyboardButton.WithRequestLocation("Location"),
                KeyboardButton.WithRequestContact("Contact"),
            });
        return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
            text: "Who or Where are you?",
            replyMarkup: requestReplyKeyboard);
    }

    public static async Task<Message> Usage(ITelegramBotClient botClient, Message message)
    {
        const string usage = "Usage:\n" +
                             "/inline   - send inline keyboard\n" +
                             "/keyboard - send custom keyboard\n" +
                             "/remove   - remove custom keyboard\n" +
                             "/photo    - send a photo\n" +
                             "/request  - request location or contact";
        return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
            text: usage,
            replyMarkup: new ReplyKeyboardRemove());
    }
}
