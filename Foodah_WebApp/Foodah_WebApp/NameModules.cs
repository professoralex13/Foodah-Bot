using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class EllaModule : ModuleBase<SocketCommandContext>
    {
        [Command("ella")]
        public async Task EllaAsync()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Heres Ella");
            embed.WithColor(Color.Orange);
            embed.WithImageUrl("http://randomfox.ca/images/3.jpg");
            await ReplyAsync("", false, embed.Build());
        }
    }
    public class EmilyModule : ModuleBase<SocketCommandContext>
    {
        [Command("emily")]
        public async Task EmilyAsync()
        {
            var embed = new EmbedBuilder();
            embed.WithColor(Color.Purple);
            embed.WithTitle("Heres Emily");
            embed.WithImageUrl("https://thisnzlife.co.nz/wp-content/uploads/2017/06/Llamas_jpg9.jpg");
            await ReplyAsync("", false, embed.Build());

        }
    }
    public class BarzeenModule : ModuleBase<SocketCommandContext>
    {
        [Command("barzeen")]
        public async Task BarzeenAsync()
        {
            var embed = new EmbedBuilder();
            embed.WithColor(new Color(255, 255, 255));
            embed.WithTitle("Heres Barzeen");
            embed.WithImageUrl("https://i.imgflip.com/hy0vi.jpg");
            await ReplyAsync("", false, embed.Build());

        }
    }
    public class JamesModule : ModuleBase<SocketCommandContext>
    {
        [Command("james")]
        public async Task JamesAsync()
        {
            var embed = new EmbedBuilder();
            embed.WithColor(new Color(0, 0, 0));
            embed.WithTitle("Heres James");
            embed.WithImageUrl("https://pics.me.me/thumb_ea-%E5%8F%A3%E3%83%A8-kerchow-lightning-mcqueens-ka-chow-know-your-53001052.png");
            await ReplyAsync("", false, embed.Build());

        }
    }
    public class AlexModule : ModuleBase<SocketCommandContext>
    {
        [Command("alex")]
        public async Task AlexAsync()
        {
            var embed = new EmbedBuilder();
            embed.WithColor(Color.Red);
            embed.WithTitle("Heres Alex");
            embed.WithImageUrl("https://drive.google.com/uc?export=view&id=1YH7GrHzPEvt6KBpcJPIDNUU6ry3P_0Te");

            await ReplyAsync("", false, embed.Build());

        }
    }
    public class JoranModule : ModuleBase<SocketCommandContext>
    {
        [Command("joran")]
        public async Task JoranAsync()
        {
            var embed = new EmbedBuilder();
            embed.WithColor(Color.DarkerGrey);
            embed.WithTitle("Heres Joran");
            embed.WithImageUrl("https://assets.bwbx.io/images/users/iqjWHBFdfxIU/iKIWgaiJUtss/v2/1000x-1.jpg");

            await ReplyAsync("", false, embed.Build());

        }
    }

