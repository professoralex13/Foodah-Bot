using Discord;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodah_Bot_Interface
{
    public class DefineModule
    {
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
    }
}
