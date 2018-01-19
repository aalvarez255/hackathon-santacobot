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
        private string _apiKey = "JDSdwy2UK3xpxFWQmCdjgz7UhuePgTow";
        private string _locale = "es";
        private int _numberOfGifs = 50;
        private string _tenorSearchUrl = "https://api.giphy.com/v1/gifs/search?api_key={0}&limit={1}&q={2}&lang={3}";
        private string _tenorTrendingUrl = "https://api.giphy.com/v1/gifs/trending?key={0}&limit={1}";

        public async Task<string> GetGifUrl(string gifType)
        {
            gifType = HttpUtility.UrlEncode(gifType);
            string url = string.Format(_tenorSearchUrl, _apiKey, _numberOfGifs, gifType, _locale);
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
                JArray resultsArray = JObject.Parse(jsonResponse)["data"] as JArray;
                JObject resultJsob = resultsArray.Children().ElementAt(randomGifIndex) as JObject;      
                string gifUrl = resultJsob.GetValue("embed_url").ToString();
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