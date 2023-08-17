﻿using Integrated.Constants;
using Integrated.Interfaces;
using Integrated.TranslateAPIModels;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Integrated.Services
{
    public class TranslateAPIService : ITranslateAPIService
    {
        public async Task<(bool isSuccessful, string TranslatedWord)> GetTranslatedWordAsync(string from, string to, string word)
        {
            try
            {
                string str1 = "[{\"Text\":" + "\"" + word + "\"}]";
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"https://microsoft-translator-text.p.rapidapi.com/translate?to%5B0%5D={to}&api-version=3.0&from={from}&profanityAction=NoAction&textType=plain"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", APIConstants.API_KEY },
                        { "X-RapidAPI-Host", APIConstants.API_HOST },
                    },
                    Content = new StringContent(str1)
                    {
                        Headers =
                        {
                            ContentType = new MediaTypeHeaderValue("application/json")
                        }
                    }
                };
                var response = await client.SendAsync(request);
                if((int)response.StatusCode>400 ) { }
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<List<Translater>>(body);
                var tranlatedWord = json[0].Translations[0].Text;
                if (tranlatedWord != null) return (true, tranlatedWord);
                else return (false, "No translation found for this word");

            }
            catch (Exception)
            {
                return (false, "No Internet");
            }
        }
    }
}
