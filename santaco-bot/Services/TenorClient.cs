using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace santaco_bot.Services
{
    public class TenorClient
    {
        private string _apiKey = "71QAPD4U4KJ7";
        private string _locale = "es-ES";
        private int _numberOfGifs = 50;
        private string _tenorSearchUrl = "https://api.tenor.com/v1/search?q=${0}&key={1}&limit={2}&locale={3}";
        private string _tenorTrendingUrl = "https://api.tenor.com/v1/trending?key={0}&limit={1}";

        public async Task<string> GetGifUrl(string gifType)
        {
            string url = string.Format(_tenorSearchUrl, gifType, _apiKey, _numberOfGifs, _locale);
            return await GetRequest(url);
        }

        public async Task<string> GetTrendingGifUrl()
        {
            string url = string.Format(_tenorTrendingUrl, _apiKey, _numberOfGifs);
            return await GetRequest(url);
        }

        private async Task<string> GetRequest(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Request to Tenor API failed");

                int randomGifIndex = GetRandomInt();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                JArray resultsArray = JObject.Parse(jsonResponse)["results"] as JArray;
                JObject resultJsob = resultsArray.Children().ElementAt(randomGifIndex) as JObject;
                JArray mediaArray = resultJsob["media"] as JArray;
                JObject firstMedia = mediaArray.Children().First() as JObject;
                JObject gifMedia = firstMedia["gif"] as JObject;
                string gifUrl = gifMedia.GetValue("url").ToString();
                return gifUrl;
            }
        }

        private int GetRandomInt()
        {
            Random rnd = new Random();
            return rnd.Next(0, _numberOfGifs - 1);
        }
    }
}