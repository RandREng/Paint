using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace RandREng.GraphClient
{
    public static class Ext
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            var json = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static async Task<HttpResponseMessage> PostAsJson(this HttpClient client, string url, object obj, IAuthenticationProvider2 authenticationProvider = null)
        {
            if (authenticationProvider != null)
            {
                client.DefaultRequestHeaders.Authorization = await authenticationProvider.GetAuthenticationHeaderAsync();
            }
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await client.PostAsync(url, content);
        }
    }

}
