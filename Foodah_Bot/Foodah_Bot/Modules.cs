using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodah_Bot
{
    public class TestModule : ModuleBase<SocketCommandContext>
    {
        [Command("yeet")]
        [Summary("yeets")]
        public Task YeetAsync([Remainder] [Summary("the thing to yeet")] string toYeet)
            => ReplyAsync("Yeeted " + toYeet);
    }
    public class EllaModule : ModuleBase<SocketCommandContext>
    {
        [Command("ella")]
        public async Task EllaAsync()
        {
            var embed = new EmbedBuilder();

            embed.WithTitle("Heres Ella");

            embed.WithImageUrl("http://randomfox.ca/images/3.jpg");

            await ReplyAsync("", false, embed.Build());
        }
    }
    public class JoranModule : ModuleBase<SocketCommandContext>
    {
        [Command("joran")]
        public async Task JoranAsync()
        {
            var embed = new EmbedBuilder();

            embed.WithTitle("Heres Joran");

            embed.WithImageUrl("https://assets.bwbx.io/images/users/iqjWHBFdfxIU/iKIWgaiJUtss/v2/1000x-1.jpg");

            await ReplyAsync("", false, embed.Build());

        }
    }
}
