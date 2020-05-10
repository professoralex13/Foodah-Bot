using Microsoft.Azure.WebJobs.Host.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace Foodah_WebApp
{
    public class Startup 
    {
        private DiscordSocketClient _client;
        private CommandService _service;
        private CommandHandler _commands;

        public static void Run()
        => new Startup().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            Console.WriteLine("Starting Foodah Command Client");
            _client = new DiscordSocketClient();
            _service = new CommandService();
            _commands = new CommandHandler(_client, _service);
            _client.Log += Log;
            //  You can assign your bot token to a string, and pass that in to connect.
            //  This is, however, insecure, particularly if you plan to have your code hosted in a public repository.
            var token = Environment.GetEnvironmentVariable("FOODAHTOKEN");
            // Some alternative options would be to keep your token in an Environment Variable or a standalone file.
            // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
            // var token = File.ReadAllText("token.txt");
            // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await _commands.InstallCommandsAsync();
            // Block this task until the program is closed.
            // _guild = _client.GetGuild(672344980970536960);
            //_general = _guild.GetTextChannel(672661558895050782);
            //_updates = _guild.GetTextChannel(673063695827337247);
            await Task.Delay(-1);
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}