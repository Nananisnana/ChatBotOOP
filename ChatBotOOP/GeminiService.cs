using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChatBotOOP
{
    public class GeminiService : IChatService
    {
        
        private string apiKey = "AIzaSyBdAxTt4CcMaCORzSBeZyX8qbuEKcIEA3w";

        
        private string apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent";

        public async Task<string> GetResponseAsync(string userMessage)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUrl = $"{apiUrl}?key={apiKey}";

                var requestData = new
                {
                    contents = new[]
                    {
                        new { parts = new[] { new { text = userMessage } } }
                    }
                };

                string jsonContent = JsonConvert.SerializeObject(requestData);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(requestUrl, httpContent);
                    string responseString = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = JObject.Parse(responseString);
                        string botAnswer = jsonResponse["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString();
                        return botAnswer ?? "Cevap boş geldi.";
                    }
                    else
                    {
                        
                        return $"HATA: {response.StatusCode} - {responseString}";
                    }
                }
                catch (Exception ex)
                {
                    return "Bağlantı Hatası: " + ex.Message;
                }
            }
        }
    }
}