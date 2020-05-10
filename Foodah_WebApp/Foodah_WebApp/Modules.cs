using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using RestSharp;
using RestSharp.Deserializers;
    public class BallsModule : ModuleBase<SocketCommandContext>
    {
        [Command("balls")]
        public async Task BallsAsync()
        {
            string msg = Context.User.Username + " no";
            await ReplyAsync(msg);
        }
    }
    public class PenisModule : ModuleBase<SocketCommandContext>
    {
        [Command("penis")]
        public async Task PenisAsync()
        {
            await ReplyAsync("oh yes, mines 12 inches long");
        }
    }
    public class PeniModule : ModuleBase<SocketCommandContext>
    {
        [Command("peni")]
        public async Task PeniAsync()
        {
            await ReplyAsync("ahh yes many many penis i likeee");
        }
    }
    public class LyricsModule : ModuleBase<SocketCommandContext>
    {
        [Command("lyrics")]
        public async Task LyricsAsync(string song)
        {
            var client = new RestClient("https://canarado-lyrics.p.rapidapi.com/lyrics/" + song);
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "canarado-lyrics.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "375051a029msh7a073e4fcbc28b6p1835e7jsn4ac2ae174de6");
            IRestResponse response = client.Execute(request);
            await ReplyAsync(response.Content.Split(':')[7]);
        }
    }
    public class EmojiModule : ModuleBase<SocketCommandContext>
    {
        [Command("add emoji")]
        public async Task EmojiAsync(string name)
        {
            var user = Context.User as SocketGuildUser;
            var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Emoji");
            if (!user.Roles.Contains(role))
            {
                await ReplyAsync("you do not have emoji role");
                return;
            }
            if (Context.Message.Attachments.Count == 0)
            {
                await ReplyAsync("Please attach a png or jpg file");
                return;
            }
            var fileSuffix = Context.Message.Attachments.FirstOrDefault().Filename.Split('.')[1];
            if (fileSuffix == "png" || fileSuffix == "jpg")
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(Context.Message.Attachments.FirstOrDefault().Url);

                await Context.Guild.CreateEmoteAsync(name, new Discord.Image(stream));
                await ReplyAsync("Added Emoji with name " + name);
            }
            else
            {
                await ReplyAsync("File must be a jpg or png");
                return;
            }

        }
        [Command("add emoji")]
        public async Task EmojiAsync()
        {
            await ReplyAsync("Please add a name");
        }
    }
    public class RemindMalestromModule : ModuleBase<SocketCommandContext>
    {
        [Command("notify malestrom")]
        public async Task MalestromAsync(string Id)
        {
            await ReplyAsync(Context.Guild.GetUser(ulong.Parse(Id)).Mention + " GET YO ASS OVER HERE MALESTROM IS STARTING");
            await Context.Channel.DeleteMessageAsync(Context.Message);
        }
    }
    public class DefineModule : ModuleBase<SocketCommandContext>
    {
        [Command("define")]
        public async Task DefineAsync(string item)
        {
            await ReplyAsync("", false, Define(item));
        }
        public static Embed Define(string item)
        {
            var client = new RestClient("https://mashape-community-urban-dictionary.p.rapidapi.com/define?term=" + item);
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "mashape-community-urban-dictionary.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "375051a029msh7a073e4fcbc28b6p1835e7jsn4ac2ae174de6");
            IRestResponse response = client.Execute(request);
            var embed = new EmbedBuilder();
            embed.WithTitle("Urban Dictionary Defines " + item + " as:");
            embed.WithDescription(response.Content.Split('"')[5]);
            return embed.Build();
        }
        [Command("define")]
        public async Task DefineAsync()
        {
            await ReplyAsync("Ummmm, idk what do you want me to define?");
        }
    }
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task HelpAsync()
        {
            await ReplyAsync("idk..... whats in it for me?");
            await Task.Delay(1000);
            await ReplyAsync("actually no, I dont want to help you");
            await Task.Delay(2000);
            await ReplyAsync("Fuck off " + Context.User.Mention);
        }
    }
    public class WeatherModule : ModuleBase<SocketCommandContext>
    {
        [Command("whats the weather")]
        public async Task WeatherAsync()
        {
            await ReplyAsync("IDFK what the weather is where you are");
            await Task.Delay(1000);
            await ReplyAsync("I dont spy on people's locations like you do you *pervert*");
        }
    }
    public class ExplainModule : ModuleBase<SocketCommandContext>
    {
        [Command("explain")]
        public async Task ExplainAsync()
        {
            await ReplyAsync("Ok, heres what I can do");
            var embed = new EmbedBuilder();
            embed.WithTitle("Foodah Commands:");
            var description = "Balls" + Environment.NewLine + "Help" + Environment.NewLine;
            description += "yeet" + Environment.NewLine + "random" + Environment.NewLine;
            description += "i love" + Environment.NewLine + "Peoples Names" + Environment.NewLine;
            description += "whats the weather" + Environment.NewLine + "define";
            embed.WithDescription(description);
            await ReplyAsync("", false, embed.Build());
            await ReplyAsync("So yeah, I can do alot of stuff");
        }
    }
    public class YeetModule : ModuleBase<SocketCommandContext>
    {
        [Command("yeet")]
        public async Task YeetAsync(string toYeet)
        {
            await ReplyAsync("YEEEEEEEEEEET");
            if (toYeet == "me")
                toYeet = "you";
            if (toYeet == "yourself")
                toYeet = "myself";
            await ReplyAsync("I Yeeted " + toYeet + " are you happy now");
        }
        [Command("yeet")]
        public async Task YeetAsync()
        {
            await ReplyAsync("YEEEEEEEEEEET");
            await ReplyAsync("I Yeeted, wait, I didnt actually yeet anything, give me something to yeet");
        }
    }
    public class RandomModule : ModuleBase<SocketCommandContext>
    {
        [Command("random")]
        public async Task RandomAsync(string thing)
        {
            if (thing == "Derpo" || thing == "derpo")
            {
                await HandleDerpo();
            }
            else
            {
                await ReplyAsync("well i cant give you a random thing of that");
            }
        }
        public async Task HandleDerpo()
        {
            var embed = new EmbedBuilder();
            embed.WithColor(new Discord.Color(255, 255, 255));
            var random = new Random();
            switch (random.Next(0, 5))
            {
                case (0):
                    embed.WithImageUrl("https://cdn.discordapp.com/attachments/601140927213469733/707522147328983080/Untitled170_20200408114842.png");
                    embed.WithTitle("Heres Derp Number 1");
                    break;
                case (1):
                    embed.WithImageUrl("https://cdn.discordapp.com/attachments/601140927213469733/707522147941089280/Untitled173_20200326183715.png");
                    embed.WithTitle("Heres Derp Number 2");
                    break;
                case (2):
                    embed.WithImageUrl("https://cdn.discordapp.com/attachments/601140927213469733/707522148494737468/Untitled173_20200326180033.png");
                    embed.WithTitle("Heres Derp Number 3");
                    break;
                case (3):
                    embed.WithImageUrl("https://cdn.discordapp.com/attachments/601140927213469733/707522149115625572/JPEG_20200326_175809.jpg");
                    embed.WithTitle("Heres Derp Number 4");

                    break;
                case (4):
                    embed.WithImageUrl("https://cdn.discordapp.com/attachments/601140927213469733/707522476174737429/Untitled237_20200506212137.png");
                    embed.WithTitle("Heres Derp Number 5");
                    break;
            }
            await ReplyAsync("", false, embed.Build());
        }
        [Command("random")]
        public async Task RandomAsync()
        {
            await ReplyAsync("idk wtf u want me to give you a random thing of");
        }
    }
    public class LoveModule : ModuleBase<SocketCommandContext>
    {
        [Command("i love")]
        public async Task LoveAsync(string joran)
        {
            if (joran == "alex")
            {
                await ReplyAsync(Context.User.Username + " loves you " + Context.Client.GetUser("Ashie", "7333").Mention);
            }
            else if (joran == "joran")
            {
                await ReplyAsync(Context.User.Username + " loves you " + Context.Client.GetUser("Da1Penguan", "1715").Mention);
            }
            else
            {
                await ReplyAsync(Context.User.Username + " loves you " + joran);
            }

        }
    }


