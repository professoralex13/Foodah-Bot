using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
namespace Foodah_Bot_Interface
{
    public class Program
    {
        private DiscordSocketClient _client;
        private CommandService _service;
        private SocketGuild _guild;
        private SocketTextChannel _general;
        private SocketTextChannel _updates;
        private SocketTextChannel _talkFoodah;
        bool channelsLoaded;
        public static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            Console.WriteLine("Starting Foodah Interface Client, once client has loaded input loadchannels");

            _client = new DiscordSocketClient();
            _service = new CommandService();
            _client.Log += Log;

            //  You can assign your bot token to a string, and pass that in to connect.
            //  This is, however, insecure, particularly if you plan to have your code hosted in a public repository.

            // Some alternative options would be to keep your token in an Environment Variable or a standalone file.
            // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
            // var token = File.ReadAllText("token.txt");
            // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;
            var token = "";
            if (Environment.GetEnvironmentVariable("FOODAHTOKEN") == null)
            {
                Console.WriteLine("Cannot Find Auth Token, Please Input it");
                while (true)
                {
                    try
                    {
                        token = Console.ReadLine();
                        await _client.LoginAsync(TokenType.Bot, token);
                        await _client.StartAsync();
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Incorrect Token");
                    }
                }
            }
            else
            {
                token = Environment.GetEnvironmentVariable("FOODAHTOKEN");
                await _client.LoginAsync(TokenType.Bot, token);
                await _client.StartAsync();
            }
            
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
                var cmd = Console.ReadLine().Split(splitters, 3, StringSplitOptions.RemoveEmptyEntries);
                if (cmd[0] == "sendmsg")
                {
                    if (channelsLoaded)
                        HandleSendMsg(cmd);
                    else
                        Console.WriteLine("Load Channels First");
                }
                else if (cmd[0] == "loadchannels")
                    LoadChannels();
                else if (cmd[0] == "start")
                {
                    if (cmd[1] == "msgsession")
                    {
                        if (channelsLoaded)
                            HandleMessageSession(cmd);
                        else
                            Console.WriteLine("Load Channels First");
                    }
                }
                else if (cmd[0] == "deletemsg")
                {
                    HandleMessageDelete(cmd);
                }
                else if(cmd[0] == "DMUser")
                {
                    HandleDMSession(cmd);   
                }
                else
                {
                    Console.WriteLine("Cannot find command of that name");
                }
            }
        }
        void HandleDMSession(string[] cmd)
        {
            if (!channelsLoaded)
                Console.WriteLine("Load channels first");
            SocketGuildUser user = _guild.GetUser(ulong.Parse(cmd[1]));
            while (true)
            {
                user.SendMessageAsync(Console.ReadLine());
            }
        }
        void HandleMessageDelete(string[] cmd)
        {
            if (cmd.Length == 1)
            {
                Console.WriteLine("Missing Argument");
                return;
            }
            else if (cmd[1] == "general")
            {
                while (true)
                {
                    Console.WriteLine("Input Message ID");
                    ulong id = 0;
                    if (ulong.TryParse(Console.ReadLine(), out id))
                    {
                        _general.DeleteMessageAsync(id);
                        Console.WriteLine("Message was deleted");
                        break;
                    }
                    else
                        Console.WriteLine("That is not a message ID");
                }
            }
        }
        void HandleMessageSession(string[] cmd)
        {
            Console.WriteLine("Message session started in " + cmd[2]);
            if (cmd[2] == "general")
            {
                while (true)
                {
                    var msg = Console.ReadLine();
                    var msgsplit = msg.Split(' ');
                    if (msg == "exit")
                    {
                        Console.WriteLine("Message Session Left");
                        break;
                    }
                    else if (msgsplit[0] == "define")
                    {
                        _general.SendMessageAsync("", false, DefineModule.Define(msgsplit[1]));
                        Console.WriteLine("Definition Sent");
                    }

                    else
                        _general.SendMessageAsync(msg);
                }
            }
            if (cmd[2] == "talk")
            {
                while (true)
                {
                    var msg = Console.ReadLine();
                    var msgsplit = msg.Split(' ');
                    if (msg == "exit")
                    {
                        Console.WriteLine("Message Session Left");
                        break;
                    }
                    else if (msgsplit[0] == "define")
                    {
                        _talkFoodah.SendMessageAsync("", false, DefineModule.Define(msgsplit[1]));
                        Console.WriteLine("Definition Sent");
                    }

                    else
                        _talkFoodah.SendMessageAsync(msg);
                }
            }
        }
        void HandleSendMsg(string[] cmd)
        {
            if (cmd[1] == "general")
            {
                _general.SendMessageAsync(cmd[2]);
                Console.WriteLine("Message was sent in general");
            }
            else if (cmd[1] == "updates")
            {
                _updates.SendMessageAsync(cmd[2]);
                Console.WriteLine("Message was sent in Updates");

            }
            else if (cmd[1] == "talk")
            {
                _talkFoodah.SendMessageAsync(cmd[2]);
                Console.WriteLine("Message was sent in talk to foodah");
            }
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
            _talkFoodah = _guild.GetTextChannel(707424511275958282);
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

