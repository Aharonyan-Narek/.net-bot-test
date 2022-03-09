# AspNetTelegramBotExample
An example of the implementation of bot telegrams in C#.

## Before using

Create a config file.json with the following content

```json
{
  "BOT_TOKEN":"{yourtoken}"
}
```
And replace {yourtoken} with your bot's token

## For developers

The files are arranged as follows:

##### AspNetTelegramBotExample/Program.cs
- Initializing the bot and launching long polling.
##### AspNetTelegramBotExample/Handlers.cs
- A router for any interactions with the bot. Messages first of all get there, and then they are distributed by actions
##### AspNetTelegramBotExample/Actions.cs
- The logic of your messages and responses
