namespace Client.Model
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Text.Json;

    public class Service
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private readonly string serviceHost;
        private readonly ushort servicePort;

        public Service()
        {
            this.serviceHost = "localhost";
            this.servicePort = 5000;
        }

        public Item[] GetItems(string searchText)
        {
            string callUri = String.Format("GetItem/{0}", searchText);
            Item[] items = this.CallWebService<Item[]>(HttpMethod.Get, callUri);
            return items;
        }

        public DataType CallWebService<DataType>(HttpMethod httpMethod, string webServiceUri)
        {
            Task<string> webServiceCall = this.CallWebService(httpMethod, webServiceUri);
            webServiceCall.Wait();
            string jsonResponseContent = webServiceCall.Result;
            DataType result = this.ConvertJson<DataType>(jsonResponseContent);
            return result;
        }

        public async Task<string> CallWebService(HttpMethod httpMethod, string callUri)
        {
            string httpUri = String.Format("http://{0}:{1}/{2}", this.serviceHost, this.servicePort, callUri);
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(httpMethod, httpUri);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            return httpResponseContent;
        }

        private DataType ConvertJson<DataType>(string json)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.PropertyNameCaseInsensitive = true;
            return JsonSerializer.Deserialize<DataType>(json, jsonSerializerOptions);
        }
    }
}
