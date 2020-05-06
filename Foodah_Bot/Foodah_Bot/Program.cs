using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
namespace Foodah_Bot
{
    public class Program
    {
        private DiscordSocketClient _client;
        private CommandService _service;
        private CommandHandler _commands;
        private SocketGuild _guild;
        private SocketTextChannel _general;
        private SocketTextChannel _updates;

        bool channelsLoaded;
        public static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _service = new CommandService();
            _commands = new CommandHandler(_client, _service);
            _client.Log += Log;

            //  You can assign your bot token to a string, and pass that in to connect.
            //  This is, however, insecure, particularly if you plan to have your code hosted in a public repository.
            var token = "NzA3NDIwNDQ4NDcxNjQ2MjE4.XrIlVw.4uU5IQkPyfqD5utL27QiNuu2IyE";

            // Some alternative options would be to keep your token in an Environment Variable or a standalone file.
            // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
            // var token = File.ReadAllText("token.txt");
            // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await _commands.InstallCommandsAsync();
            // Block this task until the program is closed.
            HandleConsole();
            _guild = _client.GetGuild(672344980970536960);
            _general = _guild.GetTextChannel(672661558895050782);
            _updates = _guild.GetTextChannel(673063695827337247);
        }
        public void HandleConsole()
        {
            Console.WriteLine("Starting Control Console");
            while (true)
            {
                char[] splitters = { ' ' };
                var cmd = Console.ReadLine().Split(splitters,3, StringSplitOptions.RemoveEmptyEntries);
                if(cmd[0] == "sendmsg")
                {
                    if (channelsLoaded)
                        HandleSendMsg(cmd);
                    else
                        Console.WriteLine("Load Channels First");
                }
                if (cmd[0] == "loadchannels")
                    LoadChannels();
                if(cmd[0] == "start")
                {
                    if (cmd[1] == "msgsession")
                    {
                        if (channelsLoaded)
                            HandleMessageSession(cmd);
                        else
                            Console.WriteLine("Load Channels First");
                    }

                }
            }
        }
        void HandleMessageSession(string[] cmd)
        {
            Console.WriteLine("Message session started in " + cmd[2]);
            if(cmd[2] == "general")
            {
                while(true)
                {
                    var msg = Console.ReadLine();
                    if(msg == "exit")
                    {

                    }
                }
            }
        }
        void HandleSendMsg(string[] cmd)
        {
            if (cmd[1] == "general")
                _general.SendMessageAsync(cmd[2]);
            else if (cmd[1] == "updates")
                _updates.SendMessageAsync(cmd[2]);
            else
            {
                Console.WriteLine("That is not an available channel");
            }
        }
        void LoadChannels()
        {
            _guild = _client.GetGuild(672344980970536960);
            _general = _guild.GetTextChannel(672661558895050782);
            _updates = _guild.GetTextChannel(673063695827337247);
            Console.WriteLine("Channels Loaded");
            channelsLoaded = true;
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
